using Lykke.Service.OperationsRepository.AutorestClient.Models;
using Microsoft.Rest;

namespace Lykke.Service.OperationsRepository.Client.Models.CashOperations
{
    public class LimitTradeEventResponse : BaseCashOperationResponse<LimitTradeEvent>
    {
        public LimitTradeEvent Operation { get; set; }

        public static LimitTradeEventResponse NullResponse => new LimitTradeEventResponse { Operation = null };

        public static LimitTradeEventResponse Prepare(HttpOperationResponse<object> apiResponse)
        {
            var error = apiResponse.Body as ErrorResponse;
            var result = apiResponse.Body as LimitTradeEvent;

            if (error != null)
            {
                return new LimitTradeEventResponse
                {
                    Error = new ErrorModel
                    {
                        Message = error.ErrorMessage
                    }
                };
            }

            if (result != null)
            {
                return new LimitTradeEventResponse
                {
                    Operation = result
                };
            }

            return NullResponse;
        }

        public override LimitTradeEvent GetPayload()
        {
            return Operation;
        }
    }
}