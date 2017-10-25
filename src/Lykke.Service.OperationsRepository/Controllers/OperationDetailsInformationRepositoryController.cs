using Common.Log;
using Lykke.Service.OperationsHistory.HistoryWriter.Abstractions;
using Lykke.Service.OperationsRepository.Core.Entities;
using Lykke.Service.OperationsRepository.Core.OperationsDetails;
using Lykke.Service.OperationsRepository.Models;
using Lykke.Service.OperationsRepository.Validation;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.SwaggerGen.Annotations;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Lykke.Service.OperationsRepository.Controllers
{
    [Route("api/[controller]")]
    public class OperationDetailsInformationRepositoryController : Controller
    {
        private readonly IOperationDetailsInformationRepository _operationDetailsInformationRepo;
        private readonly ILog _log;

        public OperationDetailsInformationRepositoryController(IOperationDetailsInformationRepository operationDetailsInformationRepo,
                                                    ILog log)
        {
            _operationDetailsInformationRepo = operationDetailsInformationRepo;
            _log = log;
        }

        [HttpPost]
        [SwaggerOperation("OperationDetailsInformation_Create")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateAsync([FromBody] OperationDetailsInformation operationDetailsInfo)
        {
            if (!CommonValidator.ValidateClientId(operationDetailsInfo.ClientId))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(operationDetailsInfo.ClientId)));
            }

            if (!CommonValidator.ValidateRowKeyId(operationDetailsInfo.Id))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(operationDetailsInfo.Id)));
            }

            if (!CommonValidator.TransactionId(operationDetailsInfo.TransactionId))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(operationDetailsInfo.TransactionId)));
            }

            await _operationDetailsInformationRepo.CreateAsync(operationDetailsInfo);

            return Ok();
        }

        [HttpPost("register")]
        [SwaggerOperation("OperationDetailsInformation_Register")]
        [ProducesResponseType(typeof(IdResponseModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Register([FromBody] OperationDetailsInformation operationDetailsInfo)
        {
            if (!CommonValidator.ValidateClientId(operationDetailsInfo.ClientId))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(operationDetailsInfo.ClientId)));
            }

            if (!CommonValidator.ValidateRowKeyId(operationDetailsInfo.Id))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(operationDetailsInfo.Id)));
            }

            if (!CommonValidator.TransactionId(operationDetailsInfo.TransactionId))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(operationDetailsInfo.TransactionId)));
            }

            var id = await _operationDetailsInformationRepo.RegisterAsync(operationDetailsInfo);

            return Ok(new IdResponseModel { Id = id });
        }

        [HttpGet]
        [SwaggerOperation("OperationDetailsInformation_Get")]
        [ProducesResponseType(typeof(IEnumerable<OperationDetailsInformation>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAsync([FromQuery] string clientId)
        {
            if (!CommonValidator.ValidateClientId(clientId))
            {
                return BadRequest(ErrorResponse.InvalidParameter(nameof(clientId)));
            }

            return Ok(await _operationDetailsInformationRepo.GetAsync(clientId));
        }

        [HttpGet("getByRecordId")]
        [SwaggerOperation("OperationDetailsInformation_GetByRecordId")]
        [ProducesResponseType(typeof(OperationDetailsInformation), (int)HttpStatusCode.OK)]
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

            var record = await _operationDetailsInformationRepo.GetAsync(clientId, recordId);

            if (record == null)
            {
                return NoContent();
            }

            return Ok(record);
        }
    }
}
