using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using BookShop.Notifications.Manager.Managers.Interfaces;
using BookShop.Notifications.Manager.Models;
using RabbitMQ.Client;

namespace BookShop.Notifications.Manager.Providers
{
    public class NotificationSettingsProvider : ProviderBase
    {
        private readonly IStorageManagerFactory<INotificationSettingsStorageManager> _factory;

        public NotificationSettingsProvider(IConnection connection, string queueName, IStorageManagerFactory<INotificationSettingsStorageManager> factory)
            : base(connection, queueName)
        {
            _factory = factory;
        }

        public override async Task<string> GetResponseAsync(string commandName, string message)
        {
            switch (commandName)
            {
                case "get-by-user-id":
                    var userId = JsonSerializer.Deserialize<long>(message);
                    return await GetByUserIdAsync(userId);

                case "delete-by-user-id":
                    var deleteUserId = JsonSerializer.Deserialize<long>(message);
                    return await DeleteByUserIdAsync(deleteUserId); 

                case "get-all":
                    return await GetAllAsync();
                case "get-by-id":
                    var id = JsonSerializer.Deserialize<long>(message);
                    return await GetByIdAsync(id);
                case "get-by-ids":
                    var ids = JsonSerializer.Deserialize<IEnumerable<long>>(message);
                    return await GetByIdsAsync(ids);
                case "create":
                    var newItem = JsonSerializer.Deserialize<NotificationSetting>(message);
                    return await CreateAsync(newItem);
                case "update":
                    var updatedItem = JsonSerializer.Deserialize<NotificationSetting>(message);
                    return await UpdateByUserAsync(updatedItem);
                case "delete":
                    var deleteId = JsonSerializer.Deserialize<long>(message);
                    return await DeleteAsync(deleteId);
            }

            throw new ArgumentException("invalid command name");
        }
        private async Task<string> GetByUserIdAsync(long id)
        {
            using var items = _factory.Create();
            var item = await items.GetByUserIdAsync(id);
            return JsonSerializer.Serialize(item);
        }

        private async Task<string> GetAllAsync()
        {
            using var items = _factory.Create();
            var resultList = await items.GetAllAsync();
            return JsonSerializer.Serialize(resultList);
        }

        private async Task<string> GetByIdAsync(long id)
        {
            using var items = _factory.Create();
            var item = await items.GetByIdAsync(id);
            return JsonSerializer.Serialize(item);
        }

        private async Task<string> GetByIdsAsync(IEnumerable<long> ids)
        {
            using var items = _factory.Create();
            var resultList = await items.GetByIdsAsync(ids);
            return JsonSerializer.Serialize(resultList);
        }
        private async Task<string> CreateAsync(NotificationSetting entity)
        {
            using var items = _factory.Create();
            var rowsAffected = await items.CreateAsync(entity);
            return JsonSerializer.Serialize(rowsAffected);
        }

        private async Task<string> UpdateByUserAsync(NotificationSetting entity)
        {
            using var items = _factory.Create();
            var rowsAffected = await items.UpdateByUserAsync(entity);
            return JsonSerializer.Serialize(rowsAffected);
        }

        private async Task<string> DeleteAsync(long id)
        {
            using var items = _factory.Create();
            var rowsAffected = await items.DeleteAsync(id);
            return JsonSerializer.Serialize(rowsAffected);
        }
        private async Task<string> DeleteByUserIdAsync(long id)
        {
            using var items = _factory.Create();
            var rowsAffected = await items.DeleteByUserIdAsync(id);
            return JsonSerializer.Serialize(rowsAffected);
        }

    }
}