using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookShop.Orders.Manager.Models;

namespace BookShop.Orders.Manager.Managers.Interfaces
{
    public interface IOrderedBooksStorageManager : IDisposable
    {
        public Task<IReadOnlyList<OrderedBook>> GetAsync();
        public Task<IReadOnlyList<OrderedBook>> GetAsync(IEnumerable<long> ids);
        public Task<OrderedBook> GetAsync(long id);
        public Task<int> CreateAsync(OrderedBook orderedBook);
        public Task<int> UpdateAsync(OrderedBook orderedBook);
        public Task<int> DeleteAsync(long id);
    }
}
