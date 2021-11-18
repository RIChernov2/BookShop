using System.Threading.Tasks;
using BookShop.Helpers;
using BookShop.MessageBrokerClients;
using Microsoft.AspNetCore.Mvc;
using Data.Entities;
using BookShop.MessageBrokerClients;
using Microsoft.AspNetCore.Authorization;

namespace BookShop.Controllers
{
    /// <summary>
    /// Управление сообщениями
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("notification-settings")]
    public class NotificationSettingsController : Controller
    {
        private readonly NotificationSettingsRpcClient _rpcClient;

        public NotificationSettingsController(NotificationSettingsRpcClient rpcClient)
        {
            _rpcClient = rpcClient;
        }

        /// <summary>
        /// Получить настройку нотификации по Id пользователя.
        /// Создать базовую настройку при первом обращении
        /// </summary>
        /// <returns>
        /// NotificationSetting
        /// </returns>
        [HttpGet("get-by-user-id")]
        public async Task<IActionResult> GetByUserId([FromQuery] long userId)
            => Ok(await _rpcClient.GetByUserIdAsync(userId));

        /// <summary>
        /// Получить настройку нотификации по Id
        /// </summary>
        /// <returns>
        /// NotificationSetting
        /// </returns>
        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetById([FromQuery] long id)
            => Ok(await _rpcClient.GetByIdAsync(id));

        /// <summary>
        /// Получить настройки нотификации по Ids
        /// </summary>
        /// <returns>
        /// List{NotificationSetting}
        /// </returns>
        [HttpGet("get-by-ids")]
        public async Task<IActionResult> GetByIds([FromQuery] string ids)
            => Ok(await _rpcClient.GetByIdsAsync(QueryParser.ParseIds(ids)));

        /// <summary>
        /// Получить все настройки нотификации
        /// </summary>
        /// <returns>
        /// List{NotificationSetting}
        /// </returns>
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
            => Ok(await _rpcClient.GetAllAsync());

        /// <summary>
        /// Создать настройку нотификации
        /// </summary>
        /// <returns>
        /// Количество изменений
        /// </returns>
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] NotificationSetting item)
            => Ok(await _rpcClient.CreateAsync(item));

        /// <summary>
        /// Обновить данные у настройки нотификации.
        /// Поиск идет по айди пользователя, NotigicationSettingId игнорируется. 
        /// </summary>
        /// <returns>
        /// Количество изменений
        /// </returns>
        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] NotificationSetting item)
            => Ok(await _rpcClient.UpdateAsync(item));

        /// <summary>
        /// Удалить настройку нотификации по Id
        /// </summary>
        /// <returns>
        /// Количество изменений
        /// </returns>
        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromQuery] long id)
            => Ok(await _rpcClient.Delete(id));

        /// <summary>
        /// Удалить настройку нотификации по Id
        /// </summary>
        /// <returns>
        /// Количество изменений
        /// </returns>
        [HttpPost("delete-by-user-id")]
        public async Task<IActionResult> DeleteByUserId([FromQuery] long id)
            => Ok(await _rpcClient.DeleteByUserIdAsync(id));
    }
}
