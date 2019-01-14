namespace Lykke.Service.OperationsRepository.Contract
{
    public enum CashOutRequestStatus
    {
        Pending = 0,
        Confirmed = 1,
        Declined = 2,
        Processed = 3,
        ClientConfirmation = 4,
        CanceledByClient = 5,
        CanceledByTimeout = 6,
        RequestForDocs = 7,
    }

    public enum CashOutVolumeSize
    {
        Unknown,
        High,
        Low
    }

    public enum CashOutRequestTradeSystem
    {
        Spot,
        Margin
    }
}