namespace InnoclinicAutho.Persistence.Extensions;

using InnoclinicAutho.Application.Interfaces;
using InnoclinicAutho.Infrastructure.Authentification;
using InnoclinicAutho.Infrastructure.Implementations;
using InnoclinicAutho.Persistence.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDbContext(this IServiceCollection serviceCollection, string? connectionString)
    {
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("Connection string is not found.");
        }

        serviceCollection.AddDbContext<ApiDbContext>(options => options.UseNpgsql(connectionString));
        serviceCollection.AddScoped<IApiDbContext>(provider => provider.GetRequiredService<ApiDbContext>());

        return serviceCollection;
    }

    public static IServiceCollection AddJwtAuth(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddSingleton<IJwtService, JwtService>();
        serviceCollection.AddScoped<IPasswordHasherNode, PasswordHasher>();

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
}
