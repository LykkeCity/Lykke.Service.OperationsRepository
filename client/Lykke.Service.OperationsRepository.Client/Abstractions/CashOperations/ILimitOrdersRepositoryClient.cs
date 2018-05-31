using System.Collections.Generic;
using System.Threading.Tasks;
using Lykke.Service.OperationsRepository.AutorestClient.Models;

namespace Lykke.Service.OperationsRepository.Client.Abstractions.CashOperations
{
    public interface ILimitOrdersRepositoryClient
    {
        Task<ILimitOrder> GetByIdAsync(string clientId, string orderId);
        Task<ILimitOrder> CancelByIdAsync(string clientId, string orderId);
        Task<ILimitOrder> FinalizeAsync(string clientId, string orderId, LimitOrderFinalizeRequest request);
        Task<IEnumerable<ILimitOrder>> GetByClientIdAsync(string clientId);
        Task<IEnumerable<ILimitOrder>> GetActiveByClientIdAsync(string clientId);
        Task<ILimitOrder> AddAsync(LimitOrderCreateRequest order);
        Task RemoveAsync(string clientId, string orderId);
    }
}