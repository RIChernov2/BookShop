using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.BooksAndAuthors.Manager.Providers
{
    public abstract class BaseProvider
    {
        private readonly string _queueName;
        private IModel _channel;
        public AsyncEventingBasicConsumer Consumer { get; private set; }

        protected BaseProvider(IConnection connection, string queueName)
        {
            _queueName = queueName;
            _channel = connection.CreateModel();
            ConfigureChannel();
            //_channel.QueueDeclare(queue: _queueName,
            //    durable: false,      //Чтобы убедиться, что сообщения не потеряны при аварийном завершении работы
            //    exclusive: false,
            //    autoDelete: false,
            //    arguments: null);
            //_channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false); // не отправлять новое сообщение, пока не обработается и не подтвердится предыдущее
        }

        public abstract Task<string> HandleMassage(string commandName, string message);

        private void ConfigureChannel()
        {
            var queue = _channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
            if ( queue.MessageCount != 0 ) _channel.QueuePurge(queue.QueueName);
            //Channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: true, arguments: null);
            _channel.BasicQos(0, 1, false);
        }

        public void SetDeliverySettings()
        {

            Console.WriteLine("[*] Waiting for messages.");
            var Consumer = new AsyncEventingBasicConsumer(_channel);
            _channel.BasicConsume(queue: _queueName, autoAck: false, consumer: Consumer);
            Consumer.Received += OnMessageReceivedAsync;
        }

        private async Task OnMessageReceivedAsync(object model, BasicDeliverEventArgs eventArgs)
        {
            var body = eventArgs.Body.ToArray();
            var props = eventArgs.BasicProperties;
            string response = "";

            try
            {
                if (props.Headers.TryGetValue("command", out var commandByte))
                {
                    var command = Encoding.UTF8.GetString((byte[])commandByte);
                    var message = Encoding.UTF8.GetString(body);
                    response = await HandleMassage(command, message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("OnMessageReceived. Error: " + ex.Message);
            }
            finally
            {
                var body1 = Encoding.UTF8.GetBytes(response);
                _channel.BasicPublish(exchange: "",
                                    routingKey: props.ReplyTo,
                                    basicProperties: props,
                                    body: body1);
                _channel.BasicAck(deliveryTag: eventArgs.DeliveryTag, multiple: false); // подтверждение сообщения
            }
        }
    }
}
