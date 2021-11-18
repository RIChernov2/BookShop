using System.Threading.Tasks;
using BookShop.Helpers;
using Core.StorageManagers.Interfaces;
using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Controllers
{
    /// <summary>
    /// Управление корзинами пользователей
    /// </summary>
    [Authorize]
    [Route("carts")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly ICartsStorageManager _carts;

        public CartsController(ICartsStorageManager carts)
        {
            _carts = carts;
        }

        /// <summary>
        /// Получить корзину пользователя по ИД
        /// </summary>
        /// <returns>
        /// Корзина пользователя(Cart)
        /// </returns>
        [HttpGet("get-by-id")]
        public async Task<IActionResult> Get([FromQuery] long id)
        {
            return Ok(await _carts.GetAsync(id));
        }

        /// <summary>
        /// Получить корзину пользователя по его id.
        /// Если для данного пользователя не существует корзины - создает ее.
        /// </summary>
        /// <returns>
        /// Корзина пользователя
        /// </returns>
        [HttpGet("get-by-user-id")]
        public async Task<IActionResult> GetByUserIdAsync([FromQuery] long userId)
        {
            return Ok(await _carts.GetByUserIdAsync(userId));
        }

        /// <summary>
        /// Получить список корзин по их ИД
        /// </summary>
        /// <returns>
        /// Коллекция корзин пользователей (IEnumerable{Cart})
        /// </returns>
        [HttpGet("get-by-ids")]
        public async Task<IActionResult> Get([FromQuery] string ids)
        {
            return Ok(await _carts.GetAsync(QueryParser.ParseIds(ids)));
        }

        /// <summary>
        /// Получить список всех корзин пользователей
        /// </summary>
        /// <returns>
        /// Коллекция корзин пользователей (IEnumerable{Cart})
        /// </returns>
        [HttpGet("get-all")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _carts.GetAsync());
        }

        /// <summary>
        /// Создать корзину пользователя
        /// </summary>
        /// <returns>
        /// Количество изменений
        /// </returns>
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] Cart cart)
        {
            return Ok(await _carts.CreateAsync(cart));
        }
        /// <summary>
        /// Обновить информацию о корзине пользователя
        /// </summary>
        /// <returns>
        /// Количество изменений
        /// </returns>
        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] Cart cart)
        {
            return Ok(await _carts.UpdateAsync(cart));
        }

        /// <summary>
        /// Удалить корзину пользователя по ИД
        /// </summary>
        /// <returns>
        /// Количество изменений
        /// </returns>
        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromQuery] long id)
        {
            return Ok(await _carts.DeleteAsync(id));
        }
     }
}
