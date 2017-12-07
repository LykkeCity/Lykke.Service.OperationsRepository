// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Lykke.Service.OperationsRepository.AutorestClient.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    public partial class ILimitTradeEvent
    {
        /// <summary>
        /// Initializes a new instance of the ILimitTradeEvent class.
        /// </summary>
        public ILimitTradeEvent()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the ILimitTradeEvent class.
        /// </summary>
        /// <param name="orderType">Possible values include: 'Buy',
        /// 'Sell'</param>
        /// <param name="status">Possible values include: 'InOrderBook',
        /// 'Processing', 'Matched', 'NotEnoughFunds',
        /// 'ReservedVolumeGreaterThanBalance', 'NoLiquidity', 'UnknownAsset',
        /// 'Dust', 'Cancelled', 'LeadToNegativeSpread'</param>
        public ILimitTradeEvent(string clientId = default(string), string id = default(string), string orderId = default(string), System.DateTime? createdDt = default(System.DateTime?), OrderType? orderType = default(OrderType?), double? volume = default(double?), string assetId = default(string), string assetPair = default(string), double? price = default(double?), OrderStatus? status = default(OrderStatus?), bool? isHidden = default(bool?))
        {
            ClientId = clientId;
            Id = id;
            OrderId = orderId;
            CreatedDt = createdDt;
            OrderType = orderType;
            Volume = volume;
            AssetId = assetId;
            AssetPair = assetPair;
            Price = price;
            Status = status;
            IsHidden = isHidden;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "ClientId")]
        public string ClientId { get; private set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Id")]
        public string Id { get; private set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "OrderId")]
        public string OrderId { get; private set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "CreatedDt")]
        public System.DateTime? CreatedDt { get; private set; }

        /// <summary>
        /// Gets possible values include: 'Buy', 'Sell'
        /// </summary>
        [JsonProperty(PropertyName = "OrderType")]
        public OrderType? OrderType { get; private set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Volume")]
        public double? Volume { get; private set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "AssetId")]
        public string AssetId { get; private set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "AssetPair")]
        public string AssetPair { get; private set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Price")]
        public double? Price { get; private set; }

        /// <summary>
        /// Gets possible values include: 'InOrderBook', 'Processing',
        /// 'Matched', 'NotEnoughFunds', 'ReservedVolumeGreaterThanBalance',
        /// 'NoLiquidity', 'UnknownAsset', 'Dust', 'Cancelled',
        /// 'LeadToNegativeSpread'
        /// </summary>
        [JsonProperty(PropertyName = "Status")]
        public OrderStatus? Status { get; private set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "IsHidden")]
        public bool? IsHidden { get; private set; }

    }
}
