using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Log;
using Lykke.Service.OperationsRepository.AutorestClient;
using Lykke.Service.OperationsRepository.AutorestClient.Models;
using Lykke.Service.OperationsRepository.Client.Abstractions.CashOperations;

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

        public Task<LimitTradeEvent> CreateAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<LimitTradeEvent>> GetAsync(string clientId)
        {
            throw new System.NotImplementedException();
        } 
    }
}