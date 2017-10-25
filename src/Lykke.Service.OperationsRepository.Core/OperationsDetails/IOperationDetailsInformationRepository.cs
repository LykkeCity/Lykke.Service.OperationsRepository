using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lykke.Service.OperationsRepository.Core.OperationsDetails
{
    public interface IOperationDetailsInformationRepository
    {
        Task<string> RegisterAsync(IOperationDetailsInformation operation);
        Task CreateAsync(IOperationDetailsInformation operation);
        Task<IEnumerable<IOperationDetailsInformation>> GetAsync(string clientId);
        Task<IOperationDetailsInformation> GetAsync(string clientId, string id);
    }
}
