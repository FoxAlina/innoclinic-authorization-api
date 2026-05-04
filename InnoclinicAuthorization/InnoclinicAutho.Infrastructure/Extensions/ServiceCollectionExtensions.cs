namespace InnoclinicAutho.Infrastructure.Extensions;

using InnoclinicAutho.Application.Interfaces;
using InnoclinicAutho.Application.Interfaces.Repositories;
using InnoclinicAutho.Application.UseCases.Common;
using InnoclinicAutho.Infrastructure.Authentification;
using InnoclinicAutho.Infrastructure.Caching;
using InnoclinicAutho.Infrastructure.Implementations;
using InnoclinicAutho.Infrastructure.JWT_Blacklisting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Localization;
using InnoclinicAutho.Domain.Exceptions;

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

                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = async context =>
                    {
                        var rawToken = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

                        if (String.IsNullOrEmpty(rawToken)) return;

                        var blacklistService = context.HttpContext.RequestServices.GetRequiredService<IJwtBlackListService>();
                        if (await blacklistService.IsBlacklistedAsync(rawToken))
                        {
                            context.Fail("This token has been blacklisted.");
                        }
                        else
                        {
                            var userRepo = context.HttpContext.RequestServices.GetRequiredService<IUserRepository>();
                            var userRolesRepo = context.HttpContext.RequestServices.GetRequiredService<IUserRoleRepository>();
                            var jwtService = context.HttpContext.RequestServices.GetRequiredService<IJwtService>();

                            var userId = jwtService.ReadTokenUserId(rawToken);
                            var user = await userRepo.GetByIdAsync(userId);

                            if (user != null)
                            {
                                var currentUser = context.HttpContext.RequestServices.GetRequiredService<IUser>();
                                currentUser.Id = user.Id;
                                currentUser.Roles = (await userRolesRepo.GetAllRoleNamesByUserIdAsync(user.Id)).ToList();
                            }
                            else
                            {
                                context.Fail("User does not exist.");
                                context.Response.Redirect("/login");
                            }
                        }
                    }
                };

                options.RequireHttpsMetadata = true;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["JwtSettings:Secret"])),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

        serviceCollection.AddAuthorization();

        serviceCollection.AddScoped<IJwtBlackListService, JwtBlacklistService>();

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
