using System;
using Lykke.Service.OperationsRepository.AutorestClient.Models;
using Microsoft.Rest;

namespace Lykke.Service.OperationsRepository.Client.Models.CashOperations
{
    public class LimitOrderResponse : BaseCashOperationResponse<ILimitOrder>
    {
        public ILimitOrder Order { set; get; }
        
        public static LimitOrderResponse Prepare(HttpOperationResponse<object> apiResponse)
        {
            var error = apiResponse.Body as ErrorResponse;
            var result = apiResponse.Body as ILimitOrder;

            if (error != null)
            {
                return new LimitOrderResponse
                {
                    Error = new ErrorModel
                    {
                        Message = error.ErrorMessage
                    }
                };
            }

            if (result != null)
            {
                return new LimitOrderResponse
                {
                    Order = result
                };
            }

            throw new ArgumentException("Unknown response object");
        }
        
        public override ILimitOrder GetPayload()
        {
            return Order;
        }
    }
}