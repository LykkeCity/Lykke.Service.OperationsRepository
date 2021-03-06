﻿using Lykke.Service.OperationsRepository.Contract;
using Lykke.Service.OperationsRepository.Contract.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lykke.Service.OperationsRepository.Core.CashOperations
{
    public interface ICashOutAttemptRepository : ICashOutBaseRepository
    {
        Task<string> InsertRequestAsync<T>(ICashOutRequest request, PaymentSystem paymentSystem, T paymentFields, CashOutRequestTradeSystem tradeSystem);
        Task<IEnumerable<ICashOutRequest>> GetAllAttempts();
        Task<ICashOutRequest> SetBlockchainHash(string clientId, string requestId, string hash);
        Task<ICashOutRequest> SetPending(string clientId, string requestId);
        Task<ICashOutRequest> SetConfirmed(string clientId, string requestId);
        Task<ICashOutRequest> SetDocsRequested(string clientId, string requestId);
        Task<ICashOutRequest> SetDeclined(string clientId, string requestId);
        Task<ICashOutRequest> SetCanceledByClient(string clientId, string requestId);
        Task<ICashOutRequest> SetCanceledByTimeout(string clientId, string requestId);
        Task<ICashOutRequest> SetProcessed(string clientId, string requestId);
        Task<ICashOutRequest> SetHighVolume(string clientId, string requestId);
        Task<ICashOutRequest> SetIsSettledOffchain(string clientId, string requestId);

        Task<IEnumerable<ICashOutRequest>> GetHistoryRecordsAsync(DateTime @from, DateTime to);
        Task<IEnumerable<ICashOutRequest>> GetRelatedRequestsAsync(string requestId);
    }
}
