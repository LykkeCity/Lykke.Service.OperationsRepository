// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Lykke.Service.OperationsRepository.AutorestClient.Models
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using System.Runtime;
    using System.Runtime.Serialization;

    /// <summary>
    /// Defines values for CashOutRequestStatus.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum CashOutRequestStatus
    {
        [EnumMember(Value = "ClientConfirmation")]
        ClientConfirmation,
        [EnumMember(Value = "Pending")]
        Pending,
        [EnumMember(Value = "RequestForDocs")]
        RequestForDocs,
        [EnumMember(Value = "Confirmed")]
        Confirmed,
        [EnumMember(Value = "Declined")]
        Declined,
        [EnumMember(Value = "CanceledByClient")]
        CanceledByClient,
        [EnumMember(Value = "CanceledByTimeout")]
        CanceledByTimeout,
        [EnumMember(Value = "Processed")]
        Processed
    }
    internal static class CashOutRequestStatusEnumExtension
    {
        internal static string ToSerializedValue(this CashOutRequestStatus? value)
        {
            return value == null ? null : ((CashOutRequestStatus)value).ToSerializedValue();
        }

        internal static string ToSerializedValue(this CashOutRequestStatus value)
        {
            switch( value )
            {
                case CashOutRequestStatus.ClientConfirmation:
                    return "ClientConfirmation";
                case CashOutRequestStatus.Pending:
                    return "Pending";
                case CashOutRequestStatus.RequestForDocs:
                    return "RequestForDocs";
                case CashOutRequestStatus.Confirmed:
                    return "Confirmed";
                case CashOutRequestStatus.Declined:
                    return "Declined";
                case CashOutRequestStatus.CanceledByClient:
                    return "CanceledByClient";
                case CashOutRequestStatus.CanceledByTimeout:
                    return "CanceledByTimeout";
                case CashOutRequestStatus.Processed:
                    return "Processed";
            }
            return null;
        }

        internal static CashOutRequestStatus? ParseCashOutRequestStatus(this string value)
        {
            switch( value )
            {
                case "ClientConfirmation":
                    return CashOutRequestStatus.ClientConfirmation;
                case "Pending":
                    return CashOutRequestStatus.Pending;
                case "RequestForDocs":
                    return CashOutRequestStatus.RequestForDocs;
                case "Confirmed":
                    return CashOutRequestStatus.Confirmed;
                case "Declined":
                    return CashOutRequestStatus.Declined;
                case "CanceledByClient":
                    return CashOutRequestStatus.CanceledByClient;
                case "CanceledByTimeout":
                    return CashOutRequestStatus.CanceledByTimeout;
                case "Processed":
                    return CashOutRequestStatus.Processed;
            }
            return null;
        }
    }
}
