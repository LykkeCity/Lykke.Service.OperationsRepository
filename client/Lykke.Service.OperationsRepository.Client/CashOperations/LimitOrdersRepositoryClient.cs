using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lykke.Service.OperationsRepository.AutorestClient;
using Lykke.Service.OperationsRepository.AutorestClient.Models;
using Lykke.Service.OperationsRepository.Client.Abstractions.CashOperations;
using Lykke.Service.OperationsRepository.Client.Models.CashOperations;

namespace Lykke.Service.OperationsRepository.Client.CashOperations
{
    public class LimitOrdersRepositoryClient : BaseRepositoryClient, ILimitOrdersRepositoryClient, IDisposable
    {
        private ILimitOrders _apiClient;

        public LimitOrdersRepositoryClient(string serviceUrl, int timeoutInSeconds)
        {
            var operationsApi = new OperationsRepositoryAPI(new Uri(serviceUrl))
            {
                HttpClient = { Timeout = TimeSpan.FromSeconds(timeoutInSeconds) }
            };
            _apiClient = new LimitOrders(operationsApi);
        }

        public void Dispose()
        {
            _apiClient = null;
        }

        public async Task<ILimitOrder> GetByIdAsync(string clientId, string orderId)
        {
            var response = await _apiClient.GetOrderByIdWithHttpMessagesAsync(clientId, orderId);

            return LimitOrderResponse
                .Prepare(response)
                .Validate()
                .GetPayload();
        }

        public async Task RemoveAsync(string clientId, string orderId)
        {
            await _apiClient.RemoveOrderWithHttpMessagesAsync(clientId, orderId);
        }

        public async Task<IEnumerable<ILimitOrder>> GetByClientIdAsync(string clientId)
        {
            var response = await _apiClient.GetOrdersByClientIdWithHttpMessagesAsync(clientId);

            return LimitOrdersResponse
                .Prepare(response)
                .Validate()
                .GetPayload();
        }

        public async Task<IEnumerable<ILimitOrder>> GetActiveByClientIdAsync(string clientId)
        {
            var response = await _apiClient.GetActiveOrdersByClientIdWithHttpMessagesAsync(clientId);

            return LimitOrdersResponse
                .Prepare(response)
                .Validate()
                .GetPayload();
        }

        public async Task<ILimitOrder> AddAsync(LimitOrderCreateRequest order)
        {
            var response = await _apiClient.AddOrderWithHttpMessagesAsync(order);

            return LimitOrderResponse
                .Prepare(response)
                .Validate()
                .GetPayload();
        }

        public async Task<ILimitOrder> FinalizeAsync(string clientId, string orderId, LimitOrderFinalizeRequest request)
        {
            var response = await _apiClient.FinalizeOrderWithHttpMessagesAsync(clientId, orderId, request);

            return LimitOrderResponse
                .Prepare(response)
                .Validate()
                .GetPayload();
        }

        public async Task CancelMultipleAsync(string clientId, LimitOrderCancelMultipleRequest request)
        {
            await _apiClient.CancelMultipleOrdersWithHttpMessagesAsync(clientId, request);
        }

        public async Task<ILimitOrder> CancelByIdAsync(string clientId, string orderId)
        {
            var response = await _apiClient.CancelOrderWithHttpMessagesAsync(clientId, orderId);

            return LimitOrderResponse
                .Prepare(response)
                .Validate()
                .GetPayload();
        }
    }
}