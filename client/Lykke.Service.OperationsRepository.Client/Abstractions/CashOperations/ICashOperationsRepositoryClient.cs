using System.Threading.Tasks;
using Lykke.Service.OperationsRepository.AutorestClient.Models;
using Lykke.Service.OperationsRepository.Client.Models.CashOperations;

namespace Lykke.Service.OperationsRepository.Client.Abstractions.CashOperations
{
    public interface ICashOperationsRepositoryClient
    {
        Task<CashOperationIdResponse> RegisterAsync(CashInOutOperation operation);
        Task<CashOperationsResponse> GetAsync(string clientId);
        Task<CashOperationResponse> GetAsync(string clientId, string recordId);
        Task UpdateBlockchainHashAsync(string clientId, string id, string hash);
        Task SetBtcTransaction(string clientId, string id, string bcnTransactionId);
        Task SetIsSettledAsync(string clientId, string id, bool offchain);
        Task<CashOperationsResponse> GetByHashAsync(string blockchainHash);
        Task<CashOperationsResponse> GetByMultisigAsync(string multisig);
        Task<CashOperationsResponse> GetByMultisigsAsync(string[] multisigs);
    }
}