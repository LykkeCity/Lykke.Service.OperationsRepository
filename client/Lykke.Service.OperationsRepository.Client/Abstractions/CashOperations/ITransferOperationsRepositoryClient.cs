using System.Threading.Tasks;
using Lykke.Service.OperationsRepository.AutorestClient.Models;
using Lykke.Service.OperationsRepository.Client.Models.CashOperations;

namespace Lykke.Service.OperationsRepository.Client.Abstractions.CashOperations
{
    public interface ITransferOperationsRepositoryClient
    {
        Task<TransferEventResponse> RegisterAsync(TransferEvent transferEvent);
        Task<TransferEventsResponse> GetAsync(string clientId);
        Task<TransferEventResponse> GetAsync(string clientId, string id);
        Task UpdateBlockChainHashAsync(string clientId, string id, string blockChainHash);
        Task SetBtcTransactionAsync(string clientId, string id, string btcTransaction);
        Task SetIsSettledIfExistsAsync(string clientId, string id, bool offchain);
        Task<TransferEventsResponse> GetByHashAsync(string blockchainHash);
        Task<TransferEventsResponse> GetByMultisigAsync(string multisig);
        Task<TransferEventsResponse> GetByMultisigsAsync(string[] multisigs);
    }
}