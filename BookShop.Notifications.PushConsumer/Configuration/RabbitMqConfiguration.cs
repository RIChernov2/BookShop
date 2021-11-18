using BookShop.Notifications.Manager.PushConsumer.Models;
using BookShop.Notifications.PushConsumer.Providers;
using RabbitMQ.Client;

namespace BookShop.Notifications.PushConsumer.Configuration
{
    public class RabbitMqConfiguration
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

            var counsumer = new ConsumerImitation(connection, rabbitMqSettings.PushRequestQueue);
            counsumer.StartListening();
        }
    }
}
