using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzureStorage;
using Lykke.Service.OperationsRepository.Contract;
using Lykke.Service.OperationsRepository.Core.CashOperations;

namespace Lykke.Service.OperationsRepository.AzureRepositories.CashOperations
{
    public class LimitOrderEntity : BaseEntity, ILimitOrder
    {
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

    public class LimitOrdersRepository : ILimitOrdersRepository
    {
        private readonly INoSQLTableStorage<LimitOrderEntity> _tableStorage;

        public LimitOrdersRepository(INoSQLTableStorage<LimitOrderEntity> tableStorage)
        {
            _tableStorage = tableStorage;
        }

        public async Task<ILimitOrder> GetOrderAsync(string id, string clientId)
        {
            return await _tableStorage.GetDataAsync(LimitOrderEntity.ByClientId.GeneratePartitionKey(clientId), id);
        }

        public async Task InOrderBookAsync(ILimitOrder limitOrder)
        {
            limitOrder.Status = OrderStatus.InOrderBook.ToString();

            var byClientEntity = LimitOrderEntity.ByClientId.Create(limitOrder);
            var byClientEntityActive = LimitOrderEntity.ByClientIdActive.Create(limitOrder);

            await _tableStorage.InsertOrMergeAsync(byClientEntity);
            await _tableStorage.InsertOrMergeAsync(byClientEntityActive);
        }

        public Task RemoveAsync(string orderId, string clientId)
        {
            return Task.WhenAll(
                _tableStorage.DeleteAsync(LimitOrderEntity.ByClientId.GeneratePartitionKey(clientId), orderId),
                _tableStorage.DeleteAsync(LimitOrderEntity.ByClientIdActive.GeneratePartitionKey(clientId), orderId)
            );
        }

        public async Task FinalizeAsync(ILimitOrder order, OrderStatus status)
        {
            order.Status = status.ToString();

            var byClientEntity = LimitOrderEntity.ByClientId.Create(order);

            await _tableStorage.InsertOrMergeAsync(byClientEntity);

            await _tableStorage.DeleteAsync(LimitOrderEntity.ByClientIdActive.GeneratePartitionKey(order.ClientId), order.Id);
        }

        public async Task CancelAsync(ILimitOrder order)
        {
            order.Status = OrderStatus.Cancelled.ToString();

            var byClientEntity = LimitOrderEntity.ByClientId.Create(order);

            await _tableStorage.InsertOrMergeAsync(byClientEntity);

            await _tableStorage.DeleteAsync(LimitOrderEntity.ByClientIdActive.GeneratePartitionKey(order.ClientId), order.Id);
        }

        public async Task CancelMultipleAsync(IEnumerable<ILimitOrder> orders)
        {
            foreach (var order in orders)
            {
                order.Status = OrderStatus.Cancelled.ToString();
            }

            await _tableStorage.InsertOrMergeBatchAsync(orders.Select(LimitOrderEntity.ByClientId.Create));
            await _tableStorage.DeleteAsync(orders.Select(LimitOrderEntity.ByClientIdActive.Create).Select(x =>
            {
                x.ETag = "*";
                return x;
            }));
        }

        public async Task<IEnumerable<ILimitOrder>> GetActiveOrdersAsync(string clientId)
        {
            var partitionKey = LimitOrderEntity.ByClientIdActive.GeneratePartitionKey(clientId);

            return await _tableStorage.GetDataAsync(partitionKey);
        }

        public async Task<IEnumerable<ILimitOrder>> GetOrdersAsync(string clientId)
        {
            var partitionKey = LimitOrderEntity.ByClientId.GeneratePartitionKey(clientId);

            return await _tableStorage.GetDataAsync(partitionKey);
        }
    }
}