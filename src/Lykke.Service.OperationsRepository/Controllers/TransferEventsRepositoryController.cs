﻿using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Lykke.Service.OperationsRepository.Core.CashOperations;
using Lykke.Service.OperationsRepository.Models;
using Lykke.Service.OperationsRepository.Validation;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Lykke.Service.OperationsRepository.Controllers
{
    [Route("api/[controller]")]
    public class TransferEventsRepositoryController : Controller
    {
        private readonly ITransferEventsRepository _transferEventsRepo;

        public TransferEventsRepositoryController(ITransferEventsRepository transferEventsRepo)
        {
            _transferEventsRepo = transferEventsRepo;
        }

        [HttpPost("Register")]
        [SwaggerOperation("TransferOperations_Register")]
        [ProducesResponseType(typeof(TransferEvent), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> RegisterAsync([FromBody] TransferEvent transferEvent)
        {
            if (!CashOperationsValidator.ValidateTransferEvent(transferEvent))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(transferEvent)));
            }

            var result = await _transferEventsRepo.RegisterAsync(transferEvent);

            return Ok(result);
        }

        [HttpGet]
        [SwaggerOperation("TransferOperations_Get")]
        [ProducesResponseType(typeof(IEnumerable<TransferEvent>), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAsync([FromQuery] string clientId)
        {
            if (!CommonValidator.ValidateClientId(clientId))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(clientId)));
            }

            return Ok(await _transferEventsRepo.GetAsync(clientId));
        }

        [HttpGet("GetByRecordId")]
        [SwaggerOperation("TransferOperations_GetByRecordId")]
        [ProducesResponseType(typeof(TransferEvent), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAsync([FromQuery] string clientId, [FromQuery] string id)
        {
            if (!CommonValidator.ValidateClientId(clientId))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(clientId)));
            }
            if (!CommonValidator.ValidateRecordId(id))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(id)));
            }

            var record = await _transferEventsRepo.GetAsync(clientId, id);
            if (record == null)
            {
                return NoContent();
            }

            return Ok(record);
        }

        [HttpPost("UpdateBlockchainHash")]
        [SwaggerOperation("TransferOperations_UpdateBlockchainHash")]
        [ProducesResponseType(typeof(void), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateBlockChainHashAsync([FromQuery] string clientId, [FromQuery] string id,
            [FromQuery] string blockChainHash)
        {
            if (!CommonValidator.ValidateClientId(clientId))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(clientId)));
            }
            if (!CommonValidator.ValidateRecordId(id))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(id)));
            }
            if (!CashOperationsValidator.ValidateBlockchainHash(blockChainHash))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(blockChainHash)));
            }

            await _transferEventsRepo.UpdateBlockChainHashAsync(clientId, id, blockChainHash);

            return Ok();
        }

        [HttpPost("SetBtcTransaction")]
        [SwaggerOperation("TransferOperations_SetBtcTransaction")]
        [ProducesResponseType(typeof(void), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SetBtcTransactionAsync([FromQuery] string clientId, [FromQuery] string id,
            [FromQuery] string btcTransaction)
        {
            if (!CommonValidator.ValidateClientId(clientId))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(clientId)));
            }
            if (!CommonValidator.ValidateRecordId(id))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(id)));
            }
            if (!CashOperationsValidator.ValidateTransactionId(btcTransaction))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(btcTransaction)));
            }

            await _transferEventsRepo.SetBtcTransactionAsync(clientId, id, btcTransaction);

            return Ok();
        }

        [HttpPost("SetIsSettledIfExists")]
        [SwaggerOperation("TransferOperations_SetIsSettledIfExists")]
        [ProducesResponseType(typeof(void), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SetIsSettledIfExistsAsync([FromQuery] string clientId, [FromQuery] string id,
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

            await _transferEventsRepo.SetIsSettledIfExistsAsync(clientId, id, offchain);

            return Ok();
        }

        [HttpGet("GetByHash")]
        [SwaggerOperation("TransferOperations_GetByHash")]
        [ProducesResponseType(typeof(IEnumerable<TransferEvent>), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetByHashAsync([FromQuery] string blockchainHash)
        {
            if (!CashOperationsValidator.ValidateBlockchainHash(blockchainHash))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(blockchainHash)));
            }

            return Ok(await _transferEventsRepo.GetByHashAsync(blockchainHash));
        }

        [HttpGet("GetByMultisig")]
        [SwaggerOperation("TransferOperations_GetByMultisig")]
        [ProducesResponseType(typeof(IEnumerable<TransferEvent>), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetByMultisigAsync([FromQuery] string multisig)
        {
            if (!CommonValidator.ValidateMultisig(multisig))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(multisig)));
            }

            return Ok(await _transferEventsRepo.GetByMultisigAsync(multisig));
        }

        [HttpGet("GetByMultisigs")]
        [SwaggerOperation("TransferOperations_GetByMultisigs")]
        [ProducesResponseType(typeof(IEnumerable<TransferEvent>), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetByMultisigsAsync([FromQuery] string[] multisigs)
        {
            if (!CommonValidator.ValidateMultisig(multisigs))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(multisigs)));
            }

            return Ok(await _transferEventsRepo.GetByMultisigsAsync(multisigs));
        }
    }
}
