using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookShop.Orders.Manager.Models;

namespace BookShop.Orders.Manager.Managers.Interfaces
{
    public interface IOrdersStorageManager : IDisposable
    {
        public Task<IReadOnlyList<Order>> GetByUserIdAsync(long id);
        public Task<IReadOnlyList<Order>> GetAsync();
        public Task<IReadOnlyList<Order>> GetAsync(IEnumerable<long> ids);
        public Task<Order> GetAsync(long id);
        public Task<int[]> CreateAsync(Order order);
        public Task<int[]> UpdateAsync(Order order);
        public Task<int> DeleteAsync(long id);
    }
}