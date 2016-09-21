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
    /// Represents the information needed to customize a single transaction via the payment page.
    /// </summary>
    [DataContract]
    public partial class PostPaymentPageSessionRequestModel : IEquatable<PostPaymentPageSessionRequestModel>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="PostPaymentPageSessionRequestModel" /> class.
        /// Initializes a new instance of the <see cref="PostPaymentPageSessionRequestModel" />class.
        /// </summary>
        /// <param name="AttributeValues">Key/value collection of all custom attributes that will eventually be stored with the transaction..</param>
        /// <param name="Amount">The total amount of the transaction inclusive of all fees..</param>
        /// <param name="InitiatingPartyCreditCardFee">The fee to keep for the initiating party of this transaction in the event of a credit card transaction. This does not include transaction fees..</param>
        /// <param name="InitiatingPartyAchFee">The fee to keep for the initiating party of this transactionin the event of an ACH transaction. This does not include transaction fees..</param>
        /// <param name="SuccessUrl">The Url to which the user will be redirected upon a successful payment..</param>

        public PostPaymentPageSessionRequestModel(Dictionary<string, string> AttributeValues = null, double? Amount = null, double? InitiatingPartyCreditCardFee = null, double? InitiatingPartyAchFee = null, string SuccessUrl = null)
        {
            this.AttributeValues = AttributeValues;
            this.Amount = Amount;
            this.InitiatingPartyCreditCardFee = InitiatingPartyCreditCardFee;
            this.InitiatingPartyAchFee = InitiatingPartyAchFee;
            this.SuccessUrl = SuccessUrl;

        }


        /// <summary>
        /// Key/value collection of all custom attributes that will eventually be stored with the transaction.
        /// </summary>
        /// <value>Key/value collection of all custom attributes that will eventually be stored with the transaction.</value>
        [DataMember(Name = "attributeValues", EmitDefaultValue = false)]
        public Dictionary<string, string> AttributeValues { get; set; }

        /// <summary>
        /// The total amount of the transaction inclusive of all fees.
        /// </summary>
        /// <value>The total amount of the transaction inclusive of all fees.</value>
        [DataMember(Name = "amount", EmitDefaultValue = false)]
        public double? Amount { get; set; }

        /// <summary>
        /// The fee to keep for the initiating party of this transaction in the event of a credit card transaction. This does not include transaction fees.
        /// </summary>
        /// <value>The fee to keep for the initiating party of this transaction in the event of a credit card transaction. This does not include transaction fees.</value>
        [DataMember(Name = "initiatingPartyCreditCardFee", EmitDefaultValue = false)]
        public double? InitiatingPartyCreditCardFee { get; set; }

        /// <summary>
        /// The fee to keep for the initiating party of this transactionin the event of an ACH transaction. This does not include transaction fees.
        /// </summary>
        /// <value>The fee to keep for the initiating party of this transactionin the event of an ACH transaction. This does not include transaction fees.</value>
        [DataMember(Name = "initiatingPartyAchFee", EmitDefaultValue = false)]
        public double? InitiatingPartyAchFee { get; set; }

        /// <summary>
        /// The Url to which the user will be redirected upon a successful payment.
        /// </summary>
        /// <value>The Url to which the user will be redirected upon a successful payment.</value>
        [DataMember(Name = "successUrl", EmitDefaultValue = false)]
        public string SuccessUrl { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class PostPaymentPageSessionRequestModel {\n");
            sb.Append("  AttributeValues: ").Append(AttributeValues).Append("\n");
            sb.Append("  Amount: ").Append(Amount).Append("\n");
            sb.Append("  InitiatingPartyCreditCardFee: ").Append(InitiatingPartyCreditCardFee).Append("\n");
            sb.Append("  InitiatingPartyAchFee: ").Append(InitiatingPartyAchFee).Append("\n");
            sb.Append("  SuccessUrl: ").Append(SuccessUrl).Append("\n");

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
            return this.Equals(obj as PostPaymentPageSessionRequestModel);
        }

        /// <summary>
        /// Returns true if PostPaymentPageSessionRequestModel instances are equal
        /// </summary>
        /// <param name="other">Instance of PostPaymentPageSessionRequestModel to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(PostPaymentPageSessionRequestModel other)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            if (other == null)
                return false;

            return
                (
                    this.AttributeValues == other.AttributeValues ||
                    this.AttributeValues != null &&
                    this.AttributeValues.SequenceEqual(other.AttributeValues)
                ) &&
                (
                    this.Amount == other.Amount ||
                    this.Amount != null &&
                    this.Amount.Equals(other.Amount)
                ) &&
                (
                    this.InitiatingPartyCreditCardFee == other.InitiatingPartyCreditCardFee ||
                    this.InitiatingPartyCreditCardFee != null &&
                    this.InitiatingPartyCreditCardFee.Equals(other.InitiatingPartyCreditCardFee)
                ) &&
                (
                    this.InitiatingPartyAchFee == other.InitiatingPartyAchFee ||
                    this.InitiatingPartyAchFee != null &&
                    this.InitiatingPartyAchFee.Equals(other.InitiatingPartyAchFee)
                ) &&
                (
                    this.SuccessUrl == other.SuccessUrl ||
                    this.SuccessUrl != null &&
                    this.SuccessUrl.Equals(other.SuccessUrl)
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

                if (this.AttributeValues != null)
                    hash = hash * 59 + this.AttributeValues.GetHashCode();

                if (this.Amount != null)
                    hash = hash * 59 + this.Amount.GetHashCode();

                if (this.InitiatingPartyCreditCardFee != null)
                    hash = hash * 59 + this.InitiatingPartyCreditCardFee.GetHashCode();

                if (this.InitiatingPartyAchFee != null)
                    hash = hash * 59 + this.InitiatingPartyAchFee.GetHashCode();

                if (this.SuccessUrl != null)
                    hash = hash * 59 + this.SuccessUrl.GetHashCode();

                return hash;
            }
        }

    }
}
