using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Data.Entities;
using RabbitMQ.Client;

namespace BookShop.MessageBrokerClients
{
    public class NotificationSettingsRpcClient : RpcClient
    {
        public NotificationSettingsRpcClient(IConnection connection, string requestQueueName) : base(connection, requestQueueName)
        {
        }

        public async Task<NotificationSetting> GetByUserIdAsync(long id)
        {
            var jsonResult = await CallAsync("get-by-user-id", id.ToString());
            return JsonSerializer.Deserialize<NotificationSetting>(jsonResult);
        }
        public async Task<NotificationSetting> GetByIdAsync(long id)
        {
            var jsonResult = await CallAsync("get-by-id", id.ToString());
            return JsonSerializer.Deserialize<NotificationSetting>(jsonResult);
        }

        public async Task<List<NotificationSetting>> GetByIdsAsync(IEnumerable<long> ids)
        {
            var idsString = JsonSerializer.Serialize(ids);
            var jsonResult = await CallAsync("get-by-ids", idsString);
            return JsonSerializer.Deserialize<List<NotificationSetting>>(jsonResult);
        }

        public async Task<List<NotificationSetting>> GetAllAsync()
        {
            var jsonResult = await CallAsync("get-all", "");
            return JsonSerializer.Deserialize<List<NotificationSetting>>(jsonResult);
        }

        public async Task<int> CreateAsync(NotificationSetting entity)
        {
            var newOrder = JsonSerializer.Serialize(entity);
            var jsonResult = await CallAsync("create", newOrder);
            return int.Parse(jsonResult);
        }

        public async Task<int> UpdateAsync(NotificationSetting entity)
        {
            var updatedOrder = JsonSerializer.Serialize(entity);
            var jsonResult = await CallAsync("update", updatedOrder);
            return int.Parse(jsonResult);
        }

        public async Task<int> Delete(long id)
        {
            var jsonResult = await CallAsync("delete", id.ToString());
            return int.Parse(jsonResult);
        }

        public async Task<int> DeleteByUserIdAsync(long id)
        {
            var jsonResult = await CallAsync("delete-by-user-id", id.ToString());
            return int.Parse(jsonResult);
        }
    }
}