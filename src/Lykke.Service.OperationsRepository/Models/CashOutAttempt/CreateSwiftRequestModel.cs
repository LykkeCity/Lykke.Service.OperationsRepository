using System.ComponentModel.DataAnnotations;
using Lykke.Service.OperationsRepository.Core.CashOperations;
using Lykke.Service.OperationsRepository.Core.Domain;

namespace Lykke.Service.OperationsRepository.Models.CashOutAttempt
{
    public class CreateSwiftRequestModel
    {
        [Required]
        public string ClientId { get; set; }
        [Required]
        public string AssetId { get; set; }
        [Required]
        public double Amount { get; set; }
        [Required]
        public Swift  SwiftCredentials { get; set; }        
    }
}