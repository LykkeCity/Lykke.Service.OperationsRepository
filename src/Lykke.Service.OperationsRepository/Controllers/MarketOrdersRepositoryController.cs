using Common.Log;
using Lykke.Service.OperationsHistory.HistoryWriter.Abstractions;
using Lykke.Service.OperationsRepository.Core.Entities;
using Lykke.Service.OperationsRepository.Core.Exchange;
using Lykke.Service.OperationsRepository.Models;
using Lykke.Service.OperationsRepository.Validation;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.SwaggerGen.Annotations;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Lykke.Service.OperationsRepository.Controllers
{
    [Route("api/[controller]")]
    public class MarketOrdersRepositoryController : Controller
    {
        private readonly IMarketOrdersRepository _marketOrdersRepo;
        private readonly IHistoryWriter _historyWriter;
        private readonly ILog _log;

        public MarketOrdersRepositoryController(IMarketOrdersRepository marketOrdersRepo,
                                                    IHistoryWriter historyWriter, ILog log)
        {
            _marketOrdersRepo = marketOrdersRepo;
            _historyWriter = historyWriter;
            _log = log;
        }

        [HttpGet("GetByOrdersIds")]
        [SwaggerOperation("MarketOrdersOperations_GetByOrdersIds")]
        [ProducesResponseType(typeof(IEnumerable<MarketOrder>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetOrdersAsync([FromQuery] string[] orderIds)
        {
            if (!CommonValidator.ValidateOrderIds(orderIds))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(orderIds)));
            }

            return Ok(await _marketOrdersRepo.GetOrdersAsync(orderIds));
        }

        [HttpGet("GetByClientId")]
        [SwaggerOperation("MarketOrdersOperations_GetByClientId")]
        [ProducesResponseType(typeof(IEnumerable<MarketOrder>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetOrdersAsync([FromQuery] string clientId)
        {
            if (!CommonValidator.ValidateClientId(clientId))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(clientId)));
            }

            return Ok(await _marketOrdersRepo.GetOrdersAsync(clientId));
        }

        [HttpGet("GetByClientIdAndOrderId")]
        [SwaggerOperation("MarketOrdersOperations_GetByClientIdAndOrderId")]
        [ProducesResponseType(typeof(MarketOrder), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetOrderAsync([FromQuery] string clientId, [FromQuery]string orderId)
        {
            if (!CommonValidator.ValidateClientId(clientId))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(clientId)));
            }

            if (!CommonValidator.ValidateOrderId(orderId))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(orderId)));
            }

            return Ok(await _marketOrdersRepo.GetAsync(clientId, orderId));
        }

        [HttpGet("GetByOrderId")]
        [SwaggerOperation("MarketOrdersOperations_GetByOrderId")]
        [ProducesResponseType(typeof(MarketOrder), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetOrderAsync([FromQuery]string orderId)
        {
            if (!CommonValidator.ValidateOrderId(orderId))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(orderId)));
            }

            return Ok(await _marketOrdersRepo.GetAsync(orderId));
        }

        [HttpPost("CreateMarketOrder")]
        [SwaggerOperation("MarketOrdersOperations_CreateMarketOrder")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateAsync([FromBody] MarketOrder order)
        {
            if (!CommonValidator.ValidateClientId(order.ClientId))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(order.ClientId)));
            }

            if (!CommonValidator.ValidateRowKeyId(order.Id))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(order.Id)));
            }

            await _marketOrdersRepo.CreateAsync(order);

            return Ok();
        }
    }
}
