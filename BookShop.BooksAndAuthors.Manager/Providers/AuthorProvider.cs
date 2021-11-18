using BookShop.BooksAndAuthors.Manager.Data.Entities;
using BookShop.BooksAndAuthors.Manager.Interfaces.Managers;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace BookShop.BooksAndAuthors.Manager.Providers
{
    public class AuthorProvider : BaseProvider
    {
        private readonly IStorageManagerFactory<IAuthorsStorageManager> _factory;

        public AuthorProvider(IConnection connection, string queueName, IStorageManagerFactory<IAuthorsStorageManager> factory)
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
                    var newItem = JsonSerializer.Deserialize<Author>(message);
                    return await CreateAsync(newItem);
                case "update":
                    var updatedItem = JsonSerializer.Deserialize<Author>(message);
                    return await UpdateAsync(updatedItem);
                case "delete":
                    var deleteId = JsonSerializer.Deserialize<long>(message);
                    return await DeleteAsync(deleteId);
            }

            throw new ArgumentException("invalid command name");
            #region OLD CODE
            //try
            //{
            //    //var ctor = typeof(BookProvider).GetConstructor(new Type[] { });
            //    //var obj = ctor.Invoke(new object[] { });

            //    //var bookProvider = Activator.CreateInstance(typeof(BookProvider), new Object[] { Type.Missing });
            //    //Type type = bookProvider.GetType();
            //    //dynamic task = type.GetMethod(commandName).Invoke(bookProvider, new object[] { message });
            //    //await task;
            //    //result = task.GetAwaiter().GetResult();
            //    return await DeleteAuthor(message);
            //}
            //catch (Exception ex)
            //{
            //    throw;
            //}
            #endregion

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
        private async Task<string> CreateAsync(Author entity)
        {
            using var items = _factory.Create();
            var rowsAffected = await items.CreateAsync(entity);
            return JsonSerializer.Serialize(rowsAffected);
        }

        private async Task<string> UpdateAsync(Author entity)
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
