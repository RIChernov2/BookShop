using BookShop.Notifications.Manager.Configurations.Models;
using BookShop.Notifications.Manager.Managers.Interfaces;

namespace BookShop.Notifications.Manager.Managers
{
    public class NotificationSettingFactory : IStorageManagerFactory<INotificationSettingsStorageManager>
    {
        private readonly AppConfiguration _configuration;

        public NotificationSettingFactory(AppConfiguration configuration)
        {
            _configuration = configuration;
        }

        public INotificationSettingsStorageManager Create()
        {
            return new NotificationSettingsStorageManager(new UnitOfWork(_configuration));
        }
    }
}