using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Log;
using Lykke.Service.OperationsRepository.AutorestClient;
using Lykke.Service.OperationsRepository.Client.Abstractions.CashOperations;
using Lykke.Service.OperationsRepository.Client.Models.CashOperations;

namespace Lykke.Service.OperationsRepository.Client.CashOperations
{
    public class TestAsyncMethods: ITestAsyncMethods, IDisposable
    {
        private readonly ILog _log;
        private OperationsRepositoryAPI _apiClient;

        public TestAsyncMethods(string serviceUrl, ILog log)
        {
            _log = log;
            _apiClient = new OperationsRepositoryAPI(new Uri(serviceUrl));
        }

        public void Dispose()
        {
            if (_apiClient == null)
                return;
            _apiClient.Dispose();
            _apiClient = null;
        }

        public async Task<ClientTradesResponse> GetByMultisigAsync(string multisig)
        {
            var response = await _apiClient.ClientTradeOperations.GetByMultisigWithHttpMessagesAsync(multisig);

            return ClientTradesResponse.Prepare(response);
        }

        public async Task<ClientTradesResponse> GetByMultisigsAsync(string[] multisigs)
        {
            var response = await _apiClient.ClientTradeOperations.GetByMultisigsWithHttpMessagesAsync(multisigs);

            return ClientTradesResponse.Prepare(response);
        }
    }
}
