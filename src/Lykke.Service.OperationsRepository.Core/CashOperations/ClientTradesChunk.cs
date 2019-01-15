using System.Collections.Generic;
using Lykke.Service.OperationsRepository.Contract.Abstractions;

namespace Lykke.Service.OperationsRepository.Core.CashOperations
{
    public class ClientTradesChunk
    {
        public IEnumerable<IClientTrade> Trades { get; set; }
        public string ContinuationToken { get; set; }
    }
}
