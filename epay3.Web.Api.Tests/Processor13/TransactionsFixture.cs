using epay3.Web.Api.Sdk.Api;
using epay3.Web.Api.Sdk.Model;
using epay3.Web.Api.Tests.TestData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Net;

namespace epay3.Web.Api.Tests.Processor13
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

            _testData = new TestData.Processor13();

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
                CreditCardInformation = _testData.Mastercard,
                AttributeValues = new System.Collections.Generic.Dictionary<string, string> { { "phoneNumber", "512-234-1233" }, { "agentCode", "213498" } },
                Comments = "Sample comments",
                PayerFee = Math.Round(amount * .10, 2)
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

        [TestMethod]
        public void Should_Successfully_Use_Authorization_Id()
        {
            var postTokenRequestModel = new PostTokenRequestModel
            {
                Payer = "John Doe",
                EmailAddress = "jdoe@example.com",
                CreditCardInformation = _testData.Mastercard
            };

            var amount = Math.Round(new Random().NextDouble() * 100, 2);
            var tokenId = _tokensApi.TokensPost(postTokenRequestModel);

            var authorizationId = _transactionsApi.TransactionsAuthorize(new PostAuthorizeTransactionRequestModel
            {
                Amount = amount,
                TokenId = tokenId
            });

            Assert.IsNotNull(authorizationId);

            var postTransactionRequestModel = new PostTransactionRequestModel
            {
                Payer = "John Smith",
                EmailAddress = "jsmith@example.com",
                Amount = amount,
                AuthorizationId = authorizationId
            };

            var transactionResponse = _transactionsApi.TransactionsPost(postTransactionRequestModel, null);

            Assert.AreEqual(PaymentResponseCode.Success, transactionResponse.PaymentResponseCode);
        }

        [TestMethod]
        public void Should_Successfully_Use_Authorization_Id_With_Impersonation()
        {
            var postTokenRequestModel = new PostTokenRequestModel
            {
                Payer = "John Doe",
                EmailAddress = "jdoe@example.com",
                CreditCardInformation = _testData.Visa
            };

            var amount = Math.Round(new Random().NextDouble() * 100, 2);
            var tokenId = _tokensApi.TokensPost(postTokenRequestModel, _testData.ImpersonationAccountKey);

            var authorizationId = _transactionsApi.TransactionsAuthorize(new PostAuthorizeTransactionRequestModel
            {
                Amount = amount,
                TokenId = tokenId
            }, _testData.ImpersonationAccountKey);

            Assert.IsNotNull(authorizationId, _testData.ImpersonationAccountKey);

            var postTransactionRequestModel = new PostTransactionRequestModel
            {
                Payer = "John Smith",
                EmailAddress = "jsmith@example.com",
                Amount = amount,
                AuthorizationId = authorizationId
            };

            // This attempt should fail without the same impersonation key.
            var transactionResponse = _transactionsApi.TransactionsPost(postTransactionRequestModel);

            Assert.AreEqual(PaymentResponseCode.InvalidAuthorization, transactionResponse.PaymentResponseCode);

            // This attempt should succeed with the the correct impersonation key.
            transactionResponse = _transactionsApi.TransactionsPost(postTransactionRequestModel, _testData.ImpersonationAccountKey);

            Assert.AreEqual(PaymentResponseCode.Success, transactionResponse.PaymentResponseCode);
        }
    }
}