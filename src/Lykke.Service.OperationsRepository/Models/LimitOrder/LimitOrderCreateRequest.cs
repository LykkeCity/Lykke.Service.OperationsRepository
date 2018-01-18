using System;
using System.ComponentModel.DataAnnotations;
using Lykke.Service.OperationsRepository.Core.CashOperations;

namespace Lykke.Service.OperationsRepository.Models.LimitOrder
{
    public class LimitOrderCreateRequest : ILimitOrder
    {
        [Required]
        public double RemainingVolume { get; set; }
        [Required]
        public string MatchingId { get; set; }
        [Required]
        public string Id { get; set; }
        [Required]
        public string ClientId { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public double Volume { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public string AssetPairId { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public bool Straight { get; set; }
    }
}