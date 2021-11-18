using System.Collections.Generic;
using System.Threading.Tasks;
using BookShop.Orders.Manager.Models;

namespace BookShop.Orders.Manager.Repositories.Interfaces
{
    public interface IOrdersRepository : IGenericRepository<Order, long>
    {
        Task<IReadOnlyList<Order>> GetByUserIdAsync(long id);
    }
}