using Lykke.Service.OperationsRepository.AutorestClient.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lykke.Service.OperationsRepository.Client.Abstractions.CashOperations
{
    public interface ILimitTradeEventsRepositoryClient
    {
        Task<IEnumerable<LimitTradeEvent>> GetAsync(string clinetId);
        Task<IEnumerable<LimitTradeEvent>> GetAsync(string clientId, string orderId);
    }
}
