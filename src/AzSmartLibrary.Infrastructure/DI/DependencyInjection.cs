using AzSmartLibrary.Core.Interfaces;
using AzSmartLibrary.Infrastructure.Data;
using AzSmartLibrary.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AzSmartLibrary.Infrastructure.DI;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        
        var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING")
                               ?? configuration.GetConnectionString("DefaultConnection");

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("No se encontró la cadena de conexión. Verifica el archivo .env o appsettings.json.");
        }

        
        services.AddDbContextPool<LibraryDbContext>(options =>
            options.UseSqlServer(connectionString, sqlOptions =>
            {
                // Resiliencia  para Nube
                sqlOptions.EnableRetryOnFailure(maxRetryCount: 5);
            }));

        //Repositorios
        services.AddScoped<IAuthorRepository, AuthorRepository>();
        services.AddScoped<IBookRepository, BookRepository>();

        return services;
    }
}