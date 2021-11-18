using BookShop.Notifications.Manager.Models;
using System.Threading.Tasks;

namespace BookShop.Notifications.Manager.Repositories.Interfaces
{
    public interface INotificationSettingsRepository : IGenericRepository<NotificationSetting, long>
    {
        Task<NotificationSetting> GetByUserIdAsync(long id);
        Task<int> DeleteByUserIdAsync(long id);
    }
}