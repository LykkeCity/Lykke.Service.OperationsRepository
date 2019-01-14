using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lykke.Service.OperationsRepository.AutorestClient;
using Lykke.Service.OperationsRepository.AutorestClient.Models;
using Lykke.Service.OperationsRepository.Client.Abstractions.CashOperations;
using Lykke.Service.OperationsRepository.Client.Models.CashOperations;

namespace Lykke.Service.OperationsRepository.Client.CashOperations
{
    public class CashOutAttemptRepositoryClient: BaseRepositoryClient, ICashOutAttemptOperationsRepositoryClient, IDisposable
    {
        private ICashOutAttemptOperations _apiClient;

        public CashOutAttemptRepositoryClient(string serviceUrl, int timeoutInSeconds)
        {
            var operationsApi = new OperationsRepositoryAPI(new Uri(serviceUrl))
            {
                HttpClient = { Timeout = TimeSpan.FromSeconds(timeoutInSeconds) }
            };
            _apiClient = new CashOutAttemptOperations(operationsApi);
        }

        public void Dispose()
        {
            _apiClient = null;
        }

        public async Task<string> InsertRequestAsync<T>(CashOutAttemptEntity request, PaymentSystemModel paymentSystem, T paymentFields,
            CashOutRequestTradeSystem tradeSystem)
        {
            var response = await _apiClient.InsertRequestWithHttpMessagesAsync(new CashOutAttemptInsertRequest
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
            var response = await _apiClient.GetAllAttemptsWithHttpMessagesAsync();

            return CashOutAttemptsResponse
                .Prepare(response)
                .Validate()
                .GetPayload();
        }

        public async Task SetBlockchainHash(string clientId, string requestId, string hash)
        {
            await _apiClient.SetBlockchainHashWithHttpMessagesAsync(clientId, requestId, hash);
        }

        public async Task<CashOutAttemptEntity> SetPending(string clientId, string requestId)
        {
            var response = await _apiClient.SetPendingWithHttpMessagesAsync(clientId, requestId);

            return CashOutAttemptResponse
                .Prepare(response)
                .Validate()
                .GetPayload();
        }

        public async Task<CashOutAttemptEntity> SetConfirmed(string clientId, string requestId)
        {
            var response = await _apiClient.SetConfirmedWithHttpMessagesAsync(clientId, requestId);

            return CashOutAttemptResponse
                .Prepare(response)
                .Validate()
                .GetPayload();
        }

        public async Task<CashOutAttemptEntity> SetDocsRequested(string clientId, string requestId)
        {
            var response = await _apiClient.SetDocsRequestedWithHttpMessagesAsync(clientId, requestId);

            return CashOutAttemptResponse
                .Prepare(response)
                .Validate()
                .GetPayload();
        }

        public async Task<CashOutAttemptEntity> SetDeclined(string clientId, string requestId)
        {
            var response = await _apiClient.SetDeclinedWithHttpMessagesAsync(clientId, requestId);

            return CashOutAttemptResponse
                .Prepare(response)
                .Validate()
                .GetPayload();
        }

        public async Task<CashOutAttemptEntity> SetCanceledByClient(string clientId, string requestId)
        {
            var response = await _apiClient.SetCanceledByClientWithHttpMessagesAsync(clientId, requestId);

            return CashOutAttemptResponse
                .Prepare(response)
                .Validate()
                .GetPayload();
        }

        public async Task<CashOutAttemptEntity> SetCanceledByTimeout(string clientId, string requestId)
        {
            var response = await _apiClient.SetCanceledByTimeoutWithHttpMessagesAsync(clientId, requestId);

            return CashOutAttemptResponse
                .Prepare(response)
                .Validate()
                .GetPayload();
        }

        public async Task SetIsSettledOffchain(string clientId, string requestId)
        {
            await _apiClient.SetIsSettledOffchainWithHttpMessagesAsync(clientId, requestId);
        }

        public async Task<IEnumerable<CashOutAttemptEntity>> GetHistoryRecordsAsync(DateTime @from, DateTime to)
        {
            var response = await _apiClient.GetHistoryRecordsWithHttpMessagesAsync(@from, @to);

            return CashOutAttemptsResponse
                .Prepare(response)
                .Validate()
                .GetPayload();
        }

        public async Task<IEnumerable<CashOutAttemptEntity>> GetRequestsAsync(string clientId)
        {
            var response = await _apiClient.GetRequestsWithHttpMessagesAsync(clientId);

            return CashOutAttemptsResponse
                .Prepare(response)
                .Validate()
                .GetPayload();
        }

        public async Task<IEnumerable<CashOutAttemptEntity>> GetRelatedRequestsAsync(string requestId)
        {
            var response = await _apiClient.GetRelatedRequestsWithHttpMessagesAsync(requestId);

            return CashOutAttemptsResponse
                .Prepare(response)
                .Validate()
                .GetPayload();
        }

        public async Task<CashOutAttemptEntity> GetAsync(string clientId, string id)
        {
            var response = await _apiClient.GetWithHttpMessagesAsync(clientId, id);

            return CashOutAttemptResponse
                .Prepare(response)
                .Validate()
                .GetPayload();
        }

        public async Task<CashOutAttemptEntity> SetProcessed(string clientId, string requestId)
        {
            var response = await _apiClient.SetProcessedWithHttpMessagesAsync(clientId, requestId);

            return CashOutAttemptResponse
                .Prepare(response)
                .Validate()
                .GetPayload();
        }

        public async Task<CashOutAttemptEntity> SetHighVolume(string clientId, string requestId)
        {
            var response = await _apiClient.SetHighVolumeWithHttpMessagesAsync(clientId, requestId);

            return CashOutAttemptResponse
                .Prepare(response)
                .Validate()
                .GetPayload();
        }
    }
}
