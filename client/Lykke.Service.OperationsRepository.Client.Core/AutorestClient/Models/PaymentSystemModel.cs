// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Lykke.Service.OperationsRepository.AutorestClient.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    public partial class PaymentSystemModel
    {
        /// <summary>
        /// Initializes a new instance of the PaymentSystemModel class.
        /// </summary>
        public PaymentSystemModel()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the PaymentSystemModel class.
        /// </summary>
        public PaymentSystemModel(string value = default(string))
        {
            Value = value;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Value")]
        public string Value { get; set; }

    }
}
