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
    class MarketOrdersRepositoryClient : BaseRepositoryClient, IMarketOrdersRepositoryClient, IDisposable
    {
        private readonly ILog _log;
        private OperationsRepositoryAPI _apiClient;

        public MarketOrdersRepositoryClient(string serviceUrl, ILog log, int timeout)
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

        public async Task<IEnumerable<MarketOrder>> GetOrdersAsync(string[] orderIds)
        {
            var response = await _apiClient.MarketOrdersOperations.GetByOrdersIdsWithHttpMessagesAsync(orderIds);

            return MarketOrdersResponse
                .Prepare(response)
                .Validate()
                .GetPayload();
        }

        public async Task<MarketOrder> GetAsync(string orderId)
        {
            var response = await _apiClient.MarketOrdersOperations.GetByOrderIdWithHttpMessagesAsync(orderId);

            return MarketOrderResponse
                .Prepare(response)
                .Validate()
                .GetPayload();
        }

        public async Task<MarketOrder> GetAsync(string clientId, string orderId)
        {
            var response = await _apiClient.MarketOrdersOperations.GetByClientIdAndOrderIdWithHttpMessagesAsync(clientId, orderId);

            return MarketOrderResponse
                .Prepare(response)
                .Validate()
                .GetPayload();
        }

        public async Task<IEnumerable<MarketOrder>> GetOrdersAsync(string clientId)
        {
            try
            {
                var response = await _apiClient.MarketOrdersOperations.GetByClientIdWithHttpMessagesAsync(clientId);

                return MarketOrdersResponse
                    .Prepare(response)
                    .Validate()
                    .GetPayload();

            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task CreateAsync(MarketOrder marketOrder)
        {
            await _apiClient.MarketOrdersOperations.CreateMarketOrderWithHttpMessagesAsync(marketOrder);
        }
    }
}
