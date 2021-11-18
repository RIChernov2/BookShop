using BookShop.Notifications.Manager.Configurations.Models;
using BookShop.Notifications.Manager.Handlers.Interfaces;
using BookShop.Notifications.Manager.Models;
using RabbitMQ.Client;
using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BookShop.Notifications.Manager.Handlers
{
    public class PushSender : IPushSender
    {
        private ConnectionFactory _factory;
        private string _queueName;

        public PushSender(AppConfiguration configuration)
        {
            var rabbitMqSettings = configuration.RabbitMq;
            _factory = new ConnectionFactory
            {
                HostName = rabbitMqSettings.Host,
                UserName = rabbitMqSettings.Username,
                Password = rabbitMqSettings.Password
            };

            _queueName = rabbitMqSettings.PushRequestQueue;

            using ( var connection = _factory.CreateConnection() )
            {
                using ( var channel = connection.CreateModel() )
                {
                    channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
                }
            }
        }

        public void Send(Message message)
        {
            Publish(message);
        }

        private void Publish(Message message)
        {
            using ( var connection = _factory.CreateConnection() )
            {
                using ( var channel = connection.CreateModel() )
                {
                    var stringMessage = JsonSerializer.Serialize(message);
                    byte[] messageBytes = Encoding.UTF8.GetBytes(stringMessage);

                    channel.BasicPublish(exchange: "", routingKey: _queueName, basicProperties: null, body: messageBytes);
                }
            }
        }
    }
}
