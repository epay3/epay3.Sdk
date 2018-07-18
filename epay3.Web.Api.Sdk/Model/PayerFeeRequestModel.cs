using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace epay3.Web.Api.Sdk.Model
{
    [DataContract]
    public class PayerFeeRequestModel
    {
        /// <summary>
        /// The amount from which to calculate the payer fee.
        /// </summary>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public decimal Amount { get; set; }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}
