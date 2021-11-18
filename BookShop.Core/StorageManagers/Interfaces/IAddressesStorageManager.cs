using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Entities;

namespace Core.StorageManagers.Interfaces
{
    public interface IAddressesStorageManager
    {
        public Task<IReadOnlyList<Address>> GetAsync();
        public Task<IReadOnlyList<Address>> GetAsync(IEnumerable<long> ids);
        public Task<Address> GetAsync(long id);
        Task<int> CreateAsync(Address book);
        Task<int> UpdateAsync(Address book);
        Task<int> DeleteAsync(long id);
        public Task<IReadOnlyList<Address>> GetByUserIdAsync(long id);
    }
}