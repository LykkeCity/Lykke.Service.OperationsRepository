using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AzureStorage;
using Lykke.Service.OperationsRepository.Contract;
using Lykke.Service.OperationsRepository.Contract.Abstractions;
using Lykke.Service.OperationsRepository.Core.CashOperations;

namespace Lykke.Service.OperationsRepository.AzureRepositories.CashOperations
{
    public class LimitTradeEventEntity : BaseEntity, ILimitTradeEvent
    {
        public string Id => RowKey;
        public string OrderId { get; set; }
        public string ClientId { get; set; }
        public DateTime CreatedDt { get; set; }
        public OrderType OrderType { get; set; }
        public double Volume { get; set; }
        public string AssetId { get; set; }
        public string AssetPair { get; set; }
        public double Price { get; set; }
        public OrderStatus Status { get; set; }
        public bool IsHidden { get; set; }

        public static string GeneratePartitionKey(string clientId)
        {
            return clientId;
        }

        public static LimitTradeEventEntity Create(string orderId, string clientId, OrderType type, double volume,
            string assetId,
            string assetPair, double price, OrderStatus status, DateTime dateTime)
        {
            return new LimitTradeEventEntity
            {
                PartitionKey = GeneratePartitionKey(clientId),
                RowKey = Guid.NewGuid().ToString(),
                Status = status,
                ClientId = clientId,
                OrderType = type,
                Volume = volume,
                AssetId = assetId,
                AssetPair = assetPair,
                CreatedDt = dateTime,
                OrderId = orderId,
                Price = price
            };
        }
    }

    public class LimitTradeEventsRepository : ILimitTradeEventsRepository
    {
        private readonly INoSQLTableStorage<LimitTradeEventEntity> _storage;

        public LimitTradeEventsRepository(INoSQLTableStorage<LimitTradeEventEntity> storage)
        {
            _storage = storage;
        }

        public async Task<ILimitTradeEvent> CreateEvent(string orderId, string clientId, OrderType type, double volume, string assetId,
            string assetPair, double price, OrderStatus status, DateTime dateTime)
        {
            var newEntity = LimitTradeEventEntity.Create(orderId, clientId, type, volume, assetId,
                assetPair, price, status, dateTime);

            await _storage.InsertAsync(newEntity);

            return newEntity;
        }

        public async Task<IEnumerable<ILimitTradeEvent>> GetEventsAsync(string clientId)
        {
            return await _storage.GetDataAsync(LimitTradeEventEntity.GeneratePartitionKey(clientId));
        }
    }
}
