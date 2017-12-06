using Lykke.Service.OperationsRepository.AutorestClient.Models;
using Lykke.Service.OperationsRepository.Client.Models.CashOperations;
using Microsoft.Rest;

namespace Lykke.Service.OperationsRepository.Client.Models.Exchange
{
    public class LimitOrderResponse : BaseCashOperationResponse<LimitOrder>
    {
        public LimitOrder Operation { get; set; }

        public static LimitOrderResponse NullResponse => new LimitOrderResponse { Operation = null };

        public static LimitOrderResponse Prepare(HttpOperationResponse<object> apiResponse)
        {
            var error = apiResponse.Body as ErrorResponse;
            var result = apiResponse.Body as LimitOrder;

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
                    Operation = result
                };
            }

            return NullResponse;
        }

        public override LimitOrder GetPayload()
        {
            return Operation;
        }
    }
}
