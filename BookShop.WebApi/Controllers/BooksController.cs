using System.Threading.Tasks;
using BookShop.Helpers;
using BookShop.MessageBrokerClients;
using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Controllers
{
    /// <summary>
    /// Управление книгами
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly BooksRpcClient _rpcClient;

        public BooksController(BooksRpcClient rpcClient)
        {
            _rpcClient = rpcClient;
        }

        /// <summary>
        /// Получить книгу по идентификатору
        /// </summary>
        /// <returns>
        /// Книгa (Book)
        /// </returns>
        [HttpGet("get-by-id")]
        public async Task<IActionResult> Get([FromQuery] long id)
            => Ok(await _rpcClient.GetById(id));
    

        /// <summary>
        /// Получить список книг по идентификаторам
        /// </summary>
        /// <returns>
        /// Коллекция книг (IEnumerable{Book})
        /// </returns>
        [HttpGet("get-by-ids")]
        public async Task<IActionResult> Get([FromQuery] string ids)
            => Ok(await _rpcClient.GetByIds(QueryParser.ParseIds(ids)));

        /// <summary>
        /// Получить список всех книг, и их авторов
        /// </summary>
        /// <returns>
        /// Коллекция книг (IEnumerable{Book})
        /// </returns>
        [HttpGet("get-all")]
        public async Task<IActionResult> Get()
            => Ok(await _rpcClient.GetAll());

        /// <summary>
        /// Создать книгу
        /// </summary>
        /// <returns>
        /// Количество изменений
        /// </returns>
        [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] Book book)
            => Ok(await _rpcClient.Create(book));

        /// <summary>
        /// Обновить информацию по книге
        /// </summary>
        /// <returns>
        /// Количество изменений
        /// </returns>
        [Authorize]
        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] Book book)
            => Ok(await _rpcClient.Update(book));

        /// <summary>
        /// Удалить книгу по идентификатору
        /// </summary>
        /// <returns>
        /// Количество изменений
        /// </returns>
        [Authorize]
        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromQuery] long id)
            => Ok(await _rpcClient.Delete(id));
    }
}