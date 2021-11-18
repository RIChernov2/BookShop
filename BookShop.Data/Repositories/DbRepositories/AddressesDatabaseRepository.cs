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
    public class AddressesDatabaseRepository : BaseDatabaseRepository, IAddressesRepository
    {
        public AddressesDatabaseRepository(IDbTransaction transaction)
            : base(transaction)
        {
        }

        public async Task<IReadOnlyList<Address>> GetByUserIdAsync(long id)
        {
            var sql = @"
                SELECT * FROM addresses
                WHERE user_id = @id
            ";
            var result = await Connection.QueryAsync<Address>(sql, new { id });
            return result.ToImmutableList();
        }

        public async Task<Address> GetAsync(long id)
        {
            var sql = @"
                SELECT * FROM addresses
                WHERE address_id = @id
            ";
            var result = await Connection.QueryAsync<Address>(sql, new { id });
            return result.SingleOrDefault();
        }

        public async Task<IReadOnlyList<Address>> GetAsync(IEnumerable<long> ids)
        {
            var sql = @"
                SELECT * FROM addresses 
                WHERE address_id = any (@ids)
            ";
            var result = await Connection.QueryAsync<Address>(sql, new { ids });
            return result.ToImmutableList();
        }
        
        public async Task<IReadOnlyList<Address>> GetAsync()
        {
            var sql = "SELECT * FROM addresses";
            var result = await Connection.QueryAsync<Address>(sql);
            return result.ToImmutableList();
        }

        public async Task<int> CreateAsync(Address entity)
        {
            if ( entity == null || entity.UserId == 0) return 0;

            var sql = @"
                INSERT INTO addresses (user_id, address_name, country, district, city, street, home, apartments)
                VALUES (@UserId, @AddressName, @Country, @District, @City, @Street, @Home, @Apartments)
            ";
            return await Connection.ExecuteAsync(sql, entity, Transaction);
        }

        public async Task<int> CreateRangeAsync(List<Address> entities)
        {
            if (entities.Count == 0 || entities.Any(e => e.UserId == 0)) return 0;

            var sql = @"
                INSERT INTO addresses (user_id, address_name, country, district, city, street, home, apartments)
                VALUES (@UserId, @AddressName, @Country, @District, @City, @Street, @Home, @Apartments)
            ";
            return await Connection.ExecuteAsync(sql, entities, Transaction);
        }

        public async Task<int> UpdateAsync(Address entity)
        {
            if ( entity == null || entity.UserId == 0) return 0;

            var sql = @"
                UPDATE addresses
                SET user_id = @UserId,
                    address_name = @AddressName,
                    country = @Country,
                    district = @District,
                    city = @City,
                    street = @Street,
                    home = @Home,
                    apartments = @Apartments
                WHERE address_id = @AddressId
            ";
            return await Connection.ExecuteAsync(sql, entity, Transaction);
        }

        public async Task<int> DeleteAsync(long id)
        {
            var sql = @"
                DELETE FROM addresses
                WHERE address_id = @id
            ";
            return await Connection.ExecuteAsync(sql, new { id }, Transaction);
        }

        public async Task<int> DeleteByUserIdAsync(long userId)
        {
            var sql = @"
                DELETE FROM addresses
                WHERE user_id = @userId
            ";
            return await Connection.ExecuteAsync(sql, new { userId }, Transaction);
        }
    }
}