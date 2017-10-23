using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Common.Log;
using Lykke.Service.OperationsHistory.HistoryWriter.Abstractions;
using Lykke.Service.OperationsRepository.AzureRepositories.CashOperations;
using Lykke.Service.OperationsRepository.Core.CashOperations;
using Lykke.Service.OperationsRepository.Models;
using Lykke.Service.OperationsRepository.Models.CashOutAttempt;
using Lykke.Service.OperationsRepository.Validation;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.SwaggerGen.Annotations;

namespace Lykke.Service.OperationsRepository.Controllers
{
    [Route("api/[controller]")]
    public class CashOutAttemptRepositoryController: Controller
    {
        private readonly ICashOutAttemptRepository _cashOutAttemptRepo;
        private readonly IWithdrawLimitsRepository _withdrawLimitsRepository;
        private readonly IHistoryWriter _historyWriter;
        private readonly ILog _log;

        public CashOutAttemptRepositoryController(ICashOutAttemptRepository cashOutAttemptRepo, IWithdrawLimitsRepository withdrawLimitsRepository, IHistoryWriter historyWriter, ILog log)
        {
            _cashOutAttemptRepo = cashOutAttemptRepo;
            _withdrawLimitsRepository = withdrawLimitsRepository;
            _historyWriter = historyWriter;
            _log = log;
        }

