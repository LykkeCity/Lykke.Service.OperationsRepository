using System;
using System.Collections.Generic;
using System.Text;
using Lykke.Service.OperationsRepository.AutorestClient.Models;
using Microsoft.Rest;

namespace Lykke.Service.OperationsRepository.Client.Models.CashOperations
{
    public class CashOutAttemptIdResponse: BaseCashOperationResponse
    {
        public string Id { get; set; }

        public static CashOutAttemptIdResponse Prepare(HttpOperationResponse<object> apiResponse)
        {
            var error = apiResponse.Body as ErrorResponse;
            var result = apiResponse.Body as string;

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
                    Id = result
                };
            }

            throw new ArgumentException("Unknown response object");
        }
    }
}
