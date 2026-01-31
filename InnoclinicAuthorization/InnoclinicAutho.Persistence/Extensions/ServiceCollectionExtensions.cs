namespace InnoclinicAutho.Persistence.Extensions;

using InnoclinicAutho.Application.Interfaces;
using InnoclinicAutho.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

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
}
