using epay3.Web.Api.Sdk.Api;
using epay3.Web.Api.Sdk.Model;
using epay3.Web.Api.Tests.TestData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Net;

namespace epay3.Web.Api.Tests.Processor12
{
    [TestClass]
    public class When_Posting_A_Transaction
    {
        private TokensApi _tokensApi;
        private TransactionsApi _transactionsApi;
        private ITestData _testData;

        [TestInitialize]
        public void Initialize()
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            _testData = new TestData.Processor12();

            _transactionsApi = new TransactionsApi(_testData.Uri);
            _tokensApi = new TokensApi(_testData.Uri);

            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(_testData.Key + ":" + _testData.Secret);

            _tokensApi.Configuration.AddDefaultHeader("Authorization", "Basic " + Convert.ToBase64String(plainTextBytes));
            _transactionsApi.Configuration.AddDefaultHeader("Authorization", "Basic " + Convert.ToBase64String(plainTextBytes));
        }

        [TestMethod]
        public void Should_Successfully_Process_And_Void_Credit_Card()
        {
            var amount = Math.Round(new Random().NextDouble() * 100, 2);
            var postTransactionRequestModel = new PostTransactionRequestModel
            {
                Payer = "John Smith",
                EmailAddress = "jsmith@example.com",
                Amount = amount,
                CreditCardInformation = _testData.Amex,
                AttributeValues = new System.Collections.Generic.Dictionary<string, string> { { "phoneNumber", "512-234-1233" }, { "agentCode", "213498" } },
                Comments = "Sample comments",
                PayerFee = amount * .10
            };

            var response = _transactionsApi.TransactionsPost(postTransactionRequestModel, null);

            // Should return a valid Id.
            Assert.IsTrue(response.Id > 0);

            // Should successfully void a transaction.
            Assert.AreEqual(ReversalResponseCode.Success, _transactionsApi.TransactionsVoid(response.Id.Value, new PostVoidTransactionRequestModel { SendReceipt = false }).ReversalResponseCode);

            var getTransactionResponseModel = _transactionsApi.TransactionsGet(response.Id.Value);

            Assert.IsNotNull(getTransactionResponseModel);
            Assert.AreEqual("512-234-1233", getTransactionResponseModel.AttributeValues.Single(x => x.ParameterName == "phoneNumber").Value);
            Assert.IsNotNull(getTransactionResponseModel.Events.SingleOrDefault(x => x.EventType == EventType.Sale));
            Assert.IsNotNull(getTransactionResponseModel.Events.SingleOrDefault(x => x.EventType == EventType.Void));

            // Should not be able to void the transaction more than once.
            Assert.AreEqual(ReversalResponseCode.PreviouslyVoided, _transactionsApi.TransactionsVoid(response.Id.Value, new PostVoidTransactionRequestModel { SendReceipt = false }).ReversalResponseCode);
        }

        [TestMethod]
        public void Should_Fail_With_Invalid_Authorization_Id()
        {
            var amount = Math.Round(new Random().NextDouble() * 100, 2);
            var postTransactionRequestModel = new PostTransactionRequestModel
            {
                Payer = "John Smith",
                EmailAddress = "jsmith@example.com",
                Amount = amount,
                AuthorizationId = "INVALID_ID"
            };

            var response = _transactionsApi.TransactionsPost(postTransactionRequestModel, null);

            Assert.AreEqual(PaymentResponseCode.InvalidAuthorization, response.PaymentResponseCode);
        }
    }
}