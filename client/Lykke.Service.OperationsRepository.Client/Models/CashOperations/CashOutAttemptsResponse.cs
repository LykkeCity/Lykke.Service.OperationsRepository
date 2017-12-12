using System;
using System.Collections.Generic;
using Lykke.Service.OperationsRepository.AutorestClient.Models;
using Microsoft.Rest;

namespace Lykke.Service.OperationsRepository.Client.Models.CashOperations
{
    public class CashOutAttemptsResponse : BaseCashOperationResponse<IEnumerable<CashOutAttemptEntity>>
    {
        public IEnumerable<CashOutAttemptEntity> Operations { get; set; }

        public static CashOutAttemptsResponse Prepare(HttpOperationResponse<object> apiResponse)
        {
            var error = apiResponse.Body as ErrorResponse;
            var result = apiResponse.Body as IEnumerable<CashOutAttemptEntity>;

            if (error != null)
            {
                return new CashOutAttemptsResponse
                {
                    Error = new ErrorModel
                    {
                        Message = error.ErrorMessage
                    }
                };
            }

            if (result != null)
            {
                return new CashOutAttemptsResponse
                {
                    Operations = result
                };
            }

            throw new ArgumentException("Unknown response object");
        }

        public override IEnumerable<CashOutAttemptEntity> GetPayload()
        {
            return Operations;
        }
    }
}
