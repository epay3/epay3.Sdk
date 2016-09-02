using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Runtime.Serialization;
using System.Text;

namespace epay3.Web.Api.Sdk.Model
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class BankAccountInformationModel :  IEquatable<BankAccountInformationModel>
    { 
    
        /// <summary>
        /// The type of the bank account.
        /// </summary>
        /// <value>The type of the bank account.</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum AccountTypeEnum {
            
            [EnumMember(Value = "PersonalChecking")]
            Personalchecking,
            
            [EnumMember(Value = "PersonalSavings")]
            Personalsavings,
            
            [EnumMember(Value = "CorporateChecking")]
            Corporatechecking,
            
            [EnumMember(Value = "CorporateSavings")]
            Corporatesavings
        }

    
        /// <summary>
        /// The type of the bank account.
        /// </summary>
        /// <value>The type of the bank account.</value>
        [DataMember(Name="accountType", EmitDefaultValue=false)]
        public AccountTypeEnum? AccountType { get; set; }
    
        /// <summary>
        /// Name that is on the bank account.
        /// </summary>
        /// <value>Name that is on the bank account.</value>
        [DataMember(Name="accountHolder", EmitDefaultValue=false)]
        public string AccountHolder { get; set; }
    
        /// <summary>
        /// First name of the person authorizing the transaction.
        /// </summary>
        /// <value>First name of the person authorizing the transaction.</value>
        [DataMember(Name="firstName", EmitDefaultValue=false)]
        public string FirstName { get; set; }
    
        /// <summary>
        /// Last name of the person authorizing the transaction.
        /// </summary>
        /// <value>Last name of the person authorizing the transaction.</value>
        [DataMember(Name="lastName", EmitDefaultValue=false)]
        public string LastName { get; set; }
    
        /// <summary>
        /// The 9-digit routing number.
        /// </summary>
        /// <value>The 9-digit routing number.</value>
        [DataMember(Name="routingNumber", EmitDefaultValue=false)]
        public string RoutingNumber { get; set; }
    
        /// <summary>
        /// The bank account number.
        /// </summary>
        /// <value>The bank account number.</value>
        [DataMember(Name="accountNumber", EmitDefaultValue=false)]
        public string AccountNumber { get; set; }
    
        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class BankAccountInformationModel {\n");
            sb.Append("  AccountHolder: ").Append(AccountHolder).Append("\n");
            sb.Append("  FirstName: ").Append(FirstName).Append("\n");
            sb.Append("  LastName: ").Append(LastName).Append("\n");
            sb.Append("  AccountType: ").Append(AccountType).Append("\n");
            sb.Append("  RoutingNumber: ").Append(RoutingNumber).Append("\n");
            sb.Append("  AccountNumber: ").Append(AccountNumber).Append("\n");
            
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
            return this.Equals(obj as BankAccountInformationModel);
        }

        /// <summary>
        /// Returns true if BankAccountInformationModel instances are equal
        /// </summary>
        /// <param name="other">Instance of BankAccountInformationModel to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(BankAccountInformationModel other)
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
                    this.FirstName == other.FirstName ||
                    this.FirstName != null &&
                    this.FirstName.Equals(other.FirstName)
                ) && 
                (
                    this.LastName == other.LastName ||
                    this.LastName != null &&
                    this.LastName.Equals(other.LastName)
                ) && 
                (
                    this.AccountType == other.AccountType ||
                    this.AccountType != null &&
                    this.AccountType.Equals(other.AccountType)
                ) && 
                (
                    this.RoutingNumber == other.RoutingNumber ||
                    this.RoutingNumber != null &&
                    this.RoutingNumber.Equals(other.RoutingNumber)
                ) && 
                (
                    this.AccountNumber == other.AccountNumber ||
                    this.AccountNumber != null &&
                    this.AccountNumber.Equals(other.AccountNumber)
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
                
                if (this.FirstName != null)
                    hash = hash * 59 + this.FirstName.GetHashCode();
                
                if (this.LastName != null)
                    hash = hash * 59 + this.LastName.GetHashCode();
                
                if (this.AccountType != null)
                    hash = hash * 59 + this.AccountType.GetHashCode();
                
                if (this.RoutingNumber != null)
                    hash = hash * 59 + this.RoutingNumber.GetHashCode();
                
                if (this.AccountNumber != null)
                    hash = hash * 59 + this.AccountNumber.GetHashCode();
                
                return hash;
            }
        }

    }
}
