using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lykke.Service.OperationsRepository.AutorestClient;
using Lykke.Service.OperationsRepository.AutorestClient.Models;
using Lykke.Service.OperationsRepository.Client.Abstractions.CashOperations;
using Lykke.Service.OperationsRepository.Client.Models.CashOperations;

namespace Lykke.Service.OperationsRepository.Client.CashOperations
{
    public class TradeOperationsRepositoryClient: BaseRepositoryClient, ITradeOperationsRepositoryClient, IDisposable
    {
        private IClientTradeOperations _apiClient;

        public TradeOperationsRepositoryClient(string serviceUrl, int timeoutInSeconds)
        {
            var operationsApi = new OperationsRepositoryAPI(new Uri(serviceUrl))
            {
                HttpClient = { Timeout = TimeSpan.FromSeconds(timeoutInSeconds) }
            };
            _apiClient = new ClientTradeOperations(operationsApi);
        }

        public void Dispose()
        {
            _apiClient = null;
        }

        public async Task<IEnumerable<ClientTrade>> SaveAsync(ClientTrade[] clientTrades)
        {
            var response = await _apiClient.SaveWithHttpMessagesAsync(clientTrades);

            return ClientTradesResponse
                .Prepare(response)
                .Validate()
                .GetPayload();
        }

        public async Task<IEnumerable<ClientTrade>> GetAsync(string clientId)
        {
            var response = await _apiClient.GetWithHttpMessagesAsync(clientId);

            return ClientTradesResponse
                .Prepare(response)
                .Validate()
                .GetPayload();
        }

        public async Task<IEnumerable<ClientTrade>> GetAsync(DateTime @from, DateTime to)
        {
            var response = await _apiClient.GetByDatesWithHttpMessagesAsync(@from, to);

            return ClientTradesResponse
                .Prepare(response)
                .Validate()
                .GetPayload();
        }

        public async Task<ClientTradesChunk> GetByDatesAsync(DateTime from, DateTime to, string continuationToken)
        {
            var response = await _apiClient.GetByDatesWithChunksWithHttpMessagesAsync(from, to, continuationToken);

            return ClientTradesChunkResponse
                .Prepare(response)
                .Validate()
                .GetPayload();
        }

        public async Task<ClientTrade> GetAsync(string clientId, string recordId)
        {
            var response = await _apiClient.GetByRecordIdWithHttpMessagesAsync(clientId, recordId);

            return ClientTradeResponse
                .Prepare(response)
                .Validate()
                .GetPayload();
        }

        public async Task UpdateBlockchainHashAsync(string clientId, string recordId, string hash)
        {
            await _apiClient.UpdateBlockchainHashWithHttpMessagesAsync(
                clientId,
                recordId,
                hash);
        }

        public async Task SetDetectionTimeAndConfirmations(string clientId, string recordId, DateTime detectTime, int confirmations)
        {
            await _apiClient.SetDetectionTimeAndConfirmationsWithHttpMessagesAsync(
                detectTime,
                confirmations,
                clientId,
                recordId);
        }

        public async Task SetBtcTransactionAsync(string clientId, string recordId, string btcTransactionId)
        {
            await _apiClient.SetBtcTransactionWithHttpMessagesAsync(
                clientId,
                recordId,
                btcTransactionId);
        }

        public async Task SetIsSettledAsync(string clientId, string id, bool offchain)
        {
            await _apiClient.SetIsSettledWithHttpMessagesAsync(
                offchain,
                clientId,
                id);
        }

        public async Task<IEnumerable<ClientTrade>> ScanByDtAsync(DateTime @from, DateTime to)
        {
            var measureResult = await this.MeasureTime(() => _apiClient.ScanByDtWithHttpMessagesAsync(@from, to));

            return ClientTradesResponse
                .Prepare(measureResult.ActionResult)
                .Validate()
                .GetPayload();
        }

        public async Task<IEnumerable<ClientTrade>> GetByOrderAsync(string orderId)
        {
            var response = await _apiClient.GetByOrderWithHttpMessagesAsync(orderId);

            return ClientTradesResponse
                .Prepare(response)
                .Validate()
                .GetPayload();
        }
    }
}
