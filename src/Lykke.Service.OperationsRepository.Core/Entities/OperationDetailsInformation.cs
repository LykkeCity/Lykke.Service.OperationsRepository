using Lykke.Service.OperationsRepository.Core.OperationsDetails;
using System;

namespace Lykke.Service.OperationsRepository.Core.Entities
{
    public class OperationDetailsInformation : IOperationDetailsInformation
    {
        public string Id { get; set; }
        public string TransactionId { get; set; }
        public string ClientId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Comment { get; set; }
    }
}
