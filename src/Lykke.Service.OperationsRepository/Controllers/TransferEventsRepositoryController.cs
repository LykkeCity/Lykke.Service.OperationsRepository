using System;
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
    public class TransferEventsRepositoryController : Controller
    {
        private readonly ITransferEventsRepository _transferEventsRepo;

        public TransferEventsRepositoryController(ITransferEventsRepository transferEventsRepo)
        {
            _transferEventsRepo = transferEventsRepo;
        }

        [HttpPost("Register")]
        [SwaggerOperation("Register")]
        [ProducesResponseType(typeof(ITransferEvent), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> RegisterAsync([FromBody] ITransferEvent transferEvent)
        {
            if (!CashOperationsValidator.ValidateTransferEvent(transferEvent))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(transferEvent)));
            }

            return Ok(await _transferEventsRepo.RegisterAsync(transferEvent));
        }

        [HttpGet]
        [SwaggerOperation("Get")]
        [ProducesResponseType(typeof(IEnumerable<ITransferEvent>), (int) HttpStatusCode.OK)]
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
        [SwaggerOperation("GetByRecordId")]
        [ProducesResponseType(typeof(ITransferEvent), (int) HttpStatusCode.OK)]
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

            return Ok(await _transferEventsRepo.GetAsync(clientId, id));
        }

        [HttpPost("UpdateBlockchainHash")]
        [SwaggerOperation("UpdateBlockchainHash")]
        [ProducesResponseType(typeof(void), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateBlockChainHashAsync([FromBody] string clientId, [FromBody] string id,
            [FromBody] string blockChainHash)
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
        [SwaggerOperation("SetBtcTransaction")]
        [ProducesResponseType(typeof(void), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SetBtcTransactionAsync([FromBody] string clientId, [FromBody] string id,
            [FromBody] string btcTransaction)
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
        [SwaggerOperation("SetIsSettledIfExists")]
        [ProducesResponseType(typeof(void), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SetIsSettledIfExistsAsync([FromBody] string clientId, [FromBody] string id,
            [FromBody] bool offchain)
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
        [SwaggerOperation("GetByHash")]
        [ProducesResponseType(typeof(IEnumerable<ITransferEvent>), (int) HttpStatusCode.OK)]
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
        [SwaggerOperation("GetByMultisig")]
        [ProducesResponseType(typeof(IEnumerable<ITransferEvent>), (int) HttpStatusCode.OK)]
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
        [SwaggerOperation("GetByMultisigs")]
        [ProducesResponseType(typeof(IEnumerable<ITransferEvent>), (int) HttpStatusCode.OK)]
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
