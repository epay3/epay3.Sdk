using Microsoft.VisualStudio.TestTools.UnitTesting;
using epay3.Web.Api.Sdk.Api;
using epay3.Web.Api.Sdk.Model;
using epay3.Web.Api.Sdk.Client;
using System.Net;
using System.Linq;

namespace epay3.Web.Api.Tests
{
    [TestClass]
    public class PaymentPageSessionsFixture
    {
        private PaymentPageSessionsApi _paymentPageSessionsApi;

        [TestInitialize]
        public void Initialize()
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            _paymentPageSessionsApi = new PaymentPageSessionsApi(TestApiSettings.Uri);

            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(TestApiSettings.Key + ":" + TestApiSettings.Secret);

            _paymentPageSessionsApi.Configuration.AddDefaultHeader("Authorization", "Basic " + System.Convert.ToBase64String(plainTextBytes));
        }

        [TestMethod]
        public void Should_Return_An_Id_Upon_Success_With_No_Processing_Id()
        {
            var postPaymentPageSessionRequestModel = new PostPaymentPageSessionRequestModel
            {
                Amount = 100,
                AttributeValues = new System.Collections.Generic.Dictionary<string, string> {
                    { "parameter 1", "value 1" },
                    { "parameter 2", "value 2" }
                },
                SuccessUrl = "https://www.example.com"
            };

            var id = _paymentPageSessionsApi.PaymentPageSessionsPost(postPaymentPageSessionRequestModel, null);

            // Should return a valid Id.
            Assert.IsNotNull(id);
        }

        [TestMethod]
        public void Should_Return_An_Id_Upon_Success_With_A_Processing_Id()
        {
            var postPaymentPageSessionRequestModel = new PostPaymentPageSessionRequestModel
            {
                Amount = 100,
                InitiatingPartyFee = 20
            };

            var id = _paymentPageSessionsApi.PaymentPageSessionsPost(postPaymentPageSessionRequestModel, TestApiSettings.ImpersonationAccountKey);

            // Should return a valid Id.
            Assert.IsNotNull(id);
        }

        [TestMethod]
        public void Should_Fail_With_An_Invalid_Processing_Account_Id()
        {
            var postPaymentPageSessionRequestModel = new PostPaymentPageSessionRequestModel
            {
                Amount = 100,
                InitiatingPartyFee = 20
            };

            try
            {
                var id = _paymentPageSessionsApi.PaymentPageSessionsPost(postPaymentPageSessionRequestModel, "INVALID KEY");

                Assert.Fail();
            }
            catch(ApiException exception)
            {
                Assert.AreEqual(401, exception.ErrorCode);
            }
        }

        [TestMethod]
        public void Should_Validate_Against_A_High_Initiator_Fee()
        {
            var postPaymentPageSessionRequestModel = new PostPaymentPageSessionRequestModel
            {
                Amount = 100,
                InitiatingPartyFee = 80
            };

            try
            {
                var id = _paymentPageSessionsApi.PaymentPageSessionsPost(postPaymentPageSessionRequestModel, TestApiSettings.ImpersonationAccountKey);

                Assert.Fail();
            }
            catch (ApiException exception)
            {
                Assert.AreEqual(400, exception.ErrorCode);
            }
        }

        [TestMethod]
        public void Should_Require_Processing_AccountId_If_Initiator_Fee_Is_Not_Null()
        {
            var postPaymentPageSessionRequestModel = new PostPaymentPageSessionRequestModel
            {
                Amount = 100,
                InitiatingPartyFee = 80
            };

            try
            {
                var id = _paymentPageSessionsApi.PaymentPageSessionsPost(postPaymentPageSessionRequestModel, null);

                Assert.Fail();
            }
            catch (ApiException exception)
            {
                Assert.AreEqual(400, exception.ErrorCode);
            }
        }
    }
}
