using Common.Log;
using Lykke.Service.OperationsRepository.AutorestClient;
using Lykke.Service.OperationsRepository.AutorestClient.Models;
using Lykke.Service.OperationsRepository.Client.Abstractions.OperationsDetails;
using Lykke.Service.OperationsRepository.Client.Models.OperationsDetails;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lykke.Service.OperationsRepository.Client.OperationsDetails
{
    public class OperationDetailsInformationClient : IOperationDetailsInformationClient, IDisposable
    {
        private readonly ILog _log;
        private OperationsRepositoryAPI _apiClient;

        public OperationDetailsInformationClient(string serviceUrl, ILog log, int timeout)
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

        public async Task<IEnumerable<OperationDetailsInformation>> GetAsync(string clinetId)
        {
            try
            {
                var response = await _apiClient.OperationDetailsInformation.GetWithHttpMessagesAsync(clinetId);

                return OperationDetailsInformationsResponse
                    .Prepare(response)
                    .Validate()
                    .GetPayload();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<OperationDetailsInformation> GetAsync(string clientId, string recordId)
        {
            var response = await _apiClient.OperationDetailsInformation.GetByRecordIdWithHttpMessagesAsync(clientId, recordId);

            return OperationDetailsInformationResponse
                .Prepare(response)
                .Validate()
                .GetPayload();
        }

        public async Task CreateAsync(OperationDetailsInformation operationDetailsInfo)
        {
            await _apiClient.OperationDetailsInformation.CreateWithHttpMessagesAsync(operationDetailsInfo);
        }

        public async Task<string> RegisterAsync(OperationDetailsInformation operationDetailsInfo)
        {
            var response = await _apiClient.OperationDetailsInformation.RegisterWithHttpMessagesAsync(operationDetailsInfo);

            return OperationDetailsInformationIdResponse
                .Prepare(response)
                .Validate()
                .GetPayload();
        }
    }
}
