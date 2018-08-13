using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Common.Log;
using Lykke.Service.OperationsRepository.Contract;
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

        [HttpPost("{clientId}/{orderId}/cancel")]
        [SwaggerOperation("LimitOrders_CancelOrder")]
        [ProducesResponseType(typeof(ILimitOrder), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> CancelOrderByIdAsync(string clientId, string orderId)
        {
            if (!CommonValidator.ValidateLimitOrderId(orderId))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(orderId)));
            }

            var order = await _limitOrdersRepository.GetOrderAsync(orderId, clientId);
            if (order == null)
            {
                return NotFound(ErrorResponse.InvalidParameter(nameof(orderId)));
            }

            if ((OrderStatus)Enum.Parse(typeof(OrderStatus), order.Status) == OrderStatus.Cancelled)
                return BadRequest(ErrorResponse.InvalidParameter(nameof(orderId)));

            await _limitOrdersRepository.CancelAsync(order);

            order = await _limitOrdersRepository.GetOrderAsync(orderId, clientId);

            return Ok(order);
        }

        [HttpPost("{clientId}/cancelMultiple")]
        [SwaggerOperation("LimitOrders_CancelMultipleOrders")]
        [ProducesResponseType(typeof(void), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CancelMultipleOrdersByClientIdAsync(string clientId, [FromBody] LimitOrderCancelMultipleRequest model)
        {
            if (!CommonValidator.ValidateClientId(clientId))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(clientId)));
            }
            if (!CommonValidator.ValidateAssetPairId(model.AssetPairId))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(model.AssetPairId)));
            }

            var activeOrders = await _limitOrdersRepository.GetActiveOrdersAsync(clientId);

            var relevantOrders = activeOrders.Where(x => model.AssetPairId == null || x.AssetPairId == model.AssetPairId);
            
            if(relevantOrders.Any())
                await _limitOrdersRepository.CancelMultipleAsync(relevantOrders);

            return Ok();
        }

        [HttpPost("{clientId}/{orderId}/finalize")]
        [SwaggerOperation("LimitOrders_FinalizeOrder")]
        [ProducesResponseType(typeof(ILimitOrder), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> FinalizeOrderAsync(string clientId, string orderId, [FromBody] LimitOrderFinalizeRequest model)
        {
            if (!CommonValidator.ValidateLimitOrderId(orderId))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(orderId)));
            }

            var order = await _limitOrdersRepository.GetOrderAsync(orderId, clientId);
            if (order == null)
            {
                return NotFound(ErrorResponse.InvalidParameter(nameof(orderId)));
            }

            if (model.OrderStatus == OrderStatus.InOrderBook)
                return BadRequest(ErrorResponse.InvalidParameter(nameof(model.OrderStatus)));

            if ((OrderStatus)Enum.Parse(typeof(OrderStatus), order.Status) != OrderStatus.InOrderBook)
                return Ok(order);

            await _limitOrdersRepository.FinalizeAsync(order, model.OrderStatus);

            order = await _limitOrdersRepository.GetOrderAsync(orderId, clientId);

            return Ok(order);
        }

        [HttpPost("{clientId}/{orderId}/remove")]
        [SwaggerOperation("LimitOrders_RemoveOrder")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> RemoveOrderByIdAsync(string clientId, string orderId)
        {
            if (!CommonValidator.ValidateLimitOrderId(orderId))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(orderId)));
            }

            if (!CommonValidator.ValidateClientId(clientId))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(clientId)));
            }

            var order = await _limitOrdersRepository.GetOrderAsync(orderId, clientId);
            if (order == null)
            {
                return NotFound(ErrorResponse.InvalidParameter(nameof(orderId)));
            }

            await _limitOrdersRepository.RemoveAsync(orderId, clientId);

            return Ok();
        }

        [HttpGet("{clientId}/{orderId}")]
        [SwaggerOperation("LimitOrders_GetOrderById")]
        [ProducesResponseType(typeof(ILimitOrder), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetOrderByIdAsync(string clientId, string orderId)
        {
            if (!CommonValidator.ValidateLimitOrderId(orderId))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(orderId)));
            }

            var order = await _limitOrdersRepository.GetOrderAsync(orderId, clientId);
            if (order == null)
            {
                return NotFound(ErrorResponse.InvalidParameter(nameof(orderId)));
            }

            return Ok(order);
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