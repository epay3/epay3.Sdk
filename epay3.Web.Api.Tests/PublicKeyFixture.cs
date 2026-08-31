using epay3.Web.Api.Sdk.Api;
using epay3.Web.Api.Sdk.Client;
using epay3.Web.Api.Sdk.Model;
using epay3.Web.Api.Tests.TestData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;

namespace epay3.Web.Api.Tests
{
    [TestClass]
    public class When_Using_A_Public_Key
    {
        private TokensApi _tokensApi;
        private TransactionsApi _transactionsApi;
        private ITestData _testData;

        [TestInitialize]
        public void Initialize()
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            _testData = new TestData.Processor7();

            _tokensApi = new TokensApi(_testData.Uri);
            _transactionsApi = new TransactionsApi(_testData.Uri);

            _tokensApi.Configuration.AddDefaultHeader("Authorization", "Api-Key " + _testData.PublicKey);
            _transactionsApi.Configuration.AddDefaultHeader("Authorization", "Api-Key " + _testData.PublicKey);
        }

        [TestMethod]
        public void Should_Successfully_Store_Credit_Card_Token()
        {
            var postTokenRequestModel = new PostTokenRequestModel
            {
                Payer = "John Doe",
                EmailAddress = "jdoe@example.com",
                CreditCardInformation = _testData.Visa
            };

            var id = _tokensApi.TokensPost(postTokenRequestModel);

            // Should return a valid Id.
            Assert.IsTrue(!string.IsNullOrWhiteSpace(id));

            try
            {
                _tokensApi.TokensDelete(id);

                // Should not be able to delete a token.
                Assert.Fail();
            }
            catch (ApiException)
            {
            }
        }

        [TestMethod]
        public void Should_Successfully_Store_Credit_Card_Token_Amex()
        {
            var postTokenRequestModel = new PostTokenRequestModel
            {
                Payer = "John Doe",
                EmailAddress = "jdoe@example.com",
                CreditCardInformation = _testData.Amex
            };

            var id = _tokensApi.TokensPost(postTokenRequestModel);

            // Should return a valid Id.
            Assert.IsTrue(!string.IsNullOrWhiteSpace(id));

            try
            {
                _tokensApi.TokensDelete(id);

                // Should not be able to delete a token.
                Assert.Fail();
            }
            catch (ApiException)
            {
            }
        }

        [TestMethod]
        public void Should_Successfully_Store_Credit_Card_Token_With_Impersonation()
        {
            var postTokenRequestModel = new PostTokenRequestModel
            {
                Payer = "John Doe",
                EmailAddress = "jdoe@example.com",
                CreditCardInformation = _testData.Visa
            };

            var id = _tokensApi.TokensPost(postTokenRequestModel, _testData.ImpersonationAccountKey);

            // Should return a valid Id.
            Assert.IsTrue(!string.IsNullOrWhiteSpace(id));

            try
            {
                _tokensApi.TokensDelete(id, _testData.ImpersonationAccountKey);

                // Should not be able to delete a token.
                Assert.Fail();
            }
            catch (ApiException)
            {
            }
        }

        [TestMethod]
        public void Should_Not_Successfully_Process_A_Transaction()
        {
            var amount = System.Math.Round(new System.Random().NextDouble() * 100, 2);
            var postTransactionRequestModel = new PostTransactionRequestModel
            {
                Payer = "John Smith",
                EmailAddress = "jsmith@example.com",
                Amount = amount,
                CreditCardInformation = new CreditCardInformationModel
                {
                    AccountHolder = "John Smith",
                    CardNumber = "5454545454545454",
                    Cvc = "123",
                    Month = 12,
                    Year = System.DateTime.Now.Year + 2,
                    PostalCode = "54321"
                },
                AttributeValues = new System.Collections.Generic.Dictionary<string, string> { { "phoneNumber", "512-234-1233" }, { "agentCode", "213498" } },
                Comments = "Sample comments",
                PayerFee = amount * .10
            };

            try
            {
                var response = _transactionsApi.TransactionsPost(postTransactionRequestModel, null);

                // Should not be able to delete a token.
                Assert.Fail();
            }
            catch (ApiException)
            {
            }
        }
    }
}