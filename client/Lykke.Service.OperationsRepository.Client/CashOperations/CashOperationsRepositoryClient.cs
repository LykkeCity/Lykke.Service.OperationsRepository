using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lykke.Service.OperationsRepository.AutorestClient;
using Lykke.Service.OperationsRepository.AutorestClient.Models;
using Lykke.Service.OperationsRepository.Client.Abstractions.CashOperations;
using Lykke.Service.OperationsRepository.Client.Models.CashOperations;

namespace Lykke.Service.OperationsRepository.Client.CashOperations
{
    public class CashOperationsRepositoryClient: BaseRepositoryClient, ICashOperationsRepositoryClient, IDisposable
    {
        private ICashOperations _apiClient;

        public CashOperationsRepositoryClient(string serviceUrl, int timeoutInSeconds)
        {
            var operationsApi = new OperationsRepositoryAPI(new Uri(serviceUrl))
            {
                HttpClient = { Timeout = TimeSpan.FromSeconds(timeoutInSeconds) }
            };
            _apiClient = new AutorestClient.CashOperations(operationsApi);
        }

        public void Dispose()
        {
            _apiClient = null;
        }

        public async Task<string> RegisterAsync(CashInOutOperation operation)
        {
            var response = await _apiClient.RegisterWithHttpMessagesAsync(operation);

            return CashOperationIdResponse
                .Prepare(response)
                .Validate()
                .GetPayload();
        }

        public async Task<IEnumerable<CashInOutOperation>> GetAsync(string clientId)
        {
            var response = await _apiClient.GetWithHttpMessagesAsync(clientId);

            return CashOperationsResponse
                .Prepare(response)
                .Validate()
                .GetPayload();
        }

        public async Task<CashInOutOperation> GetAsync(string clientId, string recordId)
        {
            var response = await _apiClient.GetByRecordIdWithHttpMessagesAsync(clientId, recordId);

            return CashOperationResponse
                .Prepare(response)
                .Validate()
                .GetPayload();
        }

        public async Task UpdateBlockchainHashAsync(string clientId, string id, string hash)
        {
            await _apiClient.UpdateBlockchainHashWithHttpMessagesAsync(clientId, id, hash);
        }

        public async Task SetBtcTransaction(string clientId, string id, string bcnTransactionId)
        {
            await _apiClient.SetBtcTransactionWithHttpMessagesAsync(clientId, id, bcnTransactionId);
        }

        public async Task SetIsSettledAsync(string clientId, string id, bool offchain)
        {
            await _apiClient.SetIsSettledWithHttpMessagesAsync(offchain, clientId, id);
        }

        public async Task<IEnumerable<CashInOutOperation>> GetByHashAsync(string blockchainHash)
        {
            var response = await _apiClient.GetByHashWithHttpMessagesAsync(blockchainHash);

            return CashOperationsResponse
                .Prepare(response)
                .Validate()
                .GetPayload();
        }

        public async Task<IEnumerable<CashInOutOperation>> GetByMultisigAsync(string multisig)
        {
            var response = await _apiClient.GetByMultisigWithHttpMessagesAsync(multisig);

            return CashOperationsResponse
                .Prepare(response)
                .Validate()
                .GetPayload();
        }

        public async Task<IEnumerable<CashInOutOperation>> GetByMultisigsAsync(string[] multisigs)
        {
            var response = await _apiClient.GetByMultisigsWithHttpMessagesAsync(multisigs);

            return CashOperationsResponse
                .Prepare(response)
                .Validate()
                .GetPayload();
        }

    }
}
