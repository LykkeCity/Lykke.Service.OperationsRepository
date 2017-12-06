using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lykke.Service.OperationsRepository.Core.Exchange
{
    public interface ILimitOrdersRepository
    {
        Task<ILimitOrder> GetOrderAsync(string orderId);
        Task<IEnumerable<ILimitOrder>> GetActiveOrdersAsync(string clientId);
        Task<IEnumerable<ILimitOrder>> GetOrdersAsync(IEnumerable<string> orderIds);
        Task<IEnumerable<ILimitOrder>> GetOrdersAsync(string clientId);
    }
}
