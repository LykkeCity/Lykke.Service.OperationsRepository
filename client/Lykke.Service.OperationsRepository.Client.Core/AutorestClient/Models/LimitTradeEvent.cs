// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Lykke.Service.OperationsRepository.AutorestClient.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    public partial class LimitTradeEvent
    {
        /// <summary>
        /// Initializes a new instance of the LimitTradeEvent class.
        /// </summary>
        public LimitTradeEvent()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the LimitTradeEvent class.
        /// </summary>
        /// <param name="orderType">Possible values include: 'Buy',
        /// 'Sell'</param>
        /// <param name="status">Possible values include: 'InOrderBook',
        /// 'Processing', 'Matched', 'NotEnoughFunds',
        /// 'ReservedVolumeGreaterThanBalance', 'NoLiquidity', 'UnknownAsset',
        /// 'Dust', 'Cancelled', 'LeadToNegativeSpread', 'TooSmallVolume',
        /// 'Runtime', 'InvalidFee'</param>
        public LimitTradeEvent(System.DateTime createdDt, OrderType orderType, double volume, double price, OrderStatus status, bool isHidden, string clientId = default(string), string id = default(string), string orderId = default(string), string assetId = default(string), string assetPair = default(string))
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
        public string ClientId { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Id")]
        public string Id { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "OrderId")]
        public string OrderId { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "CreatedDt")]
        public System.DateTime CreatedDt { get; set; }

        /// <summary>
        /// Gets or sets possible values include: 'Buy', 'Sell'
        /// </summary>
        [JsonProperty(PropertyName = "OrderType")]
        public OrderType OrderType { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Volume")]
        public double Volume { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "AssetId")]
        public string AssetId { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "AssetPair")]
        public string AssetPair { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Price")]
        public double Price { get; set; }

        /// <summary>
        /// Gets or sets possible values include: 'InOrderBook', 'Processing',
        /// 'Matched', 'NotEnoughFunds', 'ReservedVolumeGreaterThanBalance',
        /// 'NoLiquidity', 'UnknownAsset', 'Dust', 'Cancelled',
        /// 'LeadToNegativeSpread', 'TooSmallVolume', 'Runtime', 'InvalidFee'
        /// </summary>
        [JsonProperty(PropertyName = "Status")]
        public OrderStatus Status { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "IsHidden")]
        public bool IsHidden { get; set; }

        /// <summary>
        /// Validate the object.
        /// </summary>
        /// <exception cref="Microsoft.Rest.ValidationException">
        /// Thrown if validation fails
        /// </exception>
        public virtual void Validate()
        {
        }
    }
}
