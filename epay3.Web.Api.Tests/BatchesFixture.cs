using epay3.Web.Api.Sdk.Api;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace epay3.Web.Api.Tests
{
    [TestClass]
    public class BatchesFixture
    {
        private BatchesApi _batchesApi;

        [TestInitialize]
        public void Initialize()
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            _batchesApi = new BatchesApi(TestApiSettings.Uri);

            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(TestApiSettings.Key + ":" + TestApiSettings.Secret);

            _batchesApi.Configuration.AddDefaultHeader("Authorization", "Basic " + System.Convert.ToBase64String(plainTextBytes));
        }

        [TestMethod]
        public void Should_Get_Successfully()
        {
            var result = _batchesApi.BatchesGet();

            // Should get successfully.
            Assert.IsTrue(result.Batches.Count > 0);
            Assert.IsTrue(result.TotalRecords > 0);
            Assert.IsTrue(result.Batches.Any(x => x.Id == 129)); 
        }

        [TestMethod]
        public void Should_Get_Successfully_With_Impersonation_Key()
        {
            var result = _batchesApi.BatchesGet(null, TestApiSettings.InvoicesImpersonationAccountKey);

            // Should get successfully.
            Assert.IsTrue(result.Batches.Count > 0);
            Assert.IsTrue(result.TotalRecords > 0);
        }
    }
}
