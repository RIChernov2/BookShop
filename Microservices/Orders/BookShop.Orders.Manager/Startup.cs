using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BookShop.Orders.Manager.Configurations;
using BookShop.Orders.Manager.Configurations.Models;
using Microsoft.Extensions.Configuration;

namespace BookShop.Orders.Manager
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration.Get<AppConfiguration>();
        }

        public AppConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            DapperConfiguration.Configure();
            RabbitMqConfiguration.Configure(Configuration);
            services.ConfigureMigrator(Configuration);
            services.ConfigureNLog(Configuration);
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if ( env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.RunMigrator();
        }
    }
}
