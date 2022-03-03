using epay3.Web.Api.Sdk.Api;
using epay3.Web.Api.Sdk.Model;
using epay3.Web.Api.Tests.TestData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Net;

namespace epay3.Web.Api.Tests
{
    [TestClass]
    public class When_Searching_Transactions
    {
        private TransactionsApi _transactionsApi;
        private BatchesApi _batchesApi;
        private ITestData _testData;

        [TestInitialize]
        public void Initialize()
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            _testData = new TestData.Processor7();

            _transactionsApi = new TransactionsApi(_testData.Uri);

            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(_testData.Key + ":" + _testData.Secret);

            _transactionsApi.Configuration.AddDefaultHeader("Authorization", "Basic " + System.Convert.ToBase64String(plainTextBytes));
        }

        [TestMethod]
        public void Should_Successfully_Find_Transactions()
        {
            // Search results can be iterated through, with each returned transaction coming back in the form of a GetTransactionResponseModel.
            var searchResults = _transactionsApi.TransactionsSearch(beginDate: DateTime.Parse("1/1/2020"), endDate: DateTime.UtcNow,
                transactionSearchTypeId: TransactionSearchType.Processed, minAmount: -200m, maxAmount: 1000m, pageSize: 5, page: 1, impersonationAccountKey: _testData.ImpersonationAccountKey);
            Assert.IsNotNull(searchResults);

            // Additionally, every parameter when searching for transactions is optional.
            var searchAllResults = _transactionsApi.TransactionsSearch();
            Assert.IsTrue(searchAllResults.TotalRecords > 0);
            Assert.IsNotNull(searchAllResults);
        }

        [TestMethod]
        public void Should_Successfully_Finds_Transactions_For_Batch()
        {
            _batchesApi = new BatchesApi(_testData.Uri);

            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(_testData.Key + ":" + _testData.Secret);

            _batchesApi.Configuration.AddDefaultHeader("Authorization", "Basic " + System.Convert.ToBase64String(plainTextBytes));

            var batchSearchResults = _batchesApi.BatchesGet(1);

            Assert.IsTrue(batchSearchResults.Batches.Any());

            var transactionSearchResults = _transactionsApi.TransactionsSearch(DateTime.MinValue, null, null, null, null, batchSearchResults.Batches.First().Id, null, null, null);

            Assert.IsTrue(transactionSearchResults.Transactions.Any());
        }
    }
}