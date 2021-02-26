using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace epay3.Web.Api.Sdk.Model
{
    /// <summary>
    /// Creates an Auto Pay for recurring payments.
    /// </summary>
    [DataContract]
    public class PostAutoPayRequestModel
    {
        /// <summary>
        /// The public token id of each recurring payment.
        /// </summary>
        /// <value>The public token id of each recurring payment.</value>
        [DataMember(Name = "publicTokenId", EmitDefaultValue = false)]
        public string PublicTokenId { get; set; }

        /// <summary>
        /// The search values for a recurring payment.
        /// </summary>
        /// <value>The search values for a recurring payment.</value>
        [DataMember(Name = "attributeValues", EmitDefaultValue = false)]
        public Dictionary<string, string> AttributeValues { get; set; }

        /// <summary>
        /// The associated email of each recurring payment.
        /// </summary>
        /// <value>The associated email of each recurring payment.</value>
        [DataMember(Name = "emailAddress", EmitDefaultValue = false)]
        public string EmailAddress { get; set; }
    }
}
