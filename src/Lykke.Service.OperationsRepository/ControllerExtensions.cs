using Lykke.Service.OperationsHistory.HistoryWriter.Model;
using Lykke.Service.OperationsRepository.Controllers;
using Lykke.Service.OperationsRepository.Core.CashOperations;
using Newtonsoft.Json;

namespace Lykke.Service.OperationsRepository
{
    public static class ControllerExtensions
    {
        public static HistoryEntry MapFrom(this CashOperationsRepositoryController controller,
            ICashInOutOperation source)
        {
            return new HistoryEntry
            {
                ClientId = source.ClientId,
                Currency = source.AssetId,
                DateTime = source.DateTime,
                OpType = "CashInOut",
                Amount = source.Amount,
                CustomData = JsonConvert.SerializeObject(source)
            };
        }

        public static HistoryEntry MapFrom(this CashOutAttemptRepositoryController controller, 
            ICashOutRequest source)
        {
            return new HistoryEntry
            {
                ClientId = source.ClientId,
                Currency = source.AssetId,
                OpType = "CashOutAttempt",
                DateTime = source.DateTime,
                Amount = source.Amount,
                CustomData = JsonConvert.SerializeObject(source)
            };
        }

        public static HistoryEntry MapFrom(this ClientTradesRepositoryController controller,
            IClientTrade source)
        {
            return new HistoryEntry
            {
                ClientId = source.ClientId,
                Amount = source.Amount,
                Currency = source.AssetId,
                DateTime = source.DateTime,
                OpType = "ClientTrade",
                CustomData = JsonConvert.SerializeObject(source)
            };
        }

        public static HistoryEntry MapFrom(this TransferEventsRepositoryController controller, 
            ITransferEvent source)
        {
            return new HistoryEntry
            {
                ClientId = source.ClientId,
                DateTime = source.DateTime,
                Amount = source.Amount,
                Currency = source.AssetId,
                OpType = "TransferEvent",
                CustomData = JsonConvert.SerializeObject(source)
            };
        }
    }
}
