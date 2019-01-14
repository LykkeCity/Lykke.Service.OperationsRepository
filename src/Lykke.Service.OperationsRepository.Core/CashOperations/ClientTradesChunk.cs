using System.Collections.Generic;

namespace Lykke.Service.OperationsRepository.Core.CashOperations
{
    public class ClientTradesChunk
    {
        public IEnumerable<ClientTrade> Trades { get; set; }
        public string ContinuationToken { get; set; }
    }
}
