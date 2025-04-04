

using System.Text;
using Application.Abstractions;
using Application.Extensions;
using Application.Options;
using Application.Services;
using Azure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Model.Context;
using Model.Extensions;
using Model.Repositories;
using YunShopBE.Extensions;

namespace YunShopBE {
    public class Program {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);
            //var keyVaultEndpoint = new Uri(Environment.GetEnvironmentVariable("KEYVAULT_ENDPOINT"));
            //builder.Configuration.AddAzureKeyVault(keyVaultEndpoint, new DefaultAzureCredential());
            // Add services to the container.
            builder.Services.AddWebServices(builder.Configuration)
                .AddModelServices(builder.Configuration)
                .AddApplicationServices(builder.Configuration);
            builder.Services.Configure<HashingOptions>(builder.Configuration.GetSection("SecretKey"));


            var app = builder.Build();
            app.AddWebMiddlewares();

        }
    }
}
