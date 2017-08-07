using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Lykke.Service.OperationsRepository.AzureRepositories.CashOperations;
using Lykke.Service.OperationsRepository.Core.CashOperations;
using Microsoft.AspNetCore.Mvc;

namespace Lykke.Service.OperationsRepository.Models.CashOutAttempt
{
    public class InsertRequestModel
    {
        [Required]
        public CashOutAttemptEntity Request;
        [Required]
        public PaymentSystem PaymentSystem;
        [Required]
        public object PaymentFields;
        [Required]
        public CashOutRequestTradeSystem TradeSystem;
    }
}
