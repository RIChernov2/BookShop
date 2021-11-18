using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using BookShop.BooksAndAuthors.Manager.Configurations.Models;
using BookShop.BooksAndAuthors.Manager.Data.Entities;
using BookShop.BooksAndAuthors.Manager.Interfaces.Repositories;
using Dapper;

namespace BookShop.BooksAndAuthors.Manager.Repositories
{
    public class AuthorsDatabaseRepository : BaseDatabaseRepository, IAuthorsRepository
    {
        public AuthorsDatabaseRepository(IDbTransaction transaction, AppConfiguration сonfiguration)
            : base(transaction, сonfiguration) { }

        public async Task<Author> GetAsync(long authorId)
        {

            var sql = $@"
                SELECT * FROM {SchemaName}.authors
                WHERE authors.author_id = @authorId
            ";
            var result = await Connection.QueryAsync<Author>(sql, new { authorId });
            return result.SingleOrDefault();
        }

        public async Task<IReadOnlyList<Author>> GetAsync(IEnumerable<long> authorIds)
        {
            var sql = $@"
                SELECT * FROM {SchemaName}.authors
                WHERE authors.author_id = any (@authorIds)
            ";
            var result = await Connection.QueryAsync<Author>(sql, new { authorIds });
            return result.ToImmutableList();
        }

        public async Task<IReadOnlyList<Author>> GetAsync()
        {
            var sql = $@"
                SELECT * FROM {SchemaName}.authors
            ";
            var result = await Connection.QueryAsync<Author>(sql);
            return result.ToImmutableList();
        }

        public async Task<int> CreateAsync(Author entity)
        {
            if ( entity == null ) return 0;

            var sql = $@"
                INSERT INTO {SchemaName}.authors (name, surname, age)
                VALUES (@Name, @Surname, @Age)
            ";
            return await Connection.ExecuteAsync(sql, entity, Transaction);
        }

        public async Task<int> UpdateAsync(Author entity)
        {
            if ( entity == null ) return 0;

            var sql = $@"
                UPDATE {SchemaName}.authors
                SET name = @Name,
                    surname = @Surname,
                    age = @Age
                WHERE author_id = @AuthorId
            ";
            return await Connection.ExecuteAsync(sql, entity, Transaction);
        }

        public async Task<int> DeleteAsync(long authorId)
        {
            var sql = $@"
                DELETE FROM {SchemaName}.authors
                WHERE author_id = @AuthorId
            ";
            return await Connection.ExecuteAsync(sql, new { authorId }, Transaction);
        }
    }
}