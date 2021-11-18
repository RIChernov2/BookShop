using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using BookShop.Orders.Manager.Configurations.Models;
using BookShop.Orders.Manager.Models;
using BookShop.Orders.Manager.Repositories.Interfaces;
using Dapper;

namespace BookShop.Orders.Manager.Repositories
{
    public class OrderedBooksDatabaseRepository : BaseDatabaseRepository, IOrderedBooksRepository
    {
        public OrderedBooksDatabaseRepository(IDbTransaction transaction, AppConfiguration ñonfiguration)
            : base(transaction, ñonfiguration) { }

        public async Task<OrderedBook> GetAsync(long orderedBookId)
        {
            var sql = $@"
                SELECT * FROM {SchemaName}.ordered_books
                WHERE ordered_book_id = @orderedBookId
            ";
            var result = await Connection.QueryAsync<OrderedBook>(sql, new { orderedBookId });

            return result.SingleOrDefault();
        }

        public async Task<IReadOnlyList<OrderedBook>> GetAsync(IEnumerable<long> ids)
        {
            var sql = $@"
                SELECT * FROM {SchemaName}.ordered_books
                WHERE ordered_book_id = any (@ids)
            ";
            var result = await Connection.QueryAsync<OrderedBook> (sql, new { ids });

            return result.ToImmutableList();
        }

        public async Task<IReadOnlyList<OrderedBook>> GetAsync()
        {
            var sql = $@"SELECT * FROM {SchemaName}.ordered_books";
            var result = await Connection.QueryAsync<OrderedBook>(sql);

            return result.ToImmutableList();
        }

        public async Task<int> CreateAsync(OrderedBook entity)
        {
            if ( entity == null ) return 0;

            var sql = $@"
                INSERT INTO {SchemaName}.ordered_books (order_id, book_id, retail_price, amount)
                VALUES (@OrderId, @BookId, @RetailPrice, @Amount)
            ";
            return await Connection.ExecuteAsync(sql, entity, Transaction);
        }

        public async Task<int> CreateRangeAsync(List<OrderedBook> entities)
        {
            if (entities.Count == 0) return 0;

            var sql = $@"
                INSERT INTO {SchemaName}.ordered_books (order_id, book_id, retail_price, amount)
                VALUES (@OrderId, @BookId, @RetailPrice, @Amount)
            ";
            return await Connection.ExecuteAsync(sql, entities, Transaction);
        }

        public async Task<int> UpdateAsync(OrderedBook entity)
        {
            if ( entity == null ) return 0;

            var sql = $@"
                UPDATE {SchemaName}.ordered_books
                SET order_id = @OrderId,
                    book_id = @BookId,
                    retail_price = @RetailPrice,
                    amount = @Amount
                WHERE ordered_book_id = @OrderedBookId
            ";
            return await Connection.ExecuteAsync(sql, entity, Transaction);
        }

        public async Task<int> DeleteAsync(long orderedBookId)
        {
            var sql = $@"
                DELETE FROM {SchemaName}.ordered_books
                WHERE ordered_book_id = @orderedBookId
            ";
            return await Connection.ExecuteAsync(sql, new { orderedBookId }, Transaction);
        }

        public async Task<int> DeleteByOrderIdAsync(long orderedId)
        {
            var sql = $@"
                DELETE FROM {SchemaName}.ordered_books
                WHERE order_id = @orderedId
            ";
            return await Connection.ExecuteAsync(sql, new { orderedId }, Transaction);
        }
    }
}