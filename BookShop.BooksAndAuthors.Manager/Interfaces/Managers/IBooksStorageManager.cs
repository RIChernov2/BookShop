using BookShop.BooksAndAuthors.Manager.Data.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookShop.BooksAndAuthors.Manager.Interfaces.Managers
{
    public interface IBooksStorageManager : IDisposable
    {
        Task<IReadOnlyList<Book>> GetAllAsync();
        Task<IReadOnlyList<Book>> GetByIdsAsync(IEnumerable<long> ids);
        Task<Book> GetByIdAsync(long id);
        Task<int> CreateAsync(Book book);
        Task<int> UpdateAsync(Book book);
        Task<int> DeleteAsync(long id);
    }
}
