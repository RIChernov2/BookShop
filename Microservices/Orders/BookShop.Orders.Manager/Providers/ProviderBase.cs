using System;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace BookShop.Orders.Manager.Providers
{
    public abstract class ProviderBase
    {
        private readonly string _queueName;
        public IModel Channel { get; }
        public AsyncEventingBasicConsumer Consumer { get; private set; }

        protected ProviderBase(IConnection connection, string queueName)
        {
            _queueName = queueName;
            Channel = connection.CreateModel();
            ConfigureChannel();
        }

        public abstract Task<string> GetResponse(string commandName, string message);

        public void StartListening()
        {
            Consumer = new AsyncEventingBasicConsumer(Channel);
            Channel.BasicConsume(queue: _queueName, autoAck: false, consumer: Consumer);
            Consumer.Received += OnMessageReceivedAsync;
        }

        private void ConfigureChannel()
        {
            Channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
            Channel.BasicQos(0, 1, false);
        }

        private async Task OnMessageReceivedAsync(object model, BasicDeliverEventArgs eventArgs)
        {
            var body = eventArgs.Body.ToArray();
            var props = eventArgs.BasicProperties;
            var replyProps = Channel.CreateBasicProperties();
            replyProps.CorrelationId = props.CorrelationId;
            replyProps.Headers = props.Headers;

            string response = "";
            try
            {
                if (eventArgs.BasicProperties.Headers.TryGetValue("command", out var byteCommand))
                {
                    var message = Encoding.UTF8.GetString(body);
                    var command = Encoding.UTF8.GetString((byte[])byteCommand);
                    response = await GetResponse(command, message);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while handling message: " + e.Message);
            }
            finally
            {
                var responseBytes = Encoding.UTF8.GetBytes(response);
                Channel.BasicPublish(
                    exchange: "",
                    routingKey: props.ReplyTo,
                    basicProperties: replyProps,
                    body: responseBytes);
                Channel.BasicAck(deliveryTag: eventArgs.DeliveryTag, multiple: false);
            }
        }
    }
}
