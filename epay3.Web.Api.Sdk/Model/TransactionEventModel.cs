using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using System.Text;

namespace epay3.Web.Api.Sdk.Model
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class TransactionEventModel :  IEquatable<TransactionEventModel>
    { 
        /// <summary>
        /// The type of event.
        /// </summary>
        /// <value>The type of event.</value>
        [DataMember(Name="eventType", EmitDefaultValue=false)]
        public EventType? EventType { get; set; }
    
        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionEventModel" /> class.
        /// Initializes a new instance of the <see cref="TransactionEventModel" />class.
        /// </summary>
        /// <param name="EventDate">The date of the event..</param>
        /// <param name="EventType">The type of event..</param>
        /// <param name="Comments">Additional context describing the event if applicable..</param>

        public TransactionEventModel(DateTime? EventDate = null, EventType? EventType = null, string Comments = null)
        {
            this.EventDate = EventDate;
            this.EventType = EventType;
            this.Comments = Comments;
            
        }
        
    
        /// <summary>
        /// The date of the event.
        /// </summary>
        /// <value>The date of the event.</value>
        [DataMember(Name="eventDate", EmitDefaultValue=false)]
        public DateTime? EventDate { get; set; }
    
        /// <summary>
        /// Additional context describing the event if applicable.
        /// </summary>
        /// <value>Additional context describing the event if applicable.</value>
        [DataMember(Name="comments", EmitDefaultValue=false)]
        public string Comments { get; set; }
    
        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class TransactionEventModel {\n");
            sb.Append("  EventDate: ").Append(EventDate).Append("\n");
            sb.Append("  EventType: ").Append(EventType).Append("\n");
            sb.Append("  Comments: ").Append(Comments).Append("\n");
            
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
            return this.Equals(obj as TransactionEventModel);
        }

        /// <summary>
        /// Returns true if TransactionEventModel instances are equal
        /// </summary>
        /// <param name="other">Instance of TransactionEventModel to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(TransactionEventModel other)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            if (other == null)
                return false;

            return 
                (
                    this.EventDate == other.EventDate ||
                    this.EventDate != null &&
                    this.EventDate.Equals(other.EventDate)
                ) && 
                (
                    this.EventType == other.EventType ||
                    this.EventType != null &&
                    this.EventType.Equals(other.EventType)
                ) && 
                (
                    this.Comments == other.Comments ||
                    this.Comments != null &&
                    this.Comments.Equals(other.Comments)
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
                
                if (this.EventDate != null)
                    hash = hash * 59 + this.EventDate.GetHashCode();
                
                if (this.EventType != null)
                    hash = hash * 59 + this.EventType.GetHashCode();
                
                if (this.Comments != null)
                    hash = hash * 59 + this.Comments.GetHashCode();
                
                return hash;
            }
        }

    }
}
