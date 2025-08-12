using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace epay3.Web.Api.Sdk.Model
{
    /// <summary>
    /// The type of the bank account.
    /// </summary>
    /// <value>The type of the bank account.</value>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum AccountType
    {

        [EnumMember(Value = "PersonalChecking")]
        Personalchecking,

        [EnumMember(Value = "PersonalSavings")]
        Personalsavings,

        [EnumMember(Value = "CorporateChecking")]
        Corporatechecking,

        [EnumMember(Value = "CorporateSavings")]
        Corporatesavings
    }

    /// <summary>
    /// The type of transaction.
    /// </summary>
    /// <value>The type of transaction.</value>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum TransactionType
    {

        [EnumMember(Value = "Ach")]
        Ach = 1,

        [EnumMember(Value = "Visa")]
        Visa = 2,

        [EnumMember(Value = "MasterCard")]
        Mastercard = 3,

        [EnumMember(Value = "Discover")]
        Discover = 4,

        [EnumMember(Value = "AmericanExpress")]
        Americanexpress = 5,

        [EnumMember(Value = "Jcb")]
        Jcb = 6
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum AcceptedPaymentMethod
    {

        [EnumMember(Value = "CreditCard")]
        CreditCard = 1,

        [EnumMember(Value = "Ach")]
        Ach = 2
    }

    /// <summary>
    /// The interval by which the payments should be run.
    /// </summary>
    /// <value>The interval by which the payments should be run.</value>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum IntervalType
    {

        [EnumMember(Value = "Day")]
        Day,

        [EnumMember(Value = "Week")]
        Week,

        [EnumMember(Value = "Month")]
        Month,

        [EnumMember(Value = "Year")]
        Year
    }

    /// <summary>
    /// The type of transaction search to perform
    /// </summary>
    /// <value>The interval by which the payments should be run.</value>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum TransactionSearchType
    {
        [EnumMember(Value = "Processed")]
        Processed,

        [EnumMember(Value = "Rejected")]
        Rejected,

        [EnumMember(Value = "Chargeback")]
        Chargeback,
    }

    /// <summary>
    /// Gets or Sets PaymentResponseCode
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum PaymentResponseCode
    {
        [EnumMember(Value = "GenericDecline")]
        // An error has occurred.
        GenericDecline = 0,

        [EnumMember(Value = "Success")]
        // The transaction was successful.
        Success = 1,

        [EnumMember(Value = "DoNotHonor")]
        // The issuing bank has put a temporary hold on the card.
        DoNotHonor = 2,

        [EnumMember(Value = "InvalidAccountNumber")]
        // The account number is not valid.
        InvalidAccountNumber = 3,

        [EnumMember(Value = "InsufficientFunds")]
        // The account does not have enough funds to cover the transaction.
        InsufficientFunds = 4,

        [EnumMember(Value = "DeclineCvvFail")]
        // The CVV2/CID is invalid.
        DeclineCvvFail = 5,

        [EnumMember(Value = "ExceedsApprovalAmountLimit")]
        // This transaction exceeds to the daily approval limit for the card.
        ExceedsApprovalAmountLimit = 6,

        [EnumMember(Value = "NoSuchIssuer")]
        // The card number references an issuer that does not exist. Do not process the transaction.
        NoSuchIssuer = 7,

        [EnumMember(Value = "InvalidPaymentType")]
        // This payment type is not accepted by the issuer.
        InvalidPaymentType = 8,

        [EnumMember(Value = "InvalidExpirationDate")]
        // The expiration date is invalid.
        InvalidExpirationDate = 9,

        [EnumMember(Value = "LostOrStolenCard")]
        // The card has been designated as lost or stolen; contact the issuing bank.
        LostOrStolenCard = 10,

        [EnumMember(Value = "ExpiredCard")]
        // The card is expired.
        ExpiredCard = 11,

        [EnumMember(Value = "HardDuplicateTransaction")]
        // A duplicate transaction was submitted in the last few minutes.
        HardDuplicateTransaction = 12,

        [EnumMember(Value = "InvalidToken")]
        // The payment token is invalid.
        InvalidToken = 13,

        [EnumMember(Value = "InvalidAuthorization")]
        // The authorization is invalid.
        InvalidAuthorization = 14,

        [EnumMember(Value = "InvalidRoutingNumber")]
        // The routing number is invalid.
        InvalidRoutingNumber = 15,

        [EnumMember(Value = "SoftDuplicateTransaction")]
        // A duplicate transaction was submitted in the last 24 hours.
        SoftDuplicateTransaction = 16,
    }

    /// <summary>
    /// Gets or Sets ReversalResponseCode
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ReversalResponseCode
    {
        [EnumMember(Value = "GenericDecline")]
        // An error has occurred.
        GenericDecline,

        [EnumMember(Value = "Success")]
        // The reversal was successful.
        Success,

        [EnumMember(Value = "PreviouslyVoided")]
        // The transaction was previously voided.
        PreviouslyVoided,

        [EnumMember(Value = "AlreadySettled")]
        // The transaction has already settled and cannot be voided.
        AlreadySettled,

        [EnumMember(Value = "PreviouslyRejected")]
        // The transaction was previously rejected.
        PreviouslyRejected,

        [EnumMember(Value = "CannotBeVoided")]
        // This transaction cannot be voided because is in the process of settling. If the transaction is settling, you can issue a refund once that process is completed.
        CannotBeVoided
    }

    /// <summary>
    /// The type of event.
    /// </summary>
    /// <value>The type of event.</value>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum EventType
    {
        [EnumMember(Value = "Sale")]
        Sale,

        [EnumMember(Value = "Credit")]
        Credit,

        [EnumMember(Value = "Statement")]
        Statement,

        [EnumMember(Value = "Reject")]
        Reject,

        [EnumMember(Value = "Chargeback")]
        Chargeback,

        [EnumMember(Value = "Refund")]
        Refund,

        [EnumMember(Value = "Settle")]
        Settle,

        [EnumMember(Value = "GeneralError")]
        Generalerror,

        [EnumMember(Value = "Alert")]
        Alert,

        [EnumMember(Value = "Void")]
        Void,

        [EnumMember(Value = "Return")]
        Return,

        [EnumMember(Value = "Send")]
        Send,

        [EnumMember(Value = "Debit")]
        Debit,

        [EnumMember(Value = "ChargebackReported")]
        ChargebackReported,

        [EnumMember(Value = "Authorize")]
        Authorize,

        [EnumMember(Value = "Capture")]
        Capture,

        [EnumMember(Value = "Hold")]
        Hold,

        [EnumMember(Value = "ChargebackReversal")]
        ChargebackReversal,

        [EnumMember(Value = "ChargebackRepresentmentClosed")]
        ChargebackRepresentmentClosed,

        [EnumMember(Value = "ChargebackDisputed")]
        ChargebackDisputed,

        [EnumMember(Value = "ChargebackDeclinedToDispute")]
        ChargebackDeclinedToDispute,

        [EnumMember(Value = "ChargebackDisputeFailed")]
        ChargebackDisputeFailed,

        [EnumMember(Value = "ClientNotificationSent")]
        ClientNotificationSent,

        [EnumMember(Value = "Report")]
        Report,

        [EnumMember(Value = "Batch")]
        Batch
    }

    /// <summary>
    /// The Currency
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Currency
    {
        [EnumMember(Value = "USD")]
        USD = 1,

        [EnumMember(Value = "CAD")]
        CAD = 2
    }
}
