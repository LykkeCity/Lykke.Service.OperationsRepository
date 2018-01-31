﻿using System;

namespace Lykke.Service.OperationsRepository.Contract.Abstractions
{
    /// <summary>
    /// Base cash operation
    /// </summary>
    public interface IBaseCashOperation
    {
        /// <summary>
        /// Record Id
        /// </summary>
        string Id { get; }

        string AssetId { get; }

        string ClientId { get; }

        double Amount { get; }

        DateTime DateTime { get; }

        bool IsHidden { get; }

        decimal FeeSize { get; }

        FeeType FeeType { get; }
    }
}