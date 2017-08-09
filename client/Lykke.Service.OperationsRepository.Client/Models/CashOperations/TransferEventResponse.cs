using System;
using System.Collections.Generic;
using System.Text;
using Lykke.Service.OperationsRepository.AutorestClient.Models;
using Microsoft.Rest;

namespace Lykke.Service.OperationsRepository.Client.Models.CashOperations
{
    public class TransferEventResponse: BaseCashOperationResponse<TransferEvent>
    {
        public TransferEvent Operation { get; set; }

        public static TransferEventResponse NullResponse => new TransferEventResponse {Operation = null};

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

            return NullResponse;
        }

        public override TransferEvent GetPayload()
        {
            return Operation;
        }
    }
}
