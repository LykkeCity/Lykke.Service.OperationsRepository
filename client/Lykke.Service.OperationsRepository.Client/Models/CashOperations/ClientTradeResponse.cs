using System;
using Lykke.Service.OperationsRepository.AutorestClient.Models;
using Microsoft.Rest;

namespace Lykke.Service.OperationsRepository.Client.Models.CashOperations
{
    public class ClientTradeResponse: BaseCashOperationResponse
    {
        public ClientTrade Operation { get; set; }

        public static ClientTradeResponse Prepare(HttpOperationResponse<object> apiResponse)
        {
            var error = apiResponse.Body as ErrorResponse;
            var result = apiResponse.Body as ClientTrade;

            if (error != null)
            {
                return new ClientTradeResponse
                {
                    Error = new ErrorModel
                    {
                        Message = error.ErrorMessage
                    }
                };
            }

            if (result != null)
            {
                return new ClientTradeResponse
                {
                    Operation = result
                };
            }

            throw new ArgumentException("Unknown response object");
        }
    }
}
