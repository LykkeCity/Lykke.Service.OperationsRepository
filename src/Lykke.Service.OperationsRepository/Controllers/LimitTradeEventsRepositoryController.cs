using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
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

        public LimitTradeEventsRepositoryController(ILimitTradeEventsRepository limitTradeEventsRepo)
        {
            _limitTradeEventsRepository = limitTradeEventsRepo;
        }

        [HttpPost]
        [SwaggerOperation("LimitTradeEventOperations_CreateEvent")]
        [ProducesResponseType(typeof(LimitTradeEvent), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateAsync([FromBody] LimitTradeEventInsertRequest model)
        {
            var result = await _limitTradeEventsRepository.CreateEvent(
                model.OrderId,
                model.ClientId,
                model.Type,
                model.Volume,
                model.AssetId,
                model.AssetPair,
                model.Price,
                model.Status,
                model.DateTime);

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