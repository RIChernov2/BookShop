using BookShop.Notifications.Manager.Handlers.Interfaces;
using BookShop.Notifications.Manager.Models;
using BookShop.Notifications.Manager.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace BookShop.Notifications.Manager.Handlers
{
    public class NotificationsRouter: INotificationsRouter
    {
        private IEmailSender _emailSender;
        private IPushSender _pushSender;

        public NotificationsRouter(IEmailSender emailSender, IPushSender pushSender)
        {
            _emailSender = emailSender;
            _pushSender = pushSender;
        }

        public void AgregateMessage(Message message,NotificationSetting setting)
        {
            string subject = TypeToSubject(message.Type);
            string text = message.Text;
            if (setting.EmailSettings.Get(message.Type))
            {
                try
                {
                    _emailSender.Send(setting.Email, subject, text);
                }
                catch ( System.Exception e )
                {

                    System.Console.WriteLine(" ***** Email sending failure => " + e.Message);
                }
            }
            if ( setting.PushSettings.Get(message.Type) )
            {
                try
                {
                    _pushSender.Send(message);
                }
                catch ( System.Exception e )
                {

                    System.Console.WriteLine(" ***** Push sending failure => " + e.Message);
                }
               
            }
        }

        private string TypeToSubject(MessageType type) => type switch
        {
            MessageType.Info => "Info",
            MessageType.Warning => "Warning",
            MessageType.Ads => "Ads",
            _ => throw new NotImplementedException(),
        };
    }
}
