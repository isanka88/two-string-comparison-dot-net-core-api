using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TestServer_Data.Context;
using TestServer_Service.Interfaces;
using TestServer_Service.Services;

namespace TestServer_Service.Infrastructre
{
    public static class DependancyRegitar
    {
        public static IServiceCollection AddServiceCollectionLibrary(this IServiceCollection services)
        {
            // Initilize inmemory database context
            services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase("DiffDb"));


            // register services for dependancy injection 
            services.AddScoped<IComparisonService, ComparisonService>();
            services.AddScoped<IUserInputService, UserInputService>();

            return services;
        }
    }
}
