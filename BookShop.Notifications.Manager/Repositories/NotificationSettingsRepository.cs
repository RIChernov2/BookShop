using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using BookShop.Notifications.Manager.Configurations.Models;
using BookShop.Notifications.Manager.Models;
using BookShop.Notifications.Manager.Repositories.Interfaces;
using Dapper;

namespace BookShop.Notifications.Manager.Repositories
{
    public class NotificationSettingsRepository : BaseDatabaseRepository, INotificationSettingsRepository
    {
        public NotificationSettingsRepository(IDbTransaction transaction, AppConfiguration сonfiguration)
            : base(transaction, сonfiguration)
        {
        }

  
        // доп метод, который создает базовые настройки, при первом обращении по айди пользователя.
        public async Task<NotificationSetting> GetByUserIdAsync(long id)
        {
            var sql = $@"
                SELECT * FROM {SchemaName}.notification_settings
                WHERE notification_settings.user_id = @id
            ";
            var result = await Connection.QueryAsync<NotificationSetting>(sql, new { id });
                
            return result.SingleOrDefault();
        }

        public async Task<NotificationSetting> GetByIdAsync(long id)
        {
            var sql = $@"
                SELECT * FROM {SchemaName}.notification_settings
                WHERE notification_settings.notification_setting_id = @id
            ";
            var result = await Connection.QueryAsync<NotificationSetting>(sql, new { id });

            return result.SingleOrDefault();
        }

        public async Task<IReadOnlyList<NotificationSetting>> GetByIdsAsync(IEnumerable<long> ids)
        {
            var sql = $@"
                SELECT * FROM {SchemaName}.notification_settings
                WHERE notification_settings.notification_setting_id = any (@ids)
            ";
            var result = await Connection.QueryAsync<NotificationSetting>(sql, new { ids });
            return result.ToImmutableList();
        }

        public async Task<IReadOnlyList<NotificationSetting>> GetAllAsync()
        {
            var sql = $@"
                SELECT * FROM {SchemaName}.notification_settings
            ";
            var result = await Connection.QueryAsync<NotificationSetting>(sql);
            return result.ToImmutableList();
        }
        
        public async Task<int> CreateAsync(NotificationSetting entity)
        {
            if ( entity == null ) return 0;

            var sql = $@"
                INSERT INTO {SchemaName}.notification_settings (user_id, email_settings, push_settings, email)
                VALUES (@UserId, @EmailSettings, @PushSettings, @Email)
            ";
            return await Connection.ExecuteAsync(sql, entity, Transaction);
        }

        public async Task<int> UpdateByUserAsync(NotificationSetting entity)
        {
            if ( entity == null ) return 0;

            var sql = $@"
                UPDATE {SchemaName}.notification_settings
                SET email_settings = @EmailSettings,
                    push_settings = @PushSettings,
                    email = @Email
                WHERE user_id = @UserId
            ";

            return await Connection.ExecuteAsync(sql, entity, Transaction);
        }

        public async Task<int> DeleteAsync(long id)
        {
            var sql = $@"
                DELETE FROM {SchemaName}.notification_settings
                WHERE notification_setting_id = @id
            ";
            return await Connection.ExecuteAsync(sql, new { id }, Transaction);
        }

        public async Task<int> DeleteByUserIdAsync(long id)
        {
            var sql = $@"
                DELETE FROM {SchemaName}.notification_settings
                WHERE user_id = @id
            ";
            return await Connection.ExecuteAsync(sql, new { id }, Transaction);
        }
    }
}