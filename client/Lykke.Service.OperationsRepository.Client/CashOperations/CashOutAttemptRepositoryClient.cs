using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Common.Log;
using Lykke.Service.OperationsRepository.AutorestClient;
using Lykke.Service.OperationsRepository.AutorestClient.Models;
using Lykke.Service.OperationsRepository.Client.Abstractions.CashOperations;
using Lykke.Service.OperationsRepository.Client.Models.CashOperations;

namespace Lykke.Service.OperationsRepository.Client.CashOperations
{
    public class CashOutAttemptRepositoryClient: ICashOutAttemptOperationsRepositoryClient, IDisposable
    {
        private readonly ILog _log;
        private OperationsRepositoryAPI _apiClient;

        public CashOutAttemptRepositoryClient(string serviceUrl, ILog log, int timeout)
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

        public async Task<string> InsertRequestAsync<T>(CashOutAttemptEntity request, PaymentSystemModel paymentSystem, T paymentFields,
            CashOutRequestTradeSystem tradeSystem)
        {
            var response =
                await _apiClient
                    .CashOutAttemptOperations
                    .InsertRequestWithHttpMessagesAsync(new InsertRequestModel
                    {
                        Request = request,
                        PaymentSystem = paymentSystem,
                        PaymentFields = paymentFields,
                        TradeSystem = tradeSystem
                    });

            return CashOutAttemptIdResponse
                .Prepare(response)
                .Validate()
                .GetPayload();
        }

        public async Task<IEnumerable<CashOutAttemptEntity>> GetAllAttempts()
        {
            var response = await _apiClient.CashOutAttemptOperations.GetAllAttemptsWithHttpMessagesAsync();

            return CashOutAttemptsResponse
                .Prepare(response)
                .Validate()
                .GetPayload();
        }

        public async Task SetBlockchainHash(string clientId, string requestId, string hash)
        {
            await _apiClient.CashOutAttemptOperations.SetBlockchainHashWithHttpMessagesAsync(clientId, requestId, hash);
        }

        public async Task<CashOutAttemptEntity> SetPending(string clientId, string requestId)
        {
            var response =
                await _apiClient.CashOutAttemptOperations.SetPendingWithHttpMessagesAsync(clientId, requestId);

            return CashOutAttemptResponse
                .Prepare(response)
                .Validate()
                .GetPayload();
        }

        public async Task<CashOutAttemptEntity> SetConfirmed(string clientId, string requestId)
        {
            var response =
                await _apiClient.CashOutAttemptOperations.SetConfirmedWithHttpMessagesAsync(clientId, requestId);

            return CashOutAttemptResponse
                .Prepare(response)
                .Validate()
                .GetPayload();
        }

        public async Task<CashOutAttemptEntity> SetDocsRequested(string clientId, string requestId)
        {
            var response =
                await _apiClient.CashOutAttemptOperations.SetDocsRequestedWithHttpMessagesAsync(clientId, requestId);

            return CashOutAttemptResponse
                .Prepare(response)
                .Validate()
                .GetPayload();
        }

        public async Task<CashOutAttemptEntity> SetDeclined(string clientId, string requestId)
        {
            var response =
                await _apiClient.CashOutAttemptOperations.SetDeclinedWithHttpMessagesAsync(clientId, requestId);

            return CashOutAttemptResponse
                .Prepare(response)
                .Validate()
                .GetPayload();
        }

        public async Task<CashOutAttemptEntity> SetCanceledByClient(string clientId, string requestId)
        {
            var response =
                await _apiClient.CashOutAttemptOperations.SetCanceledByClientWithHttpMessagesAsync(clientId, requestId);

            return CashOutAttemptResponse
                .Prepare(response)
                .Validate()
                .GetPayload();
        }

        public async Task<CashOutAttemptEntity> SetCanceledByTimeout(string clientId, string requestId)
        {
            var response =
                await _apiClient.CashOutAttemptOperations.SetCanceledByTimeoutWithHttpMessagesAsync(clientId, requestId);

            return CashOutAttemptResponse
                .Prepare(response)
                .Validate()
                .GetPayload();
        }

        public async Task SetIsSettledOffchain(string clientId, string requestId)
        {
            await _apiClient.CashOutAttemptOperations.SetIsSettledOffchainWithHttpMessagesAsync(clientId, requestId);
        }

        public async Task<IEnumerable<CashOutAttemptEntity>> GetHistoryRecordsAsync(DateTime @from, DateTime to)
        {
            var response = await _apiClient.CashOutAttemptOperations.GetHistoryRecordsWithHttpMessagesAsync(@from, @to);

            return CashOutAttemptsResponse
                .Prepare(response)
                .Validate()
                .GetPayload();
        }

        public async Task<IEnumerable<CashOutAttemptEntity>> GetRequestsAsync(string clientId)
        {
            var response = await _apiClient.CashOutAttemptOperations.GetRequestsWithHttpMessagesAsync(clientId);

            return CashOutAttemptsResponse
                .Prepare(response)
                .Validate()
                .GetPayload();
        }

        public async Task<CashOutAttemptEntity> GetAsync(string clientId, string id)
        {
            var response = await _apiClient.CashOutAttemptOperations.GetWithHttpMessagesAsync(clientId, id);

            return CashOutAttemptResponse
                .Prepare(response)
                .Validate()
                .GetPayload();
        }

        public async Task<CashOutAttemptEntity> SetProcessed(string clientId, string requestId)
        {
            var response = await _apiClient.CashOutAttemptOperations.SetProcessedWithHttpMessagesAsync(clientId, requestId);

            return CashOutAttemptResponse
                .Prepare(response)
                .Validate()
                .GetPayload();
        }

        public async Task<CashOutAttemptEntity> SetHighVolume(string clientId, string requestId)
        {
            var response = await _apiClient.CashOutAttemptOperations.SetHighVolumeWithHttpMessagesAsync(clientId, requestId);

            return CashOutAttemptResponse
                .Prepare(response)
                .Validate()
                .GetPayload();
        }
    }
}
