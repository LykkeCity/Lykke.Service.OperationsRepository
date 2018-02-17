﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzureStorage;
using Lykke.Service.OperationsRepository.Contract;
using Lykke.Service.OperationsRepository.Contract.Abstractions;
using Lykke.Service.OperationsRepository.Core.CashOperations;
using Microsoft.WindowsAzure.Storage.Table;

namespace Lykke.Service.OperationsRepository.AzureRepositories.CashOperations
{

    public class ClientTradeEntity : TableEntity, IClientTrade
    {
        public string Id => RowKey;

        public DateTime DateTime { get; set; }
        public bool IsHidden { get; set; }
        public string LimitOrderId { get; set; }
        public string MarketOrderId { get; set; }
        public double Price { get; set; }
        public DateTime? DetectionTime { get; set; }
        public int Confirmations { get; set; }
        public string OppositeLimitOrderId { get; set; }
        public bool IsLimitOrderResult { get; set; }
        public double Amount => Volume;
        public string AssetId { get; set; }
        public string AssetPairId { get; set; }
        public string BlockChainHash { get; set; }
        public string Multisig { get; set; }
        public string TransactionId { get; set; }
        public string AddressFrom { get; set; }
        public string AddressTo { get; set; }
        public bool? IsSettled { get; set; }
        public string StateField { get; set; }
        public TransactionStates State
        {
            get
            {
                TransactionStates type = TransactionStates.InProcessOnchain;
                if (!string.IsNullOrEmpty(StateField))
                {
                    Enum.TryParse(StateField, out type);
                }
                return type;
            }
            set { StateField = value.ToString(); }
        }
        public double Volume { get; set; }
        public string ClientId { get; set; }
        public double FeeSize { get; set; }
        public FeeType FeeType { get; set; }
        public string FeeTypeText
        {
            get
            {
                return FeeType.ToString();
            }

            set
            {
                if (Enum.TryParse(value, out FeeType tmpType)) FeeType = tmpType;
                else FeeType = FeeType.Unknown;
            }
        }

        public static class ByClientId
        {
            public static string GeneratePartitionKey(string clientId)
            {
                return clientId;
            }

            public static string GenerateRowKey(string tradeId)
            {
                return tradeId;
            }

            public static ClientTradeEntity Create(IClientTrade src)
            {
                var entity = CreateNew(src);
                entity.RowKey = GenerateRowKey(src.Id);
                entity.PartitionKey = GeneratePartitionKey(src.ClientId);
                return entity;
            }
        }        

        public static class ByDt
        {
            public static string GeneratePartitionKey()
            {
                return "dt";
            }

            public static string GenerateRowKey(string tradeId)
            {
                return tradeId;
            }

            public static string GetRowKeyPart(DateTime dt)
            {
                //ME rowkey format e.g. 20160812180446244_00130
                return $"{dt.Year}{dt.Month.ToString("00")}{dt.Day.ToString("00")}{dt.Hour.ToString("00")}{dt.Minute.ToString("00")}";
            }

            public static ClientTradeEntity Create(IClientTrade src)
            {
                var entity = CreateNew(src);
                entity.RowKey = GenerateRowKey(src.Id);
                entity.PartitionKey = GeneratePartitionKey();
                return entity;
            }
        }

        public static class ByOrder
        {
            public static string GeneratePartitionKey(string orderId)
            {
                return orderId;
            }

            public static string GenerateRowKey(string tradeId)
            {
                return tradeId;
            }

            public static ClientTradeEntity Create(IClientTrade src)
            {
                var entity = CreateNew(src);
                entity.RowKey = GenerateRowKey(src.Id);
                entity.PartitionKey = GeneratePartitionKey(src.LimitOrderId);
                return entity;
            }
        }

        public static ClientTradeEntity CreateNew(IClientTrade src)
        {
            return new ClientTradeEntity
            {
                ClientId = src.ClientId,
                AssetId = src.AssetId,
                AssetPairId = src.AssetPairId,
                DateTime = src.DateTime,
                LimitOrderId = src.LimitOrderId,
                MarketOrderId = src.MarketOrderId,
                Volume = src.Amount,
                BlockChainHash = src.BlockChainHash,
                Price = src.Price,
                IsHidden = src.IsHidden,
                AddressFrom = src.AddressFrom,
                AddressTo = src.AddressTo,
                Multisig = src.Multisig,
                DetectionTime = src.DetectionTime,
                Confirmations = src.Confirmations,
                IsSettled = src.IsSettled,
                State = src.State,
                TransactionId = src.TransactionId,
                IsLimitOrderResult = src.IsLimitOrderResult,
                OppositeLimitOrderId = src.OppositeLimitOrderId,
                FeeSize = src.FeeSize,
                FeeType = src.FeeType
            };
        }
    }

