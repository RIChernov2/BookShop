using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookShop.Notifications.Manager.Configurations.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace BookShop.Notifications.Manager.Configurations
{
    public static class NLogConfiguration
    {
        public static void ConfigureNLog(this IServiceCollection services, AppConfiguration configuration)
        {
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.SetMinimumLevel(LogLevel.Trace);
                loggingBuilder.AddNLog(configuration.NLogConfiguration);
            })
            .BuildServiceProvider();
        }
    }
}
