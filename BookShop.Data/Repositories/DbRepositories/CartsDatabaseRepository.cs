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
    public class CartsDatabaseRepository : BaseDatabaseRepository, ICartsRepository
    {
        public CartsDatabaseRepository(IDbTransaction transaction)
            : base(transaction)
        {
        }

        public async Task<Cart> GetAsync(long id)
        {
            var sql = @"SELECT * FROM carts
                        WHERE carts.cart_id = @id";
            var result = await Connection.QueryAsync<Cart>(sql, new { id });
            return result.SingleOrDefault();
        }

        public async Task<IReadOnlyList<Cart>> GetAsync(IEnumerable<long> ids)
        {
            var sql = @"SELECT * FROM carts
                        WHERE carts.cart_id = any (@ids)";
            var result = await Connection.QueryAsync<Cart>(sql, new { ids });
            return result.ToImmutableList();
        }

        public async Task<Cart> GetByUserIdAsync(long id)
        {
            var sql = @"SELECT * FROM carts
                        WHERE carts.user_id = @id";
            var result = await Connection.QueryAsync<Cart>(sql, new { id });
            return result.SingleOrDefault();
        }

        public async Task<IReadOnlyList<Cart>> GetAsync()
        {
            var sql = @"SELECT * FROM carts";
            var result = await Connection.QueryAsync<Cart>(sql);
            return result.ToImmutableList();
        }

        public async Task<int> CreateAsync(Cart entity)
        {
            var sql = @"INSERT INTO carts (user_id)
                        VALUES (@UserId)";
            return await Connection.ExecuteAsync(sql, entity, Transaction);
        }

        public async Task<int> CreateAsync(long userId)
        {
            var sql = @"INSERT INTO carts (user_id)
                        VALUES (@userId)";
            return await Connection.ExecuteAsync(sql, new { userId }, Transaction);
        }

        public async Task<int> UpdateAsync(Cart entity)
        {
            if (entity == null)
                return 0;

            var sql = @"UPDATE carts
                        SET user_id = @UserId
                        WHERE cart_id = @CartId";
            return await Connection.ExecuteAsync(sql, entity, Transaction);
        }

        public async Task<int> DeleteAsync(long id)
        {
            var sql = @"DELETE FROM carts WHERE cart_id = @id";
            return await Connection.ExecuteAsync(sql, new { id }, Transaction);

        }
    }
}
