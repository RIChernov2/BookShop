using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using BookShop.Notifications.Manager.Configurations.Models;
using BookShop.Notifications.Manager.Handlers;
using BookShop.Notifications.Manager.Handlers.Interfaces;
using BookShop.Notifications.Manager.Managers.Interfaces;
using BookShop.Notifications.Manager.Models;

namespace BookShop.Notifications.Manager.Managers
{
    public class MessagesStorageManager : IMessagesStorageManager
    {
        private readonly IUnitOfWork _uow;
        AppConfiguration _configuration;
        private INotificationsRouter _router;
        public MessagesStorageManager(IUnitOfWork uow, AppConfiguration configuration)
        {
            _uow = uow;
            _configuration = configuration;
            _router = new NotificationsRouter(new EmailSender(_configuration), new PushSender(_configuration));
        }

        public async Task<IReadOnlyList<Message>> GetAlldAsync()
            => await _uow.MessagesRepository.GetAllAsync();

        public async Task<IReadOnlyList<Message>> GetByIdsAsync(IEnumerable<long> ids)
            => await _uow.MessagesRepository.GetByIdsAsync(ids);

        public async Task<Message> GetByIdAsync(long id)
            => await _uow.MessagesRepository.GetByIdAsync(id);

        public async Task<IReadOnlyList<Message>> GetByUserIdAsync(long userId)
            => await _uow.MessagesRepository.GetByUserIdAsync(userId);

        public async Task<IReadOnlyList<Message>> GetByUserAndSettingsAsync(long userId)
        {
            var settings = GetSettings(userId).PushSettings;
            var result = await _uow.MessagesRepository.GetByUserIdAsync(userId);
            return result.Where(x => settings.Get(x.Type)).ToImmutableList();
        }

        private NotificationSetting GetSettings(long userId)
        {
            // запрашиваем из БД настройки оповещения
            using var notificationSettingsStorageManager
                = new NotificationSettingFactory(_configuration).Create();
            return notificationSettingsStorageManager.GetByUserIdAsync(userId).Result;
        }

        public async Task<int> CreateAsync(Message item)
        {
            var result = await _uow.MessagesRepository.CreateAsync(item);
            _uow.Commit();

            // отправка сообщения, без await, так как не требуется ждать
            // отправляем задачу в отельный поток из TP
            if ( result == 1 ) Task.Run(()=>AgregateMessageAsync(item));
            return result;
        }



        private void AgregateMessageAsync(Message item)
        {
            try
            {
                var setting = GetSettings(item.UserId);
                _router.AgregateMessage(item, setting);
            }
            catch ( Exception e )
            {
                // дублирую проверку на всякий случай, чтобы не падало создание сообщения
                Console.WriteLine("\n***** Email / push sending failure  failure (ExecuteAsync => AgregateMessageAsync) => " + e.Message + "\n");
            }
        }

        public async Task<int> UpdateAsync(Message item)
        {
            var result = await _uow.MessagesRepository.UpdateByUserAsync(item);
            _uow.Commit();
            return result;
        }

        public async Task<int> DeleteAsync(long id)
        {
            var result = await _uow.MessagesRepository.DeleteAsync(id);
            _uow.Commit();
            return result;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if ( disposing )
            {
                ReleaseUnmanagedResources();
            }
        }

        private void ReleaseUnmanagedResources()
        {
            _uow.Dispose();
        }

        ~MessagesStorageManager()
        {
            Dispose(false);
        }
    }
}
