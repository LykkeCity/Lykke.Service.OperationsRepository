// Code generated by Microsoft (R) AutoRest Code Generator 1.1.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace Lykke.Service.OperationsRepository.AutorestClient.Models
{
    using Lykke.Service;
    using Lykke.Service.OperationsRepository;
    using Lykke.Service.OperationsRepository.AutorestClient;
    using Newtonsoft.Json;
    using System.Linq;

    public partial class TransferEvent
    {
        /// <summary>
        /// Initializes a new instance of the TransferEvent class.
        /// </summary>
        public TransferEvent()
        {
          CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the TransferEvent class.
        /// </summary>
        /// <param name="state">Possible values include: 'InProcessOnchain',
        /// 'SettledOnchain', 'InProcessOffchain', 'SettledOffchain',
        /// 'SettledNoChain'</param>
        public TransferEvent(string id = default(string), string clientId = default(string), System.DateTime? dateTime = default(System.DateTime?), bool? isHidden = default(bool?), string fromId = default(string), string assetId = default(string), double? amount = default(double?), string blockChainHash = default(string), string multisig = default(string), string transactionId = default(string), string addressFrom = default(string), string addressTo = default(string), bool? isSettled = default(bool?), TransactionStates? state = default(TransactionStates?))
        {
            Id = id;
            ClientId = clientId;
            DateTime = dateTime;
            IsHidden = isHidden;
            FromId = fromId;
            AssetId = assetId;
            Amount = amount;
            BlockChainHash = blockChainHash;
            Multisig = multisig;
            TransactionId = transactionId;
            AddressFrom = addressFrom;
            AddressTo = addressTo;
            IsSettled = isSettled;
            State = state;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Id")]
        public string Id { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "ClientId")]
        public string ClientId { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "DateTime")]
        public System.DateTime? DateTime { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "IsHidden")]
        public bool? IsHidden { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "FromId")]
        public string FromId { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "AssetId")]
        public string AssetId { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Amount")]
        public double? Amount { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "BlockChainHash")]
        public string BlockChainHash { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Multisig")]
        public string Multisig { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "TransactionId")]
        public string TransactionId { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "AddressFrom")]
        public string AddressFrom { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "AddressTo")]
        public string AddressTo { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "IsSettled")]
        public bool? IsSettled { get; set; }

        /// <summary>
        /// Gets or sets possible values include: 'InProcessOnchain',
        /// 'SettledOnchain', 'InProcessOffchain', 'SettledOffchain',
        /// 'SettledNoChain'
        /// </summary>
        [JsonProperty(PropertyName = "State")]
        public TransactionStates? State { get; set; }

    }
}
