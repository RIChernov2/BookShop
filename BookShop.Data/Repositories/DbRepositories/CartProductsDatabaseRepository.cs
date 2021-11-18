using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Data.Entities;
using Data.Repositories.Interfaces;

namespace Data.Repositories.DbRepositories
{
    public class CartProductsDatabaseRepository : BaseDatabaseRepository, ICartProductsRepository
    {
        public CartProductsDatabaseRepository(IDbTransaction transaction)
            : base(transaction)
        {

        }

        public async Task<CartProduct> GetAsync(long id)
        {
            var sql = @"SELECT * FROM cart_products
                        WHERE cart_product_id = @id";
            var result = await Connection.QueryAsync<CartProduct>(sql, new { id });
            return result.SingleOrDefault();
        }

        public async Task<IReadOnlyList<CartProduct>> GetAsync(IEnumerable<long> ids)
        {
            var sql = @"SELECT * FROM cart_products
                        WHERE cart_product_id = any (@ids)";
            var result = await Connection.QueryAsync<CartProduct>(sql, new { ids });
            return result.ToImmutableList();
        }

        public async Task<IReadOnlyList<CartProduct>> GetByCartIdAsync(long id)
        {
            var sql = @"SELECT * FROM cart_products
                        WHERE cart_id = @id";
            var result = await Connection.QueryAsync<CartProduct>(sql, new { id });
            return result.ToImmutableList();
        }

        public async Task<IReadOnlyList<CartProduct>> GetAsync()
        {
            var sql = @"SELECT * FROM cart_products";
            var result = await Connection.QueryAsync<CartProduct>(sql);
            return result.ToImmutableList();
        }

        public async Task<int> CreateAsync(CartProduct entity)
        {
            if (entity == null)
                return 0;

            var sql = @"INSERT INTO cart_products (cart_id, book_id, amount)
                        VALUES (@CartId, @BookId, @Amount)";
            return await Connection.ExecuteAsync(sql, entity, Transaction);
        }

        public async Task<int> CreateRangeAsync(List<CartProduct> entities)
        {
            if (entities == null || entities.Count == 0)
                return 0;

            var sql = @"INSERT INTO cart_products (cart_id, book_id, amount)
                        VALUES (@CartId, @BookId, @Amount)";
            return await Connection.ExecuteAsync(sql, entities, Transaction);
        }

        public async Task<int> UpdateAsync(CartProduct entity)
        {
            if (entity == null)
                return 0;

            var sql = @"UPDATE cart_products
                        SET cart_id = @CartId, 
                            book_id = @BookId,
                            amount = @Amount
                        WHERE cart_product_id = @CartProductId";
            return await Connection.ExecuteAsync(sql, entity, Transaction);
        }

        public async Task<int> DeleteAsync(long id)
        {
            var sql = @"DELETE FROM cart_products WHERE cart_product_id = @id";
            return await Connection.ExecuteAsync(sql, new { id }, Transaction);
        }

        public async Task<int> DeleteByCartAsync(long cartId)
        {
            var sql = @"DELETE FROM cart_products WHERE cart_id = @cartId";
            return await Connection.ExecuteAsync(sql, new { cartId }, Transaction);
        }
    }
}
