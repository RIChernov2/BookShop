using BookShop.Notifications.Manager.Configurations.Models;
using BookShop.Notifications.Manager.Handlers;
using BookShop.Notifications.Manager.Managers.Interfaces;

namespace BookShop.Notifications.Manager.Managers
{
    public class MessagesFactory : IStorageManagerFactory<IMessagesStorageManager>
    {
        private readonly AppConfiguration _configuration;

        public MessagesFactory(AppConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IMessagesStorageManager Create()
        {
            return new MessagesStorageManager(new UnitOfWork(_configuration), _configuration);
        }
    }
}