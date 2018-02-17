using System;
using System.Collections.Generic;
using Lykke.Service.OperationsRepository.AutorestClient.Models;
using Microsoft.Rest;

namespace Lykke.Service.OperationsRepository.Client.Models.CashOperations
{
    public class LimitOrdersResponse : BaseCashOperationResponse<IEnumerable<ILimitOrder>>
    {
        public IEnumerable<ILimitOrder> Orders { set; get; }
        
        public static LimitOrdersResponse Prepare(HttpOperationResponse<object> apiResponse)
        {
            var error = apiResponse.Body as ErrorResponse;
            var result = apiResponse.Body as IEnumerable<ILimitOrder>;

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
                    Orders = result
                };
            }

            throw new ArgumentException("Unknown response object");
        }
        
        public override IEnumerable<ILimitOrder> GetPayload()
        {
            return Orders;
        }
    }
}