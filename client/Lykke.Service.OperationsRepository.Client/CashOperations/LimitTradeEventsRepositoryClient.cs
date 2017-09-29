using Common.Log;
using Lykke.Service.OperationsRepository.AutorestClient;
using Lykke.Service.OperationsRepository.AutorestClient.Models;
using Lykke.Service.OperationsRepository.Client.Abstractions.CashOperations;
using Lykke.Service.OperationsRepository.Client.Models.CashOperations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lykke.Service.OperationsRepository.Client.CashOperations
{
    public class LimitTradeEventsRepositoryClient : BaseRepositoryClient, ILimitTradeEventsRepositoryClient, IDisposable
    {
        private readonly ILog _log;
        private OperationsRepositoryAPI _apiClient;

        public LimitTradeEventsRepositoryClient(string serviceUrl, ILog log, int timeout)
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

        public async Task<IEnumerable<LimitTradeEvent>> GetAsync(string clinetId)
        {
            var response = await _apiClient.LimitTradeEvents.GetWithHttpMessagesAsync(clinetId);

            return LimitTradeEventResponse
                .Prepare(response)
                .Validate()
                .GetPayload();
        }

        public async Task<IEnumerable<LimitTradeEvent>> GetAsync(string clientId, string orderId)
        {
            var response = await _apiClient.LimitTradeEvents.GetByOrderIdWithHttpMessagesAsync(clientId, orderId);

            return LimitTradeEventResponse
                .Prepare(response)
                .Validate()
                .GetPayload();
        }
    }
}
