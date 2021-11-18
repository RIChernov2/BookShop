using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Entities;

namespace Data.Repositories.Interfaces
{
    public interface ICartProductsRepository : IGenericRepository<CartProduct, long>
    {
        Task<IReadOnlyList<CartProduct>> GetByCartIdAsync(long id);
        Task<int> CreateRangeAsync(List<CartProduct> entities);
        Task<int> DeleteByCartAsync(long cartId);
    }
}
