using Library.Application.Interfaces;
using Library.Application.Interfaces.Repositories;
using Library.Domain;
using Library.Persistance.DbPatterns;
using Library.Persistance.DbPatterns.Repositories;
using Library.Persistance.Interfaces;
using Library.Persistance.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Persistance.Dependencies;

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
        
        services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 6;

                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<LibraryDBContext>()
            .AddDefaultTokenProviders();
        
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IAuthorRepository, AuthorRepository>();
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IBorrowRecordRepository, BorrowRecordRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        return services;
    }
}