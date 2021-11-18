using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Data.Entities;
using RabbitMQ.Client;

namespace BookShop.MessageBrokerClients
{
    public class MessagesRpcClient : RpcClient
    {
        public MessagesRpcClient(IConnection connection, string requestQueueName) : base(connection, requestQueueName)
        {
        }

        public async Task<Message> GetByIdAsync(long id)
        {
            var jsonResult = await CallAsync("get-by-id", id.ToString());
            return JsonSerializer.Deserialize<Message>(jsonResult);
        }
        public async Task<List<Message>> GetByUserIdAsync(long userId)
        {
            var jsonResult = await CallAsync("get-by-user-id", userId.ToString());
            return JsonSerializer.Deserialize<List<Message>>(jsonResult);
        }

        public async Task<List<Message>> GetByUserAndSettingsAsync(long userId)
        {
            var jsonResult = await CallAsync("get-by-user-and-settings", userId.ToString());
            return JsonSerializer.Deserialize<List<Message>>(jsonResult);
        }

        public async Task<List<Message>> GetByIdsAsync(IEnumerable<long> ids)
        {
            var idsString = JsonSerializer.Serialize(ids);
            var jsonResult = await CallAsync("get-by-ids", idsString);
            return JsonSerializer.Deserialize<List<Message>>(jsonResult);
        }

        public async Task<List<Message>> GetAllAsync()
        {
            var jsonResult = await CallAsync("get-all", "");
            return JsonSerializer.Deserialize<List<Message>>(jsonResult);
        }

        public async Task<int> CreateAsync(Message entity)
        {
            var newOrder = JsonSerializer.Serialize(entity);
            var jsonResult = await CallAsync("create", newOrder);
            return int.Parse(jsonResult);
        }

        public async Task<int> UpdateAsync(Message entity)
        {
            var updatedOrder = JsonSerializer.Serialize(entity);
            var jsonResult = await CallAsync("update", updatedOrder);
            return int.Parse(jsonResult);
        }

        public async Task<int> DeleteAsync(long id)
        {
            var jsonResult = await CallAsync("delete", id.ToString());
            return int.Parse(jsonResult);
        }
    }
}