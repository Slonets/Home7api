using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Interfaces;

namespace Infrastructure
{
    public static class ServiceExtensions
    {
        //винесення репозиторію
        public static void AddRepository(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }

        //винесення рядка підключення
        public static void AddDbContext(this IServiceCollection services, string connection)
        {
            services.AddDbContext<ShopDbContext>(options =>
            {
                options.UseSqlServer(connection);
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });
        }
    }
}
