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
    public class OrdersProvider : ProviderBase
    {
        private readonly IStorageManagerFactory<IOrdersStorageManager> _osmFactory;
        private readonly Logger _logger;

        public OrdersProvider(IConnection connection, string queueName, IStorageManagerFactory<IOrdersStorageManager> osmFactory)
            : base(connection, queueName)
        {
            _osmFactory = osmFactory;
            _logger = LogManager.GetCurrentClassLogger();
        }

        public override async Task<string> GetResponse(string commandName, string message)
        {
            _logger.Info($"GetResponse method. ${commandName}.");

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
                case "get-by-user-id":
                    var userId = JsonSerializer.Deserialize<long>(message);
                    return await GetOrdersByUserId(userId);
                case "create":
                    var newOrder = JsonSerializer.Deserialize<Order>(message);
                    return await CreateOrder(newOrder);
                case "update":
                    var updatedOrder = JsonSerializer.Deserialize<Order>(message);
                    return await UpdateOrder(updatedOrder);
                case "delete":
                    var deleteId = JsonSerializer.Deserialize<long>(message);
                    return await DeleteOrder(deleteId);
            }

            throw new ArgumentException("invalid command name");
        }

        private async Task<string> GetAllOrders()
        {
            using var orders = _osmFactory.Create();
            var orderList = await orders.GetAsync();
            return JsonSerializer.Serialize(orderList);
        }

        private async Task<string> GetOrderById(long id)
        {
            using var orders = _osmFactory.Create();
            var order = await orders.GetAsync(id);
            return JsonSerializer.Serialize(order);
        }

        private async Task<string> GetOrdersByIds(IEnumerable<long> ids)
        {
            using var orders = _osmFactory.Create();
            var orderList = await orders.GetAsync(ids);
            return JsonSerializer.Serialize(orderList);
        }

        private async Task<string> GetOrdersByUserId(long userId)
        {
            using var orders = _osmFactory.Create();
            var orderList = await orders.GetByUserIdAsync(userId);
            return JsonSerializer.Serialize(orderList);
        }

        private async Task<string> CreateOrder(Order entity)
        {
            using var orders = _osmFactory.Create();
            var rowsAffected = await orders.CreateAsync(entity);
            var serialized = JsonSerializer.Serialize(rowsAffected);
            return serialized;
        }

        private async Task<string> UpdateOrder(Order entity)
        {
            using var orders = _osmFactory.Create();
            var rowsAffected = await orders.UpdateAsync(entity);
            return JsonSerializer.Serialize(rowsAffected);
        }

        private async Task<string> DeleteOrder(long id)
        {
            using var orders = _osmFactory.Create();
            var rowsAffected = await orders.DeleteAsync(id);
            return JsonSerializer.Serialize(rowsAffected);
        }
    }
}