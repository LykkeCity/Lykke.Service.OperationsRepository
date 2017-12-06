using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lykke.Service.OperationsRepository.Core.CashOperations
{
    public interface ILimitTradeEventsRepository
    {
        Task<IEnumerable<ILimitTradeEvent>> GetEventsAsync(string clientId);
        Task<IEnumerable<ILimitTradeEvent>> GetEventsAsync(string clientId, string orderId);
    }
}
