using Microsoft.Extensions.DependencyInjection;
using AzSmartLibrary.Application.Interfaces;
using AzSmartLibrary.Application.Services;

namespace AzSmartLibrary.Application.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Registramos los servicios de lógica de negocio
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IBookService, BookService>();

            return services;
        }
    }
}
