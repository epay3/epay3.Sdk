using System;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using epay3.Web.Api.Sdk.Api;
using epay3.Web.Api.Sdk.Model;
using epay3.Web.Api.Sdk.Client;
using System.Collections.Generic;
using System.Linq;

namespace epay3.Web.Api.Tests
{
    [TestClass]
    public class When_posting_An_AutoPay
    {
        private AutoPayApi _autoPayApi;
        private string _tokenId;

        [TestInitialize]
        public void Initialize()
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            _autoPayApi = new AutoPayApi(TestApiSettings.Uri);
            var tokensApi = new TokensApi(TestApiSettings.Uri);


            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(TestApiSettings.Key + ":" + TestApiSettings.Secret);

            _autoPayApi.Configuration.AddDefaultHeader("Authorization", "Basic " + System.Convert.ToBase64String(plainTextBytes));
            tokensApi.Configuration.AddDefaultHeader("Authorization", "Basic " + System.Convert.ToBase64String(plainTextBytes));
            var postTokenRequestModel = new PostTokenRequestModel
            {
                Payer = "John Doe",
                EmailAddress = "jdoe@example.com",
                CreditCardInformation = new CreditCardInformationModel
                {
                    AccountHolder = "John Doe",
                    CardNumber = "4457119922390123",
                    Cvc = "123",
                    Month = 12,
                    Year = 2024,
                    PostalCode = "54321"
                }
            };

            _tokenId = tokensApi.TokensPost(postTokenRequestModel, TestApiSettings.ImpersonationAccountKey);
        }
        [TestMethod]
        public void Should_Create_And_Get()
        {
            var autopayRequestModel = new PostAutoPayRequestModel
            {
                Email = "test@test.com",
                AttributeValues = new Dictionary<string, string>()
                {
                    ["accountCode"] = "123",
                    ["postalCode"] = "78701"
                },
                PublicTokenId = _tokenId,
                MaxAmount = 1000
            };
            var createdId = _autoPayApi.AutoPayPost(autopayRequestModel);
            var gotten = _autoPayApi.AutoPayGet(createdId);

            Assert.IsNotNull(gotten);
            Assert.AreEqual(_tokenId, gotten.TokenId);
        }
    }
}
