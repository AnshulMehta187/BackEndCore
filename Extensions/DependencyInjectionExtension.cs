using DTO;
using Microsoft.Extensions.DependencyInjection;
using Repository;
using Service.Interfaces;

namespace API.Extensions
{
    public static class DependencyInjectionExtensions
    {
        /// <summary>
        /// Extension class
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureDependencyInjection(this IServiceCollection services)
        {
            // Reference DI
            services.RegisterAssemblies(DependencyLifetime.Scoped,
                new[] { "Service", "Repository", "DTO" },
                new[] { typeof(IService), typeof(IRepository<>), typeof(IMapper<,>) }
            );
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}
