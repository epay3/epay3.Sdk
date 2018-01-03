using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using System.Text;

namespace epay3.Web.Api.Sdk.Model
{
    /// <summary>
    /// Voids a transaction.
    /// </summary>
    [DataContract]
    public partial class PostVoidTransactionRequestModel :  IEquatable<PostVoidTransactionRequestModel>
    { 
        /// <summary>
        /// Set to true if an e-receipt confirmation should be sent to all parties upon a successful void.
        /// </summary>
        /// <value>Set to true if an e-receipt confirmation should be sent to all parties upon a successful void.</value>
        [DataMember(Name="sendReceipt", EmitDefaultValue=false)]
        public bool? SendReceipt { get; set; }
    
        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class PostVoidTransactionRequestModel {\n");
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
            return this.Equals(obj as PostVoidTransactionRequestModel);
        }

        /// <summary>
        /// Returns true if PostVoidTransactionRequestModel instances are equal
        /// </summary>
        /// <param name="other">Instance of PostVoidTransactionRequestModel to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(PostVoidTransactionRequestModel other)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            if (other == null)
                return false;

            return 
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
                
                if (this.SendReceipt != null)
                    hash = hash * 59 + this.SendReceipt.GetHashCode();
                
                return hash;
            }
        }
    }
}
