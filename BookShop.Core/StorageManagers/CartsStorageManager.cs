using System.Collections.Generic;
using System.Threading.Tasks;
using Core.StorageManagers.Interfaces;
using Data.Entities;
using Data.Repositories.Interfaces;

namespace Core.StorageManagers
{
    public class CartsStorageManager : ICartsStorageManager
    {
        private readonly IUnitOfWork _uow;

        public CartsStorageManager(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<Cart> GetAsync(long id)
            => await _uow.CartsRepository.GetAsync(id);

        public async Task<Cart> GetByUserIdAsync(long userId)
        {
            var result = await _uow.CartsRepository.GetByUserIdAsync(userId);
            if (result == null) await _uow.CartsRepository.CreateAsync(userId);
            return await _uow.CartsRepository.GetByUserIdAsync(userId);
        }

        public async Task<IReadOnlyList<Cart>> GetAsync(IEnumerable<long> ids)
            => await _uow.CartsRepository.GetAsync(ids);

        public async Task<IReadOnlyList<Cart>> GetAsync()
            => await _uow.CartsRepository.GetAsync();

        public async Task<int> CreateAsync(Cart cart)
        {
            var result = await _uow.CartsRepository.CreateAsync(cart);
            _uow.Commit();
            return result;
        }

        public async Task<int> CreateAsync(long userId)
        {
            var result = await _uow.CartsRepository.CreateAsync(userId);
            _uow.Commit();
            return result;
        }

        public async Task<int> UpdateAsync(Cart cart)
        {
            var result = await _uow.CartsRepository.UpdateAsync(cart);
            _uow.Commit();
            return result;
        }

        public async Task<int> DeleteAsync(long id)
        {
            var result = await _uow.CartsRepository.DeleteAsync(id);
            _uow.Commit();
            return result;
        }
    }
}
