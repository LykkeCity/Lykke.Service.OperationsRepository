// Code generated by Microsoft (R) AutoRest Code Generator 1.1.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace Lykke.Service.OperationsRepository.AutorestClient
{
    using Lykke.Service;
    using Lykke.Service.OperationsRepository;
    using Models;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Extension methods for ClientTradeOperations.
    /// </summary>
    public static partial class ClientTradeOperationsExtensions
    {
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='clientTrades'>
            /// </param>
            public static object Save(this IClientTradeOperations operations, IList<ClientTrade> clientTrades = default(IList<ClientTrade>))
            {
                return operations.SaveAsync(clientTrades).GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='clientTrades'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<object> SaveAsync(this IClientTradeOperations operations, IList<ClientTrade> clientTrades = default(IList<ClientTrade>), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.SaveWithHttpMessagesAsync(clientTrades, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='clientId'>
            /// </param>
            public static object Get(this IClientTradeOperations operations, string clientId = default(string))
            {
                return operations.GetAsync(clientId).GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='clientId'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<object> GetAsync(this IClientTradeOperations operations, string clientId = default(string), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.GetWithHttpMessagesAsync(clientId, null, cancellationToken).ConfigureAwait(false))
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
            public static object GetByDates(this IClientTradeOperations operations, System.DateTime? fromParameter = default(System.DateTime?), System.DateTime? to = default(System.DateTime?))
            {
                return operations.GetByDatesAsync(fromParameter, to).GetAwaiter().GetResult();
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
            public static async Task<object> GetByDatesAsync(this IClientTradeOperations operations, System.DateTime? fromParameter = default(System.DateTime?), System.DateTime? to = default(System.DateTime?), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.GetByDatesWithHttpMessagesAsync(fromParameter, to, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='clientId'>
            /// </param>
            /// <param name='recordId'>
            /// </param>
            public static object GetByRecordId(this IClientTradeOperations operations, string clientId = default(string), string recordId = default(string))
            {
                return operations.GetByRecordIdAsync(clientId, recordId).GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='clientId'>
            /// </param>
            /// <param name='recordId'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<object> GetByRecordIdAsync(this IClientTradeOperations operations, string clientId = default(string), string recordId = default(string), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.GetByRecordIdWithHttpMessagesAsync(clientId, recordId, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='clientId'>
            /// </param>
            /// <param name='recordId'>
            /// </param>
            /// <param name='hash'>
            /// </param>
            public static ErrorResponse UpdateBlockchainHash(this IClientTradeOperations operations, string clientId = default(string), string recordId = default(string), string hash = default(string))
            {
                return operations.UpdateBlockchainHashAsync(clientId, recordId, hash).GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='clientId'>
            /// </param>
            /// <param name='recordId'>
            /// </param>
            /// <param name='hash'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<ErrorResponse> UpdateBlockchainHashAsync(this IClientTradeOperations operations, string clientId = default(string), string recordId = default(string), string hash = default(string), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.UpdateBlockchainHashWithHttpMessagesAsync(clientId, recordId, hash, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='clientId'>
            /// </param>
            /// <param name='recordId'>
            /// </param>
            /// <param name='detectTime'>
            /// </param>
            /// <param name='confirmations'>
            /// </param>
            public static ErrorResponse SetDetectionTimeAndConfirmations(this IClientTradeOperations operations, string clientId = default(string), string recordId = default(string), System.DateTime? detectTime = default(System.DateTime?), int? confirmations = default(int?))
            {
                return operations.SetDetectionTimeAndConfirmationsAsync(clientId, recordId, detectTime, confirmations).GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='clientId'>
            /// </param>
            /// <param name='recordId'>
            /// </param>
            /// <param name='detectTime'>
            /// </param>
            /// <param name='confirmations'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<ErrorResponse> SetDetectionTimeAndConfirmationsAsync(this IClientTradeOperations operations, string clientId = default(string), string recordId = default(string), System.DateTime? detectTime = default(System.DateTime?), int? confirmations = default(int?), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.SetDetectionTimeAndConfirmationsWithHttpMessagesAsync(clientId, recordId, detectTime, confirmations, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='clientId'>
            /// </param>
            /// <param name='recordId'>
            /// </param>
            /// <param name='btcTransactionId'>
            /// </param>
            public static ErrorResponse SetBtcTransaction(this IClientTradeOperations operations, string clientId = default(string), string recordId = default(string), string btcTransactionId = default(string))
            {
                return operations.SetBtcTransactionAsync(clientId, recordId, btcTransactionId).GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='clientId'>
            /// </param>
            /// <param name='recordId'>
            /// </param>
            /// <param name='btcTransactionId'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<ErrorResponse> SetBtcTransactionAsync(this IClientTradeOperations operations, string clientId = default(string), string recordId = default(string), string btcTransactionId = default(string), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.SetBtcTransactionWithHttpMessagesAsync(clientId, recordId, btcTransactionId, null, cancellationToken).ConfigureAwait(false))
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
            /// <param name='offchain'>
            /// </param>
            public static ErrorResponse SetIsSettled(this IClientTradeOperations operations, string clientId = default(string), string id = default(string), bool? offchain = default(bool?))
            {
                return operations.SetIsSettledAsync(clientId, id, offchain).GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='clientId'>
            /// </param>
            /// <param name='id'>
            /// </param>
            /// <param name='offchain'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<ErrorResponse> SetIsSettledAsync(this IClientTradeOperations operations, string clientId = default(string), string id = default(string), bool? offchain = default(bool?), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.SetIsSettledWithHttpMessagesAsync(clientId, id, offchain, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='multisig'>
            /// </param>
            public static object GetByMultisig(this IClientTradeOperations operations, string multisig = default(string))
            {
                return operations.GetByMultisigAsync(multisig).GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='multisig'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<object> GetByMultisigAsync(this IClientTradeOperations operations, string multisig = default(string), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.GetByMultisigWithHttpMessagesAsync(multisig, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='multisigs'>
            /// </param>
            public static object GetByMultisigs(this IClientTradeOperations operations, IList<string> multisigs = default(IList<string>))
            {
                return operations.GetByMultisigsAsync(multisigs).GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='multisigs'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<object> GetByMultisigsAsync(this IClientTradeOperations operations, IList<string> multisigs = default(IList<string>), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.GetByMultisigsWithHttpMessagesAsync(multisigs, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

    }
}