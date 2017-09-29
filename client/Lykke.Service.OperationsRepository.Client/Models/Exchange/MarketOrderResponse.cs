using Lykke.Service.OperationsRepository.AutorestClient.Models;
using Lykke.Service.OperationsRepository.Client.Models.CashOperations;
using Microsoft.Rest;

namespace Lykke.Service.OperationsRepository.Client.Models.Exchange
{
    class MarketOrderResponse : BaseCashOperationResponse<MarketOrder>
    {
        public MarketOrder Operation { get; set; }

        public static MarketOrderResponse NullResponse => new MarketOrderResponse { Operation = null };

        public static MarketOrderResponse Prepare(HttpOperationResponse<object> apiResponse)
        {
            var error = apiResponse.Body as ErrorResponse;
            var result = apiResponse.Body as MarketOrder;

            if (error != null)
            {
                return new MarketOrderResponse
                {
                    Error = new ErrorModel
                    {
                        Message = error.ErrorMessage
                    }
                };
            }

            if (result != null)
            {
                return new MarketOrderResponse
                {
                    Operation = result
                };
            }

            return NullResponse;
        }

        public override MarketOrder GetPayload()
        {
            return Operation;
        }
    }
}
