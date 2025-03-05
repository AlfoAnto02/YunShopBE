

using Application.Abstractions;
using Application.Options;
using Application.Services;
using Microsoft.EntityFrameworkCore;
using Model.Context;
using Model.Repositories;

namespace YunShopBE {
    public class Program {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<MyDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MyDbContext")));
            builder.Services.AddScoped<UserRepository>();
            builder.Services.AddScoped<CategoryRepository>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.Configure<HashingOptions>(builder.Configuration.GetSection("HashingOptions"));
            builder.Services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new() { Title = "YunShopBE", Version = "v1" });
            });
            builder.Services.AddScoped<ITokenService, TokenService>();

            builder.Services.Configure<JwtAuthenticationOption>(builder.Configuration.GetSection("JwtAuthentication"));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment()) {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            //app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
