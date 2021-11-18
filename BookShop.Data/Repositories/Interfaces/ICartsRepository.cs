using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Entities;

namespace Data.Repositories.Interfaces
{
    public interface ICartsRepository : IGenericRepository<Cart, long>
    {
        Task<Cart> GetByUserIdAsync(long userId);
        Task<int> CreateAsync(long userId);
    }
}
