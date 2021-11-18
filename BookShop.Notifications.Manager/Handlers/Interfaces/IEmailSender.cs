using System.Threading.Tasks;

namespace BookShop.Notifications.Manager.Handlers.Interfaces
{
    public interface IEmailSender
    {
        public void Send(string targetEmail, string subject, string message);
    }
}
