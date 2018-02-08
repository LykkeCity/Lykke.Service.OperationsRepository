using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Common.Log;
using Lykke.Service.OperationsRepository.Core.CashOperations;
using Lykke.Service.OperationsRepository.Models;
using Lykke.Service.OperationsRepository.Models.LimitOrder;
using Lykke.Service.OperationsRepository.Validation;
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
            if (!ModelState.IsValid)
            {
                var errorList = ModelState.Values.SelectMany(m => m.Errors)
                    .Select(e => string.IsNullOrWhiteSpace(e.ErrorMessage) ? e.Exception?.Message : e.ErrorMessage)
                    .ToList();

                return BadRequest(errorList);
            }
            
            await _limitOrdersRepository.InOrderBookAsync(order);
            
            return Ok(order);
        }

        [HttpPost("{orderId}/cancel")]
        [SwaggerOperation("LimitOrders_CancelOrder")]
        [ProducesResponseType(typeof(ILimitOrder), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CancelOrderByIdAsync(string orderId)
        {
            if (!CommonValidator.ValidateLimitOrderId(orderId))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(orderId)));
            }
            
            var order = await _limitOrdersRepository.GetOrderAsync(orderId);

            if (order == null)
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(orderId)));
            }
            
            await _limitOrdersRepository.CancelAsync(order);
            
            order = await _limitOrdersRepository.GetOrderAsync(orderId);
            
            return Ok(order);
        }
        
        [HttpGet("{orderId}")]
        [SwaggerOperation("LimitOrders_GetOrderById")]
        [ProducesResponseType(typeof(ILimitOrder), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetOrderByIdAsync(string orderId)
        {
            if (!CommonValidator.ValidateLimitOrderId(orderId))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(orderId)));
            }

            var result = await _limitOrdersRepository.GetOrderAsync(orderId);
            
            return Ok(result);
        }

        [HttpGet("client/{clientId}")]
        [SwaggerOperation("LimitOrders_GetOrdersByClientId")]
        [ProducesResponseType(typeof(IEnumerable<ILimitOrder>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetOrdersByClientIdAsync(string clientId)
        {
            if (!CommonValidator.ValidateClientId(clientId))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(clientId)));
            }

            var result = await _limitOrdersRepository.GetOrdersAsync(clientId);
            
            return Ok(result);
        }
        
        [HttpGet("client/active/{clientId}")]
        [SwaggerOperation("LimitOrders_GetActiveOrdersByClientId")]
        [ProducesResponseType(typeof(IEnumerable<ILimitOrder>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetActiveOrdersByClientIdAsync(string clientId)
        {
            if (!CommonValidator.ValidateClientId(clientId))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(clientId)));
            }

            var result = await _limitOrdersRepository.GetActiveOrdersAsync(clientId);
            
            return Ok(result);
        }
    }
}