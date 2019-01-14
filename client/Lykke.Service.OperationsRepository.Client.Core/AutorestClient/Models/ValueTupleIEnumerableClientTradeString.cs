// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Lykke.Service.OperationsRepository.AutorestClient.Models
{
    using Microsoft.Rest;
    using Newtonsoft.Json;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public partial class ValueTupleIEnumerableClientTradeString
    {
        /// <summary>
        /// Initializes a new instance of the
        /// ValueTupleIEnumerableClientTradeString class.
        /// </summary>
        public ValueTupleIEnumerableClientTradeString()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the
        /// ValueTupleIEnumerableClientTradeString class.
        /// </summary>
        public ValueTupleIEnumerableClientTradeString(IList<ClientTrade> item1, string item2)
        {
            Item1 = item1;
            Item2 = item2;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Item1")]
        public IList<ClientTrade> Item1 { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Item2")]
        public string Item2 { get; set; }

        /// <summary>
        /// Validate the object.
        /// </summary>
        /// <exception cref="ValidationException">
        /// Thrown if validation fails
        /// </exception>
        public virtual void Validate()
        {
            if (Item1 == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "Item1");
            }
            if (Item2 == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "Item2");
            }
            if (Item1 != null)
            {
                foreach (var element in Item1)
                {
                    if (element != null)
                    {
                        element.Validate();
                    }
                }
            }
        }
    }
}