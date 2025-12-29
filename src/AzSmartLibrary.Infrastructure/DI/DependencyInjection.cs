using AzSmartLibrary.Core.Interfaces;
using AzSmartLibrary.Infrastructure.Data;
using AzSmartLibrary.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AzSmartLibrary.Infrastructure.DI
{
    public static class DependencyInjection
    {
        // Método de extensión para IServiceCollection
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // 1. Obtener Cadena de Conexión
            // Prioridad: Variable de entorno (Nube/Docker/.env) -> appsettings.json
            var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING")
                                   ?? configuration.GetConnectionString("DefaultConnection");

            // 2. Configurar Contexto de Base de Datos (EF Core)
            services.AddDbContext<LibraryDbContext>(options =>
                options.UseSqlServer(connectionString, sqlOptions =>
                {
                    // Resiliencia: Reintentar automáticamente si la red falla momentáneamente
                    sqlOptions.EnableRetryOnFailure(maxRetryCount: 5);
                }));

            // 3. Registrar Repositorios (Inyección de Dependencias)
            // Scoped: Se crea una instancia por cada petición HTTP (Ideal para Web)
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IBookRepository, BookRepository>();

            return services;
        }
    }
}
