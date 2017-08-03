using System;
using System.Collections.Generic;
using System.Text;
using Lykke.Service.OperationsRepository.AutorestClient.Models;
using Microsoft.Rest;

namespace Lykke.Service.OperationsRepository.Client.Models.CashOperations
{
    public class TransferEventResponse: BaseCashOperationResponse
    {
        public TransferEvent Operation { get; set; }

        public static TransferEventResponse Prepare(HttpOperationResponse<object> apiResponse)
        {
            var error = apiResponse.Body as ErrorResponse;
            var result = apiResponse.Body as TransferEvent;

            if (error != null)
            {
                return new TransferEventResponse
                {
                    Error = new ErrorModel
                    {
                        Message = error.ErrorMessage
                    }
                };
            }

            if (result != null)
            {
                return new TransferEventResponse
                {
                    Operation = result
                };
            }

            throw new ArgumentException("Unknown response object");
        }
    }
}
