using Lykke.Service.OperationsRepository.AutorestClient.Models;
using Lykke.Service.OperationsRepository.Client.Models.CashOperations;
using Microsoft.Rest;

namespace Lykke.Service.OperationsRepository.Client.Models.OperationsDetails
{
    public class OperationDetailsInformationResponse : BaseCashOperationResponse<OperationDetailsInformation>
    {
        public OperationDetailsInformation Operation { get; set; }

        public static OperationDetailsInformationResponse NullResponse => new OperationDetailsInformationResponse { Operation = null };

        public static OperationDetailsInformationResponse Prepare(HttpOperationResponse<object> apiResponse)
        {
            var error = apiResponse.Body as ErrorResponse;
            var result = apiResponse.Body as OperationDetailsInformation;

            if (error != null)
            {
                return new OperationDetailsInformationResponse
                {
                    Error = new ErrorModel
                    {
                        Message = error.ErrorMessage
                    }
                };
            }

            if (result != null)
            {
                return new OperationDetailsInformationResponse
                {
                    Operation = result
                };
            }

            return NullResponse;
        }

        public override OperationDetailsInformation GetPayload()
        {
            return Operation;
        }
    }
}
