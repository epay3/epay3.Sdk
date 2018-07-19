﻿using epay3.Web.Api.Sdk.Api;
using epay3.Web.Api.Sdk.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;

namespace epay3.Web.Api.Tests
{
    [TestClass]
    public class When_Posting_An_Amount_To_Fee
    {
        private TransactionFeesApi _payerFeeApi;
        private TokensApi _tokensApi;

        [TestInitialize]
        public void Initialize()
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            _payerFeeApi = new TransactionFeesApi(TestApiSettings.Uri);
            _tokensApi = new TokensApi(TestApiSettings.Uri);

            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(TestApiSettings.Key + ":" + TestApiSettings.Secret);

            _payerFeeApi.Configuration.AddDefaultHeader("Authorization", "Basic " + System.Convert.ToBase64String(plainTextBytes));
            _tokensApi.Configuration.AddDefaultHeader("Authorization", "Basic " + System.Convert.ToBase64String(plainTextBytes));
        }

        [TestMethod]
        public void ShouldReturnValues()
        {
            var request = new PostTransactionFeesRequestModel(5.3m);
            var response = _payerFeeApi.TransactionFeesPost(request, null);

            Assert.IsInstanceOfType(response, typeof(PostTransactionFeesResponseModel));
            Assert.IsNotNull(response.AchPayerFee);
            Assert.IsNotNull(response.CreditCardPayerFee);
        }
    }
}
