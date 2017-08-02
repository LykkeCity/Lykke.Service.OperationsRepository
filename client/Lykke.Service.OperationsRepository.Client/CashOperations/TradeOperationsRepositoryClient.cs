using System;
using System.Threading.Tasks;
using Lykke.Service.OperationsRepository.AutorestClient;
using Lykke.Service.OperationsRepository.AutorestClient.Models;
using Lykke.Service.OperationsRepository.Client.Abstractions.CashOperations;
using Lykke.Service.OperationsRepository.Client.Models.CashOperations;

namespace Lykke.Service.OperationsRepository.Client.CashOperations
{
    public class TradeOperationsRepositoryClient: ITradeOperationsRepositoryClient, IDisposable
    {
        private OperationsRepositoryAPI _apiClient;

        public TradeOperationsRepositoryClient(string serviceUrl)
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

        public async Task<ClientTradesResponse> SaveAsync(params ClientTrade[] clientTrades)
        {
            var response = await _apiClient.ClientTradeOperations.SaveWithHttpMessagesAsync(clientTrades);
            
            return ClientTradesResponse.Prepare(response);
        }

        public async Task<ClientTradesResponse> GetAsync(string clientId)
        {
            var response = await _apiClient.ClientTradeOperations.GetWithHttpMessagesAsync(clientId);

            return ClientTradesResponse.Prepare(response);
        }

        public async Task<ClientTradesResponse> GetAsync(DateTime @from, DateTime to)
        {
            var response = await _apiClient.ClientTradeOperations.GetByDatesWithHttpMessagesAsync(@from, @to);

            return ClientTradesResponse.Prepare(response);
        }

        public async Task<ClientTradeResponse> GetAsync(string clientId, string recordId)
        {
            var response = await _apiClient.ClientTradeOperations.GetByRecordIdWithHttpMessagesAsync(clientId, recordId);

            return ClientTradeResponse.Prepare(response);
        }

        public async Task UpdateBlockChainHashAsync(string clientId, string recordId, string hash)
        {
            await _apiClient.ClientTradeOperations.UpdateBlockchainHashWithHttpMessagesAsync(clientId, recordId, hash);
        }

        public async Task SetDetectionTimeAndConfirmations(string clientId, string recordId, DateTime detectTime, int confirmations)
        {
            await _apiClient.ClientTradeOperations.SetDetectionTimeAndConfirmationsWithHttpMessagesAsync(clientId,
                recordId, detectTime, confirmations);
        }

        public async Task SetBtcTransactionAsync(string clientId, string recordId, string btcTransactionId)
        {
            await _apiClient.ClientTradeOperations.SetBtcTransactionWithHttpMessagesAsync(clientId, recordId,
                btcTransactionId);
        }

        public async Task SetIsSettledAsync(string clientId, string id, bool offchain)
        {
            await _apiClient.ClientTradeOperations.SetIsSettledWithHttpMessagesAsync(clientId, id, offchain);
        }

        public async Task<ClientTradesResponse> GetByMultisigAsync(string multisig)
        {
            var response = await _apiClient.ClientTradeOperations.GetByMultisigWithHttpMessagesAsync(multisig);

            return ClientTradesResponse.Prepare(response);
        }

        public async Task<ClientTradesResponse> GetByMultisigsAsync(string[] multisigs)
        {
            var response = await _apiClient.ClientTradeOperations.GetByMultisigsWithHttpMessagesAsync(multisigs);

            return ClientTradesResponse.Prepare(response);
        }
    }
}
