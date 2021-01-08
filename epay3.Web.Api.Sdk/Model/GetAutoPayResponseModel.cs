using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace epay3.Web.Api.Sdk.Model
{
    public class GetAutoPayResponseModel : IEquatable<GetPaymentScheduleResponseModel>
    {
        /// <summary>
        /// The unique identifier of the payment schedule.
        /// </summary>        
        /// <value>The unique identifier of the payment schedule.</value>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string Id { get; set; }
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
        [DataMember(Name = "email", EmitDefaultValue = false)]
        public string Email { get; set; }

        public bool Equals(GetPaymentScheduleResponseModel other)
        {
            throw new NotImplementedException();
        }
    }
}
