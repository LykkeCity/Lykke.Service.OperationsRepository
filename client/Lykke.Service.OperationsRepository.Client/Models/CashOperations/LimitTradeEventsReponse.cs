using Lykke.Service.OperationsRepository.AutorestClient.Models;
using Microsoft.Rest;
using System;
using System.Collections.Generic;

namespace Lykke.Service.OperationsRepository.Client.Models.CashOperations
{
    public class LimitTradeEventsReponse : BaseCashOperationResponse<IEnumerable<LimitTradeEvent>>
    {
        public IEnumerable<LimitTradeEvent> Operations { get; set; }

        public static LimitTradeEventsReponse Prepare(HttpOperationResponse<object> apiResponse)
        {
            var error = apiResponse.Body as ErrorResponse;
            var result = apiResponse.Body as IEnumerable<LimitTradeEvent>;

            if (error != null)
            {
                return new LimitTradeEventsReponse
                {
                    Error = new ErrorModel
                    {
                        Message = error.ErrorMessage
                    }
                };
            }

            if (result != null)
            {
                return new LimitTradeEventsReponse
                {
                    Operations = result
                };
            }

            throw new ArgumentException("Unknown response object");
        }

        public override IEnumerable<LimitTradeEvent> GetPayload()
        {
            return Operations;
        }
    }
}