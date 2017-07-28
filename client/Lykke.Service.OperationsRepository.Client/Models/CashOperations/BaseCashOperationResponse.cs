using System;
using System.Collections.Generic;
using System.Text;
using Lykke.Service.OperationsRepository.AutorestClient.Models;

namespace Lykke.Service.OperationsRepository.Client.Models.CashOperations
{
    public abstract class BaseCashOperationResponse
    {
        public ErrorModel Error { get; set; }
    }
}
