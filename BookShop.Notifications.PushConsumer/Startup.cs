using BookShop.Notifications.Manager.PushConsumer.Models;
using BookShop.Notifications.PushConsumer.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BookShop.Notifications.PushConsumer
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
            RabbitMqConfiguration.Configure(Configuration);
            services.ConfigureNLog(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if ( env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
        }
    }
}
