using System;
using Lykke.Service.OperationsRepository.AutorestClient.Models;
using Microsoft.Rest;

namespace Lykke.Service.OperationsRepository.Client.Models.CashOperations
{
    public class CashOutAttemptResponse : BaseCashOperationResponse
    {
        public CashOutAttemptEntity Operation { get; set; }

        public static CashOutAttemptResponse Prepare(HttpOperationResponse<object> apiResponse)
        {
            var error = apiResponse.Body as ErrorResponse;
            var result = apiResponse.Body as CashOutAttemptEntity;

            if (error != null)
            {
                return new CashOutAttemptResponse
                {
                    Error = new ErrorModel
                    {
                        Message = error.ErrorMessage
                    }
                };
            }

            if (result != null)
            {
                return new CashOutAttemptResponse
                {
                    Operation = result
                };
            }

            throw new ArgumentException("Unknown response object");
        }
    }
}
