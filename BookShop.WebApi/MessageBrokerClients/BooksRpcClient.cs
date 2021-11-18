using Data.Entities;
using RabbitMQ.Client;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace BookShop.MessageBrokerClients
{
    public class BooksRpcClient : RpcClient
    {
        public BooksRpcClient(IConnection connection, string queueName) : base (connection, queueName)
        {
        }

        public async Task<Book> GetById(long id)
        {
            var jsonResult = await CallAsync("get-by-id", id.ToString());
            return JsonSerializer.Deserialize<Book>(jsonResult);
        }

        public async Task<List<Book>> GetByIds(IEnumerable<long> ids)
        {
            var idsString = JsonSerializer.Serialize(ids);
            var jsonResult = await CallAsync("get-by-ids", idsString);
            return JsonSerializer.Deserialize<List<Book>>(jsonResult);
        }

        public async Task<List<Book>> GetAll()
        {
            var jsonResult = await CallAsync("get-all", "");
            return JsonSerializer.Deserialize<List<Book>>(jsonResult);
        }

        public async Task<int> Create(Book entity)
        {
            var book = JsonSerializer.Serialize(entity);
            var jsonResult = await CallAsync("create", book);
            return int.Parse(jsonResult);
        }

        public async Task<int> Update(Book entity)
        {
            var book = JsonSerializer.Serialize(entity);
            var jsonResult = await CallAsync("update", book);
            return int.Parse(jsonResult);
        }

        public async Task<int> Delete(long id)
        {
            var jsonResult = await CallAsync("delete", id.ToString());
            return int.Parse(jsonResult);
        }
    }
}
