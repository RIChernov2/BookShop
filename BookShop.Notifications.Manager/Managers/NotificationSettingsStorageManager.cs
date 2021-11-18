using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookShop.Notifications.Manager.Managers.Interfaces;
using BookShop.Notifications.Manager.Models;

namespace BookShop.Notifications.Manager.Managers
{
    public class NotificationSettingsStorageManager : INotificationSettingsStorageManager
    {
        private readonly IUnitOfWork _uow;

        public NotificationSettingsStorageManager(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<NotificationSetting> GetByUserIdAsync(long id)
        {
            var result = await _uow.NotificationSettingsRepository.GetByUserIdAsync(id);
            // не бывает пользователя без настроек, так что добавляем настройки "по умолчанию"
            if ( result == null )
            {
                var item = new NotificationSetting() { UserId = id };
                await CreateAsync(item);
                return await GetByUserIdAsync(id);
            }
            return result;
        }


        public async Task<IReadOnlyList<NotificationSetting>> GetAllAsync()
            => await _uow.NotificationSettingsRepository.GetAllAsync();

        public async Task<IReadOnlyList<NotificationSetting>> GetByIdsAsync(IEnumerable<long> ids)
            => await _uow.NotificationSettingsRepository.GetByIdsAsync(ids);

        public async Task<NotificationSetting> GetByIdAsync(long id)
            => await _uow.NotificationSettingsRepository.GetByIdAsync(id);

        public async Task<int> CreateAsync(NotificationSetting item)
        {
            var result = await _uow.NotificationSettingsRepository.CreateAsync(item);
            _uow.Commit();
            return result;
        }

        public async Task<int> UpdateByUserAsync(NotificationSetting item)
        {
            var result = await _uow.NotificationSettingsRepository.UpdateByUserAsync(item);
            _uow.Commit();
            return result;
        }

        public async Task<int> DeleteAsync(long id)
        {
            var result = await _uow.NotificationSettingsRepository.DeleteAsync(id);
            _uow.Commit();
            return result;
        }

        public async Task<int> DeleteByUserIdAsync(long id)
        {
            var result = await _uow.NotificationSettingsRepository.DeleteByUserIdAsync(id);
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
            if (disposing)
            {
                ReleaseUnmanagedResources();
            }
        }

        private void ReleaseUnmanagedResources()
        {
            _uow.Dispose();
        }

        ~NotificationSettingsStorageManager()
        {
            Dispose(false);
        }
    }
}
