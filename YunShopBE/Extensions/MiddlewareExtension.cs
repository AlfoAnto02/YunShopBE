using Microsoft.Extensions.FileProviders;

namespace YunShopBE.Extensions {
    public static class MiddlewareExtension {
        public static WebApplication? AddWebMiddlewares(this WebApplication app) {

            app.UseHttpsRedirection();

            // Usa le configurazioni standard per i file statici in wwwroot
            app.UseDefaultFiles(); // Serve index.html automaticamente
            app.UseStaticFiles(); // Serve i file statici da wwwroot di default

            // Configura Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
                c.RoutePrefix = "swagger";
            });

            // Configurazione middleware
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            // Mappa i controller (API)
            app.MapControllers();

            // Mappa il fallback per qualsiasi rotta non trovata, reindirizzandola a index.html
            app.MapFallbackToFile("index.html");

            // Configura headers per sicurezza e cache
            app.Use(async (context, next) =>
            {
                // Evita che il browser faccia caching delle API
                if (context.Request.Path.StartsWithSegments("/api")) {
                    context.Response.Headers.Append("Cache-Control", "no-cache, no-store, must-revalidate");
                    context.Response.Headers.Append("Pragma", "no-cache");
                    context.Response.Headers.Append("Expires", "0");
                }
                // Permetti il caching per i file statici
                else if (!context.Request.Path.StartsWithSegments("/api")) {
                    // Un mese di cache per i file statici (eccetto index.html)
                    if (!context.Request.Path.Value.EndsWith("/") &&
                        !context.Request.Path.Value.EndsWith("index.html")) {
                        context.Response.Headers.Append("Cache-Control", "public,max-age=2592000");
                    }
                }

                await next();
            });

            app.Run();
            return app;
        }

    }
}