        [HttpPost("InsertRequest")]
        [SwaggerOperation("CashOutAttemptOperations_InsertRequest")]
        [ProducesResponseType(typeof(IdResponseModel), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> InsertRequestAsync([FromBody] InsertRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                var errorList = ModelState.Values.SelectMany(m => m.Errors)
                    .Select(e => string.IsNullOrWhiteSpace(e.ErrorMessage) ? e.Exception?.Message : e.ErrorMessage)
                    .ToList();

                return BadRequest(errorList);
            }

            var id = await _cashOutAttemptRepo.InsertRequestAsync(
                request.Request,
                new PaymentSystem(request.PaymentSystem.Value),
                request.PaymentFields,
                request.TradeSystem);

            try
            {
                await _historyWriter.Push(this.MapFrom(request.Request));
            }
            catch (Exception e)
            {
                await _log.WriteErrorAsync(GetType().Name, "InsertRequestAsync", "", e, DateTime.Now);
            }
            

            return Ok(new IdResponseModel {Id = id});
        }

        [HttpPost("CreateSwiftRequest")]
        [SwaggerOperation("CashOutAttemptOperations_CreateSwiftRequest")]
        [ProducesResponseType(typeof(IdResponseModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateSwiftRequestAsync([FromBody] CreateSwiftRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                var errorList = ModelState.Values.SelectMany(m => m.Errors)
                    .Select(e => string.IsNullOrWhiteSpace(e.ErrorMessage) ? e.Exception?.Message : e.ErrorMessage)
                    .ToList();

                return BadRequest(errorList);
            }

            var volumeSize = await _withdrawLimitsRepository.GetLimitByAssetAsync(request.AssetId) <= request.Amount
                ? CashOutVolumeSize.High
                : CashOutVolumeSize.Low;

            var id = await _cashOutAttemptRepo.CreateSwiftRequestAsync(request.ClientId, request.AssetId, request.Amount, new PaymentSystem("SWIFT"), volumeSize, request.SwiftCredentials);

            try
            {
                await _historyWriter.Push(this.MapFrom(request));
            }
            catch (Exception e)
            {
                await _log.WriteErrorAsync(GetType().Name, "CreateSwiftRequestAsync", "", e, DateTime.Now);
            }


            return Ok(new IdResponseModel { Id = id });
        }

        [HttpGet("GetAllAttempts")]
        [SwaggerOperation("CashOutAttemptOperations_GetAllAttempts")]
        [ProducesResponseType(typeof(IEnumerable<CashOutAttemptEntity>), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAllAttempts()
        {
            return Ok(await _cashOutAttemptRepo.GetAllAttempts());
        }

        [HttpPost("SetBlockchainHash")]
        [SwaggerOperation("CashOutAttemptOperations_SetBlockchainHash")]
        [ProducesResponseType(typeof(void), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SetBlockchainHash([FromQuery] string clientId, [FromQuery] string requestId,
            [FromQuery] string hash)
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

            try
            {
                await _historyWriter.UpdateBlockChainHash(requestId, hash);
                await _historyWriter.UpdateState(requestId, (int)TransactionStates.SettledOnchain);
            }
            catch (Exception e)
            {
                await _log.WriteErrorAsync(GetType().Name, "SetBlockchainHash", "", e, DateTime.Now);
            }

            return Ok();
        }

        [HttpPost("SetPending")]
        [SwaggerOperation("CashOutAttemptOperations_SetPending")]
        [ProducesResponseType(typeof(CashOutAttemptEntity), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SetPending([FromQuery] string clientId, [FromQuery] string requestId)
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
        [ProducesResponseType(typeof(CashOutAttemptEntity), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SetConfirmed([FromQuery] string clientId, [FromQuery] string requestId)
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
        [ProducesResponseType(typeof(CashOutAttemptEntity), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SetDocsRequested([FromQuery] string clientId, [FromQuery] string requestId)
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
        [ProducesResponseType(typeof(CashOutAttemptEntity), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SetDeclined([FromQuery] string clientId, [FromQuery] string requestId)
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
        [ProducesResponseType(typeof(CashOutAttemptEntity), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SetCanceledByClient([FromQuery] string clientId, [FromQuery] string requestId)
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
        [ProducesResponseType(typeof(CashOutAttemptEntity), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SetCanceledByTimeout([FromQuery] string clientId, [FromQuery] string requestId)
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
        [ProducesResponseType(typeof(CashOutAttemptEntity), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SetProcessed([FromQuery] string clientId, [FromQuery] string requestId)
        {
            if (!CommonValidator.ValidateClientId(clientId))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(clientId)));
            }
            if (!CommonValidator.ValidateRecordId(requestId))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(requestId)));
            }

            return Ok(await _cashOutAttemptRepo.SetProcessed(clientId, requestId));
        }

        [HttpPost("SetHighVolume")]
        [SwaggerOperation("CashOutAttemptOperations_SetHighVolume")]
        [ProducesResponseType(typeof(CashOutAttemptEntity), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SetHighVolume([FromQuery] string clientId, [FromQuery] string requestId)
        {
            if (!CommonValidator.ValidateClientId(clientId))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(clientId)));
            }
            if (!CommonValidator.ValidateRecordId(requestId))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(requestId)));
            }

            return Ok(await _cashOutAttemptRepo.SetHighVolume(clientId, requestId));
        }

        [HttpPost("SetIsSettledOffchain")]
        [SwaggerOperation("CashOutAttemptOperations_SetIsSettledOffchain")]
        [ProducesResponseType(typeof(void), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SetIsSettledOffchain([FromQuery] string clientId, [FromQuery] string requestId)
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

            try
            {
                await _historyWriter.UpdateState(requestId, (int)TransactionStates.SettledOffchain);
            }
            catch (Exception e)
            {
                await _log.WriteErrorAsync(GetType().Name, "SetIsSettledOffchain", "", e, DateTime.Now);
            }

            return Ok();
        }

        [HttpGet("GetHistoryRecords")]
        [SwaggerOperation("CashOutAttemptOperations_GetHistoryRecords")]
        [ProducesResponseType(typeof(IEnumerable<CashOutAttemptEntity>), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetHistoryRecordsAsync([FromQuery] DateTime @from, [FromQuery] DateTime to)
        {
            return Ok(await _cashOutAttemptRepo.GetHistoryRecordsAsync(from, to));
        }

        [HttpGet("GetRequests")]
        [SwaggerOperation("CashOutAttemptOperations_GetRequests")]
        [ProducesResponseType(typeof(IEnumerable<CashOutAttemptEntity>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRequestsAsync([FromQuery] string clientId)
        {
            if (!CommonValidator.ValidateClientId(clientId))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(clientId)));
            }

            return Ok(await _cashOutAttemptRepo.GetRequestsAsync(clientId));
        }

        [HttpGet]
        [SwaggerOperation("CashOutAttemptOperations_Get")]
        [ProducesResponseType(typeof(CashOutAttemptEntity), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
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

            var record = await _cashOutAttemptRepo.GetAsync(clientId, id);

            if (record == null)
            {
                return NoContent();
            }

            return Ok(record);
        }

        [HttpGet("GetRelatedRequests")]
        [SwaggerOperation("CashOutAttemptOperations_GetRelatedRequests")]
        [ProducesResponseType(typeof(IEnumerable<CashOutAttemptEntity>), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRelatedRequestsAsync([FromQuery] string requestId)
        {
            if (!CommonValidator.ValidateRequestId(requestId))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(requestId)));
            }

            return Ok(await _cashOutAttemptRepo.GetRelatedRequestsAsync(requestId));
        }
    }
}
