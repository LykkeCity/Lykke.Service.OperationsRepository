using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Log;
using Lykke.Service.OperationsRepository.AutorestClient;
using Lykke.Service.OperationsRepository.AutorestClient.Models;
using Lykke.Service.OperationsRepository.Client.Abstractions.CashOperations;
using Lykke.Service.OperationsRepository.Client.Models.CashOperations;
using Microsoft.Rest;

namespace Lykke.Service.OperationsRepository.Client.CashOperations
{
    public class LimitOrdersRepositoryClient : BaseRepositoryClient, ILimitOrdersRepositoryClient, IDisposable
    {
        private readonly ILog _log;
        private OperationsRepositoryAPI _apiClient;

        public LimitOrdersRepositoryClient(string serviceUrl, ILog log, int timeoutInSeconds)
        {
            _log = log;
            _apiClient =
                new OperationsRepositoryAPI(new Uri(serviceUrl))
                {
                    HttpClient = { Timeout = TimeSpan.FromSeconds(timeoutInSeconds) }
                };
        }
        
        public void Dispose()
        {
            if (_apiClient == null)
                return;
            _apiClient.Dispose();
            _apiClient = null;
        }


        public async Task<ILimitOrder> GetByIdAsync(string orderId)
        {
            var response = await _apiClient.LimitOrders.GetOrderByIdWithHttpMessagesAsync(orderId);

            return LimitOrderResponse
                .Prepare(response)
                .Validate()
                .GetPayload();
        }

        public async Task RemoveAsync(string clientId, string orderId)
        {
            await _apiClient.LimitOrders.RemoveOrderWithHttpMessagesAsync(clientId, orderId);
        }

        public async Task<IEnumerable<ILimitOrder>> GetByClientIdAsync(string clientId)
        {
            var response = await _apiClient.LimitOrders.GetOrdersByClientIdWithHttpMessagesAsync(clientId);
            
            return LimitOrdersResponse
                .Prepare(response)
                .Validate()
                .GetPayload();
        }

        public async Task<IEnumerable<ILimitOrder>> GetActiveByClientIdAsync(string clientId)
        {
            var response = await _apiClient.LimitOrders.GetActiveOrdersByClientIdWithHttpMessagesAsync(clientId);
            
            return LimitOrdersResponse
                .Prepare(response)
                .Validate()
                .GetPayload();
        }

        public async Task<ILimitOrder> AddAsync(LimitOrderCreateRequest order)
        {
            var response = await _apiClient.LimitOrders.AddOrderWithHttpMessagesAsync(order);
            
            return LimitOrderResponse
                .Prepare(response)
                .Validate()
                .GetPayload();
        }

        public async Task<ILimitOrder> FinalizeAsync(LimitOrderFinalizeRequest request)
        {
            var response = await _apiClient.LimitOrders.FinalizeOrderWithHttpMessagesAsync(request);
            
            return LimitOrderResponse
                .Prepare(response)
                .Validate()
                .GetPayload();
        }

        public async Task<ILimitOrder> CancelByIdAsync(string orderId)
        {
            var response = await _apiClient.LimitOrders.CancelOrderWithHttpMessagesAsync(orderId);
            
            return LimitOrderResponse
                .Prepare(response)
                .Validate()
                .GetPayload();
        }
    }
}