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
    /// Creates a payment schedule for recurring payments.
    /// </summary>
    [DataContract]
    public partial class PostPaymentScheduleRequestModel :  IEquatable<PostPaymentScheduleRequestModel>
    { 
        /// <summary>
        /// Gets or Sets Interval
        /// </summary>
        [DataMember(Name="interval", EmitDefaultValue=false)]
        public IntervalType? Interval { get; set; }
    
        /// <summary>
        /// Name of the payer that is shown on the receipt.
        /// </summary>
        /// <value>Name of the payer that is shown on the receipt.</value>
        [DataMember(Name="payer", EmitDefaultValue=false)]
        public string Payer { get; set; }
    
        /// <summary>
        /// The recipient of the emailed receipt.
        /// </summary>
        /// <value>The recipient of the emailed receipt.</value>
        [DataMember(Name="emailAddress", EmitDefaultValue=false)]
        public string EmailAddress { get; set; }
    
        /// <summary>
        /// The token Id that represents the payment method to be used on the schedule.
        /// </summary>
        /// <value>The token Id that represents the payment method to be used on the schedule.</value>
        [DataMember(Name="tokenId", EmitDefaultValue=false)]
        public string TokenId { get; set; }
    
        /// <summary>
        /// The amount of each recurring payment.
        /// </summary>
        /// <value>The amount of each recurring payment.</value>
        [DataMember(Name="amount", EmitDefaultValue=false)]
        public double? Amount { get; set; }
    
        /// <summary>
        /// Used if the calling application has pre-calculated a payer fee. In that case, the fee will not be re-calculated. This amount, if set, will not be added to the amount field prior to processing.
        /// </summary>
        /// <value>Used if the calling application has pre-calculated a payer fee. In that case, the fee will not be re-calculated. This amount, if set, will not be added to the amount field prior to processing.</value>
        [DataMember(Name="payerFee", EmitDefaultValue=false)]
        public double? PayerFee { get; set; }
    
        /// <summary>
        /// The date of the initial payment. If no date is set, the first payment will be run immediately.
        /// </summary>
        /// <value>The date of the initial payment. If no date is set, the first payment will be run immediately.</value>
        [DataMember(Name="startDate", EmitDefaultValue=false)]
        public DateTime? StartDate { get; set; }
    
        /// <summary>
        /// The date of the last payment if the schedule is supposed to have an end date.
        /// </summary>
        /// <value>The date of the last payment if the schedule is supposed to have an end date.</value>
        [DataMember(Name="endDate", EmitDefaultValue=false)]
        public DateTime? EndDate { get; set; }
    
        /// <summary>
        /// The interval by which the payments should be run.
        /// </summary>
        /// <value>The interval by which the payments should be run.</value>
        [DataMember(Name="numberOfTotalPayments", EmitDefaultValue=false)]
        public int? NumberOfTotalPayments { get; set; }
    
        /// <summary>
        /// The number of days, weeks, etc to wait between payments.
        /// </summary>
        /// <value>The number of days, weeks, etc to wait between payments.</value>
        [DataMember(Name="intervalCount", EmitDefaultValue=false)]
        public int? IntervalCount { get; set; }
    
        /// <summary>
        /// Dictionary of custom attribute values. The key in the dictionary is the identifier of the custom attribute.
        /// </summary>
        /// <value>Dictionary of custom attribute values. The key in the dictionary is the identifier of the custom attribute.</value>
        [DataMember(Name="attributeValues", EmitDefaultValue=false)]
        public Dictionary<string, string> AttributeValues { get; set; }
    
        /// <summary>
        /// Comments that are shown on the receipt.
        /// </summary>
        /// <value>Comments that are shown on the receipt.</value>
        [DataMember(Name="comments", EmitDefaultValue=false)]
        public string Comments { get; set; }
    
        /// <summary>
        /// The fee being charged by the initiating party of this transaction. This does not include the standard transaction fees.
        /// </summary>
        /// <value>The fee being charged by the initiating party of this transaction. This does not include the standard transaction fees.</value>
        [DataMember(Name="initiatingPartyFee", EmitDefaultValue=false)]
        public double? InitiatingPartyFee { get; set; }
    
        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class PostPaymentScheduleRequestModel {\n");
            sb.Append("  Payer: ").Append(Payer).Append("\n");
            sb.Append("  EmailAddress: ").Append(EmailAddress).Append("\n");
            sb.Append("  TokenId: ").Append(TokenId).Append("\n");
            sb.Append("  Amount: ").Append(Amount).Append("\n");
            sb.Append("  PayerFee: ").Append(PayerFee).Append("\n");
            sb.Append("  StartDate: ").Append(StartDate).Append("\n");
            sb.Append("  EndDate: ").Append(EndDate).Append("\n");
            sb.Append("  NumberOfTotalPayments: ").Append(NumberOfTotalPayments).Append("\n");
            sb.Append("  Interval: ").Append(Interval).Append("\n");
            sb.Append("  IntervalCount: ").Append(IntervalCount).Append("\n");
            sb.Append("  AttributeValues: ").Append(AttributeValues).Append("\n");
            sb.Append("  Comments: ").Append(Comments).Append("\n");
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
            return this.Equals(obj as PostPaymentScheduleRequestModel);
        }

        /// <summary>
        /// Returns true if PostPaymentScheduleRequestModel instances are equal
        /// </summary>
        /// <param name="other">Instance of PostPaymentScheduleRequestModel to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(PostPaymentScheduleRequestModel other)
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
                    this.StartDate == other.StartDate ||
                    this.StartDate != null &&
                    this.StartDate.Equals(other.StartDate)
                ) && 
                (
                    this.EndDate == other.EndDate ||
                    this.EndDate != null &&
                    this.EndDate.Equals(other.EndDate)
                ) && 
                (
                    this.NumberOfTotalPayments == other.NumberOfTotalPayments ||
                    this.NumberOfTotalPayments != null &&
                    this.NumberOfTotalPayments.Equals(other.NumberOfTotalPayments)
                ) && 
                (
                    this.Interval == other.Interval ||
                    this.Interval != null &&
                    this.Interval.Equals(other.Interval)
                ) && 
                (
                    this.IntervalCount == other.IntervalCount ||
                    this.IntervalCount != null &&
                    this.IntervalCount.Equals(other.IntervalCount)
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
                
                if (this.EmailAddress != null)
                    hash = hash * 59 + this.EmailAddress.GetHashCode();
                
                if (this.TokenId != null)
                    hash = hash * 59 + this.TokenId.GetHashCode();
                
                if (this.Amount != null)
                    hash = hash * 59 + this.Amount.GetHashCode();
                
                if (this.PayerFee != null)
                    hash = hash * 59 + this.PayerFee.GetHashCode();
                
                if (this.StartDate != null)
                    hash = hash * 59 + this.StartDate.GetHashCode();
                
                if (this.EndDate != null)
                    hash = hash * 59 + this.EndDate.GetHashCode();
                
                if (this.NumberOfTotalPayments != null)
                    hash = hash * 59 + this.NumberOfTotalPayments.GetHashCode();
                
                if (this.Interval != null)
                    hash = hash * 59 + this.Interval.GetHashCode();
                
                if (this.IntervalCount != null)
                    hash = hash * 59 + this.IntervalCount.GetHashCode();
                
                if (this.AttributeValues != null)
                    hash = hash * 59 + this.AttributeValues.GetHashCode();
                
                if (this.Comments != null)
                    hash = hash * 59 + this.Comments.GetHashCode();
                
                if (this.InitiatingPartyFee != null)
                    hash = hash * 59 + this.InitiatingPartyFee.GetHashCode();
                
                return hash;
            }
        }

    }
}
