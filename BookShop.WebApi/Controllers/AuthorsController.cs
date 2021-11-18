using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BookShop.Helpers;
using BookShop.MessageBrokerClients;
using Microsoft.AspNetCore.Authorization;

namespace BookShop.Controllers
{
    /// <summary>
    /// Authors controller
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class AuthorsController : Controller
    {
        private readonly AuthorsRpcClient _rpcClient;

        public AuthorsController(AuthorsRpcClient rpcClient)
        {
            _rpcClient = rpcClient;
        }

        /// <summary>
        /// Получить автора по Id
        /// </summary>
        /// <returns>
        /// Author
        /// </returns>
        [HttpGet("get-by-id")]
        public async Task<IActionResult> Get([FromQuery] long id)
            => Ok(await _rpcClient.GetById(id));

        /// <summary>
        /// Получить авторов по Ids
        /// </summary>
        /// <returns>
        /// List{Author}
        /// </returns>
        [HttpGet("get-by-ids")]
        public async Task<IActionResult> Get([FromQuery] string ids)
            => Ok(await _rpcClient.GetByIds(QueryParser.ParseIds(ids)));

        /// <summary>
        /// Получить всех авторов
        /// </summary>
        /// <returns>
        /// List{Author}
        /// </returns>
        [HttpGet("get-all")]
        public async Task<IActionResult> Get()
            => Ok(await _rpcClient.GetAll());

        /// <summary>
        /// Создать автора
        /// </summary>
        /// <returns>
        /// Количество изменений
        /// </returns>
        [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] Author author)
            => Ok(await _rpcClient.Create(author));


        /// <summary>
        /// Обновить данные автора
        /// </summary>
        /// <returns>
        /// Количество изменений
        /// </returns>
        [Authorize]
        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] Author author)
            => Ok(await _rpcClient.Update(author));

        /// <summary>
        /// Удалить автора по Id
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
