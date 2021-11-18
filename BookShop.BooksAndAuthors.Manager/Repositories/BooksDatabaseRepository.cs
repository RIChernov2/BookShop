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
    public class BooksDatabaseRepository : BaseDatabaseRepository, IBooksRepository
    {
        public BooksDatabaseRepository(IDbTransaction transaction, AppConfiguration ñonfiguration)
            : base(transaction, ñonfiguration) { }

        public async Task<Book> GetAsync(long id)
        {
            var sql = $@"
                SELECT * FROM {SchemaName}.books
                JOIN {SchemaName}.authors ON books.author_id = authors.author_id
                WHERE books.book_id = @id
            ";
            var result = await Connection.QueryAsync<Book, Author, Book>(sql, BooksMap, new { id }, splitOn: "author_id");
            return result.SingleOrDefault();
        }

        public async Task<IReadOnlyList<Book>> GetAsync(IEnumerable<long> ids)
        {
            var sql = $@"
                SELECT * FROM {SchemaName}.books
                JOIN {SchemaName}.authors ON books.author_id = authors.author_id
                WHERE books.book_id = any (@ids)
            ";
            var result = await Connection.QueryAsync<Book, Author, Book>(sql, BooksMap, new { ids }, splitOn: "author_id");
            return result.ToImmutableList();
        }

        public async Task<IReadOnlyList<Book>> GetAsync()
        {
            var sql = $@"
                SELECT * FROM {SchemaName}.books 
                JOIN {SchemaName}.authors ON books.author_id = authors.author_id
            ";
            var result = await Connection.QueryAsync<Book, Author, Book>(sql, BooksMap, splitOn: "author_id");
            return result.ToImmutableList();
        }

        public async Task<int> CreateAsync(Book entity)
        {
            if (entity == null) return 0;

            var sql = $@"
                INSERT INTO {SchemaName}.books (author_id, title, description, price)
                VALUES ({entity.Author.AuthorId}, @Title, @Description, @Price)
            ";
            return await Connection.ExecuteAsync(sql, entity, Transaction);
        }

        public async Task<int> UpdateAsync(Book entity)
        {
            if (entity == null) return 0;

            var sql = $@"
                UPDATE {SchemaName}.books
                SET title = @Title,
                    description = @Description,
                    author_id = {entity.Author.AuthorId},
                    price = @Price
                WHERE book_id = @BookId
            ";
            return await Connection.ExecuteAsync(sql, entity, Transaction);
        }

        public async Task<int> DeleteAsync(long id)
        {
            var sql = $@"
                DELETE FROM {SchemaName}.books
                WHERE book_id = @id
            ";
            return await Connection.ExecuteAsync(sql, new { id }, Transaction);
        }

        private Book BooksMap(Book book, Author author)
        {
            book.Author = author;
            return book;
        }
    }
}