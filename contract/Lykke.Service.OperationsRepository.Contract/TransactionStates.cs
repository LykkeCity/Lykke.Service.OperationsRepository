namespace Lykke.Service.OperationsRepository.Contract
{
    public enum TransactionStates
    {
        InProcessOnchain,
        SettledOnchain,
        InProcessOffchain,
        SettledOffchain,
        SettledNoChain
    }
}