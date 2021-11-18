using System.Threading.Tasks;
using BookShop.Helpers;
using BookShop.MessageBrokerClients;
using Microsoft.AspNetCore.Mvc;
using Data.Entities;
using Microsoft.AspNetCore.Authorization;

namespace BookShop.Controllers
{
    /// <summary>
    /// Управление сообщениямия
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("messages")]
    public class MessagesController : Controller
    {
        private readonly MessagesRpcClient _rpcClient;

        public MessagesController(MessagesRpcClient rpcClient)
        {
            _rpcClient = rpcClient;
        }

        /// <summary>
        /// Получить сообщение по Id
        /// </summary>
        /// <returns>
        /// Message
        /// </returns>
        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetById([FromQuery] long id)
            => Ok(await _rpcClient.GetByIdAsync(id));

        /// <summary>
        /// Получить сообщения по Id пользователя
        /// </summary>
        /// <returns>
        /// List{Message}
        /// </returns>
        [HttpGet("get-by-user-id")]
        public async Task<IActionResult> GetByUserId([FromQuery] long userId)
            => Ok(await _rpcClient.GetByUserIdAsync(userId));

        /// <summary>
        /// Получить сообщения по Id пользователя согласно его настроек уведомления
        /// </summary>
        /// <returns>
        /// List{Message}
        /// </returns>
        [HttpGet("get-by-user-and-settings")]
        public async Task<IActionResult> GetByUserAndSettings([FromQuery] long userId)
            => Ok(await _rpcClient.GetByUserAndSettingsAsync(userId));

        /// <summary>
        /// Получить сообщения по Ids
        /// </summary>
        /// <returns>
        /// List{Message}
        /// </returns>
        [HttpGet("get-by-ids")]
        public async Task<IActionResult> GetByIds([FromQuery] string ids)
            => Ok(await _rpcClient.GetByIdsAsync(QueryParser.ParseIds(ids)));

        /// <summary>
        /// Получить все сообщения
        /// </summary>
        /// <returns>
        /// List{Message}
        /// </returns>
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
            => Ok(await _rpcClient.GetAllAsync());

        /// <summary>
        /// Создать сообщение
        /// </summary>
        /// <returns>
        /// Количество изменений
        /// </returns>
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] Message item)
            => Ok(await _rpcClient.CreateAsync(item));

        /// <summary>
        /// Обновить данные у сообщения
        /// </summary>
        /// <returns>
        /// Количество изменений
        /// </returns>
        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] Message item)
            => Ok(await _rpcClient.UpdateAsync(item));

        /// <summary>
        /// Удалить сообщение по Id
        /// </summary>
        /// <returns>
        /// Количество изменений
        /// </returns>
        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromQuery] long id)
            => Ok(await _rpcClient.DeleteAsync(id));
    }
}
