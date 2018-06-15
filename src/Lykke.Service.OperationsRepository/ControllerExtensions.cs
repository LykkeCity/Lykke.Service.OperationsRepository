using Lykke.Service.OperationsRepository.Contract;
using Lykke.Service.OperationsRepository.Contract.Abstractions;
using Lykke.Service.OperationsRepository.Contract.History;
using Lykke.Service.OperationsRepository.Controllers;
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
                OpType = nameof(OperationType.CashInOut),
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
                OpType = nameof(OperationType.ClientTrade),
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
                OpType = nameof(OperationType.TransferEvent),
                Data = JsonConvert.SerializeObject(source)
            };
        }

        public static OperationsHistoryMessage MapFrom(this LimitTradeEventsRepositoryController controller,
            ILimitTradeEvent source)
        {
            return new OperationsHistoryMessage
            {
                Id = source.Id,
                Amount = source.Volume,
                ClientId = source.ClientId,
                Currency = source.AssetId,
                DateTime = source.CreatedDt,
                OpType = nameof(OperationType.LimitTradeEvent),
                Data = JsonConvert.SerializeObject(source)
            };
        }
    }
}
