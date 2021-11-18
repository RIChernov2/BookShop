using BookShop.BooksAndAuthors.Manager.Configurations;
using BookShop.BooksAndAuthors.Manager.Configurations.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BookShop.BooksAndAuthors.Manager
{
    public class Startup
    {
        public AppConfiguration Configuration { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration.Get<AppConfiguration>();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            DapperConfiguration.AddSnakeCaseMapping();
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
