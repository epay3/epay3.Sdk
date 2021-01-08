using System;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using epay3.Web.Api.Sdk.Api;
using epay3.Web.Api.Sdk.Model;
using epay3.Web.Api.Sdk.Client;

namespace epay3.Web.Api.Tests
{
    [TestClass]
    public class When_posting_A_AutoPay
    {
        private AutoPayApi _autoPayApi;

        [TestInitialize]
        public void Initialize()
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            _autoPayApi = new AutoPayApi(TestApiSettings.Uri);

            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(TestApiSettings.Key + ":" + TestApiSettings.Secret);

            _autoPayApi.Configuration.AddDefaultHeader("Authorization", "Basic " + System.Convert.ToBase64String(plainTextBytes));
        }
        [TestMethod]
        public void Should_Create_And_Get()
        {
            //Todo add create method

            var autoPay = _autoPayApi.AutoPayGet("1");

            Assert.IsNotNull(autoPay);
            Assert.Fail();
        }
    }
}
