﻿using Lykke.Service.OperationsRepository.Contract;
using Lykke.Service.OperationsRepository.Contract.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lykke.Service.OperationsRepository.Core.CashOperations
{
    public class TransferEvent : ITransferEvent
    {
        public string Id { get; set; }
        public string ClientId { get; set; }
        public DateTime DateTime { get; set; }
        public bool IsHidden { get; set; }
        public string FromId { get; set; }
        public string AssetId { get; set; }
        public double Amount { get; set; }
        public string BlockChainHash { get; set; }
        public string Multisig { get; set; }
        public string TransactionId { get; set; }
        public string AddressFrom { get; set; }
        public string AddressTo { get; set; }
        public bool? IsSettled { get; set; }
        public TransactionStates State { get; set; }
        public double FeeSize { get; set; }
        public FeeType FeeType { get; set; }

        public static TransferEvent CreateNew(string clientId, string clientMultiSig, string fromId, string assetId, double amount,
            string transactionId, string addressFrom, string addressTo, bool isHidden = false, TransactionStates state = TransactionStates.InProcessOffchain)
        {
            return new TransferEvent
            {
                Id = Guid.NewGuid().ToString("N"),
                ClientId = clientId,
                DateTime = DateTime.UtcNow,
                FromId = fromId,
                AssetId = assetId,
                Amount = amount,
                TransactionId = transactionId,
                IsHidden = isHidden,
                AddressFrom = addressFrom,
                AddressTo = addressTo,
                Multisig = clientMultiSig,
                IsSettled = false,
                State = state
            };
        }

        public static TransferEvent CreateNewTransferAll(string clientId, string transactionId, string srcAddress)
        {
            return new TransferEvent
            {
                Id = Guid.NewGuid().ToString("N"),
                ClientId = clientId,
                DateTime = DateTime.UtcNow,
                TransactionId = transactionId,
                IsHidden = true,
                IsSettled = false,
                Multisig = srcAddress
            };
        }
    }

    public interface ITransferEventsRepository
    {
        Task<ITransferEvent> RegisterAsync(ITransferEvent transferEvent);

        Task<IEnumerable<ITransferEvent>> GetAsync(string clientId);
        Task<ITransferEvent> GetAsync(string clientId, string id);

        Task<ITransferEvent> UpdateBlockChainHashAsync(string clientId, string id, string blockChainHash);

        Task<ITransferEvent> SetBtcTransactionAsync(string clientId, string id, string btcTransaction);
        Task<ITransferEvent> SetIsSettledIfExistsAsync(string clientId, string id, bool offchain);

        Task<IEnumerable<ITransferEvent>> GetByHashAsync(string blockchainHash);
        Task<IEnumerable<ITransferEvent>> GetByMultisigAsync(string multisig);
        Task<IEnumerable<ITransferEvent>> GetByMultisigsAsync(string[] multisigs);
        Task GetDataByChunksAsync(Func<IEnumerable<ITransferEvent>, Task> chunk);
    }

}
