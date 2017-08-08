using System;
using System.Collections.Generic;
using System.Text;
using Lykke.Service.OperationsRepository.AutorestClient.Models;
using Microsoft.Rest;

namespace Lykke.Service.OperationsRepository.Client.Models.CashOperations
{
    public class CashOutAttemptIdResponse: BaseCashOperationResponse<string>
    {
        public string Id { get; set; }

        public static CashOutAttemptIdResponse Prepare(HttpOperationResponse<object> apiResponse)
        {
            var error = apiResponse.Body as ErrorResponse;
            var result = apiResponse.Body as IdResponseModel;

            if (error != null)
            {
                return new CashOutAttemptIdResponse
                {
                    Error = new ErrorModel
                    {
                        Message = error.ErrorMessage
                    }
                };
            }

            if (result != null)
            {
                return new CashOutAttemptIdResponse
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
