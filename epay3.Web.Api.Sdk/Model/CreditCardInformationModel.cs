using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using System.Text;

namespace epay3.Web.Api.Sdk.Model
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class CreditCardInformationModel :  IEquatable<CreditCardInformationModel>
    { 
        /// <summary>
        /// Name that is on the credit card account.
        /// </summary>
        /// <value>Name that is on the credit card account.</value>
        [DataMember(Name="accountHolder", EmitDefaultValue=false)]
        public string AccountHolder { get; set; }
    
        /// <summary>
        /// Number for the credit card.
        /// </summary>
        /// <value>Number for the credit card.</value>
        [DataMember(Name="cardNumber", EmitDefaultValue=false)]
        public string CardNumber { get; set; }
    
        /// <summary>
        /// Security code for the credit card.
        /// </summary>
        /// <value>Security code for the credit card.</value>
        [DataMember(Name="cvc", EmitDefaultValue=false)]
        public string Cvc { get; set; }
    
        /// <summary>
        /// Month of card expiration.
        /// </summary>
        /// <value>Month of card expiration.</value>
        [DataMember(Name="month", EmitDefaultValue=false)]
        public byte? Month { get; set; }
    
        /// <summary>
        /// Year of card expiration.
        /// </summary>
        /// <value>Year of card expiration.</value>
        [DataMember(Name="year", EmitDefaultValue=false)]
        public int? Year { get; set; }
    
        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class CreditCardInformationModel {\n");
            sb.Append("  AccountHolder: ").Append(AccountHolder).Append("\n");
            sb.Append("  CardNumber: ").Append(CardNumber).Append("\n");
            sb.Append("  Cvc: ").Append(Cvc).Append("\n");
            sb.Append("  Month: ").Append(Month).Append("\n");
            sb.Append("  Year: ").Append(Year).Append("\n");
            
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
            return this.Equals(obj as CreditCardInformationModel);
        }

        /// <summary>
        /// Returns true if CreditCardInformationModel instances are equal
        /// </summary>
        /// <param name="other">Instance of CreditCardInformationModel to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(CreditCardInformationModel other)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            if (other == null)
                return false;

            return 
                (
                    this.AccountHolder == other.AccountHolder ||
                    this.AccountHolder != null &&
                    this.AccountHolder.Equals(other.AccountHolder)
                ) && 
                (
                    this.CardNumber == other.CardNumber ||
                    this.CardNumber != null &&
                    this.CardNumber.Equals(other.CardNumber)
                ) && 
                (
                    this.Cvc == other.Cvc ||
                    this.Cvc != null &&
                    this.Cvc.Equals(other.Cvc)
                ) && 
                (
                    this.Month == other.Month ||
                    this.Month != null &&
                    this.Month.Equals(other.Month)
                ) && 
                (
                    this.Year == other.Year ||
                    this.Year != null &&
                    this.Year.Equals(other.Year)
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
                
                if (this.AccountHolder != null)
                    hash = hash * 59 + this.AccountHolder.GetHashCode();
                
                if (this.CardNumber != null)
                    hash = hash * 59 + this.CardNumber.GetHashCode();
                
                if (this.Cvc != null)
                    hash = hash * 59 + this.Cvc.GetHashCode();
                
                if (this.Month != null)
                    hash = hash * 59 + this.Month.GetHashCode();
                
                if (this.Year != null)
                    hash = hash * 59 + this.Year.GetHashCode();
                
                return hash;
            }
        }

    }
}
