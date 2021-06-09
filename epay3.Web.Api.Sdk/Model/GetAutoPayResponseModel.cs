using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace epay3.Web.Api.Sdk.Model
{
    public class GetAutoPayResponseModel : IEquatable<GetAutoPayResponseModel>
    {
        /// <summary>
        /// The unique identifier of the AutoPay.
        /// </summary>        
        /// <value>The unique identifier of the AutoPay.</value>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public long Id { get; set; }
        /// <summary>
        /// The token Id that represents the payment method to be used on the payments.
        /// </summary>
        /// <value>The token Id that represents the payment method to be used on the payments.</value>
        [DataMember(Name = "tokenId", EmitDefaultValue = false)]
        public string TokenId { get; set; }
        /// <summary>
        /// The attributes associated with the AutoPay.
        /// </summary>
        /// <value>The attributes associated with the AutoPay.</value>
        [DataMember(Name = "attributes", EmitDefaultValue = false)]
        public Dictionary<string, string> Attributes { get; set; }
        /// <summary>
        /// The Email address associated with the AutoPay.
        /// </summary>
        /// <value>The Email address associated with the AutoPay.</value>
        [DataMember(Name = "emailAddress", EmitDefaultValue = false)]
        public string EmailAddress { get; set; }


        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="obj">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object obj)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            return this.Equals(obj as GetAutoPayResponseModel);
        }

        /// <summary>
        /// Returns true if GetAutoPayResponseModel instances are equal
        /// </summary>
        /// <param name="other">Instance of GetAutoPayResponseModel to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(GetAutoPayResponseModel other)
        {
            if (other == null)
                return false;
            return (
                    this.Id == other.Id ||
                    this.Id != null &&
                    this.Id.Equals(other.Id)
                ) &&
                (
                    this.TokenId == other.TokenId ||
                    this.TokenId != null &&
                    this.TokenId.Equals(other.TokenId)
                ) &&
                (
                    this.Attributes == other.Attributes ||
                    this.Attributes != null &&
                    this.Attributes.Equals(other.Attributes)
                ) &&
                (
                    this.EmailAddress == other.EmailAddress ||
                    this.EmailAddress != null &&
                    this.EmailAddress.Equals(other.EmailAddress)
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

                if (this.Id != null)
                    hash = hash * 59 + this.Id.GetHashCode();

                if (this.TokenId != null)
                    hash = hash * 59 + this.TokenId.GetHashCode();

                if (this.Attributes != null)
                    hash = hash * 59 + this.Attributes.GetHashCode();

                if (this.EmailAddress != null)
                    hash = hash * 59 + this.EmailAddress.GetHashCode();

                return hash;
            }
        }
    }
}
