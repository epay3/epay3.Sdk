﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using epay3.Web.Api.Sdk.Api;
using epay3.Web.Api.Sdk.Model;
using epay3.Web.Api.Sdk.Client;
using System.Net;
using System.Linq;

namespace epay3.Web.Api.Tests
{
    [TestClass]
    public class TokenPageSessionsFixture
    {
        private TokenPageSessionsApi _tokenPageSessionsApi;

        [TestInitialize]
        public void Initialize()
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            _tokenPageSessionsApi = new TokenPageSessionsApi(TestApiSettings.Uri);

            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(TestApiSettings.Key + ":" + TestApiSettings.Secret);

            _tokenPageSessionsApi.Configuration.AddDefaultHeader("Authorization", "Basic " + System.Convert.ToBase64String(plainTextBytes));
        }

        [TestMethod]
        public void Should_Return_An_Id_Upon_Success_With_No_Impersonation_Key()
        {
            var postTokenPageSessionRequestModel = new PostTokenPageSessionRequestModel
            {
                AttributeValues = new System.Collections.Generic.Dictionary<string, string>
                {
                    { "parameter 1", "value 1" },
                    { "parameter 2", "value 2" }
                },
                SuccessUrl = "https://www.example.com"
            };

            var id = _tokenPageSessionsApi.TokenPageSessionsPost(postTokenPageSessionRequestModel, null);

            // Should return a valid Id.
            Assert.IsNotNull(id);
        }

        [TestMethod]
        public void Should_Return_An_Id_Upon_Success_With_An_Impersonation_Key()
        {
            var postTokenPageSessionRequestModel = new PostTokenPageSessionRequestModel
            {
                AttributeValues = new System.Collections.Generic.Dictionary<string, string>
                {
                    { "param1", "parameter value 1" },
                    { "param2", "parameter value 2" }
                },
                AcceptedPaymentMethods = new System.Collections.Generic.List<AcceptedPaymentMethod> { AcceptedPaymentMethod.CreditCard },
                SuccessUrl = "https://www.example.com"
            };

            var id = _tokenPageSessionsApi.TokenPageSessionsPost(postTokenPageSessionRequestModel, TestApiSettings.ImpersonationAccountKey);

            // Should return a valid Id.
            Assert.IsNotNull(id);
        }
    }
}
