using BookShop.BooksAndAuthors.Manager.Data.Migrations;
using Microsoft.Extensions.DependencyInjection;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Initialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using BookShop.BooksAndAuthors.Manager.Configurations.Models;
using FluentMigrator.Runner.Conventions;

namespace BookShop.BooksAndAuthors.Manager.Configurations
{
    public static class FluentMigratorConfiguration
    {
        public static void ConfigureMigrator(this IServiceCollection services, AppConfiguration configuration)
        {
            services.AddSingleton<IConventionSet>(new DefaultConventionSet(configuration.SchemaName, null));

            services.AddFluentMigratorCore()
                .ConfigureRunner(config =>
                    config.AddPostgres()
                        .WithGlobalConnectionString(configuration.ConnectionStrings.DefaultConnection)
                        .ScanIn(typeof(AddedBooksAndAuthors).Assembly)
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