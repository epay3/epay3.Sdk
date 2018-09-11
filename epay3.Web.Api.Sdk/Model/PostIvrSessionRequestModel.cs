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
    public partial class PostIvrSessionRequestModel :  IEquatable<PostIvrSessionRequestModel>
    { 
    
        /// <summary>
        /// Key/value collection of all custom attributes for the IVR session.
        /// </summary>
        /// <value>Key/value collection of all custom attributes for the IVR session.</value>
        [DataMember(Name="attributeValues", EmitDefaultValue=false)]
        public Dictionary<string, string> AttributeValues { get; set; }
    
        /// <summary>
        /// The phone number of the caller.
        /// </summary>
        /// <value>The phone number of the caller.</value>
        [DataMember(Name="phoneNumber", EmitDefaultValue=false)]
        public string PhoneNumber { get; set; }
    
        /// <summary>
        /// The amount of time from creation, in seconds, before the session expires.
        /// </summary>
        /// <value>The amount of time from creation, in seconds, before the session expires.</value>
        [DataMember(Name="expiration", EmitDefaultValue=false)]
        public int? Expiration { get; set; }
    
        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class PostIvrSessionRequestModel {\n");
            sb.Append("  AttributeValues: ").Append(AttributeValues).Append("\n");
            sb.Append("  PhoneNumber: ").Append(PhoneNumber).Append("\n");
            sb.Append("  Expiration: ").Append(Expiration).Append("\n");
            
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
            return this.Equals(obj as PostIvrSessionRequestModel);
        }

        /// <summary>
        /// Returns true if PostIvrSessionRequestModel instances are equal
        /// </summary>
        /// <param name="other">Instance of PostIvrSessionRequestModel to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(PostIvrSessionRequestModel other)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            if (other == null)
                return false;

            return 
                (
                    this.AttributeValues == other.AttributeValues ||
                    this.AttributeValues != null &&
                    this.AttributeValues.SequenceEqual(other.AttributeValues)
                ) && 
                (
                    this.PhoneNumber == other.PhoneNumber ||
                    this.PhoneNumber != null &&
                    this.PhoneNumber.Equals(other.PhoneNumber)
                ) && 
                (
                    this.Expiration == other.Expiration ||
                    this.Expiration != null &&
                    this.Expiration.Equals(other.Expiration)
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
                
                if (this.AttributeValues != null)
                    hash = hash * 59 + this.AttributeValues.GetHashCode();
                
                if (this.PhoneNumber != null)
                    hash = hash * 59 + this.PhoneNumber.GetHashCode();
                
                if (this.Expiration != null)
                    hash = hash * 59 + this.Expiration.GetHashCode();
                
                return hash;
            }
        }

    }
}
