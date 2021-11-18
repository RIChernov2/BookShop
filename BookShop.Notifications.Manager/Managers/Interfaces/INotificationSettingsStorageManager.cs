using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookShop.Notifications.Manager.Models;

namespace BookShop.Notifications.Manager.Managers.Interfaces
{
    public interface INotificationSettingsStorageManager : IDisposable
    {
        public Task<NotificationSetting> GetByUserIdAsync(long id);
        public Task<int> DeleteByUserIdAsync(long id);

        public Task<IReadOnlyList<NotificationSetting>> GetAllAsync();
        public Task<IReadOnlyList<NotificationSetting>> GetByIdsAsync(IEnumerable<long> ids);
        public Task<NotificationSetting> GetByIdAsync(long id);
        public Task<int> CreateAsync(NotificationSetting order);
        public Task<int> UpdateByUserAsync(NotificationSetting order);
        public Task<int> DeleteAsync(long id);
    }
}