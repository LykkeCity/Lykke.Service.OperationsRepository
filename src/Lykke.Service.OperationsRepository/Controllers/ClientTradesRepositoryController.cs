using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Common.Log;
using Lykke.Service.OperationsRepository.Core;
using Lykke.Service.OperationsRepository.Core.CashOperations;
using Lykke.Service.OperationsRepository.Models;
using Lykke.Service.OperationsRepository.Validation;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Lykke.Service.OperationsRepository.Controllers
{
    [Route("api/[controller]")]
    public class ClientTradesRepositoryController: Controller
    {
        private readonly IClientTradesRepository _clientTradesRepo;
        private readonly IOperationsHistoryPublisher _historyPublisher;
        private readonly ILog _log;

        public ClientTradesRepositoryController(IClientTradesRepository clientTradesRepo, IOperationsHistoryPublisher historyPublisher, ILog log)
        {
            _clientTradesRepo = clientTradesRepo;
            _historyPublisher = historyPublisher;
            _log = log;
        }

        [HttpPost("Save")]
        [SwaggerOperation("ClientTradeOperations_Save")]
        [ProducesResponseType(typeof(ClientTrade[]), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SaveAsync([FromBody] ClientTrade[] clientTrades)
        {
            if (!CashOperationsValidator.ValidateClientTrades(clientTrades))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(clientTrades)));
            }

            var result = await _clientTradesRepo.SaveAsync(clientTrades);

            try
            {
                var tasks = clientTrades.Select(x => _historyPublisher.PublishAsync(this.MapFrom(x)));
                await Task.WhenAll(tasks);
            }
            catch (Exception e)
            {
                await _log.WriteErrorAsync(GetType().Name, "SaveAsync", "", e);
            }

            return Ok(result);
        }

        [HttpGet]
        [SwaggerOperation("ClientTradeOperations_Get")]
        [ProducesResponseType(typeof(IEnumerable<ClientTrade>), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAsync([FromQuery] string clientId)
        {
            if (!CommonValidator.ValidateClientId(clientId))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(clientId)));
            }

            return Ok(await _clientTradesRepo.GetAsync(clientId));
        }

        [HttpGet("GetByDates")]
        [SwaggerOperation("ClientTradeOperations_GetByDates")]
        [ProducesResponseType(typeof(IEnumerable<ClientTrade>), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAsync([FromQuery] DateTime from, [FromQuery] DateTime to)
        {
            return Ok(await _clientTradesRepo.GetAsync(from, to));
        }

        [HttpGet("GetByDatesWithChunks")]
        [SwaggerOperation("ClientTradeOperations_GetByDatesWithChunks")]
        [ProducesResponseType(typeof(ClientTradesChunk), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetByDatesAsync([FromQuery] DateTime from, [FromQuery] DateTime to, [FromQuery] string continuationToken)
        {
            var (trades, token) = await _clientTradesRepo.GetByDatesAsync(from, to, continuationToken);
            return Ok(new ClientTradesChunk
            {
                Trades = trades.Cast<ClientTrade>(),
                ContinuationToken = token,
            });
        }

        [HttpGet("GetByRecordId")]
        [SwaggerOperation("ClientTradeOperations_GetByRecordId")]
        [ProducesResponseType(typeof(ClientTrade), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAsync([FromQuery] string clientId, [FromQuery] string recordId)
        {
            if (!CommonValidator.ValidateClientId(clientId))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(clientId)));
            }
            if (!CommonValidator.ValidateRecordId(recordId))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(recordId)));
            }

            var record = await _clientTradesRepo.GetAsync(clientId, recordId);
            if (record == null)
            {
                return NoContent();
            }

            return Ok(record);
        }

        [HttpPost("UpdateBlockchainHash")]
        [SwaggerOperation("ClientTradeOperations_UpdateBlockchainHash")]
        [ProducesResponseType(typeof(void), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateBlockChainHashAsync([FromQuery] string clientId,
            [FromQuery] string recordId, [FromQuery] string hash)
        {
            if (!CommonValidator.ValidateClientId(clientId))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(clientId)));
            }
            if (!CommonValidator.ValidateRecordId(recordId))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(recordId)));
            }
            if (!CashOperationsValidator.ValidateBlockchainHash(hash))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(hash)));
            }

            var updated = await _clientTradesRepo.UpdateBlockChainHashAsync(clientId, recordId, hash);

            if (updated != null)
            {
                try
                {
                    await _historyPublisher.PublishAsync(this.MapFrom(updated));
                }
                catch (Exception e)
                {
                    await _log.WriteErrorAsync(GetType().Name, "UpdateBlockChainHashAsync", "", e);
                }
            }

            return Ok();
        }

        [HttpPost("SetDetectionTimeAndConfirmations")]
        [SwaggerOperation("ClientTradeOperations_SetDetectionTimeAndConfirmations")]
        [ProducesResponseType(typeof(void), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SetDetectionTimeAndConfirmations([FromQuery] string clientId, [FromQuery] string recordId,
            [FromQuery] DateTime detectTime, [FromQuery] int confirmations)
        {
            if (!CommonValidator.ValidateClientId(clientId))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(clientId)));
            }
            if (!CommonValidator.ValidateRecordId(recordId))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(recordId)));
            }

            var updated = await _clientTradesRepo.SetDetectionTimeAndConfirmations(clientId, recordId, detectTime, confirmations);

            if (updated != null)
            {
                try
                {
                    await _historyPublisher.PublishAsync(this.MapFrom(updated));
                }
                catch (Exception e)
                {
                    await _log.WriteErrorAsync(GetType().Name, "SetDetectionTimeAndConfirmations", "", e);
                }
            }

            return Ok();
        }

        [HttpPost("SetBtcTransaction")]
        [SwaggerOperation("ClientTradeOperations_SetBtcTransaction")]
        [ProducesResponseType(typeof(void), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SetBtcTransactionAsync([FromQuery] string clientId, [FromQuery] string recordId,
            [FromQuery] string btcTransactionId)
        {
            if (!CommonValidator.ValidateClientId(clientId))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(clientId)));
            }
            if (!CommonValidator.ValidateRecordId(recordId))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(recordId)));
            }
            if (!CashOperationsValidator.ValidateTransactionId(btcTransactionId))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(btcTransactionId)));
            }

            await _clientTradesRepo.SetBtcTransactionAsync(clientId, recordId, btcTransactionId);

            return Ok();
        }

        [HttpPost("SetIsSettled")]
        [SwaggerOperation("ClientTradeOperations_SetIsSettled")]
        [ProducesResponseType(typeof(void), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SetIsSettledAsync([FromQuery] string clientId, [FromQuery] string id,
            [FromQuery] bool offchain)
        {
            if (!CommonValidator.ValidateClientId(clientId))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(clientId)));
            }
            if (!CommonValidator.ValidateRecordId(id))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(id)));
            }

            var updated = await _clientTradesRepo.SetIsSettledAsync(clientId, id, offchain);

            if (updated != null)
            {
                try
                {
                    await _historyPublisher.PublishAsync(this.MapFrom(updated));
                }
                catch (Exception e)
                {
                    await _log.WriteErrorAsync(GetType().Name, "SetIsSettledAsync", "", e);
                }
            }

            return Ok();
        }
        
        [HttpGet("ScanByDt")]
        [SwaggerOperation("ClientTradeOperations_ScanByDt")]
        [ProducesResponseType(typeof(IEnumerable<ClientTrade>), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ScanByDtAsync([FromQuery] DateTime from, [FromQuery] DateTime to)
        {
            if (!CommonValidator.ValidateDateTime(from))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(from)));
            }
            if (!CommonValidator.ValidateDateTime(to))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(to)));
            }
            if (!CommonValidator.ValidatePeriod(from, to))
            {
                return BadRequest(new ErrorResponse {ErrorMessage = "Date {from} must be less or equal to date {to}"});
            }

            return Ok(await _clientTradesRepo.ScanByDtAsync(from, to));
        }

        [HttpGet("GetByOrder")]
        [SwaggerOperation("ClientTradeOperations_GetByOrder")]
        [ProducesResponseType(typeof(IEnumerable<ClientTrade>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetByOrderAsync([FromQuery] string orderId)
        {
            if (!CashOperationsValidator.ValidateOrderId(orderId))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(orderId)));
            }

            return Ok(await _clientTradesRepo.GetByOrderAsync(orderId));
        }
    }
}