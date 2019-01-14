using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Common.Log;
using Lykke.Service.OperationsRepository.Core;
using Lykke.Service.OperationsRepository.Core.CashOperations;
using Lykke.Service.OperationsRepository.Models;
using Lykke.Service.OperationsRepository.Models.LimitTradeEvent;
using Lykke.Service.OperationsRepository.Validation;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Lykke.Service.OperationsRepository.Controllers
{
    [Route("api/[controller]")]
    public class LimitTradeEventsRepositoryController : Controller
    {
        private readonly ILimitTradeEventsRepository _limitTradeEventsRepository;
        private readonly IOperationsHistoryPublisher _historyPublisher;
        private readonly ILog _log;

        public LimitTradeEventsRepositoryController(ILimitTradeEventsRepository limitTradeEventsRepo, IOperationsHistoryPublisher historyPublisher, ILog log)
        {
            _limitTradeEventsRepository = limitTradeEventsRepo;
            _historyPublisher = historyPublisher;
            _log = log;
        }

        [HttpPost]
        [SwaggerOperation("LimitTradeEventOperations_CreateEvent")]
        [ProducesResponseType(typeof(LimitTradeEvent), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateAsync([FromBody] LimitTradeEventInsertRequest model)
        {
            var result = await _limitTradeEventsRepository.CreateEvent(model.OrderId, model.ClientId, model.Type,
                model.Volume, model.AssetId, model.AssetPair, model.Price, model.Status, model.DateTime);

            if (result != null)
            {
                try
                {
                    await _historyPublisher.PublishAsync(this.MapFrom(result));
                }
                catch (Exception e)
                {
                    await _log.WriteErrorAsync(nameof(LimitTradeEventsRepositoryController), nameof(CreateAsync), "", e);
                }
            }

            return Ok(result);
        }

        [HttpGet("{clientId}")]
        [SwaggerOperation("LimitTradeEventOperations_GetEvents")]
        [ProducesResponseType(typeof(IEnumerable<LimitTradeEvent>), (int) HttpStatusCode.OK)]
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