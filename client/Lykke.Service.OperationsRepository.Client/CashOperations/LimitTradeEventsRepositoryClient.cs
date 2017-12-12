using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Log;
using Lykke.Service.OperationsRepository.AutorestClient;
using Lykke.Service.OperationsRepository.AutorestClient.Models;
using Lykke.Service.OperationsRepository.Client.Abstractions.CashOperations;
using Lykke.Service.OperationsRepository.Client.Models.CashOperations;

namespace Lykke.Service.OperationsRepository.Client.CashOperations
{
    public class LimitTradeEventsRepositoryClient : BaseRepositoryClient, ILimitTradeEventsRepositoryClient, IDisposable
    {
        private readonly ILog _log;
        private OperationsRepositoryAPI _apiClient;

        public LimitTradeEventsRepositoryClient(string serviceUrl, ILog log, int timeoutInSeconds)
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

        public async Task<LimitTradeEvent> CreateAsync(LimitTradeEventInsertRequest model)
        {
            var response = await _apiClient.LimitTradeEventOperations.CreateEventWithHttpMessagesAsync(model);

            return LimitTradeEventResponse
                .Prepare(response)
                .Validate()
                .GetPayload();
        }

        public async Task<IEnumerable<LimitTradeEvent>> GetAsync(string clientId)
        {
            var response = await _apiClient.LimitTradeEventOperations.GetEventsWithHttpMessagesAsync(clientId);

            return LimitTradeEventsReponse
                .Prepare(response)
                .Validate()
                .GetPayload();
        }
    }
}