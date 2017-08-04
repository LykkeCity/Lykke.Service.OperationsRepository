﻿using System;
using System.Collections.Generic;
using System.Text;
using Lykke.Service.OperationsRepository.AutorestClient.Models;

namespace Lykke.Service.OperationsRepository.Client.Models.CashOperations
{
    public abstract class BaseCashOperationResponse<T>
    {
        public ErrorModel Error { get; set; }

        public abstract T GetPayload();

        public BaseCashOperationResponse<T> Validate()
        {
            if (Error != null)
            {
                throw new Exception(Error.Message);
            }

            return this;
        }
    }
}
