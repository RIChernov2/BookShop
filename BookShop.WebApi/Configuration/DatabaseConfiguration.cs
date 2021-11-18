using System.Collections.Generic;
using BookShop.Configuration.Models;
using BookShop.Orders.Manager.Configurations.Models;
using Data.Factories;
using Data.Factories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BookShop.Configuration
{
    public static class DatabaseConfiguration
    {
        /// <summary>
        /// Регистрирует как Singleton словарь подключений <c>connectionDictionary</c>,
        /// а также как Transient фабрику <c>DbConnectionFactory</c>
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration">Файл конфигурации</param>
        public static void AddDatabaseFactory(this IServiceCollection services, AppConfiguration configuration)
        {
            var connectionDictionary = new Dictionary<DatabaseConnectionTypes, string>
            {
                {DatabaseConnectionTypes.Default, configuration.ConnectionStrings.DefaultConnection},
            };

            services.AddSingleton<IDictionary<DatabaseConnectionTypes, string>>(connectionDictionary);
            services.AddTransient<IDbConnectionFactory, DbConnectionFactory>();
        }
    }
}