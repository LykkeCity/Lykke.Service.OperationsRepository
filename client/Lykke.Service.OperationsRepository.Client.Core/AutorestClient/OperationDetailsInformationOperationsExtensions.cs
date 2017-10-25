// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Lykke.Service.OperationsRepository.AutorestClient
{
    using Models;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Extension methods for OperationDetailsInformationOperations.
    /// </summary>
    public static partial class OperationDetailsInformationOperationsExtensions
    {
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='clientId'>
            /// </param>
            public static object Get(this IOperationDetailsInformationOperations operations, string clientId = default(string))
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
            public static async Task<object> GetAsync(this IOperationDetailsInformationOperations operations, string clientId = default(string), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.GetWithHttpMessagesAsync(clientId, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='operationDetailsInfo'>
            /// </param>
            public static ErrorResponse Create(this IOperationDetailsInformationOperations operations, OperationDetailsInformation operationDetailsInfo = default(OperationDetailsInformation))
            {
                return operations.CreateAsync(operationDetailsInfo).GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='operationDetailsInfo'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<ErrorResponse> CreateAsync(this IOperationDetailsInformationOperations operations, OperationDetailsInformation operationDetailsInfo = default(OperationDetailsInformation), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.CreateWithHttpMessagesAsync(operationDetailsInfo, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='operationDetailsInfo'>
            /// </param>
            public static object Register(this IOperationDetailsInformationOperations operations, OperationDetailsInformation operationDetailsInfo = default(OperationDetailsInformation))
            {
                return operations.RegisterAsync(operationDetailsInfo).GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='operationDetailsInfo'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<object> RegisterAsync(this IOperationDetailsInformationOperations operations, OperationDetailsInformation operationDetailsInfo = default(OperationDetailsInformation), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.RegisterWithHttpMessagesAsync(operationDetailsInfo, null, cancellationToken).ConfigureAwait(false))
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
            public static object GetByRecordId(this IOperationDetailsInformationOperations operations, string clientId = default(string), string recordId = default(string))
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
            public static async Task<object> GetByRecordIdAsync(this IOperationDetailsInformationOperations operations, string clientId = default(string), string recordId = default(string), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.GetByRecordIdWithHttpMessagesAsync(clientId, recordId, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

    }
}
