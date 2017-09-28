using Lykke.Service.OperationsRepository.Core.Enumerators;
using System;

namespace Lykke.Service.OperationsRepository.Core.CashOperations
{
    public interface ILimitTradeEvent
    {
        string ClientId { get; }
        string Id { get; set; }
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
}
