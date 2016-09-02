using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace epay3.Web.Api.Sdk.Model
{
    /// <summary>
    /// Provides details of a transaction.
    /// </summary>
    [DataContract]
    public partial class GetTransactionResponseModel :  IEquatable<GetTransactionResponseModel>
    { 
    
        /// <summary>
        /// Initializes a new instance of the <see cref="GetTransactionResponseModel" /> class.
        /// Initializes a new instance of the <see cref="GetTransactionResponseModel" />class.
        /// </summary>
        /// <param name="Id">The unique identifier of the transaction..</param>
        /// <param name="Payer">The name of the payer..</param>
        /// <param name="EmailAddress">The email address of the payer..</param>
        /// <param name="TransactionType">The type of the transaction..</param>
        /// <param name="Amount">The total amount of the transaction that was charged to the payer including all fees..</param>
        /// <param name="Fee">The transaction fee charged..</param>
        /// <param name="PayerFee">The fee charnged to the payer..</param>
        /// <param name="Comments">Comments left by the payer at the initial creation of the transaction..</param>
        /// <param name="Events">A collection of all events that have occured..</param>
        /// <param name="AttributeValues">A collection of key/value pairs for any custom attribute values for this transaction..</param>
        /// <param name="Attachments">A collection of all attachments for this transaction..</param>

        public GetTransactionResponseModel(long? Id = null, string Payer = null, string EmailAddress = null, string TransactionType = null, double? Amount = null, double? Fee = null, double? PayerFee = null, string Comments = null, List<TransactionEventModel> Events = null, List<TransactionAttributeValueModel> AttributeValues = null, List<AttachmentModel> Attachments = null)
        {
            this.Id = Id;
            this.Payer = Payer;
            this.EmailAddress = EmailAddress;
            this.TransactionType = TransactionType;
            this.Amount = Amount;
            this.Fee = Fee;
            this.PayerFee = PayerFee;
            this.Comments = Comments;
            this.Events = Events;
            this.AttributeValues = AttributeValues;
            this.Attachments = Attachments;
            
        }
        
    
        /// <summary>
        /// The unique identifier of the transaction.
        /// </summary>
        /// <value>The unique identifier of the transaction.</value>
        [DataMember(Name="id", EmitDefaultValue=false)]
        public long? Id { get; set; }
    
        /// <summary>
        /// The name of the payer.
        /// </summary>
        /// <value>The name of the payer.</value>
        [DataMember(Name="payer", EmitDefaultValue=false)]
        public string Payer { get; set; }
    
        /// <summary>
        /// The email address of the payer.
        /// </summary>
        /// <value>The email address of the payer.</value>
        [DataMember(Name="emailAddress", EmitDefaultValue=false)]
        public string EmailAddress { get; set; }
    
        /// <summary>
        /// The type of the transaction.
        /// </summary>
        /// <value>The type of the transaction.</value>
        [DataMember(Name="transactionType", EmitDefaultValue=false)]
        public string TransactionType { get; set; }
    
        /// <summary>
        /// The total amount of the transaction that was charged to the payer including all fees.
        /// </summary>
        /// <value>The total amount of the transaction that was charged to the payer including all fees.</value>
        [DataMember(Name="amount", EmitDefaultValue=false)]
        public double? Amount { get; set; }
    
        /// <summary>
        /// The transaction fee charged.
        /// </summary>
        /// <value>The transaction fee charged.</value>
        [DataMember(Name="fee", EmitDefaultValue=false)]
        public double? Fee { get; set; }
    
        /// <summary>
        /// The fee charnged to the payer.
        /// </summary>
        /// <value>The fee charnged to the payer.</value>
        [DataMember(Name="payerFee", EmitDefaultValue=false)]
        public double? PayerFee { get; set; }
    
        /// <summary>
        /// Comments left by the payer at the initial creation of the transaction.
        /// </summary>
        /// <value>Comments left by the payer at the initial creation of the transaction.</value>
        [DataMember(Name="comments", EmitDefaultValue=false)]
        public string Comments { get; set; }
    
        /// <summary>
        /// A collection of all events that have occured.
        /// </summary>
        /// <value>A collection of all events that have occured.</value>
        [DataMember(Name="events", EmitDefaultValue=false)]
        public List<TransactionEventModel> Events { get; set; }
    
        /// <summary>
        /// A collection of key/value pairs for any custom attribute values for this transaction.
        /// </summary>
        /// <value>A collection of key/value pairs for any custom attribute values for this transaction.</value>
        [DataMember(Name="attributeValues", EmitDefaultValue=false)]
        public List<TransactionAttributeValueModel> AttributeValues { get; set; }
    
        /// <summary>
        /// A collection of all attachments for this transaction.
        /// </summary>
        /// <value>A collection of all attachments for this transaction.</value>
        [DataMember(Name="attachments", EmitDefaultValue=false)]
        public List<AttachmentModel> Attachments { get; set; }
    
        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class GetTransactionResponseModel {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  Payer: ").Append(Payer).Append("\n");
            sb.Append("  EmailAddress: ").Append(EmailAddress).Append("\n");
            sb.Append("  TransactionType: ").Append(TransactionType).Append("\n");
            sb.Append("  Amount: ").Append(Amount).Append("\n");
            sb.Append("  Fee: ").Append(Fee).Append("\n");
            sb.Append("  PayerFee: ").Append(PayerFee).Append("\n");
            sb.Append("  Comments: ").Append(Comments).Append("\n");
            sb.Append("  Events: ").Append(Events).Append("\n");
            sb.Append("  AttributeValues: ").Append(AttributeValues).Append("\n");
            sb.Append("  Attachments: ").Append(Attachments).Append("\n");
            
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
            return this.Equals(obj as GetTransactionResponseModel);
        }

        /// <summary>
        /// Returns true if GetTransactionResponseModel instances are equal
        /// </summary>
        /// <param name="other">Instance of GetTransactionResponseModel to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(GetTransactionResponseModel other)
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
                    this.Payer == other.Payer ||
                    this.Payer != null &&
                    this.Payer.Equals(other.Payer)
                ) && 
                (
                    this.EmailAddress == other.EmailAddress ||
                    this.EmailAddress != null &&
                    this.EmailAddress.Equals(other.EmailAddress)
                ) && 
                (
                    this.TransactionType == other.TransactionType ||
                    this.TransactionType != null &&
                    this.TransactionType.Equals(other.TransactionType)
                ) && 
                (
                    this.Amount == other.Amount ||
                    this.Amount != null &&
                    this.Amount.Equals(other.Amount)
                ) && 
                (
                    this.Fee == other.Fee ||
                    this.Fee != null &&
                    this.Fee.Equals(other.Fee)
                ) && 
                (
                    this.PayerFee == other.PayerFee ||
                    this.PayerFee != null &&
                    this.PayerFee.Equals(other.PayerFee)
                ) && 
                (
                    this.Comments == other.Comments ||
                    this.Comments != null &&
                    this.Comments.Equals(other.Comments)
                ) && 
                (
                    this.Events == other.Events ||
                    this.Events != null &&
                    this.Events.SequenceEqual(other.Events)
                ) && 
                (
                    this.AttributeValues == other.AttributeValues ||
                    this.AttributeValues != null &&
                    this.AttributeValues.SequenceEqual(other.AttributeValues)
                ) && 
                (
                    this.Attachments == other.Attachments ||
                    this.Attachments != null &&
                    this.Attachments.SequenceEqual(other.Attachments)
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
                
                if (this.Payer != null)
                    hash = hash * 59 + this.Payer.GetHashCode();
                
                if (this.EmailAddress != null)
                    hash = hash * 59 + this.EmailAddress.GetHashCode();
                
                if (this.TransactionType != null)
                    hash = hash * 59 + this.TransactionType.GetHashCode();
                
                if (this.Amount != null)
                    hash = hash * 59 + this.Amount.GetHashCode();
                
                if (this.Fee != null)
                    hash = hash * 59 + this.Fee.GetHashCode();
                
                if (this.PayerFee != null)
                    hash = hash * 59 + this.PayerFee.GetHashCode();
                
                if (this.Comments != null)
                    hash = hash * 59 + this.Comments.GetHashCode();
                
                if (this.Events != null)
                    hash = hash * 59 + this.Events.GetHashCode();
                
                if (this.AttributeValues != null)
                    hash = hash * 59 + this.AttributeValues.GetHashCode();
                
                if (this.Attachments != null)
                    hash = hash * 59 + this.Attachments.GetHashCode();
                
                return hash;
            }
        }

    }
}
