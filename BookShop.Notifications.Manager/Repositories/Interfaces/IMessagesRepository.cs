using System.Collections.Generic;
using System.Threading.Tasks;
using BookShop.Notifications.Manager.Models;

namespace BookShop.Notifications.Manager.Repositories.Interfaces
{
    public interface IMessagesRepository : IGenericRepository<Message, long>
    {
        public Task<IReadOnlyList<Message>> GetByUserIdAsync(long userId);
    }
}