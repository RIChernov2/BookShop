using BookShop.Configuration;
using BookShop.Configuration.Models;
using BookShop.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog;

namespace BookShop
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
            services.AddSingleton(Configuration);
            services.AddJson();
            services.ConfigureCors();
            services.ConfigureAuthentication(Configuration);
            services.AddControllers();
            services.AddSnakeCaseMapping();
            services.AddDatabaseFactory(Configuration);
            services.AddRepositories();
            services.AddStorageManagers();
            services.ConfigureRabbitMQ(Configuration);
            services.ConfigureSwagger();
            services.ConfigureMigrator(Configuration);
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ExceptionLoggingMiddleware>();

            app.UseCors("EnableCORS");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BookShop.WebApi v1"));
            }

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.RunMigrator();
        }
    }
}