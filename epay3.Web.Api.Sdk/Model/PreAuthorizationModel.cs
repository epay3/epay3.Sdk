using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using System.Text;

namespace epay3.Web.Api.Sdk.Model
{
    /// <summary>
    /// Contains information for offline pre-authorizations.
    /// </summary>
    [DataContract]
    public partial class PreAuthorizationModel :  IEquatable<PreAuthorizationModel>
    { 
        /// <summary>
        /// The pre-authorization code created by the issuing bank.
        /// </summary>
        /// <value>The pre-authorization code created by the issuing bank.</value>
        [DataMember(Name="preAuthorizationCode", EmitDefaultValue=false)]
        public string PreAuthorizationCode { get; set; }
    
        /// <summary>
        /// The date issuance of the pre-authorization code.
        /// </summary>
        /// <value>The date issuance of the pre-authorization code.</value>
        [DataMember(Name="preAuthorizationDate", EmitDefaultValue=false)]
        public DateTime? PreAuthorizationDate { get; set; }
    
        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class PreAuthorizationModel {\n");
            sb.Append("  PreAuthorizationCode: ").Append(PreAuthorizationCode).Append("\n");
            sb.Append("  PreAuthorizationDate: ").Append(PreAuthorizationDate).Append("\n");
            
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
            return this.Equals(obj as PreAuthorizationModel);
        }

        /// <summary>
        /// Returns true if PreAuthorizationModel instances are equal
        /// </summary>
        /// <param name="other">Instance of PreAuthorizationModel to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(PreAuthorizationModel other)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            if (other == null)
                return false;

            return 
                (
                    this.PreAuthorizationCode == other.PreAuthorizationCode ||
                    this.PreAuthorizationCode != null &&
                    this.PreAuthorizationCode.Equals(other.PreAuthorizationCode)
                ) && 
                (
                    this.PreAuthorizationDate == other.PreAuthorizationDate ||
                    this.PreAuthorizationDate != null &&
                    this.PreAuthorizationDate.Equals(other.PreAuthorizationDate)
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
                
                if (this.PreAuthorizationCode != null)
                    hash = hash * 59 + this.PreAuthorizationCode.GetHashCode();
                
                if (this.PreAuthorizationDate != null)
                    hash = hash * 59 + this.PreAuthorizationDate.GetHashCode();
                
                return hash;
            }
        }
    }
}
