using Lykke.Service.OperationsRepository.AutorestClient.Models;
using Microsoft.Rest;

namespace Lykke.Service.OperationsRepository.Client.Models.CashOperations
{
    public class CashOperationResponse: BaseCashOperationResponse<CashInOutOperation>
    {
        public CashInOutOperation Operation { get; set; }

        public static CashOperationResponse NullResponse => new CashOperationResponse { Operation = null};

        public static CashOperationResponse Prepare(HttpOperationResponse<object> apiResponse)
        {
            var error = apiResponse.Body as ErrorResponse;
            var result = apiResponse.Body as CashInOutOperation;

            if (error != null)
            {
                return new CashOperationResponse
                {
                    Error = new ErrorModel
                    {
                        Message = error.ErrorMessage
                    }
                };
            }

            if (result != null)
            {
                return new CashOperationResponse
                {
                    Operation = result
                };
            }

            return NullResponse;
        }

        public override CashInOutOperation GetPayload()
        {
            return Operation;
        }
    }
}
