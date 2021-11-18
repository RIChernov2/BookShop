using BookShop.Notifications.PushConsumer.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BookShop.Notifications.PushConsumer.Providers
{
    public class ConsumerImitation
    {

        private readonly string _queueName;
        public IModel Channel { get; }
        public AsyncEventingBasicConsumer Consumer { get; private set; }
        //Console _console;

        public ConsumerImitation(IConnection connection, string queueName)
        {
            _queueName = queueName;
            Channel = connection.CreateModel();
            ConfigureChannel();
        }

        private void ConfigureChannel()
        {
            var queue = Channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
            // if ( queue.MessageCount != 0 ) Channel.QueuePurge(queue.QueueName);
            Channel.BasicQos(0, 1, false);
        }

        public void StartListening()
        {
            Consumer = new AsyncEventingBasicConsumer(Channel);
            Channel.BasicConsume(queue: _queueName, autoAck: false, consumer: Consumer);
            Consumer.Received += OnMessageReceivedAsync;
        }

        private async Task OnMessageReceivedAsync(object model, BasicDeliverEventArgs eventArgs)
        {
            try
            {
                var body = eventArgs.Body.ToArray();
                var messageString = Encoding.UTF8.GetString(body);
                Message message = JsonSerializer.Deserialize< Message>(messageString);

                Console.WriteLine($"   *** Получено пуш уведомления для пользователя с ID = {message.UserId}");
                Console.WriteLine($"   *** ==>  Тип уведомления = \"{TypeToSubject(message.Type)}\"");
                Console.WriteLine($"   *** ==>  Текст уведомления = \"{message.Text}\"");
                Console.WriteLine(Environment.NewLine);

                Debug.WriteLine($"   *** Получено пуш уведомления для пользователя с ID = {message.UserId}");
                Debug.WriteLine($"   *** ==>  Тип уведомления = \"{TypeToSubject(message.Type)}\"");
                Debug.WriteLine($"   *** ==>  Текст уведомления = \"{message.Text}\"");

                Channel.BasicAck(deliveryTag: eventArgs.DeliveryTag, multiple: false);
            }
            catch
            {
                System.Console.WriteLine("   ==> Ошибка при обработке пуш уведомления");
            }
        }

        private string TypeToSubject(MessageType type) => type switch
        {
            MessageType.Info => "Info",
            MessageType.Warning => "Warning",
            MessageType.Ads => "Ads",
            _ => throw new NotImplementedException(),
        };
    }
}
