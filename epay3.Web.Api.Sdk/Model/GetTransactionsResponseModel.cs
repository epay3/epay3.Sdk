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
    public partial class GetTransactionsResponseModel :  IEquatable<GetTransactionsResponseModel>
    { 
        /// <summary>
        /// The transactions that make up the returned page of the search.
        /// </summary>
        [DataMember(Name="transactions", EmitDefaultValue=false)]
        public List<GetTransactionResponseModel> Transactions { get; set; }
        /// <summary>
        /// The total number of records in the search, including all pages.
        /// </summary>
        [DataMember(Name="totalRecords", EmitDefaultValue=false)]
        public int TotalRecords { get; set; }
    
        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class GetTransactionsResponseModel {\n");
            sb.Append("  Transactions: ").Append(Transactions).Append("\n");
            
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
            return this.Equals(obj as GetTransactionsResponseModel);
        }

        /// <summary>
        /// Returns true if GetTransactionsResponseModel instances are equal
        /// </summary>
        /// <param name="other">Instance of GetTransactionsResponseModel to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(GetTransactionsResponseModel other)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            if (other == null)
                return false;

            return 
                (
                    this.Transactions == other.Transactions ||
                    this.Transactions != null &&
                    this.Transactions.SequenceEqual(other.Transactions)
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
                
                if (this.Transactions != null)
                    hash = hash * 59 + this.Transactions.GetHashCode();
                
                return hash;
            }
        }

    }
}
