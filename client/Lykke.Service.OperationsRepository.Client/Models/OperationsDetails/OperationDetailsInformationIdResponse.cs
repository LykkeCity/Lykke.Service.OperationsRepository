using Lykke.Service.OperationsRepository.AutorestClient.Models;
using Lykke.Service.OperationsRepository.Client.Models.CashOperations;
using Microsoft.Rest;
using System;

namespace Lykke.Service.OperationsRepository.Client.Models.OperationsDetails
{
    public class OperationDetailsInformationIdResponse : BaseCashOperationResponse<string>
    {
        public string Id { get; set; }

        public static CashOperationIdResponse Prepare(HttpOperationResponse<object> apiResponse)
        {
            var error = apiResponse.Body as ErrorResponse;
            var result = apiResponse.Body as IdResponseModel;

            if (error != null)
            {
                return new CashOperationIdResponse
                {
                    Error = new ErrorModel
                    {
                        Message = error.ErrorMessage
                    }
                };
            }

            if (result != null)
            {
                return new CashOperationIdResponse
                {
                    Id = result.Id
                };
            }

            throw new ArgumentException("Unknown response object");
        }

        public override string GetPayload()
        {
            return Id;
        }
    }
}
