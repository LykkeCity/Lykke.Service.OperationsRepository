using Lykke.Service.OperationsRepository.Contract;

namespace Lykke.Service.OperationsRepository.Models.LimitOrder
{
    public class LimitOrderFinalizeRequest
    {
        public string OrderId { set; get; }
        public OrderStatus OrderStatus { set; get; }
    }
}