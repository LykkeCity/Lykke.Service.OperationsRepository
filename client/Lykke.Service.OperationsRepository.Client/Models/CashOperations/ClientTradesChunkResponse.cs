using System;
using System.Collections.Generic;
using System.Linq;
using Lykke.Service.OperationsRepository.AutorestClient.Models;
using Microsoft.Rest;

namespace Lykke.Service.OperationsRepository.Client.Models.CashOperations
{
    public class ClientTradesChunkResponse : BaseCashOperationResponse<ClientTradesChunk>
    {
        public IEnumerable<ClientTrade> Operations { get; set; }

        public string ContinuationToken { get; set; }

        public static ClientTradesChunkResponse Prepare(HttpOperationResponse<object> apiResponse)
        {
            if (apiResponse.Body is ErrorResponse error)
                return new ClientTradesChunkResponse
                {
                    Error = new ErrorModel
                    {
                        Message = error.ErrorMessage
                    }
                };

            if (apiResponse.Body is ClientTradesChunk tradesChunk)
                return new ClientTradesChunkResponse
                {
                    Operations = tradesChunk.Trades,
                    ContinuationToken = tradesChunk.ContinuationToken,
                };

            throw new ArgumentException("Unknown response object");
        }

        public override ClientTradesChunk GetPayload()
        {
            return new ClientTradesChunk
            {
                Trades = Operations.ToArray(),
                ContinuationToken = ContinuationToken,
            };
        }
    }
}
