using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookShop.Notifications.Manager.Models;

namespace BookShop.Notifications.Manager.Managers.Interfaces
{
    public interface IMessagesStorageManager : IDisposable
    {
        public Task<IReadOnlyList<Message>> GetAlldAsync();
        public Task<IReadOnlyList<Message>> GetByIdsAsync(IEnumerable<long> ids);
        public Task<Message> GetByIdAsync(long id);
        public Task<IReadOnlyList<Message>> GetByUserIdAsync(long userId);
        public Task<IReadOnlyList<Message>> GetByUserAndSettingsAsync(long userId);
        public Task<int> CreateAsync(Message order);
        public Task<int> UpdateAsync(Message order);
        public Task<int> DeleteAsync(long id);
    }
}