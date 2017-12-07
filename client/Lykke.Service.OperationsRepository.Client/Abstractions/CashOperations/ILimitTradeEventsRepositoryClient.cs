using System.Collections.Generic;
using System.Threading.Tasks;
using Lykke.Service.OperationsRepository.AutorestClient.Models;

namespace Lykke.Service.OperationsRepository.Client.Abstractions.CashOperations
{
    public interface ILimitTradeEventsRepositoryClient
    {
        Task<LimitTradeEvent> CreateAsync(OrderType type, double volume, double price, OrderStatus status, System.DateTime dateTime, string orderId, string clientId, string assetId, string assetPair);
        Task<IEnumerable<LimitTradeEvent>> GetAsync(string clientId);
    }
}