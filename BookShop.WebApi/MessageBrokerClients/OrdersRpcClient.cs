using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Data.Entities;
using RabbitMQ.Client;

namespace BookShop.MessageBrokerClients
{
    public class OrdersRpcClient : RpcClient
    {
        public OrdersRpcClient(IConnection connection, string requestQueueName) : base(connection, requestQueueName)
        {
        }

        public async Task<List<Order>> GetByUserId(long userId)
        {
            var jsonResult = await CallAsync("get-by-user-id", userId.ToString());
            return JsonSerializer.Deserialize<List<Order>>(jsonResult);
        }

        public async Task<Order> GetById(long id)
        {
            var jsonResult = await CallAsync("get-by-id", id.ToString());
            return JsonSerializer.Deserialize<Order>(jsonResult);
        }

        public async Task<List<Order>> GetByIds(IEnumerable<long> ids)
        {
            var idsString = JsonSerializer.Serialize(ids);
            var jsonResult = await CallAsync("get-by-ids", idsString);
            return JsonSerializer.Deserialize<List<Order>>(jsonResult);
        }

        public async Task<List<Order>> GetAll()
        {
            var jsonResult = await CallAsync("get-all", "");
            return JsonSerializer.Deserialize<List<Order>>(jsonResult);
        }

        public async Task<int[]> Create(NewOrderInfo newOrderInfo)
        {
            var orderedBooks = newOrderInfo.CartProducts
                .Select(cp => new OrderedBook(cp.Book, cp.Amount)).ToList();
            var newOrder = JsonSerializer.Serialize(new Order(-1, newOrderInfo.UserId, newOrderInfo.AddressId,
                orderedBooks, (byte)OrderStatus.UnPaid, DateTime.Now));
            var jsonResult = await CallAsync("create", newOrder);
            return JsonSerializer.Deserialize<int[]>(jsonResult);
        }

        public async Task<int[]> Update(Order entity)
        {
            var updatedOrder = JsonSerializer.Serialize(entity);
            var jsonResult = await CallAsync("update", updatedOrder);
            return JsonSerializer.Deserialize<int[]>(jsonResult);
        }

        public async Task<int> Delete(long id)
        {
            var jsonResult = await CallAsync("delete", id.ToString());
            return int.Parse(jsonResult);
        }
    }
}