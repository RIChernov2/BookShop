using System;
using BookShop.Notifications.Manager.Handlers.Interfaces;
using BookShop.Notifications.Manager.Repositories.Interfaces;

namespace BookShop.Notifications.Manager.Managers.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IMessagesRepository MessagesRepository { get; }
        INotificationSettingsRepository NotificationSettingsRepository { get; }
        //INotificationsRouter Router { get; }
        void Commit();
    }
}