using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using NLog;
using System;

namespace BookShop.BooksAndAuthors.Manager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = LogManager.GetCurrentClassLogger();
            try
            {
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception e)
            {
                logger.Error(e, "Stopped program because of exception");
                throw;
            }
            finally
            {
                LogManager.Shutdown();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
