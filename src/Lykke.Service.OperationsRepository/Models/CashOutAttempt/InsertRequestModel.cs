using System.ComponentModel.DataAnnotations;
using Lykke.Service.OperationsRepository.AzureRepositories.CashOperations;
using Lykke.Service.OperationsRepository.Contract;

namespace Lykke.Service.OperationsRepository.Models.CashOutAttempt
{
    public class InsertRequestModel
    {
        [Required]
        public CashOutAttemptEntity Request { get; set; }
        [Required]
        public PaymentSystemModel PaymentSystem { get; set; }
        [Required]
        public object PaymentFields { get; set; }
        [Required]
        public CashOutRequestTradeSystem TradeSystem { get; set; }
    }
}
