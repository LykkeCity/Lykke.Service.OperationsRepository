using AzureStorage;
using Lykke.Service.OperationsRepository.AzureRepositories.Entities;
using Lykke.Service.OperationsRepository.Core.CashOperations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lykke.Service.OperationsRepository.AzureRepositories.CashOperations
{
    public class LimitTradeEventsRepository : ILimitTradeEventsRepository
    {
        private readonly INoSQLTableStorage<LimitTradeEventEntity> _storage;

        public LimitTradeEventsRepository(INoSQLTableStorage<LimitTradeEventEntity> storage)
        {
            _storage = storage;
        }

        public async Task<IEnumerable<ILimitTradeEvent>> GetEventsAsync(string clientId)
        {
            return await _storage.GetDataAsync(LimitTradeEventEntity.GeneratePartitionKey(clientId));
        }

        public async Task<IEnumerable<ILimitTradeEvent>> GetEventsAsync(string clientId, string orderId)
        {
            return await _storage.GetDataAsync(LimitTradeEventEntity.GeneratePartitionKey(clientId), entity => entity.OrderId == orderId);
        }
    }
}
