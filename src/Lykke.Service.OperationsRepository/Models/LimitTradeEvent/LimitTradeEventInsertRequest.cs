using Lykke.Service.OperationsRepository.Contract;
using System;
using System.ComponentModel.DataAnnotations;

namespace Lykke.Service.OperationsRepository.Models.LimitTradeEvent
{
    public class LimitTradeEventInsertRequest
    {
        [Required]
        public string OrderId { get; set; }
        [Required]
        public string ClientId { get; set; }
        [Required]
        public OrderType Type { get; set; }
        [Required]
        public double Volume { get; set; }
        [Required]
        public string AssetId { get; set; }
        [Required]
        public string AssetPair { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public OrderStatus Status { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
    }
}