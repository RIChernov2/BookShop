using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Entities;

namespace Core.StorageManagers.Interfaces
{
    public interface IBooksStorageManager
    {
        Task<IReadOnlyList<Book>> Get();
        Task<IReadOnlyList<Book>> Get(IEnumerable<long> ids);
        Task<Book> Get(long id);
        Task<int> CreateAsync(Book book);
        Task<int> UpdateAsync(Book book);
        Task<int> DeleteAsync(long id);
    }
}