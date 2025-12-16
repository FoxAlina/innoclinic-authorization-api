using InnoclinicAutho.Application.Interfaces;
using InnoclinicAutho.Domain.Entities;
using InnoclinicAutho.Infrastructure.Authentification;
using InnoclinicAutho.Infrastructure.Implementations;
using InnoclinicAutho.Persistence.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace InnoclinicAutho.Persistence.Extensions
{
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

        public static IServiceCollection AddIdentity(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddIdentityCore<User>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.User.RequireUniqueEmail = true;
            })
            .AddRoles<IdentityRole<Guid>>()
            .AddEntityFrameworkStores<ApiDbContext>()
            .AddDefaultTokenProviders();

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
}
