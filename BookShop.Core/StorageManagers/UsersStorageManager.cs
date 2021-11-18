using System.Collections.Generic;
using System.Threading.Tasks;
using Core.StorageManagers.Interfaces;
using Data.Entities;
using Data.Repositories.Interfaces;

namespace Core.StorageManagers
{
    public class UsersStorageManager : IUsersStorageManager
    {
        private readonly IUnitOfWork _uow;

        public UsersStorageManager(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<User> GetByLoginModelAsync(LoginModel model)
            => await _uow.UsersRepository.GetByLoginModelAsync(model);

        public async Task<IReadOnlyList<User>> GetAsync()
            => await _uow.UsersRepository.GetAsync();

        public async Task<IReadOnlyList<User>> GetAsync(IEnumerable<long> ids) 
            => await _uow.UsersRepository.GetAsync(ids);

        public async Task<User> GetAsync(long id)
            => await _uow.UsersRepository.GetAsync(id);

        public async Task<int[]> CreateAsync(User user)
        {
            var result1 = await _uow.UsersRepository.CreateAsync(user);
            await _uow.AdressesRepository.DeleteByUserIdAsync(user.UserId);
            var result2 = await _uow.AdressesRepository.CreateRangeAsync(user.Addresses);
            _uow.Commit();
            return new[] { result1, result2 };
        }

        public async Task<int[]> UpdateAsync(User user)
        {
            var result1 = await _uow.UsersRepository.UpdateAsync(user);
            await _uow.AdressesRepository.DeleteByUserIdAsync(user.UserId);
            var result2 = await _uow.AdressesRepository.CreateRangeAsync(user.Addresses);
            _uow.Commit();
            return new[] { result1, result2 };
        }

        public async Task<int> DeleteAsync(long id)
        {
            var result = await _uow.UsersRepository.DeleteAsync(id);
            _uow.Commit();
            return result;
        }
    }
}