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
    public partial class PostPaymentPageSessionRequestModel :  IEquatable<PostPaymentPageSessionRequestModel>
    { 
        /// <summary>
        /// Key/value collection of all custom attributes that will eventually be stored with the transaction.
        /// </summary>
        /// <value>Key/value collection of all custom attributes that will eventually be stored with the transaction.</value>
        [DataMember(Name="attributeValues", EmitDefaultValue=false)]
        public Dictionary<string, string> AttributeValues { get; set; }
    
        /// <summary>
        /// The total amount of the transaction inclusive of all fees.
        /// </summary>
        /// <value>The total amount of the transaction inclusive of all fees.</value>
        [DataMember(Name="amount", EmitDefaultValue=false)]
        public double? Amount { get; set; }

        /// <summary>
        /// The fee the payer is paying. This is not additive to the Amount field.
        /// </summary>
        /// <value>The fee the payer is paying. This is not additive to the Amount field.</value>
        [DataMember(Name = "payerFee", EmitDefaultValue = false)]
        public double? PayerFee { get; set; }

        /// <summary>
        /// The fee to keep for the initiating party of this transaction in the event of a credit card transaction. This does not include transaction fees.
        /// </summary>
        /// <value>The fee to keep for the initiating party of this transaction in the event of a credit card transaction. This does not include transaction fees.</value>
        [DataMember(Name="initiatingPartyCreditCardFee", EmitDefaultValue=false)]
        public double? InitiatingPartyCreditCardFee { get; set; }
    
        /// <summary>
        /// The fee to keep for the initiating party of this transaction in the event of an ACH transaction. This does not include transaction fees.
        /// </summary>
        /// <value>The fee to keep for the initiating party of this transaction in the event of an ACH transaction. This does not include transaction fees.</value>
        [DataMember(Name="initiatingPartyAchFee", EmitDefaultValue=false)]
        public double? InitiatingPartyAchFee { get; set; }
    
        /// <summary>
        /// The Url to which the user will be redirected upon a successful payment.
        /// </summary>
        /// <value>The Url to which the user will be redirected upon a successful payment.</value>
        [DataMember(Name="successUrl", EmitDefaultValue=false)]
        public string SuccessUrl { get; set; }
    
        /// <summary>
        /// A white-list of accepted payment methods that should be shown on the payment page.
        /// </summary>
        /// <value>A white-list of accepted payment methods that should be shown on the payment page.</value>
        [DataMember(Name="acceptedPaymentMethods", EmitDefaultValue=false)]
        public List<AcceptedPaymentMethod> AcceptedPaymentMethods { get; set; }

        /// <summary>
        /// Used to pre-populate the comments section of the payment page.
        /// </summary>
        /// <value>Comments that are used to pre-populate the comments section of the payment page.</value>
        public string Comments { get; set; }
    
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
            sb.Append("  PayerFee: ").Append(PayerFee).Append("\n");
            sb.Append("  InitiatingPartyCreditCardFee: ").Append(InitiatingPartyCreditCardFee).Append("\n");
            sb.Append("  InitiatingPartyAchFee: ").Append(InitiatingPartyAchFee).Append("\n");
            sb.Append("  SuccessUrl: ").Append(SuccessUrl).Append("\n");
            sb.Append("  AcceptedPaymentMethods: ").Append(AcceptedPaymentMethods).Append("\n");
            sb.Append("  Comments: ").Append(Comments).Append("\n");

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
                    this.PayerFee == other.PayerFee ||
                    this.PayerFee != null &&
                    this.PayerFee.Equals(other.PayerFee)
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
                ) &&
                (
                    this.AcceptedPaymentMethods == other.AcceptedPaymentMethods ||
                    this.AcceptedPaymentMethods != null &&
                    this.AcceptedPaymentMethods.SequenceEqual(other.AcceptedPaymentMethods)
                ) &&
                (
                    this.Comments == other.Comments ||
                    this.Comments != null &&
                    this.Comments.SequenceEqual(other.Comments)
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

                if (this.PayerFee != null)
                    hash = hash * 59 + this.PayerFee.GetHashCode();

                if (this.InitiatingPartyCreditCardFee != null)
                    hash = hash * 59 + this.InitiatingPartyCreditCardFee.GetHashCode();
                
                if (this.InitiatingPartyAchFee != null)
                    hash = hash * 59 + this.InitiatingPartyAchFee.GetHashCode();
                
                if (this.SuccessUrl != null)
                    hash = hash * 59 + this.SuccessUrl.GetHashCode();
                
                if (this.AcceptedPaymentMethods != null)
                    hash = hash * 59 + this.AcceptedPaymentMethods.GetHashCode();

                if (this.Comments != null)
                    hash = hash * 59 + this.Comments.GetHashCode();

                return hash;
            }
        }

    }
}
