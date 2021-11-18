using System;
using System.Linq;
using System.Threading.Tasks;
using BookShop.MessageBrokerClients;
using Core.StorageManagers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace BookShop.Controllers
{
    /// <summary>
    /// Управление заказами
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("orders")]
    public class OrdersController : Controller
    {
        private readonly OrdersRpcClient _ordersRpcClient;
        private readonly MessagesRpcClient _messagesRpcClient;
        private readonly ICartProductsStorageManager _cartProductSm;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(OrdersRpcClient ordersRpcClient, MessagesRpcClient messagesRpcClient,
            ICartProductsStorageManager cartProductSm,ILogger<OrdersController> logger)
        {
            _ordersRpcClient = ordersRpcClient;
            _messagesRpcClient = messagesRpcClient;
            _cartProductSm = cartProductSm;
            _logger = logger;
        }

        /// <summary>
        /// Получить все заказы пользователя по его Id
        /// </summary>
        /// <returns>
        /// Заказ (Order)
        /// </returns>
        [HttpGet("get-by-user-id")]
        public async Task<IActionResult> GetByUserId([FromQuery] long userId)
        {
            return Ok(await _ordersRpcClient.GetByUserId(userId));
        }

        /// <summary>
        /// Получить заказ по идентификатору
        /// </summary>
        /// <returns>
        /// Заказ (Order)
        /// </returns>
        [HttpGet("get-by-id")]
        public async Task<IActionResult> Get([FromQuery] long id)
        {
            return Ok(await _ordersRpcClient.GetById(id));
        }

        /// <summary>
        /// Создает заказ на основе продуктов в корзине, Id корзины и Id адреса.
        /// В случае успеха удаляет все товара из корзины.
        /// </summary>
        /// <returns>
        /// Количество изменений в таблице заказов
        /// </returns>
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] NewOrderInfo newOrderInfo)
        {
            if (newOrderInfo.CartProducts.Count == 0)
            {
                return BadRequest("There are no cart products.");
            }
            
            var result = await _ordersRpcClient.Create(newOrderInfo);
            await _cartProductSm.DeleteByCartAsync(newOrderInfo.CartId);
            await _messagesRpcClient.CreateAsync(new Message(newOrderInfo.UserId, DateTime.Now, MessageType.Info,
                "New order has been created."));
            _logger.LogInformation($"New order created. {newOrderInfo}");
            return Ok(result);
        }

        /// <summary>
        /// Обновить информацию о заказе
        /// </summary>
        /// <returns>
        /// Количество изменений
        /// </returns>
        //[HttpPost("update")]
        //public async Task<IActionResult> Update([FromBody] Order order)
        //{
        //    _logger.LogInformation($"Order updated. {order}");
        //    return Ok(await _ordersRpcClient.Update(order));
        //}

        /// <summary>
        /// Удалить заказ по идентификатору
        /// </summary>
        /// <returns>
        /// Количество изменений
        /// </returns>
        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromQuery] long id)
        {
            var result = await _ordersRpcClient.Delete(id);
            _logger.LogInformation($"Order with id = {id} deleted.");
            return Ok(result);
        }
    }
}
