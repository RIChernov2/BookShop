using BookShop.Configuration.Models;
using BookShop.Orders.Manager.Configurations.Models;
using Data.Migrations;
using Microsoft.Extensions.DependencyInjection;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Initialization;
using Microsoft.AspNetCore.Builder;

namespace BookShop.Configuration
{
    public static class FluentMigratorConfiguration
    {
        public static void ConfigureMigrator(this IServiceCollection services, AppConfiguration configuration)
        {
            services.AddFluentMigratorCore()
                .ConfigureRunner(config =>
                    config.AddPostgres()
                        .WithGlobalConnectionString(configuration.ConnectionStrings.DefaultConnection)
                        .ScanIn(typeof(AddUsers).Assembly)
                        .For.All()
                )
                .Configure<RunnerOptions>(config =>
                {
                    config.Profile = configuration.FluentMigratorProfile;
                })
                .AddLogging(config => config.AddFluentMigratorConsole());
        }

        public static void RunMigrator(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var migrator = scope.ServiceProvider.GetService<IMigrationRunner>();
            migrator?.ListMigrations();
            migrator?.MigrateUp();
        }
    }
}