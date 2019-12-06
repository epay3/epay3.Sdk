using System;
using System.Collections.Generic;

namespace epay3.Web.Api.Sdk.Model
{
    /// <summary>
    /// Describes the details of an individual invoice.
    /// </summary>
    public class InvoiceModel
    {
        /// <summary>
        /// The unique identifier of the invoice.
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// The customer name on the invoice.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The due date of the invoice.
        /// </summary>
        public DateTime? DueDate { get; set; }
        /// <summary>
        /// The total amount due.
        /// </summary>
        public decimal? Amount { get; set; }
        /// <summary>
        /// The maximum amount the payer is allowed to pay.
        /// </summary>
        public decimal MaximumAmount { get; set; }
        /// <summary>
        /// Indicates whether a partial payment is allowed on the invoice.
        /// </summary>
        public bool AllowPartialPayment { get; set; }
        /// <summary>
        /// The actual values of the custom attributes at the invoice level.
        /// </summary>
        public List<AttributeValueModel> AttributeValues { get; set; }
        /// <summary>
        /// A collection of invoice items.
        /// </summary>
        public List<InvoiceItemModel> InvoiceItems { get; set; }

        public InvoiceModel()
        {
            AttributeValues = new List<AttributeValueModel>();
            InvoiceItems = new List<InvoiceItemModel>();
        }
    }
}
