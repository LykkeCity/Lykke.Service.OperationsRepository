﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lykke.Service.OperationsRepository.AutorestClient.Models;

namespace Lykke.Service.OperationsRepository.Client.Abstractions.CashOperations
{
    public interface ICashOutAttemptOperationsRepositoryClient
    {
        Task<string> InsertRequestAsync<T>(CashOutAttemptEntity request, PaymentSystemModel paymentSystem,
            T paymentFields, CashOutRequestTradeSystem tradeSystem);
        Task<IEnumerable<CashOutAttemptEntity>> GetAllAttempts();
        Task SetBlockchainHash(string clientId, string requestId, string hash);
        Task<CashOutAttemptEntity> SetPending(string clientId, string requestId);
        Task<CashOutAttemptEntity> SetConfirmed(string clientId, string requestId);
        Task<CashOutAttemptEntity> SetDocsRequested(string clientId, string requestId);
        Task<CashOutAttemptEntity> SetDeclined(string clientId, string requestId);
        Task<CashOutAttemptEntity> SetCanceledByClient(string clientId, string requestId);
        Task<CashOutAttemptEntity> SetCanceledByTimeout(string clientId, string requestId);
        Task<CashOutAttemptEntity> SetProcessed(string clientId, string requestId);
        Task<CashOutAttemptEntity> SetHighVolume(string clientId, string requestId);
        Task SetIsSettledOffchain(string clientId, string requestId);
        Task<IEnumerable<CashOutAttemptEntity>> GetHistoryRecordsAsync(DateTime @from, DateTime to);
        Task<IEnumerable<CashOutAttemptEntity>> GetRequestsAsync(string clientId);
        Task<IEnumerable<CashOutAttemptEntity>> GetRelatedRequestsAsync(string requestId);
        Task<CashOutAttemptEntity> GetAsync(string clientId, string id);
    }
}