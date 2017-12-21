using System;
using Lykke.Service.OperationsRepository.Contract.Abstractions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Lykke.Service.OperationsRepository.Contract.Cash
{
    public class CashOutRequestDto : ICashOutRequest
    {
        public string PaymentSystem { get; set; }
        public string PaymentFields { get; set; }
        public string BlockchainHash { get; set; }
        public CashOutRequestStatus Status { get; set; }
        public TransactionStates State { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public CashOutRequestTradeSystem TradeSystem { get; set; }
        public string AccountId { get; set; }
        public CashOutVolumeSize VolumeSize { get; set; }
        public string PreviousId { get; set; }
        public string Id { get; set; }
        public string AssetId { get; set; }
        public string ClientId { get; set; }
        public double Amount { get; set; }
        public DateTime DateTime { get; set; }
        public bool IsHidden { get; set; }
    }
}