using BookShop.Notifications.Manager.Configurations;
using BookShop.Notifications.Manager.Configurations.Models;
using BookShop.Notifications.Manager.Migrations;
using BookShop.Notifications.Manager.Repositories.Mappers;
using Dapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BookShop.Notifications.Manager
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration.Get<AppConfiguration>();
            AddMessages.SchemeName = Configuration.SchemaName;
        }

        public AppConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            DapperConfiguration.Configure();
            RabbitMqConfiguration.Configure(Configuration);
            services.ConfigureMigrator(Configuration);
            SqlMapper.AddTypeHandler(new NSettingsTypeHandler());
            services.ConfigureNLog(Configuration);
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // для инициализации стартового значения таблиц
            // Configuration.FluentMigratorProfile = "FirstStart";

            if ( env.IsDevelopment() )
            {
                app.UseDeveloperExceptionPage();
            }

            app.RunMigrator();
        }
    }
}
