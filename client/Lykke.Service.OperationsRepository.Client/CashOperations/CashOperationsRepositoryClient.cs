using System;
using System.Threading.Tasks;
using Lykke.Service.OperationsRepository.AutorestClient;
using Lykke.Service.OperationsRepository.AutorestClient.Models;
using Lykke.Service.OperationsRepository.Client.Abstractions.CashOperations;
using Lykke.Service.OperationsRepository.Client.Models.CashOperations;

namespace Lykke.Service.OperationsRepository.Client.CashOperations
{
    public class CashOperationsRepositoryClient: ICashOperationsRepositoryClient, IDisposable
    {
        private OperationsRepositoryAPI _apiClient;

        public CashOperationsRepositoryClient(string serviceUrl)
        {
            _apiClient = new OperationsRepositoryAPI(new Uri(serviceUrl));
        }

        public void Dispose()
        {
            if (_apiClient == null)
                return;
            _apiClient.Dispose();
            _apiClient = null;
        }

        public async Task<CashOperationIdResponse> RegisterAsync(CashInOutOperation operation)
        {
            var response = await _apiClient.CashOperations.RegisterWithHttpMessagesAsync(operation);

            return CashOperationIdResponse.Prepare(response);
        }

        public async Task<CashOperationsResponse> GetAsync(string clientId)
        {
            var response = await _apiClient.CashOperations.GetWithHttpMessagesAsync(clientId);

            return CashOperationsResponse.Prepare(response);
        }

        public async Task<CashOperationResponse> GetAsync(string clientId, string recordId)
        {
            var response = await _apiClient.CashOperations.GetByRecordIdWithHttpMessagesAsync(clientId, recordId);

            return CashOperationResponse.Prepare(response);
        }

        public async Task UpdateBlockchainHashAsync(string clientId, string id, string hash)
        {
            await _apiClient.CashOperations.UpdateBlockchainHashWithHttpMessagesAsync(clientId, id, hash);
        }

        public async Task SetBtcTransaction(string clientId, string id, string bcnTransactionId)
        {
            await _apiClient.CashOperations.SetBtcTransactionWithHttpMessagesAsync(clientId, id, bcnTransactionId);
        }

        public async Task SetIsSettledAsync(string clientId, string id, bool offchain)
        {
            await _apiClient.CashOperations.SetIsSettledWithHttpMessagesAsync(clientId, id, offchain);
        }

        public async Task<CashOperationsResponse> GetByHashAsync(string blockchainHash)
        {
            var response = await _apiClient.CashOperations.GetByHashWithHttpMessagesAsync(blockchainHash);

            return CashOperationsResponse.Prepare(response);
        }

        public async Task<CashOperationsResponse> GetByMultisigAsync(string multisig)
        {
            var response = await _apiClient.CashOperations.GetByMultisigWithHttpMessagesAsync(multisig);

            return CashOperationsResponse.Prepare(response);
        }

        public async Task<CashOperationsResponse> GetByMultisigsAsync(string[] multisigs)
        {
            var response = await _apiClient.CashOperations.GetByMultisigsWithHttpMessagesAsync(multisigs);

            return CashOperationsResponse.Prepare(response);
        }

    }
}
