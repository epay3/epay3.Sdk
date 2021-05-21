using System;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using epay3.Web.Api.Sdk.Api;
using epay3.Web.Api.Sdk.Model;
using epay3.Web.Api.Sdk.Client;
using System.Collections.Generic;

namespace epay3.Web.Api.Tests
{
    [TestClass]
    public class When_posting_An_AutoPay
    {

        [TestInitialize]
        public void Initialize()
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
        }

        [TestMethod]
        public void Should_Create_And_Get()
        {
            // Setup
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(TestApiSettings.InvoiceKey + ":" + TestApiSettings.InvoiceSecret);
            var autoPayApi = new AutoPayApi(TestApiSettings.Uri);
            var tokensApi = new TokensApi(TestApiSettings.Uri);
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
                    Year = DateTime.Now.Year + 1,
                    PostalCode = "54321"
                }
            };

            autoPayApi.Configuration.AddDefaultHeader("Authorization", "Basic " + System.Convert.ToBase64String(plainTextBytes));
            tokensApi.Configuration.AddDefaultHeader("Authorization", "Basic " + System.Convert.ToBase64String(plainTextBytes));

            // Create Token
            var tokenId = tokensApi.TokensPost(postTokenRequestModel);

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
            
            var createdId = autoPayApi.AutoPayPost(autopayRequestModel);
            var gotten = autoPayApi.AutoPayGet(createdId.Value);

            Assert.IsNotNull(gotten);
            Assert.AreEqual(tokenId, gotten.TokenId);
        }

        [TestMethod]
        public void Should_Create_Get_And_Delete_With_Impersonation()
        {
            // setup
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(TestApiSettings.Key + ":" + TestApiSettings.Secret);
            var autoPayApi = new AutoPayApi(TestApiSettings.Uri);
            var tokensApi = new TokensApi(TestApiSettings.Uri);
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
                    Year = DateTime.Now.Year + 1,
                    PostalCode = "54321"
                }
            };

            autoPayApi.Configuration.AddDefaultHeader("Authorization", "Basic " + System.Convert.ToBase64String(plainTextBytes));
            tokensApi.Configuration.AddDefaultHeader("Authorization", "Basic " + System.Convert.ToBase64String(plainTextBytes));

            //Create Token
            var tokenId = tokensApi.TokensPost(postTokenRequestModel, TestApiSettings.InvoicesImpersonationAccountKey);

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
            var createdId = autoPayApi.AutoPayPost(autopayRequestModel, TestApiSettings.InvoicesImpersonationAccountKey);
            var gotten = autoPayApi.AutoPayGet(createdId.Value, TestApiSettings.InvoicesImpersonationAccountKey);
            autoPayApi.AutoPayCancel(createdId.Value, TestApiSettings.InvoicesImpersonationAccountKey);

            Assert.IsNotNull(gotten);
            Assert.AreEqual(tokenId, gotten.TokenId);
        }

        [TestMethod]
        public void Should_Create_And_Delete()
        {
            // Setup
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(TestApiSettings.InvoiceKey + ":" + TestApiSettings.InvoiceSecret);
            var autoPayApi = new AutoPayApi(TestApiSettings.Uri);
            var tokensApi = new TokensApi(TestApiSettings.Uri);
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
                    Year = DateTime.Now.Year + 1,
                    PostalCode = "54321"
                }
            };

            autoPayApi.Configuration.AddDefaultHeader("Authorization", "Basic " + System.Convert.ToBase64String(plainTextBytes));
            tokensApi.Configuration.AddDefaultHeader("Authorization", "Basic " + System.Convert.ToBase64String(plainTextBytes));

            // Create Token
            var tokenId = tokensApi.TokensPost(postTokenRequestModel);

            var autopayRequestModel = new PostAutoPayRequestModel
            {
                EmailAddress = "test@test.com",
                AttributeValues = new Dictionary<string, string>()
                {
                    ["accountCode"] = "123",
                    ["postalCode"] = "78702"
                },
                PublicTokenId = tokenId
            };

            var createdId = autoPayApi.AutoPayPost(autopayRequestModel);
            var result = autoPayApi.AutoPayCancel(createdId.Value);

            Assert.IsTrue(result);
        }
    }
}
