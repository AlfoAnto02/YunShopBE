using Microsoft.Extensions.FileProviders;

namespace YunShopBE.Extensions {
    public static class MiddlewareExtension {
        public static WebApplication? AddWebMiddlewares(this WebApplication app) {

            // 1. Reindirizzamento a HTTPS (opzionale)
            app.UseHttpsRedirection();

            // 2. Servire i file statici (compreso index.html) dalla cartella di build Angular
            app.UseDefaultFiles(new DefaultFilesOptions {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "ClientApp", "yun-shop-fe", "browser"))
            });
            app.UseStaticFiles(new StaticFileOptions {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "ClientApp", "yun-shop-fe", "browser"))
            });

            // 3. (Opzionale) Configura Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
                c.RoutePrefix = "swagger";
            });

            // 4. (Opzionale) CORS, Routing, Autenticazione, Autorizzazione
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            // 5. Mappa i controller (API)
            app.MapControllers();

            // 6. Mappa il fallback per qualsiasi rotta non trovata, reindirizzandola a index.html
            app.MapFallbackToFile("index.html", new StaticFileOptions {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "ClientApp", "yun-shop-fe", "browser"))
            });


            app.Run();
            return app;
        }

    }
}
