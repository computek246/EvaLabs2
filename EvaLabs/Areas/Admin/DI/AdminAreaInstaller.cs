using EvaLabs.Common.Installers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EvaLabs.Areas.Admin.DI
{
    public class AdminAreaInstaller : IInstaller
    {
        public int Order => 6;
        
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
        }

    }
}
