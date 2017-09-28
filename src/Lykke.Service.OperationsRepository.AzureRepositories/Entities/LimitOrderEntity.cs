using Lykke.Service.OperationsRepository.Core.Exchange;
using System;

namespace Lykke.Service.OperationsRepository.AzureRepositories.Entities
{
    public class LimitOrderEntity : BaseEntity, ILimitOrder
    {

        public static class ByOrderId
        {
            public static string GeneratePartitionKey()
            {
                return "OrderId";
            }

            public static string GenerateRowKey(string orderId)
            {
                return orderId;
            }

            public static LimitOrderEntity Create(ILimitOrder limitOrder)
            {
                var entity = CreateNew(limitOrder);
                entity.RowKey = GenerateRowKey(limitOrder.Id);
                entity.PartitionKey = GeneratePartitionKey();
                return entity;
            }
        }

        public static class ByClientId
        {
            public static string GeneratePartitionKey(string clientId)
            {
                return clientId;
            }

            public static string GenerateRowKey(string orderId)
            {
                return orderId;
            }

            public static LimitOrderEntity Create(ILimitOrder limitOrder)
            {
                var entity = CreateNew(limitOrder);
                entity.RowKey = GenerateRowKey(limitOrder.Id);
                entity.PartitionKey = GeneratePartitionKey(limitOrder.ClientId);
                return entity;
            }
        }

        public static class ByClientIdActive
        {
            public static string GeneratePartitionKey(string clientId)
            {
                return "Active_" + clientId;
            }

            public static string GenerateRowKey(string orderId)
            {
                return orderId;
            }

            public static LimitOrderEntity Create(ILimitOrder limitOrder)
            {
                var entity = CreateNew(limitOrder);
                entity.RowKey = GenerateRowKey(limitOrder.Id);
                entity.PartitionKey = GeneratePartitionKey(limitOrder.ClientId);
                return entity;
            }
        }

        public static LimitOrderEntity CreateNew(ILimitOrder limitOrder)
        {
            return new LimitOrderEntity
            {
                AssetPairId = limitOrder.AssetPairId,
                ClientId = limitOrder.ClientId,
                CreatedAt = limitOrder.CreatedAt,
                Id = limitOrder.Id,
                Price = limitOrder.Price,
                Status = limitOrder.Status,
                Straight = limitOrder.Straight,
                Volume = limitOrder.Volume,
                RemainingVolume = limitOrder.RemainingVolume,
                MatchingId = limitOrder.MatchingId
            };
        }

        public DateTime CreatedAt { get; set; }

        public double Price { get; set; }
        public string AssetPairId { get; set; }

        public double Volume { get; set; }

        public string Status { get; set; }
        public bool Straight { get; set; }
        public string Id { get; set; }
        public string ClientId { get; set; }

        public double RemainingVolume { get; set; }
        public string MatchingId { get; set; }
    }
}
