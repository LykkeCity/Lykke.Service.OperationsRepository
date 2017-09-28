using Common.Log;
using Lykke.Service.OperationsHistory.HistoryWriter.Abstractions;
using Lykke.Service.OperationsRepository.Core.CashOperations;
using Lykke.Service.OperationsRepository.Core.Entities;
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
    public class LimitTradeEventsRepositoryController : Controller
    {
        private readonly ILimitTradeEventsRepository _limitTradeEventsRepo;
        private readonly IHistoryWriter _historyWriter;
        private readonly ILog _log;

        public LimitTradeEventsRepositoryController(ILimitTradeEventsRepository limitTradeEventsRepo,
                                                    IHistoryWriter historyWriter, ILog log)
        {
            _limitTradeEventsRepo = limitTradeEventsRepo;
            _historyWriter = historyWriter;
            _log = log;
        }

        [HttpGet]
        [SwaggerOperation("LimitTradeEvents_Get")]
        [ProducesResponseType(typeof(IEnumerable<LimitTradeEvent>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAsync([FromQuery] string clientId)
        {
            if (!CommonValidator.ValidateClientId(clientId))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(clientId)));
            }

            return Ok(await _limitTradeEventsRepo.GetEventsAsync(clientId));
        }

        [HttpGet("GetByOrederId")]
        [SwaggerOperation("LimitTradeEvents_GetByOrderId")]
        [ProducesResponseType(typeof(IEnumerable<LimitTradeEvent>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAsync([FromQuery] string clientId, [FromQuery] string orderId)
        {
            if (!CommonValidator.ValidateClientId(clientId))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(clientId)));
            }
            if (!CommonValidator.ValidateOrderId(orderId))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(orderId)));
            }

            var record = await _limitTradeEventsRepo.GetEventsAsync(clientId, orderId);

            if (record == null)
            {
                return NoContent();
            }

            return Ok(record);
        }
    }
}
