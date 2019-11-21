using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace epay3.Web.Api.Sdk.Model
{
    public class InvoicesResponseModel
    {
        public string PayerName { get; set; }
        public string EmailAddress { get; set; }
        /// <summary>
        /// Metadata on any custom attributes that will be displayed at the invoice level.
        /// </summary>
        public List<AttributeMetadataModel> InvoiceAttributeMetadata { get; set; }
        /// <summary>
        /// Metadata on any custom attributes that will be displayed at the invoice item level.
        /// </summary>
        public List<AttributeMetadataModel> InvoiceItemAttributeMetadata { get; set; }
        /// <summary>
        /// The collection of invoices.
        /// </summary>
        public List<InvoiceModel> Invoices { get; set; }
        /// <summary>
        /// Whether the referenced account was found.
        /// </summary>
        public InvoiceStatus Status { get; set; }

        public InvoicesResponseModel()
        {
            Invoices = new List<InvoiceModel>();
            InvoiceAttributeMetadata = new List<AttributeMetadataModel>();
            InvoiceItemAttributeMetadata = new List<AttributeMetadataModel>();
            Status = InvoiceStatus.Success;
        }
    }

    public enum InvoiceStatus
    {
        Success,
        AccountNotFound
    }
}
