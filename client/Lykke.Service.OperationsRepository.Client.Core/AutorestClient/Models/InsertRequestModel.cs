// Code generated by Microsoft (R) AutoRest Code Generator 1.1.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace Lykke.Service.OperationsRepository.AutorestClient.Models
{
    using Lykke.Service;
    using Lykke.Service.OperationsRepository;
    using Lykke.Service.OperationsRepository.AutorestClient;
    using Microsoft.Rest;
    using Newtonsoft.Json;
    using System.Linq;

    public partial class InsertRequestModel
    {
        /// <summary>
        /// Initializes a new instance of the InsertRequestModel class.
        /// </summary>
        public InsertRequestModel()
        {
          CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the InsertRequestModel class.
        /// </summary>
        /// <param name="tradeSystem">Possible values include: 'Spot',
        /// 'Margin'</param>
        public InsertRequestModel(CashOutAttemptEntity request, PaymentSystemModel paymentSystem, object paymentFields, CashOutRequestTradeSystem tradeSystem)
        {
            Request = request;
            PaymentSystem = paymentSystem;
            PaymentFields = paymentFields;
            TradeSystem = tradeSystem;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Request")]
        public CashOutAttemptEntity Request { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "PaymentSystem")]
        public PaymentSystemModel PaymentSystem { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "PaymentFields")]
        public object PaymentFields { get; set; }

        /// <summary>
        /// Gets or sets possible values include: 'Spot', 'Margin'
        /// </summary>
        [JsonProperty(PropertyName = "TradeSystem")]
        public CashOutRequestTradeSystem TradeSystem { get; set; }

        /// <summary>
        /// Validate the object.
        /// </summary>
        /// <exception cref="ValidationException">
        /// Thrown if validation fails
        /// </exception>
        public virtual void Validate()
        {
            if (Request == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "Request");
            }
            if (PaymentSystem == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "PaymentSystem");
            }
            if (PaymentFields == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "PaymentFields");
            }
        }
    }
}
