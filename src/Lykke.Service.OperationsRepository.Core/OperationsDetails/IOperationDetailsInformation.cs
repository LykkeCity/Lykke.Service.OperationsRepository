using System;

namespace Lykke.Service.OperationsRepository.Core.OperationsDetails
{
    public interface IOperationDetailsInformation
    {
        string Id { get; }
        string TransactionId { get; set; }
        string ClientId { get; set; }
        DateTime CreatedAt { get; set; }
        string Comment { get; set; }
    }
}
