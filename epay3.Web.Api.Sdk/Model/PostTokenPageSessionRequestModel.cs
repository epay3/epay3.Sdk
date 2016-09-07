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
    /// Represents the information needed to customize a single token via the token page.
    /// </summary>
    [DataContract]
    public partial class PostTokenPageSessionRequestModel :  IEquatable<PostTokenPageSessionRequestModel>
    { 
    
        /// <summary>
        /// Initializes a new instance of the <see cref="PostTokenPageSessionRequestModel" /> class.
        /// Initializes a new instance of the <see cref="PostTokenPageSessionRequestModel" />class.
        /// </summary>
        /// <param name="AttributeValues">Key/value collection of all custom attributes that will eventually be stored with the token..</param>
        /// <param name="SuccessUrl">The Url to which the user will be redirected upon a token being successfully created..</param>

        public PostTokenPageSessionRequestModel(Dictionary<string, string> AttributeValues = null, string SuccessUrl = null)
        {
            this.AttributeValues = AttributeValues;
            this.SuccessUrl = SuccessUrl;
            
        }
        
    
        /// <summary>
        /// Key/value collection of all custom attributes that will eventually be stored with the token.
        /// </summary>
        /// <value>Key/value collection of all custom attributes that will eventually be stored with the token.</value>
        [DataMember(Name="attributeValues", EmitDefaultValue=false)]
        public Dictionary<string, string> AttributeValues { get; set; }
    
        /// <summary>
        /// The Url to which the user will be redirected upon a token being successfully created.
        /// </summary>
        /// <value>The Url to which the user will be redirected upon a token being successfully created.</value>
        [DataMember(Name="successUrl", EmitDefaultValue=false)]
        public string SuccessUrl { get; set; }
    
        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class PostTokenPageSessionRequestModel {\n");
            sb.Append("  AttributeValues: ").Append(AttributeValues).Append("\n");
            sb.Append("  SuccessUrl: ").Append(SuccessUrl).Append("\n");
            
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
            return this.Equals(obj as PostTokenPageSessionRequestModel);
        }

        /// <summary>
        /// Returns true if PostTokenPageSessionRequestModel instances are equal
        /// </summary>
        /// <param name="other">Instance of PostTokenPageSessionRequestModel to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(PostTokenPageSessionRequestModel other)
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
                    this.SuccessUrl == other.SuccessUrl ||
                    this.SuccessUrl != null &&
                    this.SuccessUrl.Equals(other.SuccessUrl)
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
                
                if (this.SuccessUrl != null)
                    hash = hash * 59 + this.SuccessUrl.GetHashCode();
                
                return hash;
            }
        }

    }
}
