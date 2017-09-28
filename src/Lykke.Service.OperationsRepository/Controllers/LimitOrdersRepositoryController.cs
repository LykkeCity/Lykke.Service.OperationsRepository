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
    public class LimitOrdersRepositoryController : Controller
    {
        private readonly ILimitOrdersRepository _limitOrdersRepo;
        private readonly IHistoryWriter _historyWriter;
        private readonly ILog _log;

        public LimitOrdersRepositoryController(ILimitOrdersRepository limitOrdersRepo,
                                                    IHistoryWriter historyWriter, ILog log)
        {
            _limitOrdersRepo = limitOrdersRepo;
            _historyWriter = historyWriter;
            _log = log;
        }

        [HttpGet("GetOrder")]
        [SwaggerOperation("LimitOrdersOperations_GetOrder")]
        [ProducesResponseType(typeof(LimitOrder), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetOrderAsync([FromQuery] string orderId)
        {
            if (!CommonValidator.ValidateOrderId(orderId))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(orderId)));
            }

            return Ok(await _limitOrdersRepo.GetOrderAsync(orderId));
        }

        [HttpGet("GetActiveOrders")]
        [SwaggerOperation("LimitOrdersOperations_GetActiveOrders")]
        [ProducesResponseType(typeof(IEnumerable<LimitOrder>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetActiveOrdersAsync([FromQuery] string clientId)
        {
            if (!CommonValidator.ValidateClientId(clientId))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(clientId)));
            }

            return Ok(await _limitOrdersRepo.GetActiveOrdersAsync(clientId));
        }

        [HttpGet("GetByClientId")]
        [SwaggerOperation("LimitOrdersOperations_GetByClientId")]
        [ProducesResponseType(typeof(IEnumerable<LimitOrder>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetOrdersAsync([FromQuery] string clientId)
        {
            if (!CommonValidator.ValidateClientId(clientId))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(clientId)));
            }

            return Ok(await _limitOrdersRepo.GetOrdersAsync(clientId));
        }

        [HttpGet("GetByOrdersIds")]
        [SwaggerOperation("LimitOrdersOperations_GetByOrdersIds")]
        [ProducesResponseType(typeof(IEnumerable<LimitOrder>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetOrdersAsync([FromQuery] string[] orderIds)
        {
            if (!CommonValidator.ValidateOrderIds(orderIds))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(orderIds)));
            }

            return Ok(await _limitOrdersRepo.GetOrdersAsync(orderIds));
        }
    }
}
