﻿using System;
using System.Linq;

namespace Lykke.Service.OperationsRepository.Validation
{
    public class CommonValidator
    {
        public static bool ValidateClientId(string clientId)
        {
            return !string.IsNullOrWhiteSpace(clientId);
        }

        public static bool ValidateLimitOrderId(string orderId)
        {
            return !string.IsNullOrWhiteSpace(orderId);
        }

        public static bool ValidateAssetPairId(string assetPairId)
        {
            return !string.IsNullOrWhiteSpace(assetPairId);
        }

        public static bool ValidateRecordId(string recordId)
        {
            return !string.IsNullOrWhiteSpace(recordId);
        }

        public static bool ValidateRequestId(string requestId)
        {
            return !string.IsNullOrWhiteSpace(requestId);
        }

        public static bool ValidateRowKeyId(string id)
        {
            return !string.IsNullOrWhiteSpace(id);
        }

        public static bool ValidateMultisig(string multisig)
        {
            return !string.IsNullOrWhiteSpace(multisig);
        }

        public static bool ValidateMultisig(string[] multisig)
        {
            if (multisig.Length == 0) return false;

            return !multisig.Any(string.IsNullOrWhiteSpace);
        }

        public static bool ValidateDateTime(DateTime dt)
        {
            return dt > DateTime.MinValue;
        }

        public static bool ValidatePeriod(DateTime from, DateTime to)
        {
            return ValidateDateTime(from) && ValidateDateTime(to) && from <= to;
        }
    }
}
