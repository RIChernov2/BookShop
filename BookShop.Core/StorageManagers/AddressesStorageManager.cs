using Core.StorageManagers.Interfaces;
using Data.Entities;
using Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.StorageManagers
{
    public class AddressesStorageManager : IAddressesStorageManager
    {
        private readonly IUnitOfWork _uow;

        public AddressesStorageManager(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IReadOnlyList<Address>> GetAsync()
            => await _uow.AdressesRepository.GetAsync();

        public async Task<IReadOnlyList<Address>> GetAsync(IEnumerable<long> ids)
            => await _uow.AdressesRepository.GetAsync(ids);

        public async Task<Address> GetAsync(long id)
            => await _uow.AdressesRepository.GetAsync(id);

        public async Task<int> CreateAsync(Address adress)
        {
            var result = await _uow.AdressesRepository.CreateAsync(adress);
            _uow.Commit();
            return result;
        }

        public async Task<int> CreateRangeAsync(List<Address> entities)
        {
            var result = await _uow.AdressesRepository.CreateRangeAsync(entities);
            _uow.Commit();
            return result;
        }

        public async Task<int> UpdateAsync(Address adress)
        {
            var result = await _uow.AdressesRepository.UpdateAsync(adress);
            _uow.Commit();
            return result;
        }

        public async Task<int> DeleteAsync(long id)
        {
            var result = await _uow.AdressesRepository.DeleteAsync(id);
            _uow.Commit();
            return result;
        }

        public async Task<int> DeleteByUserIdAsync(long userId)
        {
            var result = await _uow.AdressesRepository.DeleteByUserIdAsync(userId);
            _uow.Commit();
            return result;
        }

        public async Task<IReadOnlyList<Address>> GetByUserIdAsync(long id)
            => await _uow.AdressesRepository.GetByUserIdAsync(id);
    }
}
