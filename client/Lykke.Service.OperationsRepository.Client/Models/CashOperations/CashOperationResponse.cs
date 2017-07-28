﻿using System;
using System.Collections.Generic;
using System.Text;
using Lykke.Service.OperationsRepository.AutorestClient.Models;
using Microsoft.Rest;

namespace Lykke.Service.OperationsRepository.Client.Models.CashOperations
{
    public class CashOperationResponse: BaseCashOperationResponse
    {
        public ICashInOutOperation Operation { get; set; }

        public static CashOperationResponse Prepare(HttpOperationResponse<object> apiResponse)
        {
            var error = apiResponse.Body as ErrorResponse;
            var result = apiResponse.Body as ICashInOutOperation;

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

            throw new ArgumentException("Unknown response object");
        }
    }
}
