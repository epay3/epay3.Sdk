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
    /// Creates a payment token.
    /// </summary>
    [DataContract]
    public partial class PostTokenRequestModel :  IEquatable<PostTokenRequestModel>
    { 
    
        /// <summary>
        /// Initializes a new instance of the <see cref="PostTokenRequestModel" /> class.
        /// Initializes a new instance of the <see cref="PostTokenRequestModel" />class.
        /// </summary>
        /// <param name="Payer">Name of the payer that is storing the token. (required).</param>
        /// <param name="EmailAddress">The email address of the payer. (required).</param>
        /// <param name="CreditCardInformation">Used for credit card tokens..</param>
        /// <param name="BankAccountInformation">Used for eCheck\\ACH tokens..</param>
        /// <param name="AttributeValues">Dictionary of custom attribute values. The key in the dictionary is the identifier of the custom attribute..</param>

        public PostTokenRequestModel(string Payer = null, string EmailAddress = null, CreditCardInformationModel CreditCardInformation = null, BankAccountInformationModel BankAccountInformation = null, Dictionary<string, string> AttributeValues = null)
        {
            this.CreditCardInformation = CreditCardInformation;
            this.BankAccountInformation = BankAccountInformation;
            this.AttributeValues = AttributeValues;
            
        }
        
    
        /// <summary>
        /// Name of the payer that is storing the token.
        /// </summary>
        /// <value>Name of the payer that is storing the token.</value>
        [DataMember(Name="payer", EmitDefaultValue=false)]
        public string Payer { get; set; }
    
        /// <summary>
        /// The email address of the payer.
        /// </summary>
        /// <value>The email address of the payer.</value>
        [DataMember(Name="emailAddress", EmitDefaultValue=false)]
        public string EmailAddress { get; set; }
    
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
        /// Dictionary of custom attribute values. The key in the dictionary is the identifier of the custom attribute.
        /// </summary>
        /// <value>Dictionary of custom attribute values. The key in the dictionary is the identifier of the custom attribute.</value>
        [DataMember(Name="attributeValues", EmitDefaultValue=false)]
        public Dictionary<string, string> AttributeValues { get; set; }
    
        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class PostTokenRequestModel {\n");
            sb.Append("  Payer: ").Append(Payer).Append("\n");
            sb.Append("  EmailAddress: ").Append(EmailAddress).Append("\n");
            sb.Append("  CreditCardInformation: ").Append(CreditCardInformation).Append("\n");
            sb.Append("  BankAccountInformation: ").Append(BankAccountInformation).Append("\n");
            sb.Append("  AttributeValues: ").Append(AttributeValues).Append("\n");
            
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
                    this.Payer == other.Payer ||
                    this.Payer != null &&
                    this.Payer.Equals(other.Payer)
                ) && 
                (
                    this.EmailAddress == other.EmailAddress ||
                    this.EmailAddress != null &&
                    this.EmailAddress.Equals(other.EmailAddress)
                ) && 
                (
                    this.CreditCardInformation == other.CreditCardInformation ||
                    this.CreditCardInformation != null &&
                    this.CreditCardInformation.Equals(other.CreditCardInformation)
                ) && 
                (
                    this.BankAccountInformation == other.BankAccountInformation ||
                    this.BankAccountInformation != null &&
                    this.BankAccountInformation.Equals(other.BankAccountInformation)
                ) && 
                (
                    this.AttributeValues == other.AttributeValues ||
                    this.AttributeValues != null &&
                    this.AttributeValues.SequenceEqual(other.AttributeValues)
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
                
                if (this.Payer != null)
                    hash = hash * 59 + this.Payer.GetHashCode();
                
                if (this.EmailAddress != null)
                    hash = hash * 59 + this.EmailAddress.GetHashCode();
                
                if (this.CreditCardInformation != null)
                    hash = hash * 59 + this.CreditCardInformation.GetHashCode();
                
                if (this.BankAccountInformation != null)
                    hash = hash * 59 + this.BankAccountInformation.GetHashCode();
                
                if (this.AttributeValues != null)
                    hash = hash * 59 + this.AttributeValues.GetHashCode();
                
                return hash;
            }
        }

    }
}
