using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace epay3.Web.Api.Sdk.Model
{
    /// <summary>
    /// Holds information for an individual invoice item.
    /// </summary>
    public class InvoiceItemModel
    {
        /// <summary>
        /// The Id of the invoice item.
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// A collection of custom attribute values.
        /// </summary>
        public List<AttributeValueModel> AttributeValues { get; set; }

        public InvoiceItemModel()
        {
            AttributeValues = new List<AttributeValueModel>();
        }
    }
}
