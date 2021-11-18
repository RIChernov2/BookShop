using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Entities;

namespace Core.StorageManagers.Interfaces
{
    public interface IUsersStorageManager
    {
        public Task<User> GetByLoginModelAsync(LoginModel model);
        public Task<IReadOnlyList<User>> GetAsync();
        public Task<IReadOnlyList<User>> GetAsync(IEnumerable<long> ids);
        public Task<User> GetAsync(long id);
        public Task<int[]> CreateAsync(User user);
        public Task<int[]> UpdateAsync(User user);
        public Task<int> DeleteAsync(long id);
    }
}