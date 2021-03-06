﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Lykke.Service.OperationsRepository.AutorestClient.Models;

namespace Lykke.Service.OperationsRepository.Client.Abstractions.CashOperations
{
    [PublicAPI]
    public interface ITradeOperationsRepositoryClient
    {
        Task<IEnumerable<ClientTrade>> SaveAsync(ClientTrade[] clientTrades);
        Task<IEnumerable<ClientTrade>> GetAsync(string clientId);
        [Obsolete("Use GetByDatesAsync method with continuation token")]
        Task<IEnumerable<ClientTrade>> GetAsync(DateTime from, DateTime to);
        Task<ClientTradesChunk> GetByDatesAsync(DateTime from, DateTime to, string continuationToken);
        Task<ClientTrade> GetAsync(string clientId, string recordId);
        Task UpdateBlockchainHashAsync(string clientId, string recordId, string hash);
        Task SetDetectionTimeAndConfirmations(string clientId, string recordId, DateTime detectTime, int confirmations);
        Task SetBtcTransactionAsync(string clientId, string recordId, string btcTransactionId);
        Task SetIsSettledAsync(string clientId, string id, bool offchain);
        [Obsolete("Use GetByDatesAsync method with continuation token")]
        Task<IEnumerable<ClientTrade>> ScanByDtAsync(DateTime from, DateTime to);
        Task<IEnumerable<ClientTrade>> GetByOrderAsync(string orderId);
    }
}