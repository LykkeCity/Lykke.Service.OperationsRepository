using System.Threading.Tasks;
using Lykke.Service.OperationsRepository.Client.Models.CashOperations;

namespace Lykke.Service.OperationsRepository.Client.Abstractions.CashOperations
{
    public interface ITestAsyncMethods
    {
        Task<ClientTradesResponse> GetByMultisigAsync(string multisig);
        Task<ClientTradesResponse> GetByMultisigsAsync(string[] multisigs);
    }
}