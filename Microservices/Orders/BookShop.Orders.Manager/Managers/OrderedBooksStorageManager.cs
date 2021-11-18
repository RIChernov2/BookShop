using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookShop.Orders.Manager.Managers.Interfaces;
using BookShop.Orders.Manager.Models;

namespace BookShop.Orders.Manager.Managers
{
    public class OrderedBooksStorageManager : IOrderedBooksStorageManager
    {
        private readonly IUnitOfWork _uow;

        public OrderedBooksStorageManager(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IReadOnlyList<OrderedBook>> GetAsync()
            => await _uow.OrderedBooksRepository.GetAsync();

        public async Task<IReadOnlyList<OrderedBook>> GetAsync(IEnumerable<long> ids)
            => await _uow.OrderedBooksRepository.GetAsync(ids);

        public async Task<OrderedBook> GetAsync(long id)
            => await _uow.OrderedBooksRepository.GetAsync(id);

        public async Task<int> CreateAsync(OrderedBook orderedBook)
        {
            var result = await _uow.OrderedBooksRepository.CreateAsync(orderedBook);
            _uow.Commit();
            return result;
        }

        public async Task<int> UpdateAsync(OrderedBook orderedBook)
        {
            var result = await _uow.OrderedBooksRepository.UpdateAsync(orderedBook);
            _uow.Commit();
            return result;
        }

        public async Task<int> DeleteAsync(long id)
        {
            var result = await _uow.OrderedBooksRepository.DeleteAsync(id);
            _uow.Commit();
            return result;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                ReleaseUnmanagedResources();
            }
        }

        private void ReleaseUnmanagedResources()
        {
            _uow.Dispose();
        }

        ~OrderedBooksStorageManager()
        {
            Dispose(false);
        }
    }
}
