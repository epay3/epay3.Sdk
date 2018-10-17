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
    public partial class GetTransactionFeesResponseModel :  IEquatable<GetTransactionFeesResponseModel>
    { 
        /// <summary>
        /// Gets or Sets AchPayerFee
        /// </summary>
        [DataMember(Name="achPayerFee", EmitDefaultValue=false)]
        public decimal AchPayerFee { get; set; }
    
        /// <summary>
        /// Gets or Sets CreditCardPayerFee
        /// </summary>
        [DataMember(Name="creditCardPayerFee", EmitDefaultValue=false)]
        public decimal CreditCardPayerFee { get; set; }
    
        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class GetTransactionFeesResponseModel {\n");
            sb.Append("  AchPayerFee: ").Append(AchPayerFee).Append("\n");
            sb.Append("  CreditCardPayerFee: ").Append(CreditCardPayerFee).Append("\n");
            
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
            return this.Equals(obj as GetTransactionFeesResponseModel);
        }

        /// <summary>
        /// Returns true if GetTransactionFeesResponseModel instances are equal
        /// </summary>
        /// <param name="other">Instance of GetTransactionFeesResponseModel to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(GetTransactionFeesResponseModel other)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            if (other == null)
                return false;

            return 
                (
                    this.AchPayerFee == other.AchPayerFee ||
                    this.AchPayerFee.Equals(other.AchPayerFee)
                ) && 
                (
                    this.CreditCardPayerFee == other.CreditCardPayerFee ||
                    this.CreditCardPayerFee.Equals(other.CreditCardPayerFee)
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
                
                hash = hash * 59 + this.AchPayerFee.GetHashCode();
                
                hash = hash * 59 + this.CreditCardPayerFee.GetHashCode();
                
                return hash;
            }
        }

    }
}
