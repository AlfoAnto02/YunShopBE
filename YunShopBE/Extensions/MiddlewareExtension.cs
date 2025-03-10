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

            app.UseAuthorization();

            app.UseAuthentication();

            app.UseStaticFiles();

            app.MapControllers();

            app.Run();
            return app;
        }

    }
}
