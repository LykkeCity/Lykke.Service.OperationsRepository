using System.Collections.Generic;
using System.Threading.Tasks;
using Lykke.Service.OperationsRepository.AutorestClient.Models;

namespace Lykke.Service.OperationsRepository.Client.Abstractions.CashOperations
{
    public interface ILimitTradeEventsRepositoryClient
    {
        Task<LimitTradeEvent> CreateAsync();
        Task<IEnumerable<LimitTradeEvent>> GetAsync(string clientId);
    }
}