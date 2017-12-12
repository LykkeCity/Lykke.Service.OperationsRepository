using System;
using Lykke.Service.OperationsRepository.AutorestClient.Models;
using Microsoft.Rest;

namespace Lykke.Service.OperationsRepository.Client.Models.CashOperations
{
    public class CashOperationIdResponse: BaseCashOperationResponse<string>
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
