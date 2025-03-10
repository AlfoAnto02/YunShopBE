using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Context;
using Model.Repositories;

namespace Model.Extensions {
    public static class ServiceExtension {
        public static IServiceCollection AddModelServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MyDbContext>(options => options.UseLazyLoadingProxies().UseSqlServer(configuration.GetConnectionString("MyDbContext")));
            services.AddScoped<UserRepository>();
            services.AddScoped<CategoryRepository>();
            services.AddScoped<ImageRepository>();
            services.AddScoped<ProductRepository>();
            return services;
        }
    }
}
