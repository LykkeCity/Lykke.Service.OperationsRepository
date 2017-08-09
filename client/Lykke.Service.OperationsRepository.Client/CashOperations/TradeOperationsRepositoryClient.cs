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

        public TradeOperationsRepositoryClient(string serviceUrl, ILog log, int timeout)
        {
            _log = log;
            _apiClient =
                new OperationsRepositoryAPI(new Uri(serviceUrl))
                {
                    HttpClient = {Timeout = TimeSpan.FromSeconds(timeout)}
                };
        }

        public void Dispose()
        {
            if (_apiClient == null)
                return;
            _apiClient.Dispose();
            _apiClient = null;
        }

        public async Task<IEnumerable<ClientTrade>> SaveAsync(ClientTrade[] clientTrades)
        {
            var response = await _apiClient.ClientTradeOperations.SaveWithHttpMessagesAsync(clientTrades);

            return ClientTradesResponse
                .Prepare(response)
                .Validate()
                .GetPayload();
        }

        public async Task<IEnumerable<ClientTrade>> GetAsync(string clientId)
        {
            var response = await _apiClient.ClientTradeOperations.GetWithHttpMessagesAsync(clientId);

            return ClientTradesResponse
                .Prepare(response)
                .Validate()
                .GetPayload();
        }

        public async Task<IEnumerable<ClientTrade>> GetAsync(DateTime @from, DateTime to)
        {
            var response = await _apiClient.ClientTradeOperations.GetByDatesWithHttpMessagesAsync(@from, to);

            return ClientTradesResponse
                .Prepare(response)
                .Validate()
                .GetPayload();
        }

        public async Task<ClientTrade> GetAsync(string clientId, string recordId)
        {
            var response = await _apiClient.ClientTradeOperations.GetByRecordIdWithHttpMessagesAsync(clientId, recordId);

            return ClientTradeResponse
                .Prepare(response)
                .Validate()
                .GetPayload();
        }

        public async Task UpdateBlockchainHashAsync(string clientId, string recordId, string hash)
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

        public async Task<IEnumerable<ClientTrade>> GetByMultisigAsync(string multisig)
        {
            var response = await _apiClient.ClientTradeOperations.GetByMultisigWithHttpMessagesAsync(multisig);

            return ClientTradesResponse
                .Prepare(response)
                .Validate()
                .GetPayload();
        }

        public async Task<IEnumerable<ClientTrade>> GetByMultisigsAsync(string[] multisigs)
        {
            var response = await _apiClient.ClientTradeOperations.GetByMultisigsWithHttpMessagesAsync(multisigs);

            return ClientTradesResponse
                .Prepare(response)
                .Validate()
                .GetPayload();
        }

        public async Task<IEnumerable<ClientTrade>> ScanByDtAsync(DateTime @from, DateTime to)
        {
            var response = await _apiClient.ClientTradeOperations.ScanByDtWithHttpMessagesAsync(@from, to);

            return ClientTradesResponse
                .Prepare(response)
                .Validate()
                .GetPayload();
        }
    }
}
