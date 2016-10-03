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
    /// 
    /// </summary>
    [DataContract]
    public partial class PostTransactionResponseModel :  IEquatable<PostTransactionResponseModel>
    { 
    
        /// <summary>
        /// Gets or Sets PaymentResponseCode
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum PaymentResponseCodeEnum {
            
            [EnumMember(Value = "GenericDecline")]
            Genericdecline,
            
            [EnumMember(Value = "Success")]
            Success,
            
            [EnumMember(Value = "DoNotHonor")]
            Donothonor,
            
            [EnumMember(Value = "InvalidAccountNumber")]
            Invalidaccountnumber,
            
            [EnumMember(Value = "InsufficientFunds")]
            Insufficientfunds,
            
            [EnumMember(Value = "DeclineCvvFail")]
            Declinecvvfail,
            
            [EnumMember(Value = "ExceedsApprovalAmountLimit")]
            Exceedsapprovalamountlimit,
            
            [EnumMember(Value = "NoSuchIssuer")]
            Nosuchissuer,
            
            [EnumMember(Value = "InvalidPaymentType")]
            Invalidpaymenttype,
            
            [EnumMember(Value = "InvalidExpirationDate")]
            Invalidexpirationdate,
            
            [EnumMember(Value = "LostOrStolenCard")]
            Lostorstolencard,
            
            [EnumMember(Value = "ExpiredCard")]
            Expiredcard
        }

    
        /// <summary>
        /// Gets or Sets PaymentResponseCode
        /// </summary>
        [DataMember(Name="paymentResponseCode", EmitDefaultValue=false)]
        public PaymentResponseCodeEnum? PaymentResponseCode { get; set; }
    
        /// <summary>
        /// Initializes a new instance of the <see cref="PostTransactionResponseModel" /> class.
        /// Initializes a new instance of the <see cref="PostTransactionResponseModel" />class.
        /// </summary>
        /// <param name="Id">Id.</param>
        /// <param name="Message">Message.</param>
        /// <param name="PaymentResponseCode">PaymentResponseCode.</param>

        public PostTransactionResponseModel(long? Id = null, string Message = null, PaymentResponseCodeEnum? PaymentResponseCode = null)
        {
            this.Id = Id;
            this.Message = Message;
            this.PaymentResponseCode = PaymentResponseCode;
            
        }
        
    
        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        [DataMember(Name="id", EmitDefaultValue=false)]
        public long? Id { get; set; }
    
        /// <summary>
        /// Gets or Sets Message
        /// </summary>
        [DataMember(Name="message", EmitDefaultValue=false)]
        public string Message { get; set; }
    
        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class PostTransactionResponseModel {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  Message: ").Append(Message).Append("\n");
            sb.Append("  PaymentResponseCode: ").Append(PaymentResponseCode).Append("\n");
            
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
            return this.Equals(obj as PostTransactionResponseModel);
        }

        /// <summary>
        /// Returns true if PostTransactionResponseModel instances are equal
        /// </summary>
        /// <param name="other">Instance of PostTransactionResponseModel to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(PostTransactionResponseModel other)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            if (other == null)
                return false;

            return 
                (
                    this.Id == other.Id ||
                    this.Id != null &&
                    this.Id.Equals(other.Id)
                ) && 
                (
                    this.Message == other.Message ||
                    this.Message != null &&
                    this.Message.Equals(other.Message)
                ) && 
                (
                    this.PaymentResponseCode == other.PaymentResponseCode ||
                    this.PaymentResponseCode != null &&
                    this.PaymentResponseCode.Equals(other.PaymentResponseCode)
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
                
                if (this.Id != null)
                    hash = hash * 59 + this.Id.GetHashCode();
                
                if (this.Message != null)
                    hash = hash * 59 + this.Message.GetHashCode();
                
                if (this.PaymentResponseCode != null)
                    hash = hash * 59 + this.PaymentResponseCode.GetHashCode();
                
                return hash;
            }
        }

    }
}
