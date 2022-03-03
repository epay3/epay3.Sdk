using epay3.Web.Api.Sdk.Api;
using epay3.Web.Api.Tests.TestData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Net;

namespace epay3.Web.Api.Tests
{
    [TestClass]
    public class BatchesFixture
    {
        private BatchesApi _batchesApi;
        private ITestData _testData;

        [TestInitialize]
        public void Initialize()
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            _testData = new TestData.Processor7();

            _batchesApi = new BatchesApi(_testData.Uri);

            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(_testData.Key + ":" + _testData.Secret);

            _batchesApi.Configuration.AddDefaultHeader("Authorization", "Basic " + System.Convert.ToBase64String(plainTextBytes));
        }

        [TestMethod]
        public void Should_Get_Successfully()
        {
            var result = _batchesApi.BatchesGet();

            // Should get successfully.
            Assert.IsTrue(result.Batches.Count > 0);
            Assert.IsTrue(result.TotalRecords > 0);
            Assert.IsTrue(result.Batches.All(x => x.Id != _testData.ImpersonationOnlyBatchId));
        }

        [TestMethod]
        public void Should_Get_Successfully_With_Impersonation_Key()
        {
            var result = _batchesApi.BatchesGet(null, _testData.ImpersonationAccountKey);

            // Should get successfully.
            Assert.IsTrue(result.Batches.Count > 0);
            Assert.IsTrue(result.TotalRecords > 0);
            Assert.IsTrue(result.Batches.Any(x => x.Id == _testData.ImpersonationOnlyBatchId));
        }
    }
}