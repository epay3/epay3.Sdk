using epay3.Web.Api.Sdk.Api;
using epay3.Web.Api.Sdk.Model;
using epay3.Web.Api.Tests.TestData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net;

namespace epay3.Web.Api.Tests
{
    [TestClass]
    public class When_posting_An_AutoPay
    {
        private ITestData _testData;

        private byte[] _plainTextBytes;
        private AutoPayApi _autoPayApi;
        private TokensApi _tokensApi;

        [TestInitialize]
        public void Initialize()
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            _testData = new TestData.Processor7();

            _plainTextBytes = System.Text.Encoding.UTF8.GetBytes(_testData.InvoiceKey + ":" + _testData.InvoiceSecret);
            _autoPayApi = new AutoPayApi(_testData.Uri);
            _tokensApi = new TokensApi(_testData.Uri);
            _autoPayApi.Configuration.AddDefaultHeader("Authorization", "Basic " + System.Convert.ToBase64String(_plainTextBytes));
            _tokensApi.Configuration.AddDefaultHeader("Authorization", "Basic " + System.Convert.ToBase64String(_plainTextBytes));
        }

        [TestMethod]
        public void Should_Create_And_Get()
        {
            var postTokenRequestModel = new PostTokenRequestModel
            {
                Payer = "John Doe",
                EmailAddress = "jdoe@example.com",
                CreditCardInformation = _testData.Visa
            };

            // Create Token
            var tokenId = _tokensApi.TokensPost(postTokenRequestModel);

            var id = Guid.NewGuid();
            var autopayRequestModel = new PostAutoPayRequestModel
            {
                EmailAddress = "test@test.com",
                AttributeValues = new Dictionary<string, string>()
                {
                    ["accountCode"] = "123",
                    ["postalCode"] = "78701",
                    ["uniqueId"] = id.ToString()
                },
                PublicTokenId = tokenId
            };

            var createdId = _autoPayApi.AutoPayPost(autopayRequestModel);
            var gotten = _autoPayApi.AutoPayGet(createdId.Value);

            Assert.IsNotNull(gotten);
            Assert.AreEqual(tokenId, gotten.TokenId);
        }

        [TestMethod]
        public void Should_Create_And_Get_Amex()
        {
            var postTokenRequestModel = new PostTokenRequestModel
            {
                Payer = "John Doe",
                EmailAddress = "jdoe@example.com",
                CreditCardInformation = _testData.Amex
            };

            // Create Token
            var tokenId = _tokensApi.TokensPost(postTokenRequestModel);

            var id = Guid.NewGuid();
            var autopayRequestModel = new PostAutoPayRequestModel
            {
                EmailAddress = "test@test.com",
                AttributeValues = new Dictionary<string, string>()
                {
                    ["accountCode"] = "123",
                    ["postalCode"] = "78701",
                    ["uniqueId"] = id.ToString()
                },
                PublicTokenId = tokenId
            };

            var createdId = _autoPayApi.AutoPayPost(autopayRequestModel);
            var gotten = _autoPayApi.AutoPayGet(createdId.Value);

            Assert.IsNotNull(gotten);
            Assert.AreEqual(tokenId, gotten.TokenId);
        }

        [TestMethod]
        public void Should_Create_Get_And_Delete_With_Impersonation()
        {
            // setup
            var postTokenRequestModel = new PostTokenRequestModel
            {
                Payer = "John Doe",
                EmailAddress = "jdoe@example.com",
                CreditCardInformation = _testData.Mastercard
            };

            //Create Token
            var tokenId = _tokensApi.TokensPost(postTokenRequestModel, _testData.InvoicesImpersonationAccountKey);

            var id = Guid.NewGuid();
            var autopayRequestModel = new PostAutoPayRequestModel
            {
                EmailAddress = "test@test.com",
                AttributeValues = new Dictionary<string, string>()
                {
                    ["accountCode"] = "123",
                    ["postalCode"] = "78701",
                    ["uniqueId"] = id.ToString()
                },
                PublicTokenId = tokenId
            };

            // Create and get autopay
            var createdId = _autoPayApi.AutoPayPost(autopayRequestModel, _testData.InvoicesImpersonationAccountKey);
            var gotten = _autoPayApi.AutoPayGet(createdId.Value, _testData.InvoicesImpersonationAccountKey);
            _autoPayApi.AutoPayCancel(createdId.Value, _testData.InvoicesImpersonationAccountKey);

            Assert.IsNotNull(gotten);
            Assert.AreEqual(tokenId, gotten.TokenId);
        }

        [TestMethod]
        public void Should_Create_And_Delete()
        {
            var postTokenRequestModel = new PostTokenRequestModel
            {
                Payer = "John Doe",
                EmailAddress = "jdoe@example.com",
                CreditCardInformation = _testData.Visa
            };

            // Create Token
            var tokenId = _tokensApi.TokensPost(postTokenRequestModel);

            var autopayRequestModel = new PostAutoPayRequestModel
            {
                EmailAddress = "test@test.com",
                AttributeValues = new Dictionary<string, string>()
                {
                    // randomizing accountCode to bypass duplicate prevention
                    ["accountCode"] = new Random().Next(1000,9999).ToString(),
                    ["postalCode"] = "78702"
                },
                PublicTokenId = tokenId
            };

            var createdId = _autoPayApi.AutoPayPost(autopayRequestModel);
            var result = _autoPayApi.AutoPayCancel(createdId.Value);

            Assert.IsTrue(result);
        }
    }
}