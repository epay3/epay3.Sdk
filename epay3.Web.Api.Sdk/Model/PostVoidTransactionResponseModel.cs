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
    public partial class PostVoidTransactionResponseModel :  IEquatable<PostVoidTransactionResponseModel>
    { 
        /// <summary>
        /// Gets or Sets PaymentResponseCode
        /// </summary>
        [DataMember(Name= "reversalResponseCode", EmitDefaultValue=false)]
        public ReversalResponseCode? ReversalResponseCode { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PostVoidTransactionResponseModel" /> class.
        /// Initializes a new instance of the <see cref="PostVoidTransactionResponseModel" />class.
        /// </summary>
        /// <param name="ReversalResponseCode">ReversalResponseCode.</param>
        public PostVoidTransactionResponseModel(ReversalResponseCode? ReversalResponseCode = null)
        {
            this.ReversalResponseCode = ReversalResponseCode;
        }
        
        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class PostVoidTransactionResponseModel {\n");
            sb.Append("  ReversalResponseCode: ").Append(ReversalResponseCode).Append("\n");
            
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
            return this.Equals(obj as PostVoidTransactionResponseModel);
        }

        /// <summary>
        /// Returns true if PostVoidTransactionResponseModel instances are equal
        /// </summary>
        /// <param name="other">Instance of PostVoidTransactionResponseModel to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(PostVoidTransactionResponseModel other)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            if (other == null)
                return false;

            return 
                (
                    this.ReversalResponseCode == other.ReversalResponseCode ||
                    this.ReversalResponseCode != null &&
                    this.ReversalResponseCode.Equals(other.ReversalResponseCode)
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
                
                if (this.ReversalResponseCode != null)
                    hash = hash * 59 + this.ReversalResponseCode.GetHashCode();
                
                return hash;
            }
        }

    }
}
