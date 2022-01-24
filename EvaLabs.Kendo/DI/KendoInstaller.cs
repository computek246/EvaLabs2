using EvaLabs.Common.Installers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EvaLabs.Kendo.DI
{
    public class KendoInstaller : IInstaller
    {
        public int Order => 4;

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddKendo();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

        }
    }
}
