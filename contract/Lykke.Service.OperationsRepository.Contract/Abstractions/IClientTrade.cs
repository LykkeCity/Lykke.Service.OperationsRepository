using System;

namespace Lykke.Service.OperationsRepository.Contract.Abstractions
{
    public interface IClientTrade : IBaseCashBlockchainOperation
    {
        string AssetPairId { get; set; }
        string LimitOrderId { get; }
        string MarketOrderId { get; }
        double Price { get; }
        DateTime? DetectionTime { get; set; }
        int Confirmations { get; set; }
        string OppositeLimitOrderId { get; set; }
        bool IsLimitOrderResult { get; set; }
    }
}