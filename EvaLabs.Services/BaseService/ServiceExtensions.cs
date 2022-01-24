using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace EvaLabs.Services.BaseService
{
    public static class ServiceExtensions
    {
        public static void RegisterAllServices(this IServiceCollection services)
        {
            var installer = Assembly.GetExecutingAssembly()
                .GetExportedTypes()
                .Where(x => typeof(IService).IsAssignableFrom(x))
                .Where(x => !x.IsAbstract && !x.IsGenericType && x.IsClass)
                .Select(x => new
                {
                    Interface = x.GetInterfaces().OrderBy(i => i.FullName).Last(),
                    Implementation = x
                })
                .ToList();

            installer
                .ForEach(x => services.AddScoped(x.Interface, x.Implementation));
        }
    }
}
