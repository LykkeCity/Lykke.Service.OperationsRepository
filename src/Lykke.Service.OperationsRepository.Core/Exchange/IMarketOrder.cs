using System;

namespace Lykke.Service.OperationsRepository.Core.Exchange
{
    public interface IMarketOrder : IOrderBase
    {
        DateTime MatchedAt { get; }
    }
}
