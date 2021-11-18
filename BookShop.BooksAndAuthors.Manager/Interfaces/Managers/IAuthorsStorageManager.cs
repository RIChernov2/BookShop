using BookShop.BooksAndAuthors.Manager.Data.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookShop.BooksAndAuthors.Manager.Interfaces.Managers
{
    public interface IAuthorsStorageManager : IDisposable
    {
        public Task<IReadOnlyList<Author>> GetAllAsync();
        public Task<IReadOnlyList<Author>> GetByIdsAsync(IEnumerable<long> ids);
        public Task<Author> GetByIdAsync(long id);
        Task<int> CreateAsync(Author book);
        Task<int> UpdateAsync(Author book);
        Task<int> DeleteAsync(long id);
    }
}