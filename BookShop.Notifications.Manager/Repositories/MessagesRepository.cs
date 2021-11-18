using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using BookShop.Notifications.Manager.Configurations.Models;
using BookShop.Notifications.Manager.Handlers.Interfaces;
using BookShop.Notifications.Manager.Models;
using BookShop.Notifications.Manager.Repositories.Interfaces;
using Dapper;

namespace BookShop.Notifications.Manager.Repositories
{
    public class MessagesRepository : BaseDatabaseRepository, IMessagesRepository
    {
        public MessagesRepository(IDbTransaction transaction, AppConfiguration ñonfiguration)
            : base(transaction, ñonfiguration) { }

        public async Task<IReadOnlyList<Message>> GetByUserIdAsync(long userId)
        {
            var sql = $@"
                SELECT * FROM {SchemaName}.messages
                WHERE messages.user_id = @userId
            ";
            var result = await Connection.QueryAsync<Message>(sql, new { userId });
            return result.ToImmutableList();
        }

        public async Task<Message> GetByIdAsync(long id)
        {
            var sql = $@"
                SELECT * FROM {SchemaName}.messages
                WHERE messages.message_id = @id
            ";
            var result = await Connection.QueryAsync<Message>(sql, new { id });
            return result.SingleOrDefault();
        }

        public async Task<IReadOnlyList<Message>> GetByIdsAsync(IEnumerable<long> ids)
        {
            var sql = $@"
                SELECT * FROM {SchemaName}.messages
                WHERE messages.message_id = any (@ids)
            ";
            var result = await Connection.QueryAsync<Message>(sql, new { ids });
            return result.ToImmutableList();
        }

        public async Task<IReadOnlyList<Message>> GetAllAsync()
        {
            var sql = $@"
                SELECT * FROM {SchemaName}.messages
            ";
            var result = await Connection.QueryAsync<Message>(sql);
            return result.ToImmutableList();
        }
        
        public async Task<int> CreateAsync(Message entity)
        {
            if ( entity == null ) return 0;

            var sql = $@"
                INSERT INTO {SchemaName}.messages (user_id, date, type, text)
                VALUES (@UserId, @Date, '{entity.Type}', @Text)
            ";
            return await Connection.ExecuteAsync(sql, entity, Transaction);
        }

        public async Task<int> UpdateByUserAsync(Message entity)
        {
            if ( entity == null ) return 0;

            var sql = $@"
                UPDATE {SchemaName}.messages
                SET user_id = @UserId,
                    date = @Date,
                    type = '{entity.Type}',
                    text = @Text
                WHERE message_id = @MessageId
            ";

            return await Connection.ExecuteAsync(sql, entity, Transaction);
        }

        public async Task<int> DeleteAsync(long id)
        {
            var sql = $@"
                DELETE FROM {SchemaName}.messages
                WHERE message_id = @id
            ";
            return await Connection.ExecuteAsync(sql, new { id }, Transaction);
        }
    }
}