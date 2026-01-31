namespace InnoclinicAutho.Infrastructure.Extensions;

using InnoclinicAutho.Application.Interfaces;
using InnoclinicAutho.Infrastructure.Authentification;
using InnoclinicAutho.Infrastructure.Caching;
using InnoclinicAutho.Infrastructure.Implementations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddJwtAuth(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddSingleton<IJwtService, JwtService>();
        serviceCollection.AddScoped<IHashService, HashService>();

        serviceCollection.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Secret"]!)),
                    ValidateIssuer = true,
                    ValidIssuer = configuration["JwtSettings:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = configuration["JwtSettings:Audience"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

        return serviceCollection;
    }

    public static IServiceCollection AddDistributedCache(this IServiceCollection serviceCollection, string? connectionString)
    {
        serviceCollection.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = connectionString;
        });

        serviceCollection.AddSingleton(new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false
        });

        serviceCollection.AddScoped<ICacheService, CacheService>();

        return serviceCollection;
    }
}
