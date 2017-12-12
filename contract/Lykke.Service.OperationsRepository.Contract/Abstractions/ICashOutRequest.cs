namespace Lykke.Service.OperationsRepository.Contract.Abstractions
{
    public interface ICashOutRequest : IBaseCashOperation
    {
        string PaymentSystem { get; }
        string PaymentFields { get; }
        string BlockchainHash { get; }
        CashOutRequestStatus Status { get; }
        TransactionStates State { get; }
        CashOutRequestTradeSystem TradeSystem { get; }
        string AccountId { get; }
        CashOutVolumeSize VolumeSize { get; }
        string PreviousId { get; }
    }
}