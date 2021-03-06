﻿using System;
using System.Collections.Generic;
using Lykke.Service.OperationsRepository.AutorestClient.Models;
using Microsoft.Rest;

namespace Lykke.Service.OperationsRepository.Client.Models.CashOperations
{
    public class CashOperationsResponse: BaseCashOperationResponse<IEnumerable<CashInOutOperation>>
    {
        public IEnumerable<CashInOutOperation> Operations { get; set; }

        public static CashOperationsResponse Prepare(HttpOperationResponse<object> apiResponse)
        {
            var error = apiResponse.Body as ErrorResponse;
            var result = apiResponse.Body as IEnumerable<CashInOutOperation>;

            if (error != null)
            {
                return new CashOperationsResponse
                {
                    Error = new ErrorModel
                    {
                        Message = error.ErrorMessage
                    }
                };
            }

            if (result != null)
            {
                return new CashOperationsResponse
                {
                    Operations = result
                };
            }

            throw new ArgumentException("Unknown response object");
        }

        public override IEnumerable<CashInOutOperation> GetPayload()
        {
            return Operations;
        }
    }
}
