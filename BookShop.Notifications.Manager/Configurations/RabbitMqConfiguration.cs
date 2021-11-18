using BookShop.Notifications.Manager.Configurations.Models;
using BookShop.Notifications.Manager.Managers;
using BookShop.Notifications.Manager.Providers;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace BookShop.Notifications.Manager.Configurations
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

            var factory = new MessagesFactory(configuration);
            var counsumer = new MessagesProvider(connection, rabbitMqSettings.MessagesRequestQueue, factory);
            counsumer.StartListening();

            var factory2 = new NotificationSettingFactory(configuration);
            var counsumer2 = new NotificationSettingsProvider(connection, rabbitMqSettings.NSettingsRequestQueue, factory2);
            counsumer2.StartListening();
        }
    }
}