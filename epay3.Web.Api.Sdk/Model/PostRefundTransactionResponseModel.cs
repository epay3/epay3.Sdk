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
    public partial class PostRefundTransactionResponseModel : IEquatable<PostRefundTransactionResponseModel>
    {

        /// <summary>
        /// Gets or Sets ReversalResponseCode
        /// </summary>
        [DataMember(Name = "reversalResponseCode", EmitDefaultValue = false)]
        public ReversalResponseCode? ReversalResponseCode { get; set; }

        /// <summary>
        /// The Id of the newly created refund transaction.
        /// </summary>
        /// <value>The Id of the newly created refund transaction.</value>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public long? Id { get; set; }

        public PostRefundTransactionResponseModel(long? Id = null, ReversalResponseCode? ReversalResponseCode = null)
        {
            this.Id = Id;
            this.ReversalResponseCode = ReversalResponseCode;
        }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class PostRefundTransactionResponseModel {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  ReversalResponseCode: ").Append(ReversalResponseCode).Append("\n");

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
            return this.Equals(obj as PostRefundTransactionResponseModel);
        }

        /// <summary>
        /// Returns true if PostRefundTransactionResponseModel instances are equal
        /// </summary>
        /// <param name="other">Instance of PostRefundTransactionResponseModel to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(PostRefundTransactionResponseModel other)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            if (other == null)
                return false;

            return
                (
                    this.Id == other.Id ||
                    this.Id != null &&
                    this.Id.Equals(other.Id)
                ) &&
                (
                    this.ReversalResponseCode == other.ReversalResponseCode ||
                    this.ReversalResponseCode != null &&
                    this.ReversalResponseCode.Equals(other.ReversalResponseCode)
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

                if (this.ReversalResponseCode != null)
                    hash = hash * 59 + this.ReversalResponseCode.GetHashCode();

                return hash;
            }
        }
    }
}
