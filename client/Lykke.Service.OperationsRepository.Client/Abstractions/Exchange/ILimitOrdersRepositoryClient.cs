using Lykke.Service.OperationsRepository.AutorestClient.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lykke.Service.OperationsRepository.Client.Abstractions.Exchange
{
    public interface ILimitOrdersRepositoryClient
    {
        Task<LimitOrder> GetOrderAsync(string orderId);
        Task<IEnumerable<LimitOrder>> GetActiveOrdersAsync(string clientId);
        Task<IEnumerable<LimitOrder>> GetOrdersAsync(string clientId);
        Task<IEnumerable<LimitOrder>> GetOrdersAsync(string[] orderIds);
    }
}