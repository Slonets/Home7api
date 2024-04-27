using Core.Interfaces;
using Core.ShopService;
using Microsoft.Extensions.DependencyInjection;

namespace Core
{
    public static class ServiceExtensions
    {
        public static void AddCustomService(this IServiceCollection servicesCollection)
        {
            servicesCollection.AddScoped<IFilesService, FilesService>();
            servicesCollection.AddScoped<IUnit, UnitService>();
            
        }
        public static void CustomMapper(this IServiceCollection servicesCollection)
        {
            servicesCollection.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}
