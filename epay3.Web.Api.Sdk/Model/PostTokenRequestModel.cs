using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using System.Text;

namespace epay3.Web.Api.Sdk.Model
{
    /// <summary>
    /// Creates a payment token.
    /// </summary>
    [DataContract]
    public partial class PostTokenRequestModel :  IEquatable<PostTokenRequestModel>
    { 
    
        /// <summary>
        /// Initializes a new instance of the <see cref="PostTokenRequestModel" /> class.
        /// Initializes a new instance of the <see cref="PostTokenRequestModel" />class.
        /// </summary>
        /// <param name="CreditCardInformation">Used for credit card tokens..</param>
        /// <param name="BankAccountInformation">Used for eCheck\\ACH tokens..</param>

        public PostTokenRequestModel(CreditCardInformationModel CreditCardInformation = null, BankAccountInformationModel BankAccountInformation = null)
        {
            this.CreditCardInformation = CreditCardInformation;
            this.BankAccountInformation = BankAccountInformation;
            
        }
        
    
        /// <summary>
        /// Used for credit card tokens.
        /// </summary>
        /// <value>Used for credit card tokens.</value>
        [DataMember(Name="creditCardInformation", EmitDefaultValue=false)]
        public CreditCardInformationModel CreditCardInformation { get; set; }
    
        /// <summary>
        /// Used for eCheck\\ACH tokens.
        /// </summary>
        /// <value>Used for eCheck\\ACH tokens.</value>
        [DataMember(Name="bankAccountInformation", EmitDefaultValue=false)]
        public BankAccountInformationModel BankAccountInformation { get; set; }
    
        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class PostTokenRequestModel {\n");
            sb.Append("  CreditCardInformation: ").Append(CreditCardInformation).Append("\n");
            sb.Append("  BankAccountInformation: ").Append(BankAccountInformation).Append("\n");
            
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
            return this.Equals(obj as PostTokenRequestModel);
        }

        /// <summary>
        /// Returns true if PostTokenRequestModel instances are equal
        /// </summary>
        /// <param name="other">Instance of PostTokenRequestModel to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(PostTokenRequestModel other)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            if (other == null)
                return false;

            return 
                (
                    this.CreditCardInformation == other.CreditCardInformation ||
                    this.CreditCardInformation != null &&
                    this.CreditCardInformation.Equals(other.CreditCardInformation)
                ) && 
                (
                    this.BankAccountInformation == other.BankAccountInformation ||
                    this.BankAccountInformation != null &&
                    this.BankAccountInformation.Equals(other.BankAccountInformation)
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
                
                if (this.CreditCardInformation != null)
                    hash = hash * 59 + this.CreditCardInformation.GetHashCode();
                
                if (this.BankAccountInformation != null)
                    hash = hash * 59 + this.BankAccountInformation.GetHashCode();
                
                return hash;
            }
        }

    }
}
