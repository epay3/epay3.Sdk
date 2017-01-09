using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace epay3.Web.Api.Sdk.Model
{
    /// <summary>
    /// Represents a financial transaction.
    /// </summary>
    [DataContract]
    public partial class PostTransactionRequestModel : IEquatable<PostTransactionRequestModel>
    {
        /// <summary>
        /// Name of the payer that is shown on the receipt.
        /// </summary>
        /// <value>Name of the payer that is shown on the receipt.</value>
        [DataMember(Name = "payer", EmitDefaultValue = false)]
        public string Payer { get; set; }

        /// <summary>
        /// Full name of the state in which the transaction is taking place. This impacts the payer fee calculation.
        /// </summary>
        /// <value>Full name of the state in which the transaction is taking place. This impacts the payer fee calculation.</value>
        [DataMember(Name = "state", EmitDefaultValue = false)]
        public string State { get; set; }

        /// <summary>
        /// Unique identifier for the customer.
        /// </summary>
        /// <value>Unique identifier for the customer.</value>
        [DataMember(Name = "customerId", EmitDefaultValue = false)]
        public string CustomerId { get; set; }

        /// <summary>
        /// Total amount to charge not including any payer fees.
        /// </summary>
        /// <value>Total amount to charge not including any payer fees.</value>
        [DataMember(Name = "amount", EmitDefaultValue = false)]
        public double? Amount { get; set; }

        /// <summary>
        /// The fee the payer is paying. This is not additive to the Amount field.
        /// </summary>
        /// <value>The fee the payer is paying. This is not additive to the Amount field.</value>
        [DataMember(Name = "payerFee", EmitDefaultValue = false)]
        public double? PayerFee { get; set; }

        /// <summary>
        /// Dictionary of custom attribute values. The key in the dictionary is the identifier of the custom attribute.
        /// </summary>
        /// <value>Dictionary of custom attribute values. The key in the dictionary is the identifier of the custom attribute.</value>
        [DataMember(Name = "attributeValues", EmitDefaultValue = false)]
        public Dictionary<string, string> AttributeValues { get; set; }

        /// <summary>
        /// Comments that are shown on the receipt.
        /// </summary>
        /// <value>Comments that are shown on the receipt.</value>
        [DataMember(Name = "comments", EmitDefaultValue = false)]
        public string Comments { get; set; }

        /// <summary>
        /// The recipient of the emailed receipt.
        /// </summary>
        /// <value>The recipient of the emailed receipt.</value>
        [DataMember(Name = "emailAddress", EmitDefaultValue = false)]
        public string EmailAddress { get; set; }

        /// <summary>
        /// Used to reference a previously stored payment token.
        /// </summary>
        /// <value>Used to reference a previously stored payment token.</value>
        [DataMember(Name = "tokenId", EmitDefaultValue = false)]
        public string TokenId { get; set; }

        /// <summary>
        /// Used for credit card transactions.
        /// </summary>
        /// <value>Used for credit card transactions.</value>
        [DataMember(Name = "creditCardInformation", EmitDefaultValue = false)]
        public CreditCardInformationModel CreditCardInformation { get; set; }

        /// <summary>
        /// Used for eCheck\\ACH transactions.
        /// </summary>
        /// <value>Used for eCheck\\ACH transactions.</value>
        [DataMember(Name = "bankAccountInformation", EmitDefaultValue = false)]
        public BankAccountInformationModel BankAccountInformation { get; set; }

        /// <summary>
        /// Set to true if the payer and account holder(s) should receive an e-receipt.
        /// </summary>
        /// <value>Set to true if the payer and account holder(s) should receive an e-receipt.</value>
        [DataMember(Name = "sendReceipt", EmitDefaultValue = false)]
        public bool? SendReceipt { get; set; }

        /// <summary>
        /// The fee being charged by the initiating party of this transaction. This does not include the standard transaction fees.
        /// </summary>
        /// <value>The fee being charged by the initiating party of this transaction. This does not include the standard transaction fees.</value>
        [DataMember(Name = "initiatingPartyFee", EmitDefaultValue = false)]
        public double? InitiatingPartyFee { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class PostTransactionRequestModel {\n");
            sb.Append("  Payer: ").Append(Payer).Append("\n");
            sb.Append("  State: ").Append(State).Append("\n");
            sb.Append("  CustomerId: ").Append(CustomerId).Append("\n");
            sb.Append("  Amount: ").Append(Amount).Append("\n");
            sb.Append("  PayerFee: ").Append(PayerFee).Append("\n");
            sb.Append("  AttributeValues: ").Append(AttributeValues).Append("\n");
            sb.Append("  Comments: ").Append(Comments).Append("\n");
            sb.Append("  EmailAddress: ").Append(EmailAddress).Append("\n");
            sb.Append("  TokenId: ").Append(TokenId).Append("\n");
            sb.Append("  CreditCardInformation: ").Append(CreditCardInformation).Append("\n");
            sb.Append("  BankAccountInformation: ").Append(BankAccountInformation).Append("\n");
            sb.Append("  SendReceipt: ").Append(SendReceipt).Append("\n");
            sb.Append("  InitiatingPartyFee: ").Append(InitiatingPartyFee).Append("\n");

            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="obj">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object obj)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            return this.Equals(obj as PostTransactionRequestModel);
        }

        /// <summary>
        /// Returns true if PostTransactionRequestModel instances are equal
        /// </summary>
        /// <param name="other">Instance of PostTransactionRequestModel to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(PostTransactionRequestModel other)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            if (other == null)
                return false;

            return
                (
                    this.Payer == other.Payer ||
                    this.Payer != null &&
                    this.Payer.Equals(other.Payer)
                ) &&
                (
                    this.State == other.State ||
                    this.State != null &&
                    this.State.Equals(other.State)
                ) &&
                (
                    this.CustomerId == other.CustomerId ||
                    this.CustomerId != null &&
                    this.CustomerId.Equals(other.CustomerId)
                ) &&
                (
                    this.PayerFee == other.PayerFee ||
                    this.PayerFee != null &&
                    this.PayerFee.Equals(other.PayerFee)
                ) &&
                (
                    this.Amount == other.Amount ||
                    this.Amount != null &&
                    this.Amount.Equals(other.Amount)
                ) &&
                (
                    this.AttributeValues == other.AttributeValues ||
                    this.AttributeValues != null &&
                    this.AttributeValues.SequenceEqual(other.AttributeValues)
                ) &&
                (
                    this.Comments == other.Comments ||
                    this.Comments != null &&
                    this.Comments.Equals(other.Comments)
                ) &&
                (
                    this.EmailAddress == other.EmailAddress ||
                    this.EmailAddress != null &&
                    this.EmailAddress.Equals(other.EmailAddress)
                ) &&
                (
                    this.TokenId == other.TokenId ||
                    this.TokenId != null &&
                    this.TokenId.Equals(other.TokenId)
                ) &&
                (
                    this.CreditCardInformation == other.CreditCardInformation ||
                    this.CreditCardInformation != null &&
                    this.CreditCardInformation.Equals(other.CreditCardInformation)
                ) &&
                (
                    this.BankAccountInformation == other.BankAccountInformation ||
                    this.BankAccountInformation != null &&
                    this.BankAccountInformation.Equals(other.BankAccountInformation)
                ) &&
                (
                    this.SendReceipt == other.SendReceipt ||
                    this.SendReceipt != null &&
                    this.SendReceipt.Equals(other.SendReceipt)
                ) &&
                (
                    this.InitiatingPartyFee == other.InitiatingPartyFee ||
                    this.InitiatingPartyFee != null &&
                    this.InitiatingPartyFee.Equals(other.InitiatingPartyFee)
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            // credit: http://stackoverflow.com/a/263416/677735
            unchecked // Overflow is fine, just wrap
            {
                int hash = 41;
                // Suitable nullity checks etc, of course :)

                if (this.Payer != null)
                    hash = hash * 59 + this.Payer.GetHashCode();

                if (this.State != null)
                    hash = hash * 59 + this.State.GetHashCode();

                if (this.CustomerId != null)
                    hash = hash * 59 + this.CustomerId.GetHashCode();

                if (this.Amount != null)
                    hash = hash * 59 + this.Amount.GetHashCode();

                if (this.AttributeValues != null)
                    hash = hash * 59 + this.AttributeValues.GetHashCode();

                if (this.Comments != null)
                    hash = hash * 59 + this.Comments.GetHashCode();

                if (this.EmailAddress != null)
                    hash = hash * 59 + this.EmailAddress.GetHashCode();

                if (this.TokenId != null)
                    hash = hash * 59 + this.TokenId.GetHashCode();

                if (this.CreditCardInformation != null)
                    hash = hash * 59 + this.CreditCardInformation.GetHashCode();

                if (this.BankAccountInformation != null)
                    hash = hash * 59 + this.BankAccountInformation.GetHashCode();

                if (this.SendReceipt != null)
                    hash = hash * 59 + this.SendReceipt.GetHashCode();

                if (this.InitiatingPartyFee != null)
                    hash = hash * 59 + this.InitiatingPartyFee.GetHashCode();

                return hash;
            }
        }

    }
}
