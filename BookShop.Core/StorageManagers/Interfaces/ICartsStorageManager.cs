using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Entities;

namespace Core.StorageManagers.Interfaces
{
    public interface ICartsStorageManager
    {
        Task<IReadOnlyList<Cart>> GetAsync();
        Task<Cart> GetByUserIdAsync(long id);
        Task<IReadOnlyList<Cart>> GetAsync(IEnumerable<long> ids);
        Task<Cart> GetAsync(long id);
        Task<int> CreateAsync(Cart cart);
        Task<int> CreateAsync(long userId);
        Task<int> UpdateAsync(Cart cart);
        Task<int> DeleteAsync(long id);
    }
}
