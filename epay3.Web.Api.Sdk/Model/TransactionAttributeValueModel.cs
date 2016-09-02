using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using System.Text;

namespace epay3.Web.Api.Sdk.Model
{
    /// <summary>
    /// Contains information about a single attribute value that is tied to a transaction.
    /// </summary>
    [DataContract]
    public partial class TransactionAttributeValueModel :  IEquatable<TransactionAttributeValueModel>
    { 
    
        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionAttributeValueModel" /> class.
        /// Initializes a new instance of the <see cref="TransactionAttributeValueModel" />class.
        /// </summary>
        /// <param name="Name">The user-friendly name of the attribute..</param>
        /// <param name="ParameterName">The parameter name of the attribute..</param>
        /// <param name="Value">The value of the attribute..</param>

        public TransactionAttributeValueModel(string Name = null, string ParameterName = null, string Value = null)
        {
            this.Name = Name;
            this.ParameterName = ParameterName;
            this.Value = Value;
            
        }
        
    
        /// <summary>
        /// The user-friendly name of the attribute.
        /// </summary>
        /// <value>The user-friendly name of the attribute.</value>
        [DataMember(Name="name", EmitDefaultValue=false)]
        public string Name { get; set; }
    
        /// <summary>
        /// The parameter name of the attribute.
        /// </summary>
        /// <value>The parameter name of the attribute.</value>
        [DataMember(Name="parameterName", EmitDefaultValue=false)]
        public string ParameterName { get; set; }
    
        /// <summary>
        /// The value of the attribute.
        /// </summary>
        /// <value>The value of the attribute.</value>
        [DataMember(Name="value", EmitDefaultValue=false)]
        public string Value { get; set; }
    
        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class TransactionAttributeValueModel {\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  ParameterName: ").Append(ParameterName).Append("\n");
            sb.Append("  Value: ").Append(Value).Append("\n");
            
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
            return this.Equals(obj as TransactionAttributeValueModel);
        }

        /// <summary>
        /// Returns true if TransactionAttributeValueModel instances are equal
        /// </summary>
        /// <param name="other">Instance of TransactionAttributeValueModel to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(TransactionAttributeValueModel other)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            if (other == null)
                return false;

            return 
                (
                    this.Name == other.Name ||
                    this.Name != null &&
                    this.Name.Equals(other.Name)
                ) && 
                (
                    this.ParameterName == other.ParameterName ||
                    this.ParameterName != null &&
                    this.ParameterName.Equals(other.ParameterName)
                ) && 
                (
                    this.Value == other.Value ||
                    this.Value != null &&
                    this.Value.Equals(other.Value)
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
                
                if (this.Name != null)
                    hash = hash * 59 + this.Name.GetHashCode();
                
                if (this.ParameterName != null)
                    hash = hash * 59 + this.ParameterName.GetHashCode();
                
                if (this.Value != null)
                    hash = hash * 59 + this.Value.GetHashCode();
                
                return hash;
            }
        }

    }
}
