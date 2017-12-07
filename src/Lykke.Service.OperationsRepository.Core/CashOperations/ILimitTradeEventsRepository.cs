using Lykke.Service.OperationsRepository.Core.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lykke.Service.OperationsRepository.Core.CashOperations
{
    public interface ILimitTradeEvent
    {
        string ClientId { get; }
        string Id { get; }
        string OrderId { get; }
        DateTime CreatedDt { get; }
        OrderType OrderType { get; }
        double Volume { get; }
        string AssetId { get; }
        string AssetPair { get; }
        double Price { get; }
        OrderStatus Status { get; }
        bool IsHidden { get; }
    }

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

    public interface ILimitTradeEventsRepository
    {
        Task<ILimitTradeEvent> CreateEvent(string orderId, string clientId, OrderType type, double volume, string assetId,
            string assetPair, double price, OrderStatus status, DateTime dateTime);
        Task<IEnumerable<ILimitTradeEvent>> GetEventsAsync(string clientId);
    }
}
