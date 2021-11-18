using Data.Entities;
using RabbitMQ.Client;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace BookShop.MessageBrokerClients
{
    public class AuthorsRpcClient : RpcClient
    {
        public AuthorsRpcClient(IConnection connection, string queueName) : base(connection, queueName)
        {
        }


        public async Task<Author> GetById(long id)
        {
            var jsonResult = await CallAsync("get-by-id", id.ToString());
            return JsonSerializer.Deserialize<Author>(jsonResult);
        }

        public async Task<List<Author>> GetByIds(IEnumerable<long> ids)
        {
            var idsString = JsonSerializer.Serialize(ids);
            var jsonResult = await CallAsync("get-by-ids", idsString);
            return JsonSerializer.Deserialize<List<Author>>(jsonResult);
        }

        public async Task<List<Author>> GetAll()
        {
            var jsonResult = await CallAsync("get-all", "");
            return JsonSerializer.Deserialize<List<Author>>(jsonResult);
        }

        public async Task<int> Create(Author entity)
        {
            var author = JsonSerializer.Serialize(entity);
            var jsonResult = await CallAsync("create", author);
            return int.Parse(jsonResult);
        }

        public async Task<int> Update(Author entity)
        {
            var author = JsonSerializer.Serialize(entity);
            var jsonResult = await CallAsync("update", author);
            return int.Parse(jsonResult);
        }

        public async Task<int> Delete(long id)
        {
            var jsonResult = await CallAsync("delete", id.ToString());
            return int.Parse(jsonResult);
        }
    }
}
