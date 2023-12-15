using epay3.Web.Api.Sdk.Api;
using epay3.Web.Api.Sdk.Client;
using epay3.Web.Api.Sdk.Model;
using epay3.Web.Api.Tests.TestData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;

namespace epay3.Web.Api.Tests
{
    [TestClass]
    public class When_Posting_A_Payment_Schedule
    {
        private PaymentSchedulesApi _paymentSchedulesApi;
        private TokensApi _tokensApi;
        private ITestData _testData;

        [TestInitialize]
        public void Initialize()
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            _testData = new TestData.Processor7();

            _paymentSchedulesApi = new PaymentSchedulesApi(_testData.Uri);
            _tokensApi = new TokensApi(_testData.Uri);

            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(_testData.Key + ":" + _testData.Secret);

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
            var tokenIdWithImpersonation = CreateToken(_testData.ImpersonationAccountKey);
            var postPaymentScheduleRequestModel = new PostPaymentScheduleRequestModel
            {
                Payer = "John Smith",
                EmailAddress = "jsmith@example.com",
                Amount = 100,
                TokenId = tokenIdWithImpersonation,
                Interval = IntervalType.Day,
                IntervalCount = 1
            };

            var paymentScheduleId = _paymentSchedulesApi.PaymentSchedulesPost(postPaymentScheduleRequestModel, _testData.ImpersonationAccountKey);

            Assert.IsNotNull(paymentScheduleId);

            var paymentSchedule = _paymentSchedulesApi.PaymentSchedulesGet(paymentScheduleId, _testData.ImpersonationAccountKey);

            Assert.IsNotNull(paymentSchedule);
            Assert.IsNotNull(paymentSchedule.StartDate);

            try
            {
                // Should not be able to get this payment schedule without the impersonation key.
                _paymentSchedulesApi.PaymentSchedulesGet(paymentScheduleId, null);

                Assert.Fail();
            }
            catch (ApiException exception)
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

            Assert.IsTrue(_paymentSchedulesApi.PaymentSchedulesCancel(paymentScheduleId, _testData.ImpersonationAccountKey));
        }

        [TestMethod]
        public void Should_Fail_With_Invalid_Token()
        {
            var tokenId = CreateToken(null);
            var tokenIdWithImpersonation = CreateToken(_testData.ImpersonationAccountKey);

            // Software Platform attempting to use client token without impersonation
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

            // Software platform to use its own token on behalf of a client. Allowed as of 11/28/2018
            postPaymentScheduleRequestModel = new PostPaymentScheduleRequestModel
            {
                Payer = "John Smith",
                EmailAddress = "jsmith@example.com",
                Amount = 100,
                TokenId = tokenId,
                Interval = IntervalType.Day,
                IntervalCount = 1
            };

            var result = _paymentSchedulesApi.PaymentSchedulesPost(postPaymentScheduleRequestModel, _testData.ImpersonationAccountKey);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result));
        }

        private string CreateToken(string impersonationKey)
        {
            var postTokenRequestModel = new PostTokenRequestModel
            {
                Payer = "John Doe",
                EmailAddress = "jdoe@example.com",
                CreditCardInformation = _testData.Visa
            };

            return _tokensApi.TokensPost(postTokenRequestModel, impersonationKey);
        }
    }
}