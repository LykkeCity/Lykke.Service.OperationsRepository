using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lykke.Service.OperationsRepository.AutorestClient.Models;
using Lykke.Service.OperationsRepository.Client.Models.CashOperations;

namespace Lykke.Service.OperationsRepository.Client.Abstractions.CashOperations
{
    public interface ITradeOperationsRepositoryClient
    {
        Task<ClientTradesResponse> SaveAsync(params ClientTrade[] clientTrades);
        Task<ClientTradesResponse> GetAsync(string clientId);
        Task<ClientTradesResponse> GetAsync(DateTime from, DateTime to);
        Task<ClientTradeResponse> GetAsync(string clientId, string recordId);
        Task UpdateBlockchainHashAsync(string clientId, string recordId, string hash);
        Task SetDetectionTimeAndConfirmations(string clientId, string recordId, DateTime detectTime, int confirmations);
        Task SetBtcTransactionAsync(string clientId, string recordId, string btcTransactionId);
        Task SetIsSettledAsync(string clientId, string id, bool offchain);
        Task<ClientTradesResponse> GetByMultisigAsync(string multisig);
        Task<ClientTradesResponse> GetByMultisigsAsync(string[] multisigs);
    }
}