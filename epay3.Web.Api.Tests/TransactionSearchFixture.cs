using epay3.Web.Api.Sdk.Api;
using epay3.Web.Api.Sdk.Model;
using epay3.Web.Api.Tests.TestData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
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
            var searchResults = _transactionsApi.TransactionsSearch(beginDate: DateTime.Parse("8/1/2022"), endDate: DateTime.Parse("8/30/2022"),
                transactionSearchTypeId: TransactionSearchType.Processed, minAmount: -200m, maxAmount: 1000m, pageSize: 5, page: 1, impersonationAccountKey: _testData.ImpersonationAccountKey);
            Assert.IsNotNull(searchResults);

            // Additionally, every parameter when searching for transactions is optional.
            var searchAllResults = _transactionsApi.TransactionsSearch();
            Assert.IsTrue(searchAllResults.TotalRecords > 0);
            Assert.IsNotNull(searchAllResults);
        }

        [TestMethod]
        public void Should_Successfully_Find_Chargeback_Transactions()
        {
            var chargebackSearchResults = _transactionsApi.TransactionsSearch(beginDate: DateTime.Parse("3/1/2020"), endDate: DateTime.Parse("3/31/2024"),
               transactionSearchTypeId: TransactionSearchType.Chargeback, minAmount: -200m, maxAmount: 1000m, pageSize: 5, page: 1, impersonationAccountKey: _testData.ImpersonationAccountKey);

            Assert.IsNotNull(chargebackSearchResults.Transactions);

            foreach (var transaction in chargebackSearchResults.Transactions)
            {
                Assert.IsNotNull(transaction.Events);

                // Ensure that there is at least one chargeback event in this transaction
                bool hasChargebackEvent = transaction.Events.Any(e => e.EventType == EventType.Chargeback);
                Assert.IsTrue(hasChargebackEvent, $"Transaction {transaction.Id} does have a Chargeback event");

                // Ensure that there is no chargeback reversed event in this transaction
                bool hasChargebackReversedEvent = transaction.Events.Any(e => e.EventType == EventType.ChargebackReversal);
                Assert.IsFalse(hasChargebackReversedEvent, $"Transaction {transaction.Id} should not have a ChargebackReversed event");
            }
        }

        [TestMethod]
        public void Should_Successfully_Find_Returned_Transactions()
        {
            var chargebackSearchResults = _transactionsApi.TransactionsSearch(beginDate: DateTime.Parse("3/1/2020"), endDate: DateTime.Parse("3/31/2024"),
               transactionSearchTypeId: TransactionSearchType.Rejected, minAmount: -200m, maxAmount: 1000m, pageSize: 5, page: 1, impersonationAccountKey: _testData.ImpersonationAccountKey);

            Assert.IsNotNull(chargebackSearchResults.Transactions);

            foreach (var transaction in chargebackSearchResults.Transactions)
            {
                Assert.IsNotNull(transaction.Events);

                // Ensure that there is at least one chargeback event in this transaction
                bool hasRejectEvent = transaction.Events.Any(e => e.EventType == EventType.Reject);
                Assert.IsTrue(hasRejectEvent, $"Transaction {transaction.Id} does have a Reject event");
            }
        }

        [TestMethod]
        public void Should_Ignore_CreateDate_EndDate_When_Searched_With_BeginCreateDate_EndCreateDate()
        {
            // Search transactions using beginDate and endDate
            var searchResultsWithBeginEndDate = _transactionsApi.TransactionsSearch(beginDate: DateTime.Parse("8/1/2022"), endDate: DateTime.Parse("8/30/2022"),
                transactionSearchTypeId: TransactionSearchType.Processed, minAmount: -200m, maxAmount: 1000m, pageSize: 5, page: 1, impersonationAccountKey: _testData.ImpersonationAccountKey);
            Assert.IsNotNull(searchResultsWithBeginEndDate);

            // Search transactions using beginCreateDate and endCreateDate (time period with in beginDae and endDate)
            var searchResultsWithBeginEndCreateDate = _transactionsApi.TransactionsSearch(beginDate: DateTime.Parse("8/1/2022"), endDate: DateTime.Parse("8/30/2022"),
                beginCreateDate: DateTime.Parse("8/9/2022"), endCreateDate: DateTime.Parse("8/19/2022"), transactionSearchTypeId: TransactionSearchType.Processed, minAmount: -200m, maxAmount: 1000m, pageSize: 5, page: 1, impersonationAccountKey: _testData.ImpersonationAccountKey);
            Assert.IsNotNull(searchResultsWithBeginEndCreateDate);

            // Check results are not same even if the begin/end create dates lie between begin/end dates. 
            Assert.AreNotEqual(searchResultsWithBeginEndDate.TotalRecords, searchResultsWithBeginEndCreateDate.TotalRecords);
        }

        [TestMethod]
        public void Should_Search_Using_SaleDate_When_Searched_With_BeginCreateDate_EndCreateDate()
        {
            // Search transactions using beginDate and endDate
            var searchResultsWithBeginEndDate = _transactionsApi.TransactionsSearch(beginDate: DateTime.Parse("8/1/2022"), endDate: DateTime.Parse("8/30/2022"),
                transactionSearchTypeId: TransactionSearchType.Processed, minAmount: -200m, maxAmount: 1000m, pageSize: 5, page: 1, impersonationAccountKey: _testData.ImpersonationAccountKey);
            Assert.IsNotNull(searchResultsWithBeginEndDate);

            // Search transactions using beginCreateDate and endCreateDate (time period with in beginDae and endDate)
            var searchResultsWithBeginEndCreateDate = _transactionsApi.TransactionsSearch(beginCreateDate: DateTime.Parse("8/1/2022"), endCreateDate: DateTime.Parse("8/30/2022"),
                transactionSearchTypeId: TransactionSearchType.Processed, minAmount: -200m, maxAmount: 1000m, pageSize: 5, page: 1, impersonationAccountKey: _testData.ImpersonationAccountKey);
            Assert.IsNotNull(searchResultsWithBeginEndCreateDate);

            // Transaction with id 553560 has SaleDate = '2022-08-18 21:04:25.123' and CreateDate = '2022-06-18 21:04:25.123'
            Assert.IsFalse(searchResultsWithBeginEndDate.Transactions.Any(transaction => transaction.Id == 553560));
            Assert.IsTrue(searchResultsWithBeginEndCreateDate.Transactions.Any(transaction => transaction.Id == 553560));
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