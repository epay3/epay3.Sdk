using Microsoft.VisualStudio.TestTools.UnitTesting;
using epay3.Web.Api.Sdk.Api;
using epay3.Web.Api.Sdk.Model;
using epay3.Web.Api.Sdk.Client;
using System.Net;
using System.Linq;
using epay3.Web.Api.Tests.TestData;

namespace epay3.Web.Api.Tests
{
    [TestClass]
    public class PaymentPageSessionsFixture
    {
        private PaymentPageSessionsApi _paymentPageSessionsApi;
        private ITestData _testData;

        [TestInitialize]
        public void Initialize()
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            _testData = new TestData.Processor7();

            _paymentPageSessionsApi = new PaymentPageSessionsApi(_testData.Uri);

            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(_testData.Key + ":" + _testData.Secret);

            _paymentPageSessionsApi.Configuration.AddDefaultHeader("Authorization", "Basic " + System.Convert.ToBase64String(plainTextBytes));
        }

        [TestMethod]
        public void Should_Return_An_Id_Upon_Success_With_No_Impersonation_Key()
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
        public void Should_Return_An_Id_Upon_Success_With_An_Impersonation_Key()
        {
            var postPaymentPageSessionRequestModel = new PostPaymentPageSessionRequestModel
            {
                Amount = 100,
                InitiatingPartyCreditCardFee = 20,
                InitiatingPartyAchFee = 2,
                AcceptedPaymentMethods = new System.Collections.Generic.List<AcceptedPaymentMethod> { AcceptedPaymentMethod.Ach },
                SuccessUrl = "https://www.example.com",
                PayerFee = 5
            };

            var id = _paymentPageSessionsApi.PaymentPageSessionsPost(postPaymentPageSessionRequestModel, _testData.ImpersonationAccountKey);

            // Should return a valid Id.
            Assert.IsNotNull(id);
        }

        [TestMethod]
        public void Should_Fail_With_An_Invalid_Processing_Account_Id()
        {
            var postPaymentPageSessionRequestModel = new PostPaymentPageSessionRequestModel
            {
                Amount = 100,
                InitiatingPartyCreditCardFee = 20,
                InitiatingPartyAchFee = 2
            };

            try
            {
                var id = _paymentPageSessionsApi.PaymentPageSessionsPost(postPaymentPageSessionRequestModel, "INVALID KEY");

                Assert.Fail();
            }
            catch(ApiException exception)
            {
                Assert.AreEqual(400, exception.ErrorCode);
            }
        }

        [TestMethod]
        public void Should_Validate_Against_A_High_Initiator_Credit_Card_Fee()
        {
            var postPaymentPageSessionRequestModel = new PostPaymentPageSessionRequestModel
            {
                Amount = 100,
                InitiatingPartyCreditCardFee = 101,
                InitiatingPartyAchFee = 2
            };

            try
            {
                var id = _paymentPageSessionsApi.PaymentPageSessionsPost(postPaymentPageSessionRequestModel, _testData.ImpersonationAccountKey);

                Assert.Fail();
            }
            catch (ApiException exception)
            {
                Assert.AreEqual(400, exception.ErrorCode);
            }
        }

        [TestMethod]
        public void Should_Validate_Against_A_High_Initiator_Ach_Fee()
        {
            var postPaymentPageSessionRequestModel = new PostPaymentPageSessionRequestModel
            {
                Amount = 100,
                InitiatingPartyCreditCardFee = null,
                InitiatingPartyAchFee = 101
            };

            try
            {
                var id = _paymentPageSessionsApi.PaymentPageSessionsPost(postPaymentPageSessionRequestModel, _testData.ImpersonationAccountKey);

                Assert.Fail();
            }
            catch (ApiException exception)
            {
                Assert.AreEqual(400, exception.ErrorCode);
            }
        }
        
        [TestMethod]
        public void Should_Validate_Against_A_High_Payer_Fee()
        {
            var postPaymentPageSessionRequestModel = new PostPaymentPageSessionRequestModel
            {
                Amount = 100,
                InitiatingPartyCreditCardFee = null,
                InitiatingPartyAchFee = 2,
                PayerFee = 101
            };

            try
            {
                var id = _paymentPageSessionsApi.PaymentPageSessionsPost(postPaymentPageSessionRequestModel, _testData.ImpersonationAccountKey);

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
                InitiatingPartyCreditCardFee = 20,
                InitiatingPartyAchFee = 2
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
