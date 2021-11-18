using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Data.Entities;
using RabbitMQ.Client;

namespace BookShop.MessageBrokerClients
{
    public class OrderedBooksRpcClient : RpcClient
    {
        public OrderedBooksRpcClient(IConnection connection, string requestQueueName) : base(connection, requestQueueName)
        {
        }

        public async Task<OrderedBook> GetById(long id)
        {
            var jsonResult = await CallAsync("get-by-id", id.ToString());
            return JsonSerializer.Deserialize<OrderedBook>(jsonResult);
        }

        public async Task<List<OrderedBook>> GetByIds(IEnumerable<long> ids)
        {
            var idsString = JsonSerializer.Serialize(ids);
            var jsonResult = await CallAsync("get-by-ids", idsString);
            return JsonSerializer.Deserialize<List<OrderedBook>>(jsonResult);
        }

        public async Task<List<OrderedBook>> GetAll()
        {
            var jsonResult = await CallAsync("get-all", "");
            return JsonSerializer.Deserialize<List<OrderedBook>>(jsonResult);
        }

        public async Task<int> Create(OrderedBook entity)
        {
            var newOrderedBook = JsonSerializer.Serialize(entity);
            var jsonResult = await CallAsync("create", newOrderedBook);
            return int.Parse(jsonResult);
        }

        public async Task<int> Update(OrderedBook entity)
        {
            var updatedOrderedBook = JsonSerializer.Serialize(entity);
            var jsonResult = await CallAsync("update", updatedOrderedBook);
            return int.Parse(jsonResult);
        }

        public async Task<int> Delete(long id)
        {
            var jsonResult = await CallAsync("delete", id.ToString());
            return int.Parse(jsonResult);
        }
    }
}