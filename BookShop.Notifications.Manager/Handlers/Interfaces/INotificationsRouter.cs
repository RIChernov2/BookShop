using BookShop.Notifications.Manager.Models;
using System.Threading.Tasks;

namespace BookShop.Notifications.Manager.Handlers.Interfaces
{
    public interface INotificationsRouter
    {
        public void AgregateMessage(Message message, NotificationSetting setting);
    }
}
