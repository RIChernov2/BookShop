using BookShop.BooksAndAuthors.Manager.Data.Entities;
using BookShop.BooksAndAuthors.Manager.Interfaces;
using BookShop.BooksAndAuthors.Manager.Interfaces.Managers;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BookShop.BooksAndAuthors.Manager.Providers
{
    public class BookProvider : BaseProvider
    {
        private readonly IStorageManagerFactory<IBooksStorageManager> _factory;

        public BookProvider(IConnection connection, string queueName, IStorageManagerFactory<IBooksStorageManager> factory)
            : base(connection, queueName)
        {
            _factory = factory;
        }

        public override async Task<string> HandleMassage(string commandName, string message)
        {
            switch ( commandName )
            {
                case "get-all":
                    return await GetAllAsync();
                case "get-by-id":
                    var id = JsonSerializer.Deserialize<long>(message);
                    return await GetByIdAsync(id);
                case "get-by-ids":
                    var ids = JsonSerializer.Deserialize<IEnumerable<long>>(message);
                    return await GetByIdsAsync(ids);
                case "create":
                    var newItem = JsonSerializer.Deserialize<Book>(message);
                    return await CreateAsync(newItem);
                case "update":
                    var updatedItem = JsonSerializer.Deserialize<Book>(message);
                    return await UpdateAsync(updatedItem);
                case "delete":
                    var deleteId = JsonSerializer.Deserialize<long>(message);
                    return await DeleteAsync(deleteId);
            }

            throw new ArgumentException("invalid command name");
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
        private async Task<string> CreateAsync(Book entity)
        {
            using var items = _factory.Create();
            var rowsAffected = await items.CreateAsync(entity);
            return JsonSerializer.Serialize(rowsAffected);
        }

        private async Task<string> UpdateAsync(Book entity)
        {
            using var items = _factory.Create();
            var rowsAffected = await items.UpdateAsync(entity);
            return JsonSerializer.Serialize(rowsAffected);
        }

        private async Task<string> DeleteAsync(long id)
        {
            using var items = _factory.Create();
            var rowsAffected = await items.DeleteAsync(id);
            return JsonSerializer.Serialize(rowsAffected);
        }
    }
}
