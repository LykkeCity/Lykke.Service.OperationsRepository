using System;
using System.Collections.Generic;
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
    }
}
