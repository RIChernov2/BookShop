using System.Collections.Generic;
using System.Threading.Tasks;
using BookShop.Orders.Manager.Models;

namespace BookShop.Orders.Manager.Repositories.Interfaces
{
    public interface IOrderedBooksRepository : IGenericRepository<OrderedBook, long>
    {
        public Task<int> DeleteByOrderIdAsync(long orderId);
        public Task<int> CreateRangeAsync(List<OrderedBook> entities);
    }
}