using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using System.Text;

namespace epay3.Web.Api.Sdk.Model
{
    /// <summary>
    /// Refunds a transaction either partially or in full.
    /// </summary>
    [DataContract]
    public partial class PostRefundTransactionRequestModel :  IEquatable<PostRefundTransactionRequestModel>
    { 
    
        /// <summary>
        /// Initializes a new instance of the <see cref="PostRefundTransactionRequestModel" /> class.
        /// Initializes a new instance of the <see cref="PostRefundTransactionRequestModel" />class.
        /// </summary>
        /// <param name="Amount">The amount to be refunded. Setting to null will process a full refund..</param>
        /// <param name="SendReceipt">Set to true if an e-receipt confirmation should be sent to all parties upon a successful refund..</param>

        public PostRefundTransactionRequestModel(double? Amount = null, bool? SendReceipt = null)
        {
            this.Amount = Amount;
            this.SendReceipt = SendReceipt;
            
        }
    
        /// <summary>
        /// The amount to be refunded. Setting to null will process a full refund.
        /// </summary>
        /// <value>The amount to be refunded. Setting to null will process a full refund.</value>
        [DataMember(Name="amount", EmitDefaultValue=false)]
        public double? Amount { get; set; }
    
        /// <summary>
        /// Set to true if an e-receipt confirmation should be sent to all parties upon a successful refund.
        /// </summary>
        /// <value>Set to true if an e-receipt confirmation should be sent to all parties upon a successful refund.</value>
        [DataMember(Name="sendReceipt", EmitDefaultValue=false)]
        public bool? SendReceipt { get; set; }
    
        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class PostRefundTransactionRequestModel {\n");
            sb.Append("  Amount: ").Append(Amount).Append("\n");
            sb.Append("  SendReceipt: ").Append(SendReceipt).Append("\n");
            
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
            return this.Equals(obj as PostRefundTransactionRequestModel);
        }

        /// <summary>
        /// Returns true if PostRefundTransactionRequestModel instances are equal
        /// </summary>
        /// <param name="other">Instance of PostRefundTransactionRequestModel to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(PostRefundTransactionRequestModel other)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            if (other == null)
                return false;

            return 
                (
                    this.Amount == other.Amount ||
                    this.Amount != null &&
                    this.Amount.Equals(other.Amount)
                ) && 
                (
                    this.SendReceipt == other.SendReceipt ||
                    this.SendReceipt != null &&
                    this.SendReceipt.Equals(other.SendReceipt)
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
                
                if (this.Amount != null)
                    hash = hash * 59 + this.Amount.GetHashCode();
                
                if (this.SendReceipt != null)
                    hash = hash * 59 + this.SendReceipt.GetHashCode();
                
                return hash;
            }
        }
    }
}
