using Library.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Persistance;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistance(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration["DbConnection"];
        services.AddDbContext<LibraryDBContext>(options =>
        {
            options.UseSqlite(connectionString);
        });
        services.AddScoped<ILibraryDBContext>(provider => provider.GetService<LibraryDBContext>());
        return services;
    }
}