using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions;
using Application.Options;
using Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions {
    public static class ServiceExtension {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<ISizeService, SizeService>();
            services.AddScoped<IProductSizeService, ProductSizeService>();
            return services;
        }
    }
}
