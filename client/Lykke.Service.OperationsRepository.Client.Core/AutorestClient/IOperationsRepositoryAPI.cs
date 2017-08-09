// Code generated by Microsoft (R) AutoRest Code Generator 1.1.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace Lykke.Service.OperationsRepository.AutorestClient
{
    using Lykke.Service;
    using Lykke.Service.OperationsRepository;
    using Microsoft.Rest;
    using Models;
    using Newtonsoft.Json;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// </summary>
    public partial interface IOperationsRepositoryAPI : System.IDisposable
    {
        /// <summary>
        /// The base URI of the service.
        /// </summary>
        System.Uri BaseUri { get; set; }

        /// <summary>
        /// Gets or sets json serialization settings.
        /// </summary>
        JsonSerializerSettings SerializationSettings { get; }

        /// <summary>
        /// Gets or sets json deserialization settings.
        /// </summary>
        JsonSerializerSettings DeserializationSettings { get; }


        /// <summary>
        /// Gets the ICashOperations.
        /// </summary>
        ICashOperations CashOperations { get; }

        /// <summary>
        /// Gets the ICashOutAttemptOperations.
        /// </summary>
        ICashOutAttemptOperations CashOutAttemptOperations { get; }

        /// <summary>
        /// Gets the IClientTradeOperations.
        /// </summary>
        IClientTradeOperations ClientTradeOperations { get; }

        /// <summary>
        /// Gets the ITransferOperations.
        /// </summary>
        ITransferOperations TransferOperations { get; }

        /// <param name='fromParameter'>
        /// </param>
        /// <param name='to'>
        /// </param>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        Task<HttpOperationResponse<object>> ClientTradeOperationsScanByDtWithHttpMessagesAsync(System.DateTime? fromParameter = default(System.DateTime?), System.DateTime? to = default(System.DateTime?), Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Checks service is alive
        /// </summary>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        Task<HttpOperationResponse<IsAliveResponse>> IsAliveWithHttpMessagesAsync(Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));

    }
}
