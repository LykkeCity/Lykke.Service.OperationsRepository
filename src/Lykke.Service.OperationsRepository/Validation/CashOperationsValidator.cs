using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lykke.Service.OperationsRepository.Core.CashOperations;

namespace Lykke.Service.OperationsRepository.Validation
{
    public class CashOperationsValidator
    {
        public static bool ValidateOperation(CashInOutOperation operation)
        {
            return operation != null;
        }

        public static bool ValidateBlockchainHash(string hash)
        {
            return !string.IsNullOrWhiteSpace(hash);
        }

        public static bool ValidateTransactionId(string transactionId)
        {
            return !string.IsNullOrWhiteSpace(transactionId);
        }

        public static bool ValidateOrderId(string orderId)
        {
            return !string.IsNullOrWhiteSpace(orderId);
        }

        public static bool ValidateClientTrades(ClientTrade[] trades)
        {
            if (trades.Length == 0) return false;

            return trades.All(t => t != null);
        }

        public static bool ValidateTransferEvent(ITransferEvent transferEvent)
        {
            return transferEvent != null;
        }

        public static bool ValidateCashOutRequest(ICashOutRequest request)
        {
            return request != null;
        }

        public static bool ValidatePaymentSystem(PaymentSystem system)
        {
            return system != null;
        }

        public static bool ValidatePaymentFields(object fields)
        {
            return fields != null;
        }
    }
}
