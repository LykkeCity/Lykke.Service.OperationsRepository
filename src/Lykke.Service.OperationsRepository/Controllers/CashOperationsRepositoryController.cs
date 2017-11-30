using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Common.Log;
using Lykke.Service.OperationsRepository.Core;
using Lykke.Service.OperationsRepository.Core.CashOperations;
using Lykke.Service.OperationsRepository.Models;
using Lykke.Service.OperationsRepository.Validation;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Lykke.Service.OperationsRepository.Controllers
{
    [Route("api/[controller]")]
    public class CashOperationsRepositoryController : Controller
    {
        private readonly ICashOperationsRepository _cashOperationsRepo;
        private readonly IOperationsHistoryPublisher _historyPublisher;
        private readonly ILog _log;

        public CashOperationsRepositoryController(ICashOperationsRepository cashOperationsRepo, IOperationsHistoryPublisher historyPublisher, ILog log)
        {
            _cashOperationsRepo = cashOperationsRepo;
            _historyPublisher = historyPublisher;
            _log = log;
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

            await _historyPublisher.PublishAsync(this.MapFrom(operation));

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

            var updated = await _cashOperationsRepo.UpdateBlockchainHashAsync(clientId, id, hash);

            if (updated != null)
            {
                try
                {
                    await _historyPublisher.PublishAsync(this.MapFrom(updated));
                }
                catch (Exception e)
                {
                    await _log.WriteErrorAsync(GetType().Name, "UpdateBlockchainHashAsync", "", e, DateTime.Now);
                }
            }

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

            var updated = await _cashOperationsRepo.SetIsSettledAsync(clientId, id, offchain);

            if (updated != null)
            {
                try
                {
                    await _historyPublisher.PublishAsync(this.MapFrom(updated));
                }
                catch (Exception e)
                {
                    await _log.WriteErrorAsync(GetType().Name, "SetIsSettledAsync", "", e, DateTime.Now);
                }
            }

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
