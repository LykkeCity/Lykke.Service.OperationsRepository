using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lykke.Service.OperationsRepository.AutorestClient;
using Lykke.Service.OperationsRepository.AutorestClient.Models;
using Lykke.Service.OperationsRepository.Client.Abstractions.CashOperations;
using Lykke.Service.OperationsRepository.Client.Models.CashOperations;

namespace Lykke.Service.OperationsRepository.Client.CashOperations
{
    public class LimitTradeEventsRepositoryClient : BaseRepositoryClient, ILimitTradeEventsRepositoryClient, IDisposable
    {
        private ILimitTradeEventOperations _apiClient;

        public LimitTradeEventsRepositoryClient(string serviceUrl, int timeoutInSeconds)
        {
            var operationsApi = new OperationsRepositoryAPI(new Uri(serviceUrl))
            {
                HttpClient = { Timeout = TimeSpan.FromSeconds(timeoutInSeconds) }
            };
            _apiClient = new LimitTradeEventOperations(operationsApi);
        }

        public void Dispose()
        {
            _apiClient = null;
        }

        public async Task<LimitTradeEvent> CreateAsync(LimitTradeEventInsertRequest model)
        {
            var response = await _apiClient.CreateEventWithHttpMessagesAsync(model);

            return LimitTradeEventResponse
                .Prepare(response)
                .Validate()
                .GetPayload();
        }

        public async Task<IEnumerable<LimitTradeEvent>> GetAsync(string clientId)
        {
            var response = await _apiClient.GetEventsWithHttpMessagesAsync(clientId);

            return LimitTradeEventsReponse
                .Prepare(response)
                .Validate()
                .GetPayload();
        }
    }
}