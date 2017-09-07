// Code generated by Microsoft (R) AutoRest Code Generator 1.1.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace Lykke.Service.OperationsRepository.AutorestClient
{
    using Lykke.Service;
    using Lykke.Service.OperationsRepository;
    using Models;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Extension methods for CashOutAttemptOperations.
    /// </summary>
    public static partial class CashOutAttemptOperationsExtensions
    {
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='request'>
            /// </param>
            public static object InsertRequest(this ICashOutAttemptOperations operations, InsertRequestModel request = default(InsertRequestModel))
            {
                return operations.InsertRequestAsync(request).GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='request'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<object> InsertRequestAsync(this ICashOutAttemptOperations operations, InsertRequestModel request = default(InsertRequestModel), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.InsertRequestWithHttpMessagesAsync(request, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            public static object GetAllAttempts(this ICashOutAttemptOperations operations)
            {
                return operations.GetAllAttemptsAsync().GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<object> GetAllAttemptsAsync(this ICashOutAttemptOperations operations, CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.GetAllAttemptsWithHttpMessagesAsync(null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='clientId'>
            /// </param>
            /// <param name='requestId'>
            /// </param>
            /// <param name='hash'>
            /// </param>
            public static ErrorResponse SetBlockchainHash(this ICashOutAttemptOperations operations, string clientId = default(string), string requestId = default(string), string hash = default(string))
            {
                return operations.SetBlockchainHashAsync(clientId, requestId, hash).GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='clientId'>
            /// </param>
            /// <param name='requestId'>
            /// </param>
            /// <param name='hash'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<ErrorResponse> SetBlockchainHashAsync(this ICashOutAttemptOperations operations, string clientId = default(string), string requestId = default(string), string hash = default(string), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.SetBlockchainHashWithHttpMessagesAsync(clientId, requestId, hash, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='clientId'>
            /// </param>
            /// <param name='requestId'>
            /// </param>
            public static object SetPending(this ICashOutAttemptOperations operations, string clientId = default(string), string requestId = default(string))
            {
                return operations.SetPendingAsync(clientId, requestId).GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='clientId'>
            /// </param>
            /// <param name='requestId'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<object> SetPendingAsync(this ICashOutAttemptOperations operations, string clientId = default(string), string requestId = default(string), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.SetPendingWithHttpMessagesAsync(clientId, requestId, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='clientId'>
            /// </param>
            /// <param name='requestId'>
            /// </param>
            public static object SetConfirmed(this ICashOutAttemptOperations operations, string clientId = default(string), string requestId = default(string))
            {
                return operations.SetConfirmedAsync(clientId, requestId).GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='clientId'>
            /// </param>
            /// <param name='requestId'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<object> SetConfirmedAsync(this ICashOutAttemptOperations operations, string clientId = default(string), string requestId = default(string), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.SetConfirmedWithHttpMessagesAsync(clientId, requestId, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='clientId'>
            /// </param>
            /// <param name='requestId'>
            /// </param>
            public static object SetDocsRequested(this ICashOutAttemptOperations operations, string clientId = default(string), string requestId = default(string))
            {
                return operations.SetDocsRequestedAsync(clientId, requestId).GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='clientId'>
            /// </param>
            /// <param name='requestId'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<object> SetDocsRequestedAsync(this ICashOutAttemptOperations operations, string clientId = default(string), string requestId = default(string), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.SetDocsRequestedWithHttpMessagesAsync(clientId, requestId, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='clientId'>
            /// </param>
            /// <param name='requestId'>
            /// </param>
            public static object SetDeclined(this ICashOutAttemptOperations operations, string clientId = default(string), string requestId = default(string))
            {
                return operations.SetDeclinedAsync(clientId, requestId).GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='clientId'>
            /// </param>
            /// <param name='requestId'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<object> SetDeclinedAsync(this ICashOutAttemptOperations operations, string clientId = default(string), string requestId = default(string), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.SetDeclinedWithHttpMessagesAsync(clientId, requestId, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='clientId'>
            /// </param>
            /// <param name='requestId'>
            /// </param>
            public static object SetCanceledByClient(this ICashOutAttemptOperations operations, string clientId = default(string), string requestId = default(string))
            {
                return operations.SetCanceledByClientAsync(clientId, requestId).GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='clientId'>
            /// </param>
            /// <param name='requestId'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<object> SetCanceledByClientAsync(this ICashOutAttemptOperations operations, string clientId = default(string), string requestId = default(string), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.SetCanceledByClientWithHttpMessagesAsync(clientId, requestId, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='clientId'>
            /// </param>
            /// <param name='requestId'>
            /// </param>
            public static object SetCanceledByTimeout(this ICashOutAttemptOperations operations, string clientId = default(string), string requestId = default(string))
            {
                return operations.SetCanceledByTimeoutAsync(clientId, requestId).GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='clientId'>
            /// </param>
            /// <param name='requestId'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<object> SetCanceledByTimeoutAsync(this ICashOutAttemptOperations operations, string clientId = default(string), string requestId = default(string), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.SetCanceledByTimeoutWithHttpMessagesAsync(clientId, requestId, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='clientId'>
            /// </param>
            /// <param name='requestId'>
            /// </param>
            public static object SetProcessed(this ICashOutAttemptOperations operations, string clientId = default(string), string requestId = default(string))
            {
                return operations.SetProcessedAsync(clientId, requestId).GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='clientId'>
            /// </param>
            /// <param name='requestId'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<object> SetProcessedAsync(this ICashOutAttemptOperations operations, string clientId = default(string), string requestId = default(string), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.SetProcessedWithHttpMessagesAsync(clientId, requestId, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='clientId'>
            /// </param>
            /// <param name='requestId'>
            /// </param>
            public static object SetHighVolume(this ICashOutAttemptOperations operations, string clientId = default(string), string requestId = default(string))
            {
                return operations.SetHighVolumeAsync(clientId, requestId).GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='clientId'>
            /// </param>
            /// <param name='requestId'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<object> SetHighVolumeAsync(this ICashOutAttemptOperations operations, string clientId = default(string), string requestId = default(string), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.SetHighVolumeWithHttpMessagesAsync(clientId, requestId, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='clientId'>
            /// </param>
            /// <param name='requestId'>
            /// </param>
            public static ErrorResponse SetIsSettledOffchain(this ICashOutAttemptOperations operations, string clientId = default(string), string requestId = default(string))
            {
                return operations.SetIsSettledOffchainAsync(clientId, requestId).GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='clientId'>
            /// </param>
            /// <param name='requestId'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<ErrorResponse> SetIsSettledOffchainAsync(this ICashOutAttemptOperations operations, string clientId = default(string), string requestId = default(string), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.SetIsSettledOffchainWithHttpMessagesAsync(clientId, requestId, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='fromParameter'>
            /// </param>
            /// <param name='to'>
            /// </param>
            public static object GetHistoryRecords(this ICashOutAttemptOperations operations, System.DateTime? fromParameter = default(System.DateTime?), System.DateTime? to = default(System.DateTime?))
            {
                return operations.GetHistoryRecordsAsync(fromParameter, to).GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='fromParameter'>
            /// </param>
            /// <param name='to'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<object> GetHistoryRecordsAsync(this ICashOutAttemptOperations operations, System.DateTime? fromParameter = default(System.DateTime?), System.DateTime? to = default(System.DateTime?), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.GetHistoryRecordsWithHttpMessagesAsync(fromParameter, to, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='clientId'>
            /// </param>
            public static object GetRequests(this ICashOutAttemptOperations operations, string clientId = default(string))
            {
                return operations.GetRequestsAsync(clientId).GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='clientId'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<object> GetRequestsAsync(this ICashOutAttemptOperations operations, string clientId = default(string), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.GetRequestsWithHttpMessagesAsync(clientId, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='clientId'>
            /// </param>
            /// <param name='id'>
            /// </param>
            public static object Get(this ICashOutAttemptOperations operations, string clientId = default(string), string id = default(string))
            {
                return operations.GetAsync(clientId, id).GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='clientId'>
            /// </param>
            /// <param name='id'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<object> GetAsync(this ICashOutAttemptOperations operations, string clientId = default(string), string id = default(string), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.GetWithHttpMessagesAsync(clientId, id, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='requestId'>
            /// </param>
            public static object GetRelatedRequests(this ICashOutAttemptOperations operations, string requestId = default(string))
            {
                return operations.GetRelatedRequestsAsync(requestId).GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='requestId'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<object> GetRelatedRequestsAsync(this ICashOutAttemptOperations operations, string requestId = default(string), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.GetRelatedRequestsWithHttpMessagesAsync(requestId, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

    }
}
