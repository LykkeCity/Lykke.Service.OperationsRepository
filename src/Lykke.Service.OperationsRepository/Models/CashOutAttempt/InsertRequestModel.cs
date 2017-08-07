using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Lykke.Service.OperationsRepository.AzureRepositories.CashOperations;
using Lykke.Service.OperationsRepository.Core.CashOperations;

namespace Lykke.Service.OperationsRepository.Models.CashOutAttempt
{
    public class InsertRequestModel
    {
        [Required]
        public CashOutAttemptEntity Request { get; set; }
        [Required]
        public PaymentSystem PaymentSystem { get; set; }
        [Required]
        public object PaymentFields { get; set; }
        [Required]
        public CashOutRequestTradeSystem TradeSystem { get; set; }
    }
}
