namespace YunShopBE.Extensions {
    public static class MiddlewareExtension {
        public static WebApplication? AddWebMiddlewares(this WebApplication app)
        {
            app.UseCors("AllowSpecificOrigin");

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment()) {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            
            app.UseStaticFiles();

            app.MapFallbackToFile("index.html");

            app.Run();
            return app;
        }

    }
}
