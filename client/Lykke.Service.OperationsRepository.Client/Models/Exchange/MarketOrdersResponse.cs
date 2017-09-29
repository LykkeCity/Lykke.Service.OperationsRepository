using Lykke.Service.OperationsRepository.AutorestClient.Models;
using Lykke.Service.OperationsRepository.Client.Models.CashOperations;
using Microsoft.Rest;
using System;
using System.Collections.Generic;


namespace Lykke.Service.OperationsRepository.Client.Models.Exchange
{
    public class MarketOrdersResponse : BaseCashOperationResponse<IEnumerable<MarketOrder>>
    {
        public IEnumerable<MarketOrder> Operations { get; set; }

        public static MarketOrdersResponse Prepare(HttpOperationResponse<object> apiResponse)
        {
            var error = apiResponse.Body as ErrorResponse;
            var result = apiResponse.Body as IEnumerable<MarketOrder>;

            if (error != null)
            {
                return new MarketOrdersResponse
                {
                    Error = new ErrorModel
                    {
                        Message = error.ErrorMessage
                    }
                };
            }

            if (result != null)
            {
                return new MarketOrdersResponse
                {
                    Operations = result
                };
            }

            throw new ArgumentException("Unknown response object");
        }

        public override IEnumerable<MarketOrder> GetPayload()
        {
            return Operations;
        }
    }
}
