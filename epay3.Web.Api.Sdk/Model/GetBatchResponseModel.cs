using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace epay3.Web.Api.Sdk.Model
{
    public class GetBatchResponseModel
    {
        public long Id { get; set; }
        public DateTime Created { get; set; }
        public int NumberOfCredits { get; set; }
        public decimal TotalOfCredits { get; set; }
        public int NumberOfDebits { get; set; }
        public decimal TotalOfDebits { get; set; }
        public byte Currency { get; set; }
    }
}
