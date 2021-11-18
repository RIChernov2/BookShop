using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Entities;

namespace Core.StorageManagers.Interfaces
{
    public interface IAuthorStorageManager
    {
        public Task<IReadOnlyList<Author>> GetAsync();
        public Task<IReadOnlyList<Author>> GetAsync(IEnumerable<long> ids);
        public Task<Author> GetAsync(long id);
        Task<int> CreateAsync(Author book);
        Task<int> UpdateAsync(Author book);
        Task<int> DeleteAsync(long id);
    }
}