using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Lykke.Service.OperationsRepository.Core.CashOperations;
using Lykke.Service.OperationsRepository.Models;
using Lykke.Service.OperationsRepository.Validation;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.SwaggerGen.Annotations;

namespace Lykke.Service.OperationsRepository.Controllers
{
    [Route("api/[controller]")]
    public class ClientTradesRepositoryController: Controller
    {
        private readonly IClientTradesRepository _clientTradesRepo;

        public ClientTradesRepositoryController(IClientTradesRepository clientTradesRepo)
        {
            _clientTradesRepo = clientTradesRepo;
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

            return Ok(await _clientTradesRepo.SaveAsync(clientTrades));
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

        [HttpGet("GetByRecordId")]
        [SwaggerOperation("ClientTradeOperations_GetByRecordId")]
        [ProducesResponseType(typeof(ClientTrade), (int) HttpStatusCode.OK)]
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

            return Ok(await _clientTradesRepo.GetAsync(clientId, recordId));
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

            await _clientTradesRepo.UpdateBlockChainHashAsync(clientId, recordId, hash);

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

            await _clientTradesRepo.SetDetectionTimeAndConfirmations(clientId, recordId, detectTime, confirmations);

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

            await _clientTradesRepo.SetIsSettledAsync(clientId, id, offchain);

            return Ok();
        }

        [HttpGet("GetByMultisig")]
        [SwaggerOperation("ClientTradeOperations_GetByMultisig")]
        [ProducesResponseType(typeof(IEnumerable<ClientTrade>), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetByMultisigAsync([FromQuery] string multisig)
        {
            if (!CommonValidator.ValidateMultisig(multisig))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(multisig)));
            }

            return Ok(await _clientTradesRepo.GetByMultisigAsync(multisig));
        }

        [HttpGet("GetByMultisigs")]
        [SwaggerOperation("ClientTradeOperations_GetByMultisigs")]
        [ProducesResponseType(typeof(IEnumerable<ClientTrade>), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetByMultisigsAsync([FromQuery] string[] multisigs)
        {
            if (!CommonValidator.ValidateMultisig(multisigs))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(multisigs)));
            }

            return Ok(await _clientTradesRepo.GetByMultisigsAsync(multisigs));
        }
    }
}