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
    public partial class PostTransactionFeesResponseModel :  IEquatable<PostTransactionFeesResponseModel>
    { 
    
        /// <summary>
        /// Initializes a new instance of the <see cref="PostTransactionFeesResponseModel" /> class.
        /// Initializes a new instance of the <see cref="PostTransactionFeesResponseModel" />class.
        /// </summary>
        /// <param name="AchPayerFee">AchPayerFee.</param>
        /// <param name="CreditCardPayerFee">CreditCardPayerFee.</param>

        public PostTransactionFeesResponseModel(double? AchPayerFee = null, double? CreditCardPayerFee = null)
        {
            this.AchPayerFee = AchPayerFee;
            this.CreditCardPayerFee = CreditCardPayerFee;
            
        }
        
    
        /// <summary>
        /// Gets or Sets AchPayerFee
        /// </summary>
        [DataMember(Name="achPayerFee", EmitDefaultValue=false)]
        public double? AchPayerFee { get; set; }
    
        /// <summary>
        /// Gets or Sets CreditCardPayerFee
        /// </summary>
        [DataMember(Name="creditCardPayerFee", EmitDefaultValue=false)]
        public double? CreditCardPayerFee { get; set; }
    
        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class PostTransactionFeesResponseModel {\n");
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
            return this.Equals(obj as PostTransactionFeesResponseModel);
        }

        /// <summary>
        /// Returns true if PostTransactionFeesResponseModel instances are equal
        /// </summary>
        /// <param name="other">Instance of PostTransactionFeesResponseModel to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(PostTransactionFeesResponseModel other)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            if (other == null)
                return false;

            return 
                (
                    this.AchPayerFee == other.AchPayerFee ||
                    this.AchPayerFee != null &&
                    this.AchPayerFee.Equals(other.AchPayerFee)
                ) && 
                (
                    this.CreditCardPayerFee == other.CreditCardPayerFee ||
                    this.CreditCardPayerFee != null &&
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
                
                if (this.AchPayerFee != null)
                    hash = hash * 59 + this.AchPayerFee.GetHashCode();
                
                if (this.CreditCardPayerFee != null)
                    hash = hash * 59 + this.CreditCardPayerFee.GetHashCode();
                
                return hash;
            }
        }

    }
}
