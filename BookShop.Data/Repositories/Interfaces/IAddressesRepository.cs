using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Entities;

namespace Data.Repositories.Interfaces
{
    public interface IAddressesRepository : IGenericRepository<Address, long>
    {
        Task<IReadOnlyList<Address>> GetByUserIdAsync(long id);
        Task<int> CreateRangeAsync(List<Address> entities);
        Task<int> DeleteByUserIdAsync(long userId);
    }
}