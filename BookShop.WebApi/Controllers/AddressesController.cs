using System;
using BookShop.Helpers;
using Core.StorageManagers.Interfaces;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BookShop.MessageBrokerClients;
using Microsoft.AspNetCore.Authorization;

namespace BookShop.Controllers
{
    /// <summary>
    /// Управление адресами
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AddressesController : Controller
    {
        private readonly IAddressesStorageManager _addresses;
        private readonly MessagesRpcClient _messagesRpcClient;

        public AddressesController(IAddressesStorageManager addresses, MessagesRpcClient messagesRpcClient)
        {
            _addresses = addresses;
            _messagesRpcClient = messagesRpcClient;
        }

        /// <summary>
        /// Получить адрес по идентификатору
        /// </summary>
        /// <returns>
        /// Адрес (Address)
        /// </returns>
        [HttpGet("get-by-id")]
        public async Task<IActionResult> Get([FromQuery] long id)
        {
            return Ok(await _addresses.GetAsync(id));
        }

        /// <summary>
        /// Получить список адресов по идентификаторам
        /// </summary>
        /// <returns>
        /// Коллекция адресов (IReadOnlyList{Address})
        /// </returns>
        [HttpGet("get-by-ids")]
        public async Task<IActionResult> Get([FromQuery] string ids)
        {
            return Ok(await _addresses.GetAsync(QueryParser.ParseIds(ids)));
        }
         /// <summary>
        /// Получить список всех адресов пользователей
        /// </summary>
        /// <returns>
        /// Коллекция адресов (IReadOnlyList{Address})
        /// </returns>
        [HttpGet("get-all")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _addresses.GetAsync());
        }

        /// <summary>
        /// Создать адрес
        /// </summary>
        /// <returns>
        /// Количество изменений
        /// </returns>
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] Address address)
        {
            var result = await _addresses.CreateAsync(address);
            await _messagesRpcClient.CreateAsync(new Message(address.UserId, DateTime.Now, MessageType.Info,
                "New address has been created."));
            return Ok(result);
        }

        /// <summary>
        /// Обновить информацию об адресе
        /// </summary>
        /// <returns>
        /// Количество изменений
        /// </returns>
        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] Address address)
        {
            var result = await _addresses.UpdateAsync(address);
            await _messagesRpcClient.CreateAsync(new Message(address.UserId, DateTime.Now, MessageType.Info,
                $"Address with id = {address.AddressId} has been updated."));
            return Ok(result);
        }

        /// <summary>
        /// Удалить адрес по идентификатору
        /// </summary>
        /// <returns>
        /// Количество изменений
        /// </returns>
        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromQuery] long id)
        {
            return Ok(await _addresses.DeleteAsync(id));
        }

        /// <summary>
        /// Получить список всех адресов по идентификатору пользователя
        /// </summary>
        /// <returns>
        /// Коллекция адресов (IReadOnlyList{Address})
        /// </returns>
        [HttpPost("get-by-user-id")]
        public async Task<IActionResult> GetByUserIdAsync([FromQuery] long id)
        {
            return Ok(await _addresses.GetByUserIdAsync(id));
        }
    }
}
