namespace Lykke.Service.OperationsRepository.Contract
{
    public enum CashOutRequestStatus
    {
        ClientConfirmation = 4,
        Pending = 0,
        RequestForDocs = 7,
        Confirmed = 1,
        Declined = 2,
        CanceledByClient = 5,
        CanceledByTimeout = 6,
        Processed = 3,
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