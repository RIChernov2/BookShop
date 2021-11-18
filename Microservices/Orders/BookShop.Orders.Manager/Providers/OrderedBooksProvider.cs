using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using BookShop.Orders.Manager.Managers.Interfaces;
using BookShop.Orders.Manager.Models;
using NLog;
using RabbitMQ.Client;

namespace BookShop.Orders.Manager.Providers
{
    public class OrderedBooksProvider : ProviderBase
    {
        private readonly IStorageManagerFactory<IOrderedBooksStorageManager> _obsmFactory;
        private readonly Logger _logger;

        public OrderedBooksProvider(IConnection connection, string queueName, IStorageManagerFactory<IOrderedBooksStorageManager> obsmFactory)
            : base(connection, queueName)
        {
            _obsmFactory = obsmFactory;
            _logger = LogManager.GetCurrentClassLogger();
        }

        public override async Task<string> GetResponse(string commandName, string message)
        {
            _logger.Info($"GetResponse method. {commandName}");
            switch (commandName)
            {
                case "get-all":
                    return await GetAllOrders();
                case "get-by-id":
                    var id = JsonSerializer.Deserialize<long>(message);
                    return await GetOrderById(id);
                case "get-by-ids":
                    var ids = JsonSerializer.Deserialize<IEnumerable<long>>(message);
                    return await GetOrdersByIds(ids);
                case "create":
                    var newOrderedBook = JsonSerializer.Deserialize<OrderedBook>(message);
                    return await CreateOrder(newOrderedBook);
                case "update":
                    var updatedOrderedBook = JsonSerializer.Deserialize<OrderedBook>(message);
                    return await UpdateOrder(updatedOrderedBook);
                case "delete":
                    var deleteId = JsonSerializer.Deserialize<long>(message);
                    return await DeleteOrder(deleteId);
            }

            throw new ArgumentException("invalid command name");
        }

        private async Task<string> GetAllOrders()
        {
            using var orders = _obsmFactory.Create();
            var orderList = await orders.GetAsync();
            return JsonSerializer.Serialize(orderList);
        }

        private async Task<string> GetOrderById(long id)
        {
            using var orders = _obsmFactory.Create();
            var order = await orders.GetAsync(id);
            return JsonSerializer.Serialize(order);
        }

        private async Task<string> GetOrdersByIds(IEnumerable<long> ids)
        {
            using var orders = _obsmFactory.Create();
            var orderList = await orders.GetAsync(ids);
            return JsonSerializer.Serialize(orderList);
        }
        private async Task<string> CreateOrder(OrderedBook entity)
        {
            using var orders = _obsmFactory.Create();
            var rowsAffected = await orders.CreateAsync(entity);
            return JsonSerializer.Serialize(rowsAffected);
        }

        private async Task<string> UpdateOrder(OrderedBook entity)
        {
            using var orders = _obsmFactory.Create();
            var rowsAffected = await orders.UpdateAsync(entity);
            return JsonSerializer.Serialize(rowsAffected);
        }

        private async Task<string> DeleteOrder(long id)
        {
            using var orders = _obsmFactory.Create();
            var rowsAffected = await orders.DeleteAsync(id);
            return JsonSerializer.Serialize(rowsAffected);
        }
    }
}