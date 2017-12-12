namespace Lykke.Service.OperationsRepository.Contract.Abstractions
{
    public interface ITransferEvent : IBaseCashBlockchainOperation
    {
        string FromId { get; }
    }
}