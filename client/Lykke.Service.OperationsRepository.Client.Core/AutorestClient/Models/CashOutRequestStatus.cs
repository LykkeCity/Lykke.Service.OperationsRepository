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
        [EnumMember(Value = "Pending")]
        Pending,
        [EnumMember(Value = "Confirmed")]
        Confirmed,
        [EnumMember(Value = "Declined")]
        Declined,
        [EnumMember(Value = "Processed")]
        Processed,
        [EnumMember(Value = "ClientConfirmation")]
        ClientConfirmation,
        [EnumMember(Value = "CanceledByClient")]
        CanceledByClient,
        [EnumMember(Value = "CanceledByTimeout")]
        CanceledByTimeout,
        [EnumMember(Value = "RequestForDocs")]
        RequestForDocs
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
                case CashOutRequestStatus.Pending:
                    return "Pending";
                case CashOutRequestStatus.Confirmed:
                    return "Confirmed";
                case CashOutRequestStatus.Declined:
                    return "Declined";
                case CashOutRequestStatus.Processed:
                    return "Processed";
                case CashOutRequestStatus.ClientConfirmation:
                    return "ClientConfirmation";
                case CashOutRequestStatus.CanceledByClient:
                    return "CanceledByClient";
                case CashOutRequestStatus.CanceledByTimeout:
                    return "CanceledByTimeout";
                case CashOutRequestStatus.RequestForDocs:
                    return "RequestForDocs";
            }
            return null;
        }

        internal static CashOutRequestStatus? ParseCashOutRequestStatus(this string value)
        {
            switch( value )
            {
                case "Pending":
                    return CashOutRequestStatus.Pending;
                case "Confirmed":
                    return CashOutRequestStatus.Confirmed;
                case "Declined":
                    return CashOutRequestStatus.Declined;
                case "Processed":
                    return CashOutRequestStatus.Processed;
                case "ClientConfirmation":
                    return CashOutRequestStatus.ClientConfirmation;
                case "CanceledByClient":
                    return CashOutRequestStatus.CanceledByClient;
                case "CanceledByTimeout":
                    return CashOutRequestStatus.CanceledByTimeout;
                case "RequestForDocs":
                    return CashOutRequestStatus.RequestForDocs;
            }
            return null;
        }
    }
}
