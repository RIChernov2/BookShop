using BookShop.Notifications.Manager.Models;
using System.Threading.Tasks;

namespace BookShop.Notifications.Manager.Handlers.Interfaces
{
    public interface IPushSender
    {
        public void Send(Message message);
    }
}
