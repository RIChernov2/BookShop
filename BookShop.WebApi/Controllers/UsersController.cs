using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BookShop.Helpers;
using Core.StorageManagers.Interfaces;
using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace BookShop.Controllers
{
    /// <summary>
    /// Управление пользователями
    /// </summary>
    [Authorize]
    [Route("users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersStorageManager _usersSm;
        private readonly IAddressesStorageManager _addressesSm;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUsersStorageManager usersSm, IAddressesStorageManager addressesSm, ILogger<UsersController> logger)
        {
            _usersSm = usersSm;
            _addressesSm = addressesSm;
            _logger = logger;
        }

        /// <summary>
        /// Возвращает список всех пользователей
        /// </summary>
        /// <returns>IEnumerable{User}</returns>
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _usersSm.GetAsync());
        }

        /// <summary>
        /// Возвращает пользователя по его Id
        /// </summary>
        /// <returns>User</returns>
        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetById([FromQuery] long id)
        {
            return Ok(await _usersSm.GetAsync(id));
        }

        /// <summary>
        /// Возвращает пользователей с указанными Ids
        /// </summary>
        /// <returns>User</returns>
        [HttpGet("get-by-ids")]
        public async Task<IActionResult> GetById([FromQuery] string ids)
        {
            return Ok(await _usersSm.GetAsync(QueryParser.ParseIds(ids)));
        }

        /// <summary>
        /// Создает нового пользователя
        /// </summary>
        /// <returns>Количество изменений</returns>
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] User entity)
        {
            var result = await _usersSm.CreateAsync(entity);
            _logger.LogInformation($"New user created. {entity}");
            return Ok(result);
        }

        /// <summary>
        /// Обновляет данные пользователя
        /// </summary>
        /// <returns>Количество изменений</returns>
        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] User entity)
        {
            return Ok(await _usersSm.UpdateAsync(entity));
        }

        /// <summary>
        /// Удаляет пользователя с заданным Id
        /// </summary>
        /// <returns>
        /// Количество изменений
        /// </returns>
        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromQuery] long id)
        {
            var result = await _usersSm.DeleteAsync(id);
            _logger.LogInformation($"User with id = {id} deleted.");
            return Ok(result);
        }
    }
}
