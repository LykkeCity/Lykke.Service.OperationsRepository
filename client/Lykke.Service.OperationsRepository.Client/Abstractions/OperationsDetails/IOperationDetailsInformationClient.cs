using Lykke.Service.OperationsRepository.AutorestClient.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lykke.Service.OperationsRepository.Client.Abstractions.OperationsDetails
{
    public  interface IOperationDetailsInformationClient
    {
        Task<string> RegisterAsync(OperationDetailsInformation operationDetailsInfo);
        Task CreateAsync(OperationDetailsInformation opoperationDetailsInfoeration);
        Task<IEnumerable<OperationDetailsInformation>> GetAsync(string clientId);
        Task<OperationDetailsInformation> GetAsync(string clientId, string recordId);
    }
}
