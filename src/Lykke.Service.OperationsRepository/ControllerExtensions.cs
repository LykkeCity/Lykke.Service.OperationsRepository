using Lykke.Service.OperationsRepository.Contract;
using Lykke.Service.OperationsRepository.Controllers;
using Lykke.Service.OperationsRepository.Core.CashOperations;
using Newtonsoft.Json;

namespace Lykke.Service.OperationsRepository
{
    public static class ControllerExtensions
    {
        public static OperationsHistoryMessage MapFrom(this CashOperationsRepositoryController controller,
            ICashInOutOperation source)
        {
            return new OperationsHistoryMessage
            {
                Id = source.Id,
                ClientId = source.ClientId,
                Currency = source.AssetId,
                DateTime = source.DateTime,
                OpType = "CashInOut",
                Amount = source.Amount,
                Data = JsonConvert.SerializeObject(source)
            };
        }

        public static OperationsHistoryMessage MapFrom(this CashOutAttemptRepositoryController controller,
            ICashOutRequest source)
        {
            return new OperationsHistoryMessage
            {
                Id = source.Id,
                ClientId = source.ClientId,
                Currency = source.AssetId,
                OpType = "CashOutAttempt",
                DateTime = source.DateTime,
                Amount = source.Amount,
                Data = JsonConvert.SerializeObject(source)
            };
        }

        public static OperationsHistoryMessage MapFrom(this ClientTradesRepositoryController controller,
            IClientTrade source)
        {
            return new OperationsHistoryMessage
            {
                Id = source.Id,
                ClientId = source.ClientId,
                Amount = source.Amount,
                Currency = source.AssetId,
                DateTime = source.DateTime,
                OpType = "ClientTrade",
                Data = JsonConvert.SerializeObject(source)
            };
        }

        public static OperationsHistoryMessage MapFrom(this TransferEventsRepositoryController controller,
            ITransferEvent source)
        {
            return new OperationsHistoryMessage
            {
                Id = source.Id,
                ClientId = source.ClientId,
                DateTime = source.DateTime,
                Amount = source.Amount,
                Currency = source.AssetId,
                OpType = "TransferEvent",
                Data = JsonConvert.SerializeObject(source)
            };
        }
    }
}
