using Lykke.Service.OperationsRepository.Core.CashOperations;
using Lykke.Service.OperationsRepository.Core.Enumerators;
using System;

namespace Lykke.Service.OperationsRepository.Core.Entities
{
    public class LimitTradeEvent : ILimitTradeEvent
    {
        public string ClientId { get; set; }
        public string Id { get; set; }
        public string OrderId { get; set; }
        public DateTime CreatedDt { get; set; }
        public OrderType OrderType { get; set; }
        public double Volume { get; set; }
        public string AssetId { get; set; }
        public string AssetPair { get; set; }
        public double Price { get; set; }
        public OrderStatus Status { get; set; }
        public bool IsHidden { get; set; }
    }
}
