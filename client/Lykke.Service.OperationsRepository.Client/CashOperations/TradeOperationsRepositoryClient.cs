using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Log;
using Lykke.Service.OperationsRepository.AutorestClient;
using Lykke.Service.OperationsRepository.AutorestClient.Models;
using Lykke.Service.OperationsRepository.Client.Abstractions.CashOperations;
using Lykke.Service.OperationsRepository.Client.Models.CashOperations;
using Microsoft.Rest.Serialization;

namespace Lykke.Service.OperationsRepository.Client.CashOperations
{
    public class TradeOperationsRepositoryClient: ITradeOperationsRepositoryClient, IDisposable
    {
        private readonly ILog _log;
        private OperationsRepositoryAPI _apiClient;

        public TradeOperationsRepositoryClient(string serviceUrl, ILog log)
        {
            _log = log;
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
            var response = await _apiClient.ClientTradeOperations.GetByDatesWithHttpMessagesAsync(@from, to);

            return ClientTradesResponse.Prepare(response);
        }

        public async Task<ClientTradeResponse> GetAsync(string clientId, string recordId)
        {
            var response = await _apiClient.ClientTradeOperations.GetByRecordIdWithHttpMessagesAsync(clientId, recordId);

            return ClientTradeResponse.Prepare(response);
        }

        public Task UpdateBlockchainHashAsync(string clientId, string recordId, string hash)
        {
            return _apiClient.ClientTradeOperations.UpdateBlockchainHashWithHttpMessagesAsync(clientId, recordId, hash);
        }

        public Task SetDetectionTimeAndConfirmations(string clientId, string recordId, DateTime detectTime, int confirmations)
        {
            return _apiClient.ClientTradeOperations.SetDetectionTimeAndConfirmationsWithHttpMessagesAsync(clientId,
                recordId, detectTime, confirmations);
        }

        public Task SetBtcTransactionAsync(string clientId, string recordId, string btcTransactionId)
        {
            return _apiClient.ClientTradeOperations.SetBtcTransactionWithHttpMessagesAsync(clientId, recordId,
                btcTransactionId);
        }

        public Task SetIsSettledAsync(string clientId, string id, bool offchain)
        {
            return _apiClient.ClientTradeOperations.SetIsSettledWithHttpMessagesAsync(clientId, id, offchain);
        }

        public Task<ClientTradesResponse> GetByMultisigAsync(string multisig)
        {
            var response = _apiClient.ClientTradeOperations.GetByMultisigWithHttpMessagesAsync(multisig);

            response.Wait();

            return null;

            //var prepared = ClientTradesResponse.Prepare(response.Result);

            //return Task.FromResult(prepared);
        }

        public Task<ClientTradesResponse> GetByMultisigsAsync(string[] multisigs)
        {
            throw new NotImplementedException();
            //var response = await _apiClient.ClientTradeOperations.GetByMultisigsWithHttpMessagesAsync(multisigs);

            //return ClientTradesResponse.Prepare(response);
        }
    }
}
