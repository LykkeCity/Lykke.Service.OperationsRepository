using System;

namespace Lykke.Service.OperationsRepository.Contract.History
{
    /// <summary>
    /// DTO to publish to exchange
    /// </summary>
    public class OperationsHistoryMessage
    {
        /// <summary>
        /// Identifier of the operation
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Timestamp of the operation
        /// </summary>
        public DateTime DateTime { get; set; }

        /// <summary>
        /// Total amount of the operation
        /// </summary>
        public double Amount { get; set; }

        /// <summary>
        /// Currency of the operation
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Operation type
        /// </summary>
        public string OpType { get; set; }

        /// <summary>
        /// Client identifier
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// Json object serialized to string
        /// </summary>
        public string Data { get; set; }
    }
}