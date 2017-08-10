﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Lykke.Service.OperationsHistory.HistoryWriter.Abstractions;
using Lykke.Service.OperationsRepository.Core.CashOperations;
using Lykke.Service.OperationsRepository.Models;
using Lykke.Service.OperationsRepository.Validation;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.SwaggerGen.Annotations;

namespace Lykke.Service.OperationsRepository.Controllers
{
    [Route("api/[controller]")]
    public class CashOperationsRepositoryController : Controller
    {
        private readonly ICashOperationsRepository _cashOperationsRepo;
        private readonly IHistoryWriter _historyWriter;

        public CashOperationsRepositoryController(ICashOperationsRepository cashOperationsRepo, IHistoryWriter historyWriter)
        {
            _cashOperationsRepo = cashOperationsRepo;
            _historyWriter = historyWriter;
        }

        [HttpPost("Register")]
        [SwaggerOperation("CashOperations_Register")]
        [ProducesResponseType(typeof(IdResponseModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Register([FromBody] CashInOutOperation operation)
        {
            if (!CashOperationsValidator.ValidateOperation(operation))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(operation)));
            }

            var id = await _cashOperationsRepo.RegisterAsync(operation);

            await _historyWriter.Push(this.MapFrom(operation));

            return Ok(new IdResponseModel {Id = id});
        }

        [HttpGet]
        [SwaggerOperation("CashOperations_Get")]
        [ProducesResponseType(typeof(IEnumerable<CashInOutOperation>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAsync([FromQuery] string clientId)
        {
            if (!CommonValidator.ValidateClientId(clientId))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(clientId)));
            }

            return Ok(await _cashOperationsRepo.GetAsync(clientId));
        }

        [HttpGet("GetByRecordId")]
        [SwaggerOperation("CashOperations_GetByRecordId")]
        [ProducesResponseType(typeof(CashInOutOperation), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
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

            var record = await _cashOperationsRepo.GetAsync(clientId, recordId);

            if (record == null)
            {
                return NoContent();
            }

            return Ok(record);
        }

        [HttpPost("UpdateBlockchainHash")]
        [SwaggerOperation("CashOperations_UpdateBlockchainHash")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateBlockchainHashAsync([FromQuery] string clientId, [FromQuery] string id, [FromQuery] string hash)
        {
            if (!CommonValidator.ValidateClientId(clientId))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(clientId)));
            }
            if (!CommonValidator.ValidateRowKeyId(id))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(id)));
            }
            if (!CashOperationsValidator.ValidateBlockchainHash(hash))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(hash)));
            }

            await _cashOperationsRepo.UpdateBlockchainHashAsync(clientId, id, hash);

            return Ok();
        }

        [HttpPost("SetBtcTransaction")]
        [SwaggerOperation("CashOperations_SetBtcTransaction")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SetBtcTransaction([FromQuery] string clientId,[FromQuery] string id, [FromQuery] string bcnTransactionId)
        {
            if (!CommonValidator.ValidateClientId(clientId))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(clientId)));
            }
            if (!CommonValidator.ValidateRowKeyId(id))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(id)));
            }
            if (!CashOperationsValidator.ValidateTransactionId(bcnTransactionId))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(bcnTransactionId)));
            }

            await _cashOperationsRepo.SetBtcTransaction(clientId, id, bcnTransactionId);

            return Ok();
        }

        [HttpPost("SetIsSettled")]
        [SwaggerOperation("CashOperations_SetIsSettled")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SetIsSettledAsync([FromQuery] string clientId, [FromQuery] string id, [FromQuery] bool offchain)
        {
            if (!CommonValidator.ValidateClientId(clientId))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(clientId)));
            }
            if (!CommonValidator.ValidateRowKeyId(id))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(id)));
            }

            await _cashOperationsRepo.SetIsSettledAsync(clientId, id, offchain);

            return Ok();
        }

        [HttpGet("GetByHash")]
        [SwaggerOperation("CashOperations_GetByHash")]
        [ProducesResponseType(typeof(IEnumerable<CashInOutOperation>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetByHashAsync([FromQuery] string blockchainHash)
        {
            if (!CashOperationsValidator.ValidateBlockchainHash(blockchainHash))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(blockchainHash)));
            }

            return Ok(await _cashOperationsRepo.GetByHashAsync(blockchainHash));
        }

        [HttpGet("GetByMultisig")]
        [SwaggerOperation("CashOperations_GetByMultisig")]
        [ProducesResponseType(typeof(IEnumerable<CashInOutOperation>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetByMultisigAsync([FromQuery] string multisig)
        {
            if (!CommonValidator.ValidateMultisig(multisig))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(multisig)));
            }

            return Ok(await _cashOperationsRepo.GetByMultisigAsync(multisig));
        }

        [HttpGet("GetByMultisigs")]
        [SwaggerOperation("CashOperations_GetByMultisigs")]
        [ProducesResponseType(typeof(IEnumerable<CashInOutOperation>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetByMultisigsAsync([FromQuery] string[] multisigs)
        {
            if (!CommonValidator.ValidateMultisig(multisigs))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(multisigs)));
            }

            return Ok(await _cashOperationsRepo.GetByMultisigsAsync(multisigs));
        }
    }
}
