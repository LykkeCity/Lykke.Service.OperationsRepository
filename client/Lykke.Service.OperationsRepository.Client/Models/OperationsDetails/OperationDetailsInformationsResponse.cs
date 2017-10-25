using Lykke.Service.OperationsRepository.AutorestClient.Models;
using Lykke.Service.OperationsRepository.Client.Models.CashOperations;
using Microsoft.Rest;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lykke.Service.OperationsRepository.Client.Models.OperationsDetails
{
    public class OperationDetailsInformationsResponse : BaseCashOperationResponse<IEnumerable<OperationDetailsInformation>>
    {
        public IEnumerable<OperationDetailsInformation> OperationsDetailsInformation { get; set; }

        public static OperationDetailsInformationsResponse Prepare(HttpOperationResponse<object> apiResponse)
        {
            var error = apiResponse.Body as ErrorResponse;
            var result = apiResponse.Body as IEnumerable<OperationDetailsInformation>;

            if (error != null)
            {
                return new OperationDetailsInformationsResponse
                {
                    Error = new ErrorModel
                    {
                        Message = error.ErrorMessage
                    }
                };
            }

            if (result != null)
            {
                return new OperationDetailsInformationsResponse
                {
                    OperationsDetailsInformation = result
                };
            }

            throw new ArgumentException("Unknown response object");
        }

        public override IEnumerable<OperationDetailsInformation> GetPayload()
        {
            return OperationsDetailsInformation;
        }
    }
}
