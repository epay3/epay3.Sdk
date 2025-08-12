using epay3.Web.Api.Sdk.Api;
using epay3.Web.Api.Sdk.Model;
using epay3.Web.Api.Tests.TestData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Net;

namespace epay3.Web.Api.Tests
{
    [TestClass]
    public class When_Posting_An_Amount_To_Fee
    {
        private TransactionFeesApi _transactionFeesApi;
        private TokensApi _tokensApi;
        private ITestData _testData;

        [TestInitialize]
        public void Initialize()
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            _testData = new TestData.Processor7();

            _transactionFeesApi = new TransactionFeesApi(_testData.Uri);
            _tokensApi = new TokensApi(_testData.Uri);

            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(_testData.Key + ":" + _testData.Secret);

            _transactionFeesApi.Configuration.AddDefaultHeader("Authorization", "Basic " + System.Convert.ToBase64String(plainTextBytes));
            _tokensApi.Configuration.AddDefaultHeader("Authorization", "Basic " + System.Convert.ToBase64String(plainTextBytes));
        }

        [TestMethod]
        public void ShouldReturnValues()
        {
            var response = _transactionFeesApi.TransactionFeesGet(5.3m, null, null);

            Assert.IsInstanceOfType(response, typeof(GetTransactionFeesResponseModel));
            Assert.IsNotNull(response.AchPayerFee);
            Assert.IsNotNull(response.CreditCardPayerFee);
        }

        [TestMethod]
        public void ShouldReturnValuesWithImpersonationKey()
        {
            var response = _transactionFeesApi.TransactionFeesGet(5.3m, null, _testData.ImpersonationAccountKey);

            Assert.IsInstanceOfType(response, typeof(GetTransactionFeesResponseModel));
            Assert.IsNotNull(response.AchPayerFee);
            Assert.IsNotNull(response.CreditCardPayerFee);
        }

        [TestMethod]
        public void ShouldReturnValuesEvenWithAttributeValues()
        {
            var attributeValues = new Dictionary<string, string>();
            attributeValues.Add("accountCode", "123");
            attributeValues.Add("postalCode", "55555");

            var response = _transactionFeesApi.TransactionFeesGet(5.3m, attributeValues, null);

            Assert.IsInstanceOfType(response, typeof(GetTransactionFeesResponseModel));
            Assert.IsNotNull(response.AchPayerFee);
            Assert.IsNotNull(response.CreditCardPayerFee);
        }
    }
}