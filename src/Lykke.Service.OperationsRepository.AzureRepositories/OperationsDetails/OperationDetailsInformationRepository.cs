using AzureStorage;
using Lykke.Service.OperationsRepository.AzureRepositories.Entities;
using Lykke.Service.OperationsRepository.Core.OperationsDetails;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lykke.Service.OperationsRepository.AzureRepositories.OperationsDetails
{
    public class OperationDetailsInformationRepository : IOperationDetailsInformationRepository
    {
        private readonly INoSQLTableStorage<OperationDetailsInformationEntity> _tableStorage;

        public OperationDetailsInformationRepository(INoSQLTableStorage<OperationDetailsInformationEntity> tableStorage)
        {
            _tableStorage = tableStorage;
        }

        public async Task<string> RegisterAsync(IOperationDetailsInformation operation)
        {
            var newItem = OperationDetailsInformationEntity.Create(operation);
            await _tableStorage.InsertAsync(newItem);

            return newItem.Id;
        }

        public async Task CreateAsync(IOperationDetailsInformation operation)
        {
            var newItem = OperationDetailsInformationEntity.Create(operation);
            await _tableStorage.InsertAsync(newItem);
        }

        public async Task<IEnumerable<IOperationDetailsInformation>> GetAsync(string clientId)
        {
            var partitionkey = OperationDetailsInformationEntity.GeneratePartitionKey(clientId);
            return await _tableStorage.GetDataAsync(partitionkey);
        }

        public async Task<IOperationDetailsInformation> GetAsync(string clientId, string id)
        {
            var partitionkey = OperationDetailsInformationEntity.GeneratePartitionKey(clientId);
            var rowKey = OperationDetailsInformationEntity.GenerateRowKey(id);
            return await _tableStorage.GetDataAsync(partitionkey, rowKey);
        }
    }
}
