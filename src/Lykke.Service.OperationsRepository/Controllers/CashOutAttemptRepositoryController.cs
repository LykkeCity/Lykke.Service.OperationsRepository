﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Lykke.Service.OperationsRepository.Core.CashOperations;
using Lykke.Service.OperationsRepository.Models;
using Lykke.Service.OperationsRepository.Validation;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.SwaggerGen.Annotations;

namespace Lykke.Service.OperationsRepository.Controllers
{
    [Route("api/[controller]")]
    public class CashOutAttemptRepositoryController: Controller
    {
        private readonly ICashOutAttemptRepository _cashOutAttemptRepo;

        public CashOutAttemptRepositoryController(ICashOutAttemptRepository cashOutAttemptRepo)
        {
            _cashOutAttemptRepo = cashOutAttemptRepo;
        }

        [HttpPost("InsertRequest")]
        [SwaggerOperation("CashOutAttemptOperations_InsertRequest")]
        [ProducesResponseType(typeof(string), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> InsertRequestAsync([FromBody] ICashOutRequest request,
            [FromBody] PaymentSystem paymentSystem, [FromBody] object paymentFields, [FromBody] CashOutRequestTradeSystem tradeSystem)
        {
            if (!CashOperationsValidator.ValidateCashOutRequest(request))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(request)));
            }
            if (!CashOperationsValidator.ValidatePaymentSystem(paymentSystem))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(paymentSystem)));
            }
            if (!CashOperationsValidator.ValidatePaymentFields(paymentFields))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(paymentFields)));
            }

            return Ok(await _cashOutAttemptRepo.InsertRequestAsync(request, paymentSystem, paymentFields, tradeSystem));
        }

        [HttpGet("GetAllAttempts")]
        [SwaggerOperation("CashOutAttemptOperations_GetAllAttempts")]
        [ProducesResponseType(typeof(IEnumerable<ICashOutRequest>), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAllAttempts()
        {
            return Ok(await _cashOutAttemptRepo.GetAllAttempts());
        }

        [HttpPost("SetBlockchainHash")]
        [SwaggerOperation("CashOutAttemptOperations_SetBlockchainHash")]
        [ProducesResponseType(typeof(void), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SetBlockchainHash([FromBody] string clientId, [FromBody] string requestId,
            [FromBody] string hash)
        {
            if (!CommonValidator.ValidateClientId(clientId))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(clientId)));
            }
            if (!CommonValidator.ValidateRecordId(requestId))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(requestId)));
            }
            if (!CashOperationsValidator.ValidateBlockchainHash(hash))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(hash)));
            }

            await _cashOutAttemptRepo.SetBlockchainHash(clientId, requestId, hash);

            return Ok();
        }

        [HttpPost("SetPending")]
        [SwaggerOperation("CashOutAttemptOperations_SetPending")]
        [ProducesResponseType(typeof(ICashOutRequest), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SetPending([FromBody] string clientId, [FromBody] string requestId)
        {
            if (!CommonValidator.ValidateClientId(clientId))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(clientId)));
            }
            if (!CommonValidator.ValidateRecordId(requestId))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(requestId)));
            }

            return Ok(await _cashOutAttemptRepo.SetPending(clientId, requestId));
        }

        [HttpPost("SetConfirmed")]
        [SwaggerOperation("CashOutAttemptOperations_SetConfirmed")]
        [ProducesResponseType(typeof(ICashOutRequest), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SetConfirmed([FromBody] string clientId, [FromBody] string requestId)
        {
            if (!CommonValidator.ValidateClientId(clientId))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(clientId)));
            }
            if (!CommonValidator.ValidateRecordId(requestId))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(requestId)));
            }

            return Ok(await _cashOutAttemptRepo.SetConfirmed(clientId, requestId));
        }

        [HttpPost("SetDocsRequested")]
        [SwaggerOperation("CashOutAttemptOperations_SetDocsRequested")]
        [ProducesResponseType(typeof(ICashOutRequest), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SetDocsRequested([FromBody] string clientId, [FromBody] string requestId)
        {
            if (!CommonValidator.ValidateClientId(clientId))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(clientId)));
            }
            if (!CommonValidator.ValidateRecordId(requestId))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(requestId)));
            }

            return Ok(await _cashOutAttemptRepo.SetDocsRequested(clientId, requestId));
        }

        [HttpPost("SetDeclined")]
        [SwaggerOperation("CashOutAttemptOperations_SetDeclined")]
        [ProducesResponseType(typeof(ICashOutRequest), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SetDeclined([FromBody] string clientId, [FromBody] string requestId)
        {
            if (!CommonValidator.ValidateClientId(clientId))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(clientId)));
            }
            if (!CommonValidator.ValidateRecordId(requestId))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(requestId)));
            }

            return Ok(await _cashOutAttemptRepo.SetDeclined(clientId, requestId));
        }

        [HttpPost("SetCanceledByClient")]
        [SwaggerOperation("CashOutAttemptOperations_SetCanceledByClient")]
        [ProducesResponseType(typeof(ICashOutRequest), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SetCanceledByClient([FromBody] string clientId, [FromBody] string requestId)
        {
            if (!CommonValidator.ValidateClientId(clientId))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(clientId)));
            }
            if (!CommonValidator.ValidateRecordId(requestId))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(requestId)));
            }

            return Ok(await _cashOutAttemptRepo.SetCanceledByClient(clientId, requestId));
        }

        [HttpPost("SetCanceledByTimeout")]
        [SwaggerOperation("CashOutAttemptOperations_SetCanceledByTimeout")]
        [ProducesResponseType(typeof(ICashOutRequest), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SetCanceledByTimeout([FromBody] string clientId, [FromBody] string requestId)
        {
            if (!CommonValidator.ValidateClientId(clientId))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(clientId)));
            }
            if (!CommonValidator.ValidateRecordId(requestId))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(requestId)));
            }

            return Ok(await _cashOutAttemptRepo.SetCanceledByTimeout(clientId, requestId));
        }

        [HttpPost("SetProcessed")]
        [SwaggerOperation("CashOutAttemptOperations_SetProcessed")]
        [ProducesResponseType(typeof(void), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SetProcessed([FromBody] string clientId, [FromBody] string requestId)
        {
            if (!CommonValidator.ValidateClientId(clientId))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(clientId)));
            }
            if (!CommonValidator.ValidateRecordId(requestId))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(requestId)));
            }

            await _cashOutAttemptRepo.SetProcessed(clientId, requestId);

            return Ok();
        }

        [HttpPost("SetIsSettledOffchain")]
        [SwaggerOperation("CashOutAttemptOperations_SetIsSettledOffchain")]
        [ProducesResponseType(typeof(void), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SetIsSettledOffchain([FromBody] string clientId, [FromBody] string requestId)
        {
            if (!CommonValidator.ValidateClientId(clientId))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(clientId)));
            }
            if (!CommonValidator.ValidateRecordId(requestId))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(requestId)));
            }

            await _cashOutAttemptRepo.SetIsSettledOffchain(clientId, requestId);

            return Ok();
        }

        [HttpGet("GetHistoryRecords")]
        [SwaggerOperation("CashOutAttemptOperations_GetHistoryRecords")]
        [ProducesResponseType(typeof(IEnumerable<ICashOutRequest>), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetHistoryRecordsAsync([FromQuery] DateTime @from, [FromQuery] DateTime to)
        {
            return Ok(await _cashOutAttemptRepo.GetHistoryRecordsAsync(from, to));
        }
    }
}