    public class ClientTradesRepository : IClientTradesRepository
    {
        private readonly INoSQLTableStorage<ClientTradeEntity> _tableStorage;

        public ClientTradesRepository(INoSQLTableStorage<ClientTradeEntity> tableStorage)
        {
            _tableStorage = tableStorage;
        }

        public async Task<IClientTrade[]> SaveAsync(params IClientTrade[] clientTrades)
        {
            List<IClientTrade> inserted = new List<IClientTrade>();
            foreach (var trade in clientTrades)
            {
                var byClientId = ClientTradeEntity.ByClientId.Create(trade);
                var insertByClientIdTask = _tableStorage.InsertOrMergeAsync(byClientId);                
                var insertbyDtTask = _tableStorage.InsertOrMergeAsync(ClientTradeEntity.ByDt.Create(trade));
                var insertByOrderId = trade.IsLimitOrderResult ? _tableStorage.InsertOrMergeAsync(ClientTradeEntity.ByOrder.Create(trade)) : Task.CompletedTask;

                await insertByClientIdTask;                
                await insertbyDtTask;
                await insertByOrderId;

                inserted.Add(byClientId);
            }

            return inserted.ToArray();
        }

        public async Task<IEnumerable<IClientTrade>> GetAsync(string clientId)
        {
            var partitionKey = ClientTradeEntity.ByClientId.GeneratePartitionKey(clientId);
            return await _tableStorage.GetDataAsync(partitionKey);
        }

        public async Task<IEnumerable<IClientTrade>> GetAsync(DateTime @from, DateTime to)
        {
            // ToDo - Have to optimize according to the task: https://lykkex.atlassian.net/browse/LWDEV-131
            return await _tableStorage.GetDataAsync(itm => @from <= itm.DateTime && itm.DateTime < to);
        }

        public async Task<IClientTrade> GetAsync(string clientId, string recordId)
        {
            var partitionKey = ClientTradeEntity.ByClientId.GeneratePartitionKey(clientId);
            var rowKey = ClientTradeEntity.ByClientId.GenerateRowKey(recordId);

            return await _tableStorage.GetDataAsync(partitionKey, rowKey);
        }

        public async Task<IClientTrade> UpdateBlockChainHashAsync(string clientId, string recordId, string hash)
        {
            var partitionKey = ClientTradeEntity.ByClientId.GeneratePartitionKey(clientId);
            var rowKey = ClientTradeEntity.ByClientId.GenerateRowKey(recordId);

            var clientIdRecord = await _tableStorage.GetDataAsync(partitionKey, rowKey);
            
            var dtPartitionKey = ClientTradeEntity.ByDt.GeneratePartitionKey();
            var dtRowKey = ClientTradeEntity.ByDt.GenerateRowKey(recordId);


            var result = await _tableStorage.MergeAsync(partitionKey, rowKey, entity =>
            {
                entity.BlockChainHash = hash;
                entity.State = TransactionStates.SettledOnchain;
                return entity;
            });
            
            await _tableStorage.MergeAsync(dtPartitionKey, dtRowKey, entity =>
            {
                entity.BlockChainHash = hash;
                entity.State = TransactionStates.SettledOnchain;
                return entity;
            });

            if (clientIdRecord.IsLimitOrderResult)
            {
                var orderPartition = ClientTradeEntity.ByOrder.GeneratePartitionKey(clientIdRecord.LimitOrderId);
                var orderRowKey = ClientTradeEntity.ByOrder.GenerateRowKey(recordId);

                await _tableStorage.MergeAsync(orderPartition, orderRowKey, entity =>
                {
                    entity.BlockChainHash = hash;
                    entity.State = TransactionStates.SettledOnchain;
                    return entity;
                });
            }

            return result;
        }

