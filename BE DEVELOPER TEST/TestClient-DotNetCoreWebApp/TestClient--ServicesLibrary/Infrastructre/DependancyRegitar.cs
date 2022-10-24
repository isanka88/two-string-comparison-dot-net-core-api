using Microsoft.Extensions.DependencyInjection;
using TestClient__ServicesLibrary.Interfaces;
using TestClient__ServicesLibrary.Services;

namespace TestClient__ServicesLibrary.Infrastructre
{
    public static class DependancyRegitar
    {
        public static IServiceCollection AddServiceCollectionLibrary(this IServiceCollection services)
        {
            services.AddScoped<IComparatorService, ComparatorService>();
            return services;
        }
    }
}