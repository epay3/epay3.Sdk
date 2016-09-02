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
    public partial class AttachmentModel :  IEquatable<AttachmentModel>
    { 
    
        /// <summary>
        /// Initializes a new instance of the <see cref="AttachmentModel" /> class.
        /// Initializes a new instance of the <see cref="AttachmentModel" />class.
        /// </summary>
        /// <param name="Name">The original name of the file..</param>
        /// <param name="DownloadUri">The Uri that will return the bytes of the file for download..</param>

        public AttachmentModel(string Name = null, string DownloadUri = null)
        {
            this.Name = Name;
            this.DownloadUri = DownloadUri;
            
        }
        
    
        /// <summary>
        /// The original name of the file.
        /// </summary>
        /// <value>The original name of the file.</value>
        [DataMember(Name="name", EmitDefaultValue=false)]
        public string Name { get; set; }
    
        /// <summary>
        /// The Uri that will return the bytes of the file for download.
        /// </summary>
        /// <value>The Uri that will return the bytes of the file for download.</value>
        [DataMember(Name="downloadUri", EmitDefaultValue=false)]
        public string DownloadUri { get; set; }
    
        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class AttachmentModel {\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  DownloadUri: ").Append(DownloadUri).Append("\n");
            
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
            return this.Equals(obj as AttachmentModel);
        }

        /// <summary>
        /// Returns true if AttachmentModel instances are equal
        /// </summary>
        /// <param name="other">Instance of AttachmentModel to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(AttachmentModel other)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            if (other == null)
                return false;

            return 
                (
                    this.Name == other.Name ||
                    this.Name != null &&
                    this.Name.Equals(other.Name)
                ) && 
                (
                    this.DownloadUri == other.DownloadUri ||
                    this.DownloadUri != null &&
                    this.DownloadUri.Equals(other.DownloadUri)
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
                
                if (this.Name != null)
                    hash = hash * 59 + this.Name.GetHashCode();
                
                if (this.DownloadUri != null)
                    hash = hash * 59 + this.DownloadUri.GetHashCode();
                
                return hash;
            }
        }

    }
}
