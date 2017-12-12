using System.Collections.Generic;
using System.Threading.Tasks;
using Lykke.Service.OperationsRepository.AutorestClient.Models;

namespace Lykke.Service.OperationsRepository.Client.Abstractions.CashOperations
{
    public interface ICashOperationsRepositoryClient
    {
        Task<string> RegisterAsync(CashInOutOperation operation);
        Task<IEnumerable<CashInOutOperation>> GetAsync(string clientId);
        Task<CashInOutOperation> GetAsync(string clientId, string recordId);
        Task UpdateBlockchainHashAsync(string clientId, string id, string hash);
        Task SetBtcTransaction(string clientId, string id, string bcnTransactionId);
        Task SetIsSettledAsync(string clientId, string id, bool offchain);
        Task<IEnumerable<CashInOutOperation>> GetByHashAsync(string blockchainHash);
        Task<IEnumerable<CashInOutOperation>> GetByMultisigAsync(string multisig);
        Task<IEnumerable<CashInOutOperation>> GetByMultisigsAsync(string[] multisigs);
    }
}