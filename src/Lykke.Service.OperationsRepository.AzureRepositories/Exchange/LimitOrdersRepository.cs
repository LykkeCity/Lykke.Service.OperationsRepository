using AzureStorage;
using Lykke.Service.OperationsRepository.AzureRepositories.Entities;
using Lykke.Service.OperationsRepository.Core.Exchange;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lykke.Service.OperationsRepository.AzureRepositories.Exchange
{
    public class LimitOrdersRepository : ILimitOrdersRepository
    {
        private readonly INoSQLTableStorage<LimitOrderEntity> _tableStorage;

        public LimitOrdersRepository(INoSQLTableStorage<LimitOrderEntity> tableStorage)
        {
            _tableStorage = tableStorage;
        }

        public async Task<ILimitOrder> GetOrderAsync(string orderId)
        {
            return await _tableStorage.GetDataAsync(LimitOrderEntity.ByOrderId.GeneratePartitionKey(), orderId);
        }

        public async Task<IEnumerable<ILimitOrder>> GetOrdersAsync(IEnumerable<string> orderIds)
        {
            var partitionKey = LimitOrderEntity.ByOrderId.GeneratePartitionKey();
            orderIds = orderIds.Select(LimitOrderEntity.ByOrderId.GenerateRowKey);

            return await _tableStorage.GetDataAsync(partitionKey, orderIds);
        }

        public async Task<IEnumerable<ILimitOrder>> GetOrdersAsync(string clientId)
        {
            var partitionKey = LimitOrderEntity.ByClientId.GeneratePartitionKey(clientId);

            return await _tableStorage.GetDataAsync(partitionKey);
        }

        public async Task<IEnumerable<ILimitOrder>> GetActiveOrdersAsync(string clientId)
        {
            var partitionKey = LimitOrderEntity.ByClientIdActive.GeneratePartitionKey(clientId);

            return await _tableStorage.GetDataAsync(partitionKey);
        }
    }
}
