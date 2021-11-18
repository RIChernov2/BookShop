using BookShop.Orders.Manager.Configurations.Models;
using BookShop.Orders.Manager.Managers.Factories;
using BookShop.Orders.Manager.Providers;
using RabbitMQ.Client;

namespace BookShop.Orders.Manager.Configurations
{
    public static class RabbitMqConfiguration
    {
        public static void Configure(AppConfiguration configuration)
        {
            var rabbitMqSettings = configuration.RabbitMq;
            var connectionFactory = new ConnectionFactory
            {
                HostName = rabbitMqSettings.Host,
                UserName = rabbitMqSettings.Username,
                Password = rabbitMqSettings.Password,
                DispatchConsumersAsync = true
            };
            var connection = connectionFactory.CreateConnection();

            var osmFactory = new OrdersStorageManagerFactory(configuration);
            var orderConsumer = new OrdersProvider(connection, rabbitMqSettings.OrdersRequestQueue, osmFactory);
            orderConsumer.StartListening();

            var obsmFactory = new OrderedBooksStorageManagerFactory(configuration);
            var orderedBookConsumer = new OrderedBooksProvider(connection, rabbitMqSettings.OrderedBooksRequestQueue, obsmFactory);
            orderedBookConsumer.StartListening();
        }
    }
}