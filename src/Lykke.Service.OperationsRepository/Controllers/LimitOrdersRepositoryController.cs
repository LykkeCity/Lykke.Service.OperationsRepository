using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Common.Log;
using Lykke.Service.OperationsRepository.Core.CashOperations;
using Lykke.Service.OperationsRepository.Models;
using Lykke.Service.OperationsRepository.Models.LimitOrder;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Lykke.Service.OperationsRepository.Controllers
{
    [Route("api/[controller]")]
    public class LimitOrdersRepositoryController : Controller
    {
        private readonly ILimitOrdersRepository _limitOrdersRepository;
        private readonly ILog _log;
        
        public LimitOrdersRepositoryController(ILimitOrdersRepository limitOrdersRepository, ILog log)
        {
            _limitOrdersRepository = limitOrdersRepository;
            _log = log;
        }

        [HttpPost]
        [SwaggerOperation("LimitOrders_AddOrder")]
        [ProducesResponseType(typeof(ILimitOrder), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AddOrderAsync([FromBody] LimitOrderCreateRequest order)
        {
            await _limitOrdersRepository.InOrderBookAsync(order);
            return Ok(order);
        }

        [HttpPost("{orderId}/cancel")]
        [SwaggerOperation("LimitOrders_CancelOrder")]
        [ProducesResponseType(typeof(ILimitOrder), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CancelOrderByIdAsync(string orderId)
        {
            var order = await _limitOrdersRepository.GetOrderAsync(orderId);
            await _limitOrdersRepository.CancelAsync(order);
            order = await _limitOrdersRepository.GetOrderAsync(orderId);
            return Ok(order);
        }
        
        [HttpGet("{orderId}")]
        [SwaggerOperation("LimitOrders_GetOrderById")]
        [ProducesResponseType(typeof(ILimitOrder), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetOrderByIdAsync(string orderId)
        {
            return Ok(await _limitOrdersRepository.GetOrderAsync(orderId));
        }

        [HttpGet("client/{clientId}")]
        [SwaggerOperation("LimitOrders_GetOrdersByClientId")]
        [ProducesResponseType(typeof(IEnumerable<ILimitOrder>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetOrdersByClientIdAsync(string clientId)
        {
            return Ok(await _limitOrdersRepository.GetOrdersAsync(clientId));
        }
        
        [HttpGet("client/active/{clientId}")]
        [SwaggerOperation("LimitOrders_GetActiveOrdersByClientId")]
        [ProducesResponseType(typeof(IEnumerable<ILimitOrder>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetActiveOrdersByClientIdAsync(string clientId)
        {
            return Ok(await _limitOrdersRepository.GetActiveOrdersAsync(clientId));
        }
    }
}