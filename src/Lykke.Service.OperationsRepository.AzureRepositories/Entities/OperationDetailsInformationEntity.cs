using Lykke.Service.OperationsRepository.AzureRepositories.CashOperations;
using Lykke.Service.OperationsRepository.Core.OperationsDetails;
using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace Lykke.Service.OperationsRepository.AzureRepositories.Entities
{
    public class OperationDetailsInformationEntity : TableEntity, IOperationDetailsInformation
    {
        public string Id { get; set; }
        public string TransactionId { get; set; }
        public string ClientId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Comment { get; set; }

        public static OperationDetailsInformationEntity Create(IOperationDetailsInformation src)
        {
            return new OperationDetailsInformationEntity
            {
                PartitionKey = GeneratePartitionKey(src.ClientId),
                RowKey = GenerateRowKey(src.Id),
                CreatedAt = src.CreatedAt,
                ClientId = src.ClientId,
                Comment = src.Comment,
                TransactionId = src.TransactionId
            };
        }

        public static string GeneratePartitionKey(string clientId)
        {
            return clientId;
        }

        internal static string GenerateRowKey(string id)
        {
            return id;
        }
    }
}
