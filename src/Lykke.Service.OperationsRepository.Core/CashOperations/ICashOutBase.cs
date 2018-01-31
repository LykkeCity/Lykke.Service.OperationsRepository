using Lykke.Service.OperationsRepository.Contract;
using Lykke.Service.OperationsRepository.Contract.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lykke.Service.OperationsRepository.Core.CashOperations
{
    public class SwiftCashOutRequest : ICashOutRequest
    {
        public string Id { get; set; }
        public string ClientId { get; set; }
        public string AssetId { get; set; }
        public string PaymentSystem { get; set; }
        public string PaymentFields { get; set; }
        public string BlockchainHash { get; set; }
        public CashOutRequestStatus Status { get; set; }
        public TransactionStates State { get; set; }
        public CashOutRequestTradeSystem TradeSystem { get; set; }
        public double Amount { get; set; }
        public DateTime DateTime { get; set; }
        public bool IsHidden { get; set; }
        public string AccountId { get; set; }
        public CashOutVolumeSize VolumeSize { get; set; }
        public string PreviousId { get; set; }
        public decimal FeeSize { get; set; }
        public FeeType FeeType { get; set; }
    }

    public interface ICashOutBaseRepository
    {
        Task<IEnumerable<ICashOutRequest>> GetRequestsAsync(string clientId);
        Task<ICashOutRequest> GetAsync(string clientId, string id);
    }
}
