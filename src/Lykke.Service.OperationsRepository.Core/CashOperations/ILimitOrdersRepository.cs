﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lykke.Service.OperationsRepository.Contract;

namespace Lykke.Service.OperationsRepository.Core.CashOperations
{
    public interface IOrderBase
    {
        double RemainingVolume { get; set; }
        string MatchingId { get; set; }
    }

    public interface ILimitOrder : IOrderBase
    {
        string Id { get; }
        string ClientId { get; set; }
        DateTime CreatedAt { get; set; }
        double Volume { get; set; }
        double Price { get; set; }
        string AssetPairId { get; set; }
        string Status { get; set; }
        bool Straight { get; set; }
    }

    public class MatchedOrder
    {
        public string Id { get; set; }
        public double Volume { get; set; }

        internal static MatchedOrder Create(ILimitOrder orderBase, double volume)
        {
            return new MatchedOrder
            {
                Id = orderBase.Id,
                Volume = volume
            };
        }
    }

    public class LimitOrder : ILimitOrder
    {
        public DateTime CreatedAt { get; set; }
        public DateTime MatchedAt { get; set; }
        public string Id { get; set; }
        public List<MatchedOrder> MatchedOrders { get; set; }
        public string ClientId { get; set; }
        public string BaseAsset { get; set; }
        public string AssetPairId { get; set; }
        public string Status { get; set; }
        public bool Straight { get; set; }
        public OrderType OrderAction { get; set; }
        public string BlockChain { get; set; }
        public double Volume { get; set; }
        public double Price { get; set; }
        public double RemainingVolume { get; set; }
        public string MatchingId { get; set; }

        public static LimitOrder Create(string id, string clientId, string assetPairId, double volume, double price, double remainigVolume)
        {
            return new LimitOrder
            {
                Id = id,
                ClientId = clientId,
                AssetPairId = assetPairId,
                Volume = volume,
                Price = price,
                RemainingVolume = remainigVolume,
                CreatedAt = DateTime.UtcNow
            };
        }
    }

    public interface ILimitOrdersRepository
    {
        Task<ILimitOrder> GetOrderAsync(string id, string clientId);

        Task InOrderBookAsync(ILimitOrder limitOrder);
        Task RemoveAsync(string orderId, string clientId);
        Task FinalizeAsync(ILimitOrder order, OrderStatus status);
        Task CancelAsync(ILimitOrder order);

        Task<IEnumerable<ILimitOrder>> GetActiveOrdersAsync(string clientId);
        Task<IEnumerable<ILimitOrder>> GetOrdersAsync(string clientId);
    }
}