        public async Task<IClientTrade> SetDetectionTimeAndConfirmations(string clientId, string recordId, DateTime detectTime,
            int confirmations)
        {
            var partitionKey = ClientTradeEntity.ByClientId.GeneratePartitionKey(clientId);
            var rowKey = ClientTradeEntity.ByClientId.GenerateRowKey(recordId);

            var clientIdRecord = await _tableStorage.GetDataAsync(partitionKey, rowKey);

            if (clientIdRecord == null)
                return null;
            
            var dtPartitionKey = ClientTradeEntity.ByDt.GeneratePartitionKey();
            var dtRowKey = ClientTradeEntity.ByDt.GenerateRowKey(recordId);

            var result = await _tableStorage.MergeAsync(partitionKey, rowKey, entity =>
            {
                entity.DetectionTime = detectTime;
                entity.Confirmations = confirmations;
                entity.State = TransactionStates.SettledOnchain;
                return entity;
            });
            
            await _tableStorage.MergeAsync(dtPartitionKey, dtRowKey, entity =>
            {
                entity.DetectionTime = detectTime;
                entity.Confirmations = confirmations;
                entity.State = TransactionStates.SettledOnchain;
                return entity;
            });

            return result;
        }

        public async Task<IClientTrade> SetBtcTransactionAsync(string clientId, string recordId, string btcTransactionId)
        {
            var partitionKey = ClientTradeEntity.ByClientId.GeneratePartitionKey(clientId);
            var rowKey = ClientTradeEntity.ByClientId.GenerateRowKey(recordId);

            var clientIdRecord = await _tableStorage.GetDataAsync(partitionKey, rowKey);
            
            var dtPartitionKey = ClientTradeEntity.ByDt.GeneratePartitionKey();
            var dtRowKey = ClientTradeEntity.ByDt.GenerateRowKey(recordId);

            var result = await _tableStorage.MergeAsync(partitionKey, rowKey, entity =>
            {
                entity.TransactionId = btcTransactionId;
                return entity;
            });
            
            await _tableStorage.MergeAsync(dtPartitionKey, dtRowKey, entity =>
            {
                entity.TransactionId = btcTransactionId;
                return entity;
            });

            return result;
        }

        public async Task<IClientTrade> SetIsSettledAsync(string clientId, string id, bool offchain)
        {
            var partitionKey = ClientTradeEntity.ByClientId.GeneratePartitionKey(clientId);
            var rowKey = ClientTradeEntity.ByClientId.GenerateRowKey(id);

            var clientIdRecord = await _tableStorage.GetDataAsync(partitionKey, rowKey);
            if (clientIdRecord == null)
                return null;
            
            var dtPartitionKey = ClientTradeEntity.ByDt.GeneratePartitionKey();
            var dtRowKey = ClientTradeEntity.ByDt.GenerateRowKey(id);

            var byOrderTask = Task.CompletedTask;
            if (clientIdRecord.IsLimitOrderResult)
            {
                var orderPartition = ClientTradeEntity.ByOrder.GeneratePartitionKey(clientIdRecord.LimitOrderId);
                var orderRowKey = ClientTradeEntity.ByOrder.GenerateRowKey(id);
                byOrderTask = _tableStorage.MergeAsync(orderPartition, orderRowKey, entity =>
                {
                    if (offchain)
                        entity.State = TransactionStates.SettledOffchain;
                    else
                        entity.IsSettled = true;
                    return entity;
                });
            }

            var result = await _tableStorage.MergeAsync(partitionKey, rowKey, entity =>
            {
                if (offchain)
                    entity.State = TransactionStates.SettledOffchain;
                else
                    entity.IsSettled = true;
                return entity;
            });
            
            await _tableStorage.MergeAsync(dtPartitionKey, dtRowKey, entity =>
            {
                if (offchain)
                    entity.State = TransactionStates.SettledOffchain;
                else
                    entity.IsSettled = true;
                return entity;
            });

            return result;
        }        

        public async Task<IEnumerable<IClientTrade>> GetByOrderAsync(string orderId)
        {
            return await _tableStorage.GetDataAsync(ClientTradeEntity.ByOrder.GeneratePartitionKey(orderId));
        }

        public async Task<IEnumerable<IClientTrade>> ScanByDtAsync(DateTime @from, DateTime to)
        {
            var rangeQuery = AzureStorageUtils.QueryGenerator<ClientTradeEntity>
                .RowKeyOnly.BetweenQuery(ClientTradeEntity.ByDt.GetRowKeyPart(from),
                    ClientTradeEntity.ByDt.GetRowKeyPart(to), ToIntervalOption.IncludeTo);

            var result = new List<IClientTrade>();
            await _tableStorage.ExecuteAsync(rangeQuery, yieldResult =>
            {
                result.AddRange(yieldResult);
            });

            return result;
        }        
    }
}