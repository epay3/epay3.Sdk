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
        GenericDecline,

        [EnumMember(Value = "Success")]
        // The transaction was successful.
        Success,

        [EnumMember(Value = "DoNotHonor")]
        // The issuing bank has put a temporary hold on the card.
        DoNotHonor,

        [EnumMember(Value = "InvalidAccountNumber")]
        // The account number is not valid.
        InvalidAccountNumber,

        [EnumMember(Value = "InsufficientFunds")]
        // The account does not have enough funds to cover the transaction.
        InsufficientFunds,

        [EnumMember(Value = "DeclineCvvFail")]
        // The CVV2/CID is invalid.
        DeclineCvvFail,

        [EnumMember(Value = "ExceedsApprovalAmountLimit")]
        // This transaction exceeds to the daily approval limit for the card.
        ExceedsApprovalAmountLimit,

        [EnumMember(Value = "NoSuchIssuer")]
        // The card number references an issuer that does not exist. Do not process the transaction.
        NoSuchIssuer,

        [EnumMember(Value = "InvalidPaymentType")]
        // This payment type is not accepted by the issuer.
        InvalidPaymentType,

        [EnumMember(Value = "InvalidExpirationDate")]
        // The expiration date is invalid.
        InvalidExpirationDate,

        [EnumMember(Value = "LostOrStolenCard")]
        // The card has been designated as lost or stolen; contact the issuing bank.
        LostOrStolenCard,

        [EnumMember(Value = "ExpiredCard")]
        // The card is expired.
        ExpiredCard,

        [EnumMember(Value = "DuplicateTransaction")]
        // A duplicate transaction was recently submitted. To successfully submit this transaction, please try again after a few minutes.
        DuplicateTransaction,

        [EnumMember(Value = "InvalidToken")]
        // The payment token is invalid.
        InvalidToken,

        [EnumMember(Value = "InvalidAuthorization")]
        // The authorization is invalid.
        InvalidAuthorization
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

        [EnumMember(Value = "Authorize")]
        Authorize,

        [EnumMember(Value = "Capture")]
        Capture,

        [EnumMember(Value = "Hold")]
        Hold,

        [EnumMember(Value = "ChargebackReversal")]
        ChargebackReversal,

        [EnumMember(Value = "ChargebackRepresentmentClosed")]
        ChargebackRepresentmentClosed
    }
}
