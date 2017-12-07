using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Common.Log;
using Lykke.Service.OperationsRepository.Core;
using Lykke.Service.OperationsRepository.Core.CashOperations;
using Lykke.Service.OperationsRepository.Core.Domain;
using Lykke.Service.OperationsRepository.Models;
using Lykke.Service.OperationsRepository.Validation;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Lykke.Service.OperationsRepository.Controllers
{
    [Route("api/[controller]")]
    public class LimitTradeEventsController : Controller
    {
        private readonly ILimitTradeEventsRepository _limitTradeEventsRepository;
        private readonly IOperationsHistoryPublisher _historyPublisher;
        private readonly ILog _log;

        public LimitTradeEventsController(ILimitTradeEventsRepository limitTradeEventsRepo, IOperationsHistoryPublisher historyPublisher, ILog log)
        {
            _limitTradeEventsRepository = limitTradeEventsRepo;
            _historyPublisher = historyPublisher;
            _log = log;
        }

        [HttpPost]
        [SwaggerOperation("LimitTradeEvents_CreateEvent")]
        [ProducesResponseType(typeof(ILimitTradeEvent), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateAsync([FromQuery] string orderId, [FromQuery] string clientId,
            [FromQuery] OrderType type, [FromQuery] double volume, [FromQuery] string assetId,
            [FromQuery] string assetPair, [FromQuery] double price, [FromQuery] OrderStatus status,
            [FromQuery] DateTime dateTime)
        {
            if (!CommonValidator.ValidateClientId(clientId))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(clientId)));
            }

            var result = await _limitTradeEventsRepository.CreateEvent(orderId, clientId, type, volume, assetId,
                assetPair, price, status, dateTime);

            if (result != null)
            {
                try
                {
                    await _historyPublisher.PublishAsync(this.MapFrom(result));
                }
                catch (Exception e)
                {
                    await _log.WriteErrorAsync(nameof(LimitTradeEventsController), nameof(CreateAsync), "", e, DateTime.Now);
                }
            }

            return Ok(result);
        }

        [HttpGet("{clientId}")]
        [SwaggerOperation("LimitTradeEvents_GetEvents")]
        [ProducesResponseType(typeof(IEnumerable<ILimitTradeEvent>), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAsync(string clientId)
        {
            if (!CommonValidator.ValidateClientId(clientId))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(clientId)));
            }

            return Ok(await _limitTradeEventsRepository.GetEventsAsync(clientId));
        }
    }
}