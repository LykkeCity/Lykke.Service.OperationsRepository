using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Log;
using Lykke.Service.OperationsRepository.AutorestClient;
using Lykke.Service.OperationsRepository.AutorestClient.Models;
using Lykke.Service.OperationsRepository.Client.Abstractions.CashOperations;
using Lykke.Service.OperationsRepository.Client.Models.CashOperations;

namespace Lykke.Service.OperationsRepository.Client.CashOperations
{
    public class TransferOperationsRepositoryClient: BaseRepositoryClient, ITransferOperationsRepositoryClient, IDisposable
    {
        private readonly ILog _log;
        private OperationsRepositoryAPI _apiClient;

        public TransferOperationsRepositoryClient(string serviceUrl, ILog log, int timeoutInSeconds)
        {
            _log = log;
            _apiClient =
                new OperationsRepositoryAPI(new Uri(serviceUrl))
                {
                    HttpClient = {Timeout = TimeSpan.FromSeconds(timeoutInSeconds)}
                };
        }

        public void Dispose()
        {
            if (_apiClient == null)
                return;
            _apiClient.Dispose();
            _apiClient = null;
        }

        public async Task<TransferEvent> RegisterAsync(TransferEvent transferEvent)
        {
            var response = await _apiClient.TransferOperations.RegisterWithHttpMessagesAsync(transferEvent);

            return TransferEventResponse
                .Prepare(response)
                .Validate()
                .GetPayload();
        }

        public async Task<IEnumerable<TransferEvent>> GetAsync(string clientId)
        {
            var response = await _apiClient.TransferOperations.GetWithHttpMessagesAsync(clientId);

            return TransferEventsResponse
                .Prepare(response)
                .Validate()
                .GetPayload();
        }

        public async Task<TransferEvent> GetAsync(string clientId, string id)
        {
            var response = await _apiClient.TransferOperations.GetByRecordIdWithHttpMessagesAsync(clientId, id);

            return TransferEventResponse
                .Prepare(response)
                .Validate()
                .GetPayload();
        }

        public async Task UpdateBlockChainHashAsync(string clientId, string id, string blockChainHash)
        {
            await _apiClient.TransferOperations.UpdateBlockchainHashWithHttpMessagesAsync(clientId, id, blockChainHash);
        }

        public async Task SetBtcTransactionAsync(string clientId, string id, string btcTransaction)
        {
            await _apiClient.TransferOperations.SetBtcTransactionWithHttpMessagesAsync(clientId, id, btcTransaction);
        }

        public async Task SetIsSettledIfExistsAsync(string clientId, string id, bool offchain)
        {
            await _apiClient.TransferOperations.SetIsSettledIfExistsWithHttpMessagesAsync(offchain, clientId, id);
        }

        public async Task<IEnumerable<TransferEvent>> GetByHashAsync(string blockchainHash)
        {
            var response = await _apiClient.TransferOperations.GetByHashWithHttpMessagesAsync(blockchainHash);

            return TransferEventsResponse
                .Prepare(response)
                .Validate()
                .GetPayload();
        }

        public async Task<IEnumerable<TransferEvent>> GetByMultisigAsync(string multisig)
        {
            var response = await _apiClient.TransferOperations.GetByMultisigWithHttpMessagesAsync(multisig);

            return TransferEventsResponse
                .Prepare(response)
                .Validate()
                .GetPayload();
        }

        public async Task<IEnumerable<TransferEvent>> GetByMultisigsAsync(string[] multisigs)
        {
            var response = await _apiClient.TransferOperations.GetByMultisigsWithHttpMessagesAsync(multisigs);

            return TransferEventsResponse
                .Prepare(response)
                .Validate()
                .GetPayload();
        }
    }
}
