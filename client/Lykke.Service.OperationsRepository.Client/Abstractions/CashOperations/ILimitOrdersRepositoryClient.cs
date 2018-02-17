using System.Collections.Generic;
using System.Threading.Tasks;
using Lykke.Service.OperationsRepository.AutorestClient.Models;

namespace Lykke.Service.OperationsRepository.Client.Abstractions.CashOperations
{
    public interface ILimitOrdersRepositoryClient
    {
        Task<ILimitOrder> GetByIdAsync(string orderId);
        Task<ILimitOrder> CancelByIdAsync(string orderId);
        Task<IEnumerable<ILimitOrder>> GetByClientIdAsync(string clientId);
        Task<IEnumerable<ILimitOrder>> GetActiveByClientIdAsync(string clientId);
        Task<ILimitOrder> AddAsync(LimitOrderCreateRequest order);
        Task RemoveAsync(string clientId, string orderId);
    }
}