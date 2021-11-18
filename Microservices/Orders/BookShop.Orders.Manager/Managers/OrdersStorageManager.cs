using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookShop.Orders.Manager.Managers.Interfaces;
using BookShop.Orders.Manager.Models;

namespace BookShop.Orders.Manager.Managers
{
    public class OrdersStorageManager : IOrdersStorageManager
    {
        private readonly IUnitOfWork _uow;

        public OrdersStorageManager(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IReadOnlyList<Order>> GetByUserIdAsync(long userId)
            => await _uow.OrdersRepository.GetByUserIdAsync(userId);

        public async Task<IReadOnlyList<Order>> GetAsync()
            => await _uow.OrdersRepository.GetAsync();

        public async Task<IReadOnlyList<Order>> GetAsync(IEnumerable<long> ids)
            => await _uow.OrdersRepository.GetAsync(ids);

        public async Task<Order> GetAsync(long id)
            => await _uow.OrdersRepository.GetAsync(id);

        public async Task<int[]> CreateAsync(Order order)
        {
            var createdOrderId = await _uow.OrdersRepository.CreateAsync(order);
            order.OrderedBooks = order.OrderedBooks.Select(ob =>
            {
                ob.OrderId = createdOrderId;
                return ob;
            }).ToList();
            var orderedBooksCreatedCount = await _uow.OrderedBooksRepository.CreateRangeAsync(order.OrderedBooks);
            _uow.Commit();
            return new []{ createdOrderId, orderedBooksCreatedCount };
        }

        public async Task<int[]> UpdateAsync(Order order)
        {
            var result1 = await _uow.OrdersRepository.UpdateAsync(order);
            await _uow.OrderedBooksRepository.DeleteByOrderIdAsync(order.OrderId);
            var result2 = await _uow.OrderedBooksRepository.CreateRangeAsync(order.OrderedBooks);
            _uow.Commit();
            return new[] { result1, result2 };
        }

        public async Task<int> DeleteAsync(long id)
        {
            var result = await _uow.OrdersRepository.DeleteAsync(id);
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

        ~OrdersStorageManager()
        {
            Dispose(false);
        }
    }
}
