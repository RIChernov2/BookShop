using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Entities;

namespace Core.StorageManagers.Interfaces
{
    public interface ICartProductsStorageManager
    {
        Task<CartProduct> GetAsync(long id);
        Task<IReadOnlyList<CartProduct>> GetByCartIdAsync(long id);
        Task<IReadOnlyList<CartProduct>> GetAsync(IEnumerable<long> ids);
        Task<IReadOnlyList<CartProduct>> GetAsync();
        Task<int> CreateAsync(CartProduct cartProduct);
        Task<int> CreateRangeAsync(List<CartProduct> carts);
        Task<int> UpdateAsync(CartProduct cartProduct);
        Task<int> DeleteAsync(long id);
        Task<int> DeleteByCartAsync(long cartId);
    }
}
