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
    public class UsersDatabaseRepository : BaseDatabaseRepository, IUsersRepository
    {
        public UsersDatabaseRepository(IDbTransaction transaction) : base(transaction)
        {
        }

        public async Task<User> GetByLoginModelAsync(LoginModel model)
        {
            var sql = @"
                SELECT * FROM users
                LEFT OUTER JOIN addresses ON users.user_id = addresses.user_id
                WHERE users.email = @Email AND users.password = @Password
            ";
            var result = await Connection.QueryAsync<User, Address, User>(sql, UsersMap, model, splitOn: "address_id");

            return GroupByUserId(result).SingleOrDefault();
        }

        public async Task<User> GetAsync(long userId)
        {
            var sql = @"
                SELECT * FROM users
                LEFT OUTER JOIN addresses ON users.user_id = addresses.user_id
                WHERE users.user_id = @userId
            ";
            var result = await Connection.QueryAsync<User, Address, User>(sql, UsersMap, new { userId }, splitOn: "address_id");

            return GroupByUserId(result).SingleOrDefault();
        }

        public async Task<IReadOnlyList<User>> GetAsync(IEnumerable<long> userIds)
        {
            var sql = @"
                SELECT * FROM users
                LEFT OUTER JOIN addresses ON users.user_id = addresses.user_id
                WHERE users.user_id = any (@userIds)
            ";
            var result = await Connection.QueryAsync<User, Address, User>(sql, UsersMap, new { userIds }, splitOn: "address_id");
            return GroupByUserId(result).ToImmutableList();
        }

        public async Task<IReadOnlyList<User>> GetAsync()
        {
            var sql = @"
                SELECT * FROM users
                LEFT OUTER JOIN addresses ON users.user_id = addresses.user_id
            ";
            var result = await Connection.QueryAsync<User, Address, User>(sql, UsersMap, splitOn: "address_id");
            return GroupByUserId(result).ToImmutableList(); ;
        }

        public async Task<int> CreateAsync(User entity)
        {
            if (entity == null) return 0;

            var sql = @"
                INSERT INTO users (name, surname, age, email, password)
                VALUES (@Name, @Surname, @Age, @Email, @Password)
            ";
            return await Connection.ExecuteAsync(sql, entity, Transaction);
        }

        public async Task<int> UpdateAsync(User entity)
        {
            if (entity == null) return 0;
            // Email is unique login, so it can be changed
            // Separate method for password change?

            var sql = @"
                UPDATE users
                SET name = @Name,
                    surname = @Surname,
                    age = @Age
                WHERE user_id = @UserId
            ";
            return await Connection.ExecuteAsync(sql, entity, Transaction);
        }

        public async Task<int> DeleteAsync(long userId)
        {
            var sql = @"
                DELETE FROM users
                WHERE user_id = @userId
            ";
            return await Connection.ExecuteAsync(sql, new { userId }, Transaction);
        }

        private User UsersMap(User user, Address address)
        {
            if (address != null) user.Addresses.Add(address);
            return user;
        }

        private IEnumerable<User> GroupByUserId(IEnumerable<User> users)
        {
            var userDictionary = new Dictionary<long, User>();
            foreach (var user in users)
            {
                if (!userDictionary.TryAdd(user.UserId, user))
                {
                    userDictionary[user.UserId].Addresses.Add(user.Addresses[0]);
                }
            }

            return userDictionary.Values;
        }
    }
}