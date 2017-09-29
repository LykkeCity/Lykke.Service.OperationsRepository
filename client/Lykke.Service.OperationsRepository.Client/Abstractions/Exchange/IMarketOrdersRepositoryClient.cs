using Lykke.Service.OperationsRepository.AutorestClient.Models;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Lykke.Service.OperationsRepository.Client.Abstractions.Exchange
{
    public interface IMarketOrdersRepositoryClient
    {
        Task CreateAsync(MarketOrder marketOrder);

        Task<MarketOrder> GetAsync(string orderId);
        Task<MarketOrder> GetAsync(string clientId, string orderId);

        Task<IEnumerable<MarketOrder>> GetOrdersAsync(string clientId);
        Task<IEnumerable<MarketOrder>> GetOrdersAsync(string[] orderIds);
    }
}
