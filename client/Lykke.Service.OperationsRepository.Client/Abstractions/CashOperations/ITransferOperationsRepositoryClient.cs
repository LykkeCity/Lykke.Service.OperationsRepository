using System.Collections.Generic;
using System.Threading.Tasks;
using Lykke.Service.OperationsRepository.AutorestClient.Models;

namespace Lykke.Service.OperationsRepository.Client.Abstractions.CashOperations
{
    public interface ITransferOperationsRepositoryClient
    {
        Task<TransferEvent> RegisterAsync(TransferEvent transferEvent);
        Task<IEnumerable<TransferEvent>> GetAsync(string clientId);
        Task<TransferEvent> GetAsync(string clientId, string id);
        Task UpdateBlockChainHashAsync(string clientId, string id, string blockChainHash);
        Task SetBtcTransactionAsync(string clientId, string id, string btcTransaction);
        Task SetIsSettledIfExistsAsync(string clientId, string id, bool offchain);
        Task<IEnumerable<TransferEvent>> GetByHashAsync(string blockchainHash);
        Task<IEnumerable<TransferEvent>> GetByMultisigAsync(string multisig);
        Task<IEnumerable<TransferEvent>> GetByMultisigsAsync(string[] multisigs);
    }
}