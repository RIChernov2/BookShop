using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Core.StorageManagers.Interfaces;
using Data.Entities;
using BookShop.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace BookShop.Controllers
{
    /// <summary>
    /// Управление товарами корзины
    /// </summary>
    [Authorize]
    [Route("cart-products")]
    [ApiController]
    public class CartProductsController : Controller
    {
        private readonly ICartProductsStorageManager _cartProducts;

        public CartProductsController(ICartProductsStorageManager cartProducts)
        {
            _cartProducts = cartProducts;
        }

        /// <summary>
        /// Получить товар корзины по его ИД
        /// </summary>
        /// <returns>
        /// Товар корзины(CartProduct)
        /// </returns>
        [HttpGet("get-by-id")]
        public async Task<IActionResult> Get([FromQuery] long id)
        {
            return Ok(await _cartProducts.GetAsync(id));
        }

        /// <summary>
        /// Получить список всех товаров корзины по ее ИД
        /// </summary>
        /// <returns>
        /// Коллекция товаров корзины (IEnumerable{CartProduct})
        /// </returns>
        [HttpGet("get-by-cart-id")]
        public async Task<IActionResult> GetByCartIdAsync([FromQuery] long cartId)
        {
            return Ok(await _cartProducts.GetByCartIdAsync(cartId));
        }

        /// <summary>
        /// Получить список товаров корзины по их ИД
        /// </summary>
        /// <returns>
        /// Коллекция товаров корзины (IEnumerable{CartProduct})
        /// </returns>
        [HttpGet("get-by-ids")]
        public async Task<IActionResult> Get([FromQuery] string ids)
        {
            return Ok(await _cartProducts.GetAsync(QueryParser.ParseIds(ids)));
        }

        /// <summary>
        /// Получить список всех товаров всех корзин
        /// </summary>
        /// <returns>
        /// Коллекция товаров всех корзин (IEnumerable{CartProduct})
        /// </returns>
        [HttpGet("get-all")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _cartProducts.GetAsync());
        }

        /// <summary>
        /// Создать товары корзины
        /// </summary>
        /// <returns>
        /// Количество изменений
        /// </returns>
        [HttpPost("create-range")]
        public async Task<IActionResult> CreateRange([FromBody] List<CartProduct> cartProducts)
        {
            return Ok(await _cartProducts.CreateRangeAsync(cartProducts));
        }

        /// <summary>
        /// Обновить товары для конкретной корзины. Сначала удаляем все товары из корзины,
        /// затем добавляем новые.
        /// </summary>
        /// <returns>
        /// Количество изменений
        /// </returns>
        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] List<CartProduct> cartProducts, long cartId)
        {
            await _cartProducts.DeleteByCartAsync(cartId);
            return Ok(await _cartProducts.CreateRangeAsync(cartProducts));
        }

        /// <summary>
        /// Удалить все товары из корзины с заданным Id
        /// </summary>
        /// <returns>
        /// Количество изменений
        /// </returns>
        [HttpPost("delete-by-cart-id")]
        public async Task<IActionResult> Delete([FromQuery] long cartId)
        {
            return Ok(await _cartProducts.DeleteByCartAsync(cartId));
        }
    }
}
