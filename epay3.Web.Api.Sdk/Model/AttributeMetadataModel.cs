using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace epay3.Web.Api.Sdk.Model
{
    /// <summary>
    /// Contains metadata about custom attributes.
    /// </summary>
    public class AttributeMetadataModel
    {
        /// <summary>
        /// The order in which the attribute should be displayed from left to right and top to bottom.
        /// </summary>
        public byte DisplayOrder { get; set; }
        /// <summary>
        /// The parameter name of the value as it's being read from the data source.
        /// </summary>
        public string ParameterName { get; set; }
        /// <summary>
        /// The name of the custom attribute as it's displayed on the user interface.
        /// </summary>
        public string DisplayName { get; set; }
    }
}
