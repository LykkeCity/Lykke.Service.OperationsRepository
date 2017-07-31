using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lykke.Service.OperationsRepository.AutorestClient;
using Lykke.Service.OperationsRepository.AutorestClient.Models;
using Lykke.Service.OperationsRepository.Client.Models.CashOperations;

namespace Lykke.Service.OperationsRepository.Client.Abstractions.CashOperations
{
    public interface ICashOutAttemptOperationsRepositoryClient
    {
        Task<CashOutAttemptIdResponse> InsertRequestAsync<T>(CashOutAttemptEntity request, PaymentSystem paymentSystem,
            T paymentFields, string tradeSystem);
        Task<CashOutAttemptsResponse> GetAllAttempts();
        Task SetBlockchainHash(string clientId, string requestId, string hash);
        Task<CashOutAttemptResponse> SetPending(string clientId, string requestId);
        Task<CashOutAttemptResponse> SetConfirmed(string clientId, string requestId);
        Task<CashOutAttemptResponse> SetDocsRequested(string clientId, string requestId);
        Task<CashOutAttemptResponse> SetDeclined(string clientId, string requestId);
        Task<CashOutAttemptResponse> SetCanceledByClient(string clientId, string requestId);
        Task<CashOutAttemptResponse> SetCanceledByTimeout(string clientId, string requestId);
        Task SetProcessed(string clientId, string requestId);
        Task SetIsSettledOffchain(string clientId, string requestId);
        Task<CashOutAttemptsResponse> GetHistoryRecordsAsync(DateTime @from, DateTime to);
    }
}