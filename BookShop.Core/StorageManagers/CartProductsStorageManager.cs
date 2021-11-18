using System.Collections.Generic;
using System.Threading.Tasks;
using Core.StorageManagers.Interfaces;
using Data.Entities;
using Data.Repositories.Interfaces;

namespace Core.StorageManagers
{
    public class CartProductsStorageManager : ICartProductsStorageManager
    {
        private readonly IUnitOfWork _uow;

        public CartProductsStorageManager(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<CartProduct> GetAsync(long id)
            => await _uow.CartProductsRepository.GetAsync(id);

        public async Task<IReadOnlyList<CartProduct>> GetByCartIdAsync(long id)
            => await _uow.CartProductsRepository.GetByCartIdAsync(id);

        public async Task<IReadOnlyList<CartProduct>> GetAsync(IEnumerable<long> ids)
            => await _uow.CartProductsRepository.GetAsync(ids);
        public async Task<IReadOnlyList<CartProduct>> GetAsync()
            => await _uow.CartProductsRepository.GetAsync();

        public async Task<int> CreateAsync(CartProduct cartProduct)
        {
            var result = await _uow.CartProductsRepository.CreateAsync(cartProduct);
            _uow.Commit();
            return result;
        }

        public async Task<int> CreateRangeAsync(List<CartProduct> cartProducts)
        {
            var result = await _uow.CartProductsRepository.CreateRangeAsync(cartProducts);
            _uow.Commit();
            return result;
        }

        public async Task<int> UpdateAsync(CartProduct cartProduct)
        {
            var result = await _uow.CartProductsRepository.UpdateAsync(cartProduct);
            _uow.Commit();
            return result;
        }

        public async Task<int> DeleteAsync(long id)
        {
            var result = await _uow.CartProductsRepository.DeleteAsync(id);
            _uow.Commit();
            return result;
        }

        public async Task<int> DeleteByCartAsync(long cartId)
        {
            var result = await _uow.CartProductsRepository.DeleteByCartAsync(cartId);
            _uow.Commit();
            return result;
        }
    }
}
