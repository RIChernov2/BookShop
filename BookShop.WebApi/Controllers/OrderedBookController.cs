using System.Threading.Tasks;
using BookShop.Helpers;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
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
    public class OrderedBookController : Controller
    {
        private readonly OrderedBooksRpcClient _rpcClient;

        public OrderedBookController(OrderedBooksRpcClient rpcClient)
        {
            _rpcClient = rpcClient;
        }

        /// <summary>
        /// Получить заказанную книгу по идентификатору
        /// </summary>
        /// <returns>
        /// Заказанная книга (OrderedBook)
        /// </returns>
        [HttpGet("get-by-id")]
        public async Task<IActionResult> Get([FromQuery] long id)
        {
            return Ok(await _rpcClient.GetById(id));
        }

        /// <summary>
        /// Получить список заказанных книг по идентификаторам
        /// </summary>
        /// <returns>
        /// Коллекция заказанных книг (IReadOnlyList{OrderedBook})
        /// </returns>
        [HttpGet("get-by-ids")]
        public async Task<IActionResult> Get([FromQuery] string ids)
        {
            return Ok(await _rpcClient.GetByIds(QueryParser.ParseIds(ids)));
        }

        /// <summary>
        /// Получить список всех заказанных книг
        /// </summary>
        /// <returns>
        /// Коллекция заказанных книг (IReadOnlyList{OrderedBook})
        /// </returns>
        [HttpGet("get-all")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _rpcClient.GetAll());
        }

        /// <summary>
        /// Создать заказанную книгу
        /// </summary>
        /// <returns>
        /// Количество изменений
        /// </returns>
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] OrderedBook orderedBook)
        {
            return Ok(await _rpcClient.Create(orderedBook));
        }

        /// <summary>
        /// Обновить информацию о заказазанной книге
        /// </summary>
        /// <returns>
        /// Количество изменений
        /// </returns>
        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] OrderedBook orderedBook)
        {
            return Ok(await _rpcClient.Update(orderedBook));
        }

        /// <summary>
        /// Удалить заказанную книгу по идентификатору
        /// </summary>
        /// <returns>
        /// Количество изменений
        /// </returns>
        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromQuery] long id)
        {
            return Ok(await _rpcClient.Delete(id));
        }
    }
}
