using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Data.MessageBrokerClients;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace BookShop.MessageBrokerClients
{

    public abstract class RpcClient : IRpcClient
    {
        private readonly IModel _channel;
        private readonly string _requestQueueName;
        private readonly string _responseQueueName;
        private readonly IBasicProperties _props;
        private readonly ConcurrentDictionary<string, TaskCompletionSource<string>> _pendingMessages;

        /// <summary>
        /// Базовый класс для других RabbitMQ RPC клиентов.
        /// </summary>
        /// <param name="connection">Подключение</param>
        /// <param name="requestQueueName">Имя очереди, в которую будут попадать запросы</param>
        /// <param name="responseQueueName">Имя очереди, в которую будут попадать ответы.
        ///     Если не задано - имя будет сформировано автоматически.
        /// </param>
        protected RpcClient(IConnection connection, string requestQueueName, string responseQueueName = "")
        {
            _pendingMessages = new ConcurrentDictionary<string, TaskCompletionSource<string>>();
            _channel = connection.CreateModel();
            _requestQueueName = requestQueueName;

            if (String.IsNullOrEmpty(responseQueueName))
                _responseQueueName = $"{requestQueueName}-response";

            _props = _channel.CreateBasicProperties();
            _props.Headers = new Dictionary<string, object>();
            _props.ReplyTo = _responseQueueName;

            _channel.QueueDeclare(_responseQueueName);
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += OnMessageReceived;

            _channel.BasicConsume(
                consumer: consumer,
                queue: _responseQueueName,
                autoAck: true);
        }

        /// <summary>
        /// Делает запрос к микросервису через брокер сообщений
        /// </summary>
        /// <param name="commandName">Название команды (аналог route в контроллере)</param>
        /// <param name="message">Объект в JSON формате</param>
        /// <returns>Ответ в формате JSON</returns>
        public async Task<string> CallAsync(string commandName, string message)
        {
            var tcs = new TaskCompletionSource<string>();
            var requestId = Guid.NewGuid().ToString();
            _pendingMessages[requestId] = tcs;
            Publish(commandName, message, requestId);

            return await tcs.Task;
        }

        private void Publish(string commandName, string message, string requestId)
        {
            _props.Headers["command"] = commandName;
            _props.Headers["requestId"] = requestId;
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(
                exchange: "",
                routingKey: _requestQueueName,
                basicProperties: _props,
                body: messageBytes);
        }

        private void OnMessageReceived(object sender, BasicDeliverEventArgs ea)
        {
            var body = ea.Body.ToArray();
            var response = Encoding.UTF8.GetString(body);
            if (ea.BasicProperties.Headers.TryGetValue("requestId", out var byteRequestId))
            {
                var requestId = Encoding.UTF8.GetString((byte[])byteRequestId);
                if (_pendingMessages.TryRemove(requestId, out var tcs))
                {
                    tcs.SetResult(response);
                }
            }
        }
    }
}