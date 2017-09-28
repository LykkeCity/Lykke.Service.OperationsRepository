namespace Lykke.Service.OperationsRepository.Core.Exchange
{
    public interface ILimitOrder : IOrderBase
    {
        double RemainingVolume { get; set; }
        string MatchingId { get; set; }
    }
}
