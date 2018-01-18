using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using System.Text;

namespace epay3.Web.Api.Sdk.Model
{
    /// <summary>
    /// Performs an authorization on a credit card.
    /// </summary>
    [DataContract]
    public partial class PostAuthorizeTransactionRequestModel :  IEquatable<PostAuthorizeTransactionRequestModel>
    { 
        /// <summary>
        /// Used to reference a previously stored payment token.
        /// </summary>
        /// <value>Used to reference a previously stored payment token.</value>
        [DataMember(Name="tokenId", EmitDefaultValue=false)]
        public string TokenId { get; set; }
    
        /// <summary>
        /// Total amount to authorize.
        /// </summary>
        /// <value>Total amount to authorize.</value>
        [DataMember(Name="amount", EmitDefaultValue=false)]
        public double? Amount { get; set; }
    
        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class PostAuthorizeTransactionRequestModel {\n");
            sb.Append("  TokenId: ").Append(TokenId).Append("\n");
            sb.Append("  Amount: ").Append(Amount).Append("\n");
            
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
            return this.Equals(obj as PostAuthorizeTransactionRequestModel);
        }

        /// <summary>
        /// Returns true if PostAuthorizeTransactionRequestModel instances are equal
        /// </summary>
        /// <param name="other">Instance of PostAuthorizeTransactionRequestModel to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(PostAuthorizeTransactionRequestModel other)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            if (other == null)
                return false;

            return 
                (
                    this.TokenId == other.TokenId ||
                    this.TokenId != null &&
                    this.TokenId.Equals(other.TokenId)
                ) && 
                (
                    this.Amount == other.Amount ||
                    this.Amount != null &&
                    this.Amount.Equals(other.Amount)
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
                
                if (this.TokenId != null)
                    hash = hash * 59 + this.TokenId.GetHashCode();
                
                if (this.Amount != null)
                    hash = hash * 59 + this.Amount.GetHashCode();
                
                return hash;
            }
        }

    }
}
