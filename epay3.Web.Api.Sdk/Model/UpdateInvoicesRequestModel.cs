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
    public partial class UpdateInvoicesRequestModel : IEquatable<UpdateInvoicesRequestModel>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateInvoicesRequestModel" /> class.
        /// Initializes a new instance of the <see cref="UpdateInvoicesRequestModel" />class.
        /// </summary>
        /// <param name="PaidInvoices">PaidInvoices.</param>
        /// <param name="AttributeValues">AttributeValues.</param>

        public UpdateInvoicesRequestModel(List<PaidInvoiceModel> PaidInvoices = null, Dictionary<string, string> AttributeValues = null)
        {
            this.PaidInvoices = PaidInvoices;
            this.AttributeValues = AttributeValues;

        }


        /// <summary>
        /// Gets or Sets PaidInvoices
        /// </summary>
        [DataMember(Name = "paidInvoices", EmitDefaultValue = false)]
        public List<PaidInvoiceModel> PaidInvoices { get; set; }

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
            sb.Append("class UpdateInvoicesRequestModel {\n");
            sb.Append("  PaidInvoices: ").Append(PaidInvoices).Append("\n");
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
            return this.Equals(obj as UpdateInvoicesRequestModel);
        }

        /// <summary>
        /// Returns true if UpdateInvoicesRequestModel instances are equal
        /// </summary>
        /// <param name="other">Instance of UpdateInvoicesRequestModel to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(UpdateInvoicesRequestModel other)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            if (other == null)
                return false;

            return
                (
                    this.PaidInvoices == other.PaidInvoices ||
                    this.PaidInvoices != null &&
                    this.PaidInvoices.SequenceEqual(other.PaidInvoices)
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

                if (this.PaidInvoices != null)
                    hash = hash * 59 + this.PaidInvoices.GetHashCode();

                if (this.AttributeValues != null)
                    hash = hash * 59 + this.AttributeValues.GetHashCode();

                return hash;
            }
        }

    }
}
