using Microsoft.VisualStudio.TestTools.UnitTesting;
using epay3.Web.Api.Sdk.Api;
using epay3.Web.Api.Sdk.Model;
using epay3.Web.Api.Sdk.Client;
using System.Net;
using System.Linq;

namespace epay3.Web.Api.Tests
{
    [TestClass]
    public class When_Posting_A_Payment_Schedule
    {
        private PaymentSchedulesApi _paymentSchedulesApi;
        private TokensApi _tokensApi;

        [TestInitialize]
        public void Initialize()
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            _paymentSchedulesApi = new PaymentSchedulesApi(TestApiSettings.Uri);
            _tokensApi = new TokensApi(TestApiSettings.Uri);

            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(TestApiSettings.Key + ":" + TestApiSettings.Secret);

            _paymentSchedulesApi.Configuration.AddDefaultHeader("Authorization", "Basic " + System.Convert.ToBase64String(plainTextBytes));
            _tokensApi.Configuration.AddDefaultHeader("Authorization", "Basic " + System.Convert.ToBase64String(plainTextBytes));
        }

        [TestMethod]
        public void Should_Create_And_Get()
        {
            var postPaymentScheduleRequestModel = new PostPaymentScheduleRequestModel
            {
                Payer = "John Smith",
                EmailAddress = "jsmith@example.com",
                Amount = 100,
                TokenId = CreateToken(null),
                Interval = IntervalType.Day,
                IntervalCount = 1                 
            };

            var paymentScheduleId = _paymentSchedulesApi.PaymentSchedulesPost(postPaymentScheduleRequestModel, null);

            Assert.IsNotNull(paymentScheduleId);

            var paymentSchedule = _paymentSchedulesApi.PaymentSchedulesGet(paymentScheduleId);

            Assert.IsNotNull(paymentSchedule);
            Assert.IsNotNull(paymentSchedule.StartDate);
        }

        [TestMethod]
        public void Should_Get_A_404_For_An_Invalid_Id()
        {
            try
            {
                _paymentSchedulesApi.PaymentSchedulesGet("INVALID ID");

                Assert.Fail();
            }
            catch (ApiException exception)
            {
                Assert.AreEqual(404, exception.ErrorCode);
            }
        }

        [TestMethod]
        public void Should_Cancel()
        {
            var postPaymentScheduleRequestModel = new PostPaymentScheduleRequestModel
            {
                Payer = "John Smith",
                EmailAddress = "jsmith@example.com",
                Amount = 100,
                TokenId = CreateToken(null),
                Interval = IntervalType.Day,
                IntervalCount = 1
            };

            var paymentScheduleId = _paymentSchedulesApi.PaymentSchedulesPost(postPaymentScheduleRequestModel, null);

            Assert.IsTrue(_paymentSchedulesApi.PaymentSchedulesCancel(paymentScheduleId));

            try
            {
                Assert.IsFalse(_paymentSchedulesApi.PaymentSchedulesCancel(paymentScheduleId));

                Assert.Fail();
            }
            catch
            {

            }
        }

        [TestMethod]
        public void Should_Create_And_Get_With_Impersonation()
        {
            var tokenIdWithImpersonation = CreateToken(TestApiSettings.ImpersonationAccountKey);
            var postPaymentScheduleRequestModel = new PostPaymentScheduleRequestModel
            {
                Payer = "John Smith",
                EmailAddress = "jsmith@example.com",
                Amount = 100,
                TokenId = tokenIdWithImpersonation,
                Interval = IntervalType.Day,
                IntervalCount = 1
            };

            var paymentScheduleId = _paymentSchedulesApi.PaymentSchedulesPost(postPaymentScheduleRequestModel, TestApiSettings.ImpersonationAccountKey);

            Assert.IsNotNull(paymentScheduleId);

            var paymentSchedule = _paymentSchedulesApi.PaymentSchedulesGet(paymentScheduleId, TestApiSettings.ImpersonationAccountKey);

            Assert.IsNotNull(paymentSchedule);
            Assert.IsNotNull(paymentSchedule.StartDate);

            try
            {
                // Should not be able to get this payment schedule without the impersonation key.
                _paymentSchedulesApi.PaymentSchedulesGet(paymentScheduleId, null);

                Assert.Fail();
            }
            catch(ApiException exception)
            {
                Assert.AreEqual(404, exception.ErrorCode);
            }

            try
            {
                // Should not be able to cancel this payment schedule without the impersonation key.
                _paymentSchedulesApi.PaymentSchedulesCancel(paymentScheduleId, null);

                Assert.Fail();
            }
            catch (ApiException exception)
            {
                Assert.AreEqual(404, exception.ErrorCode);
            }

            Assert.IsTrue(_paymentSchedulesApi.PaymentSchedulesCancel(paymentScheduleId, TestApiSettings.ImpersonationAccountKey));
        }

        [TestMethod]
        public void Should_Fail_With_Invalid_Token()
        {
            var tokenId = CreateToken(null);
            var tokenIdWithImpersonation = CreateToken(TestApiSettings.ImpersonationAccountKey);
            var postPaymentScheduleRequestModel = new PostPaymentScheduleRequestModel
            {
                Payer = "John Smith",
                EmailAddress = "jsmith@example.com",
                Amount = 100,
                TokenId = tokenIdWithImpersonation,
                Interval = IntervalType.Day,
                IntervalCount = 1
            };

            try
            {
                _paymentSchedulesApi.PaymentSchedulesPost(postPaymentScheduleRequestModel, null);

                Assert.Fail();
            }
            catch (ApiException exception)
            {
                Assert.AreEqual(400, exception.ErrorCode);
            }

            postPaymentScheduleRequestModel = new PostPaymentScheduleRequestModel
            {
                Payer = "John Smith",
                EmailAddress = "jsmith@example.com",
                Amount = 100,
                TokenId = tokenId,
                Interval = IntervalType.Day,
                IntervalCount = 1
            };

            try
            {
                _paymentSchedulesApi.PaymentSchedulesPost(postPaymentScheduleRequestModel, TestApiSettings.ImpersonationAccountKey);

                Assert.Fail();
            }
            catch (ApiException exception)
            {
                Assert.AreEqual(400, exception.ErrorCode);
            }
        }

        private string CreateToken(string impersonationKey)
        {
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
                    Year = 2017
                }
            };

            return _tokensApi.TokensPost(postTokenRequestModel, impersonationKey);
        }
    }
}
