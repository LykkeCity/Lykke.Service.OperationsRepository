using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lykke.Service.OperationsRepository.AutorestClient;
using Lykke.Service.OperationsRepository.AutorestClient.Models;
using Lykke.Service.OperationsRepository.Client.Abstractions.CashOperations;
using Lykke.Service.OperationsRepository.Client.Models.CashOperations;

namespace Lykke.Service.OperationsRepository.Client.CashOperations
{
    public class TransferOperationsRepositoryClient: BaseRepositoryClient, ITransferOperationsRepositoryClient, IDisposable
    {
        private ITransferOperations _apiClient;

        public TransferOperationsRepositoryClient(string serviceUrl, int timeoutInSeconds)
        {
            var operationsApi = new OperationsRepositoryAPI(new Uri(serviceUrl))
            {
                HttpClient = { Timeout = TimeSpan.FromSeconds(timeoutInSeconds) }
            };
            _apiClient = new TransferOperations(operationsApi);
        }

        public void Dispose()
        {
            _apiClient = null;
        }

        public async Task<TransferEvent> RegisterAsync(TransferEvent transferEvent)
        {
            var response = await _apiClient.RegisterWithHttpMessagesAsync(transferEvent);

            return TransferEventResponse
                .Prepare(response)
                .Validate()
                .GetPayload();
        }

        public async Task<IEnumerable<TransferEvent>> GetAsync(string clientId)
        {
            var response = await _apiClient.GetWithHttpMessagesAsync(clientId);

            return TransferEventsResponse
                .Prepare(response)
                .Validate()
                .GetPayload();
        }

        public async Task<TransferEvent> GetAsync(string clientId, string id)
        {
            var response = await _apiClient.GetByRecordIdWithHttpMessagesAsync(clientId, id);

            return TransferEventResponse
                .Prepare(response)
                .Validate()
                .GetPayload();
        }

        public async Task UpdateBlockChainHashAsync(string clientId, string id, string blockChainHash)
        {
            await _apiClient.UpdateBlockchainHashWithHttpMessagesAsync(clientId, id, blockChainHash);
        }

        public async Task SetBtcTransactionAsync(string clientId, string id, string btcTransaction)
        {
            await _apiClient.SetBtcTransactionWithHttpMessagesAsync(clientId, id, btcTransaction);
        }

        public async Task SetIsSettledIfExistsAsync(string clientId, string id, bool offchain)
        {
            await _apiClient.SetIsSettledIfExistsWithHttpMessagesAsync(offchain, clientId, id);
        }

        public async Task<IEnumerable<TransferEvent>> GetByHashAsync(string blockchainHash)
        {
            var response = await _apiClient.GetByHashWithHttpMessagesAsync(blockchainHash);

            return TransferEventsResponse
                .Prepare(response)
                .Validate()
                .GetPayload();
        }

        public async Task<IEnumerable<TransferEvent>> GetByMultisigAsync(string multisig)
        {
            var response = await _apiClient.GetByMultisigWithHttpMessagesAsync(multisig);

            return TransferEventsResponse
                .Prepare(response)
                .Validate()
                .GetPayload();
        }

        public async Task<IEnumerable<TransferEvent>> GetByMultisigsAsync(string[] multisigs)
        {
            var response = await _apiClient.GetByMultisigsWithHttpMessagesAsync(multisigs);

            return TransferEventsResponse
                .Prepare(response)
                .Validate()
                .GetPayload();
        }
    }
}
