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
    public class OrdersDatabaseRepository : BaseDatabaseRepository, IOrdersRepository
    {
        public OrdersDatabaseRepository(IDbTransaction transaction, AppConfiguration ñonfiguration)
            : base(transaction, ñonfiguration) { }

        public async Task<IReadOnlyList<Order>> GetByUserIdAsync(long userId)
        {
            var sql = $@"
                SELECT * FROM {SchemaName}.orders
                JOIN {SchemaName}.ordered_books ON orders.order_id = ordered_books.order_id
                WHERE orders.user_id = @userId
            ";
            var result = await Connection.QueryAsync<Order, OrderedBook, Order>
                (sql, OrdersMap, new { userId }, splitOn: "ordered_book_id");

            return GroupByOrderId(result).ToImmutableList();
        }

        public async Task<Order> GetAsync(long id)
        {
            var sql = $@"
                SELECT * FROM {SchemaName}.orders
                JOIN {SchemaName}.ordered_books ON orders.order_id = ordered_books.order_id
                WHERE orders.order_id = @id
            ";
            var result = await Connection.QueryAsync<Order, OrderedBook, Order>
                (sql, OrdersMap, new { id }, splitOn: "ordered_book_id");

            return GroupByOrderId(result).SingleOrDefault();
        }

        public async Task<IReadOnlyList<Order>> GetAsync(IEnumerable<long> ids)
        {
            var sql = $@"
                SELECT * FROM {SchemaName}.orders
                JOIN {SchemaName}.ordered_books ON orders.order_id = ordered_books.order_id
                WHERE orders.order_id = any (@ids)
            ";
            var result = await Connection.QueryAsync<Order, OrderedBook, Order>
                (sql, OrdersMap, new { ids }, splitOn: "ordered_book_id");

            return GroupByOrderId(result).ToImmutableList();
        }

        public async Task<IReadOnlyList<Order>> GetAsync()
        {
            var sql = $@"
                SELECT * FROM {SchemaName}.orders
                JOIN {SchemaName}.ordered_books ON orders.order_id = ordered_books.order_id
            ";
            var result = await Connection.QueryAsync<Order, OrderedBook, Order>
                (sql, OrdersMap, splitOn: "ordered_book_id");

            return GroupByOrderId(result).ToImmutableList();
        }
        
        public async Task<int> CreateAsync(Order entity)
        {
            if ( entity == null ) return 0;

            var sql = $@"
                INSERT INTO {SchemaName}.orders (user_id, address_id, order_status, creation_date)
                VALUES (@UserId, @AddressId, @OrderStatus, @CreationDate)
                RETURNING order_id;
            ";
            var result = await Connection.QueryAsync<int>(sql, entity, Transaction);

            return result.SingleOrDefault();
        }

        public async Task<int> UpdateAsync(Order entity)
        {
            if ( entity == null ) return 0;

            var sql = $@"
                UPDATE {SchemaName}.orders
                SET user_id = @UserId,
                    address_id = @AddressId,
                    order_status = @OrderStatus,
                    creation_date = @CreationDate
                WHERE order_id = @OrderId
            ";

            return await Connection.ExecuteAsync(sql, entity, Transaction);
        }

        public async Task<int> DeleteAsync(long orderId)
        {
            var sql = $@"
                DELETE FROM {SchemaName}.orders
                WHERE order_id = @orderId
            ";
            return await Connection.ExecuteAsync(sql, new { orderId }, Transaction);
        }

        private Order OrdersMap(Order order, OrderedBook orderedBook)
        {
            if (orderedBook != null) order.OrderedBooks.Add(orderedBook);
            return order;
        }

        private IEnumerable<Order> GroupByOrderId(IEnumerable<Order> orders)
        {
            Dictionary<long, Order> values = new Dictionary<long, Order>();
            foreach ( var order in orders )
            {
                if ( !values.TryAdd(order.OrderId, order) )
                {
                    values[order.OrderId].OrderedBooks.Add(order.OrderedBooks[0]);
                }
            }
            return values.Values;
        }
    }
}