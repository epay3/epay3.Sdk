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
    public partial class UpdateInvoicesRequestModel
    {
        /// <summary>
        /// The unique identifier for the transaction.
        /// </summary>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public long Id { get; set; }

        /// <summary>
        /// The name of the payer initiating the transaction.
        /// </summary>
        [DataMember(Name = "payer", EmitDefaultValue = false)]
        public string Payer { get; set; }

        /// <summary>
        /// The email address of the payer.
        /// </summary>
        [DataMember(Name = "emailAddress", EmitDefaultValue = false)]
        public string EmailAddress { get; set; }

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
    }
}
