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
    public partial class PostTransactionFeesRequestModel :  IEquatable<PostTransactionFeesRequestModel>
    { 
    
        /// <summary>
        /// Initializes a new instance of the <see cref="PostTransactionFeesRequestModel" /> class.
        /// Initializes a new instance of the <see cref="PostTransactionFeesRequestModel" />class.
        /// </summary>
        /// <param name="Amount">The amount from which to calculate the payer fee. (required).</param>

        public PostTransactionFeesRequestModel(decimal? Amount = null)
        {
            // to ensure "Amount" is required (not null)
            if (Amount == null)
            {
                throw new InvalidDataException("Amount is a required property for PostTransactionFeesRequestModel and cannot be null");
            }
            else
            {
                this.Amount = Amount.Value;
            }
            
        }
        
    
        /// <summary>
        /// The amount from which to calculate the payer fee.
        /// </summary>
        /// <value>The amount from which to calculate the payer fee.</value>
        [DataMember(Name="amount", EmitDefaultValue=false)]
        public decimal Amount { get; set; }
    
        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class PostTransactionFeesRequestModel {\n");
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
            return this.Equals(obj as PostTransactionFeesRequestModel);
        }

        /// <summary>
        /// Returns true if PostTransactionFeesRequestModel instances are equal
        /// </summary>
        /// <param name="other">Instance of PostTransactionFeesRequestModel to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(PostTransactionFeesRequestModel other)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            if (other == null)
                return false;

            return 
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
                
                if (this.Amount != null)
                    hash = hash * 59 + this.Amount.GetHashCode();
                
                return hash;
            }
        }

    }
}
