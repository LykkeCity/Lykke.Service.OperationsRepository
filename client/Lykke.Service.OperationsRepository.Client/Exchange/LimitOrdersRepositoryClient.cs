using Common.Log;
using Lykke.Service.OperationsRepository.AutorestClient;
using Lykke.Service.OperationsRepository.AutorestClient.Models;
using Lykke.Service.OperationsRepository.Client.Abstractions.Exchange;
using Lykke.Service.OperationsRepository.Client.CashOperations;
using Lykke.Service.OperationsRepository.Client.Models.Exchange;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lykke.Service.OperationsRepository.Client.Exchange
{
    public class LimitOrdersRepositoryClient : BaseRepositoryClient, ILimitOrdersRepositoryClient, IDisposable
    {
        private readonly ILog _log;
        private OperationsRepositoryAPI _apiClient;

        public LimitOrdersRepositoryClient(string serviceUrl, ILog log, int timeout)
        {
            _log = log;
            _apiClient =
                new OperationsRepositoryAPI(new Uri(serviceUrl))
                {
                    HttpClient = { Timeout = TimeSpan.FromSeconds(timeout) }
                };
        }

        public void Dispose()
        {
            if (_apiClient == null)
                return;
            _apiClient.Dispose();
            _apiClient = null;
        }

        public async Task<LimitOrder> GetOrderAsync(string orderId)
        {
            var response = await _apiClient.LimitOrdersOperations.GetOrderWithHttpMessagesAsync(orderId);

            return LimitOrderResponse
                .Prepare(response)
                .Validate()
                .GetPayload();
        }

        public async Task<IEnumerable<LimitOrder>> GetActiveOrdersAsync(string clientId)
        {
            var response = await _apiClient.LimitOrdersOperations.GetActiveOrdersWithHttpMessagesAsync(clientId);

            return LimitOrdersResponse
                .Prepare(response)
                .Validate()
                .GetPayload();
        }

        public async Task<IEnumerable<LimitOrder>> GetOrdersAsync(string clientId)
        {
            var response = await _apiClient.LimitOrdersOperations.GetByClientIdWithHttpMessagesAsync(clientId);

            return LimitOrdersResponse
                .Prepare(response)
                .Validate()
                .GetPayload();
        }

        public async Task<IEnumerable<LimitOrder>> GetOrdersAsync(string[] orderIds)
        {
            var response = await _apiClient.LimitOrdersOperations.GetByOrdersIdsWithHttpMessagesAsync(orderIds);

            return LimitOrdersResponse
                .Prepare(response)
                .Validate()
                .GetPayload();
        }
    }
}
