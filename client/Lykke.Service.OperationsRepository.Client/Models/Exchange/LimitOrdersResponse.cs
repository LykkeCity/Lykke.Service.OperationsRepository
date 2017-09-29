using Lykke.Service.OperationsRepository.AutorestClient.Models;
using Lykke.Service.OperationsRepository.Client.Models.CashOperations;
using Microsoft.Rest;
using System;
using System.Collections.Generic;

namespace Lykke.Service.OperationsRepository.Client.Models.Exchange
{
    public class LimitOrdersResponse : BaseCashOperationResponse<IEnumerable<LimitOrder>>
    {
        public IEnumerable<LimitOrder> Operations { get; set; }

        public static LimitOrdersResponse Prepare(HttpOperationResponse<object> apiResponse)
        {
            var error = apiResponse.Body as ErrorResponse;
            var result = apiResponse.Body as IEnumerable<LimitOrder>;

            if (error != null)
            {
                return new LimitOrdersResponse
                {
                    Error = new ErrorModel
                    {
                        Message = error.ErrorMessage
                    }
                };
            }

            if (result != null)
            {
                return new LimitOrdersResponse
                {
                    Operations = result
                };
            }

            throw new ArgumentException("Unknown response object");
        }

        public override IEnumerable<LimitOrder> GetPayload()
        {
            return Operations;
        }
    }
}
