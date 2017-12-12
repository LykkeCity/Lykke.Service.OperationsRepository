using System;
using System.Collections.Generic;
using Lykke.Service.OperationsRepository.AutorestClient.Models;
using Microsoft.Rest;

namespace Lykke.Service.OperationsRepository.Client.Models.CashOperations
{
    public class ClientTradesResponse : BaseCashOperationResponse<IEnumerable<ClientTrade>>
    {
        public IEnumerable<ClientTrade> Operations { get; set; }

        public static ClientTradesResponse Prepare(HttpOperationResponse<object> apiResponse)
        {
            var error = apiResponse.Body as ErrorResponse;
            var result = apiResponse.Body as IEnumerable<ClientTrade>;

            if (error != null)
            {
                return new ClientTradesResponse
                {
                    Error = new ErrorModel
                    {
                        Message = error.ErrorMessage
                    }
                };
            }

            if (result != null)
            {
                return new ClientTradesResponse
                {
                    Operations = result
                };
            }

            throw new ArgumentException("Unknown response object");
        }

        public override IEnumerable<ClientTrade> GetPayload()
        {
            return Operations;
        }
    }
}
