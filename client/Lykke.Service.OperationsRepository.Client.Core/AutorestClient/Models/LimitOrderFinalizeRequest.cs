// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Lykke.Service.OperationsRepository.AutorestClient.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    public partial class LimitOrderFinalizeRequest
    {
        /// <summary>
        /// Initializes a new instance of the LimitOrderFinalizeRequest class.
        /// </summary>
        public LimitOrderFinalizeRequest()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the LimitOrderFinalizeRequest class.
        /// </summary>
        /// <param name="orderStatus">Possible values include: 'InOrderBook',
        /// 'Processing', 'Matched', 'NotEnoughFunds',
        /// 'ReservedVolumeGreaterThanBalance', 'NoLiquidity', 'UnknownAsset',
        /// 'Dust', 'Cancelled', 'LeadToNegativeSpread', 'TooSmallVolume',
        /// 'Runtime', 'InvalidFee'</param>
        public LimitOrderFinalizeRequest(OrderStatus orderStatus)
        {
            OrderStatus = orderStatus;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or sets possible values include: 'InOrderBook', 'Processing',
        /// 'Matched', 'NotEnoughFunds', 'ReservedVolumeGreaterThanBalance',
        /// 'NoLiquidity', 'UnknownAsset', 'Dust', 'Cancelled',
        /// 'LeadToNegativeSpread', 'TooSmallVolume', 'Runtime', 'InvalidFee'
        /// </summary>
        [JsonProperty(PropertyName = "OrderStatus")]
        public OrderStatus OrderStatus { get; set; }

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
