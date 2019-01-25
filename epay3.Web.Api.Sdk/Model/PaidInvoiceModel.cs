using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace epay3.Web.Api.Sdk.Model
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class PaidInvoiceModel : IEquatable<PaidInvoiceModel>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="PaidInvoiceModel" /> class.
        /// Initializes a new instance of the <see cref="PaidInvoiceModel" />class.
        /// </summary>
        /// <param name="Id">Id.</param>
        /// <param name="PaidAmount">PaidAmount.</param>
        /// <param name="AttributeValues">AttributeValues.</param>

        public PaidInvoiceModel(string Id = null, double? PaidAmount = null, Dictionary<string, string> AttributeValues = null)
        {
            this.Id = Id;
            this.PaidAmount = PaidAmount;
            this.AttributeValues = AttributeValues;

        }


        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string Id { get; set; }

        /// <summary>
        /// Gets or Sets PaidAmount
        /// </summary>
        [DataMember(Name = "paidAmount", EmitDefaultValue = false)]
        public double? PaidAmount { get; set; }

        /// <summary>
        /// Gets or Sets AttributeValues
        /// </summary>
        [DataMember(Name = "attributeValues", EmitDefaultValue = false)]
        public Dictionary<string, string> AttributeValues { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class PaidInvoiceModel {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  PaidAmount: ").Append(PaidAmount).Append("\n");
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
            return this.Equals(obj as PaidInvoiceModel);
        }

        /// <summary>
        /// Returns true if PaidInvoiceModel instances are equal
        /// </summary>
        /// <param name="other">Instance of PaidInvoiceModel to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(PaidInvoiceModel other)
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
                    this.PaidAmount == other.PaidAmount ||
                    this.PaidAmount != null &&
                    this.PaidAmount.Equals(other.PaidAmount)
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

                if (this.Id != null)
                    hash = hash * 59 + this.Id.GetHashCode();

                if (this.PaidAmount != null)
                    hash = hash * 59 + this.PaidAmount.GetHashCode();

                if (this.AttributeValues != null)
                    hash = hash * 59 + this.AttributeValues.GetHashCode();

                return hash;
            }
        }

    }
}
