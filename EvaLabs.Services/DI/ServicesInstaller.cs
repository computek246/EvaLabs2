using System.Linq;
using EvaLabs.Common.ExtensionMethod;
using EvaLabs.Common.Installers;
using EvaLabs.Domain.Context;
using EvaLabs.Domain.Entities;
using EvaLabs.Infrastructure;
using EvaLabs.Infrastructure.SingletonFactory;
using EvaLabs.Services.BaseService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EvaLabs.Services.DI
{
    public class ServicesInstaller : IInstaller
    {
        public int Order => 5;

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddUnitOfWork<EvaContext>();
            services.RegisterAllServices();

            services.GetService<EvaContext>(x =>
            {
                if (x.Database.CanConnect())
                {
                    Singleton<Lab>.Instance = x.Labs.FirstOrDefault(e => e.IsActive);
                }
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
        }
    }
}
