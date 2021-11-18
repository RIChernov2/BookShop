using BookShop.Configuration.Models;
using BookShop.MessageBrokerClients;
using Core.StorageManagers.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace BookShop.Configuration
{
    public static class RabbitMqConfiguration
    {
        public static void ConfigureRabbitMQ(this IServiceCollection services, AppConfiguration configuration)
        {
            var rabbitMqSettings = configuration.RabbitMq;
            var connectionFactory = new ConnectionFactory {
                HostName = rabbitMqSettings.Host,
                UserName = rabbitMqSettings.Username,
                Password = rabbitMqSettings.Password
            };
            var rabbitMqConnection = connectionFactory.CreateConnection();

            services.AddSingleton(opt =>
                new OrdersRpcClient(rabbitMqConnection, rabbitMqSettings.OrdersRequestQueue));
            services.AddSingleton(opt =>
                new OrderedBooksRpcClient(rabbitMqConnection, rabbitMqSettings.OrderedBooksRequestQueue));

            services.AddSingleton(opt =>
                new MessagesRpcClient(rabbitMqConnection, rabbitMqSettings.MessagesRequestQueue));
            services.AddSingleton(opt =>
                new NotificationSettingsRpcClient(rabbitMqConnection, rabbitMqSettings.NSettingsRequestQueue));

            services.AddSingleton(opt =>
                new BooksRpcClient(rabbitMqConnection, rabbitMqSettings.BooksRequestQueue));
            services.AddSingleton(opt =>
                new AuthorsRpcClient(rabbitMqConnection, rabbitMqSettings.AuthorsRequestQueue));
        }
    }
}