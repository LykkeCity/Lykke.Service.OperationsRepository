using System;
using Lykke.Service.OperationsRepository.Contract.Abstractions;

namespace Lykke.Service.OperationsRepository.Contract.Cash
{
    public class ClientTradeDto : IClientTrade
    {
        public string LimitOrderId { get; set; }
        public string MarketOrderId { get; set; }
        public double Price { get; set; }
        public DateTime? DetectionTime { get; set; }
        public int Confirmations { get; set; }
        public string OppositeLimitOrderId { get; set; }
        public bool IsLimitOrderResult { get; set; }
        public string BlockChainHash { get; set; }
        public string Multisig { get; set; }
        public string TransactionId { get; set; }
        public string AddressFrom { get; set; }
        public string AddressTo { get; set; }
        public bool? IsSettled { get; set; }
        public TransactionStates State { get; set; }
        public string Id { get; set; }
        public string AssetId { get; set; }
        public string ClientId { get; set; }
        public double Amount { get; set; }
        public DateTime DateTime { get; set; }
        public bool IsHidden { get; set; }
        public decimal FeeSize { get; set; }
        public FeeType FeeType { get; set; }
    }
}