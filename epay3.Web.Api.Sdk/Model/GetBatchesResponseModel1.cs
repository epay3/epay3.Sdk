using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace epay3.Web.Api.Sdk.Model
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class GetBatchesResponseModel : IEquatable<GetBatchesResponseModel>
    {
        /// <summary>
        /// The batches that make up the returned page of the search.
        /// </summary>
        [DataMember(Name = "batches", EmitDefaultValue = false)]
        public List<GetBatchResponseModel> Batches { get; set; }
        /// <summary>
        /// The total number of records in the search, including all pages.
        /// </summary>
        [DataMember(Name = "totalRecords", EmitDefaultValue = false)]
        public int TotalRecords { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class GetBatchesResponseModel {\n");
            sb.Append("  Batches: ").Append(Batches).Append("\n");

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
            return this.Equals(obj as GetBatchesResponseModel);
        }

        /// <summary>
        /// Returns true if GetBatchesResponseModel instances are equal
        /// </summary>
        /// <param name="other">Instance of GetBatchesResponseModel to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(GetBatchesResponseModel other)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            if (other == null)
                return false;

            return
                (
                    this.Batches == other.Batches ||
                    this.Batches != null &&
                    this.Batches.SequenceEqual(other.Batches)
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

                if (this.Batches != null)
                    hash = hash * 59 + this.Batches.GetHashCode();

                return hash;
            }
        }

    }
}
