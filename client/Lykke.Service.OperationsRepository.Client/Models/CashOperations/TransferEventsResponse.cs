using System;
using System.Collections.Generic;
using Lykke.Service.OperationsRepository.AutorestClient.Models;
using Microsoft.Rest;

namespace Lykke.Service.OperationsRepository.Client.Models.CashOperations
{
    public class TransferEventsResponse : BaseCashOperationResponse
    {
        public IEnumerable<TransferEvent> Operations { get; set; }

        public static TransferEventsResponse Prepare(HttpOperationResponse<object> apiResponse)
        {
            var error = apiResponse.Body as ErrorResponse;
            var result = apiResponse.Body as IEnumerable<TransferEvent>;

            if (error != null)
            {
                return new TransferEventsResponse
                {
                    Error = new ErrorModel
                    {
                        Message = error.ErrorMessage
                    }
                };
            }

            if (result != null)
            {
                return new TransferEventsResponse
                {
                    Operations = result
                };
            }

            throw new ArgumentException("Unknown response object");
        }
    }
}
