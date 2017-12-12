namespace Lykke.Service.OperationsRepository.Contract.Abstractions
{
    public interface ICashInOutOperation : IBaseCashBlockchainOperation
    {
        bool IsRefund { get; set; }
        CashOperationType Type { get; set; }
    }
}