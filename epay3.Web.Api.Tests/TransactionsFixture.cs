using Microsoft.VisualStudio.TestTools.UnitTesting;
using epay3.Web.Api.Sdk.Api;
using epay3.Web.Api.Sdk.Model;
using epay3.Web.Api.Sdk.Client;
using System.Net;
using System.Linq;

namespace epay3.Web.Api.Tests
{
    [TestClass]
    public class When_Posting_A_Transaction
    {
        private TransactionsApi _transactionsApi;

        [TestInitialize]
        public void Initialize()
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            _transactionsApi = new TransactionsApi(TestApiSettings.Uri);

            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(TestApiSettings.Key + ":" + TestApiSettings.Secret);

            _transactionsApi.Configuration.AddDefaultHeader("Authorization", "Basic " + System.Convert.ToBase64String(plainTextBytes));
        }

        [TestMethod]
        public void Should_Fail_To_Authorize_With_Invalid_Credentials()
        {
            try
            {
                var postTransactionRequestModel = new PostTransactionRequestModel
                {
                    Payer = "John Doe",
                    EmailAddress = "jsmith@example.com",
                    Amount = 100
                };

                _transactionsApi.Configuration.DefaultHeader["Authorization"] = null;

                _transactionsApi.TransactionsPost(postTransactionRequestModel, null);

                Assert.Fail();
            }
            catch (ApiException apiException)
            {
                Assert.AreEqual(401, apiException.ErrorCode);
            }
        }

        [TestMethod]
        public void Should_Successfully_Process_And_Void_Credit_Card()
        {
            var postTransactionRequestModel = new PostTransactionRequestModel
            {
                Payer = "John Smith",
                EmailAddress = "jsmith@example.com",
                Amount = System.Math.Round(new System.Random().NextDouble() * 100, 2),
                CreditCardInformation = new CreditCardInformationModel
                {
                    AccountHolder = "John Smith",
                    CardNumber = "4242424242424242",
                    Cvc = "123",
                    Month = 12,
                    Year = System.DateTime.Now.Year + 2
                },
                AttributeValues = new System.Collections.Generic.Dictionary<string, string> { { "phoneNumber", "512-234-1233" }, { "agentCode", "213498" } },
                Comments = "Sample comments"
            };

            var response = _transactionsApi.TransactionsPost(postTransactionRequestModel, null);

            // Should return a valid Id.
            Assert.IsTrue(response.Id > 0);

            // Should successfully void a transaction.
            Assert.IsTrue(_transactionsApi.TransactionsVoid(response.Id.Value, false));

            var getTransactionResponseModel = _transactionsApi.TransactionsGet(response.Id);

            Assert.IsNotNull(getTransactionResponseModel);
            Assert.AreEqual("512-234-1233", getTransactionResponseModel.AttributeValues.Single(x=>x.ParameterName=="phoneNumber").Value);
            Assert.IsNotNull(getTransactionResponseModel.Events.SingleOrDefault(x => x.EventTypeId == TransactionEventModel.EventTypeIdEnum.Sale));
            Assert.IsNotNull(getTransactionResponseModel.Events.SingleOrDefault(x => x.EventTypeId == TransactionEventModel.EventTypeIdEnum.Void));

            try
            {
                // Should not be able to void the transaction more than once.
                Assert.IsTrue(!_transactionsApi.TransactionsVoid(response.Id.Value, false));

                Assert.Fail();
            }
            catch (ApiException exception)
            {
                Assert.AreEqual(400, exception.ErrorCode);
            }
        }

        [TestMethod]
        public void Should_Successfully_Process_And_Void_Ach()
        {
            var postTransactionRequestModel = new PostTransactionRequestModel
            {
                Payer = "John Smith",
                EmailAddress = "jsmith@example.com",
                Amount = System.Math.Round(new System.Random().NextDouble() * 100, 2),
                BankAccountInformation = new BankAccountInformationModel
                {
                    AccountHolder = "John Smith",
                    FirstName = "John",
                    LastName = "Smith",
                    AccountNumber = "4242424242424242",
                    RoutingNumber = "111000025",
                    AccountType = BankAccountInformationModel.AccountTypeEnum.Personalsavings
                },
                AttributeValues = new System.Collections.Generic.Dictionary<string, string> { { "phoneNumber", "512-234-1233" }, { "agentCode", "213498" } },
                Comments = "Sample comments"
            };

            var response = _transactionsApi.TransactionsPost(postTransactionRequestModel, null);

            // Should return a valid Id.
            Assert.IsTrue(response.Id > 0);

            // Should successfully void a transaction.
            Assert.IsTrue(_transactionsApi.TransactionsVoid(response.Id.Value, false));

            var getTransactionResponseModel = _transactionsApi.TransactionsGet(response.Id.Value);

            Assert.IsNotNull(getTransactionResponseModel);
            Assert.AreEqual("512-234-1233", getTransactionResponseModel.AttributeValues.Single(x => x.ParameterName == "phoneNumber").Value);
            Assert.IsNotNull(getTransactionResponseModel.Events.SingleOrDefault(x => x.EventTypeId == TransactionEventModel.EventTypeIdEnum.Sale));
            Assert.IsNotNull(getTransactionResponseModel.Events.SingleOrDefault(x => x.EventTypeId == TransactionEventModel.EventTypeIdEnum.Void));

            try
            {
                // Should not be able to void the transaction more than once.
                Assert.IsTrue(!_transactionsApi.TransactionsVoid(response.Id.Value, false));

                Assert.Fail();
            }
            catch(ApiException exception)
            {
                Assert.AreEqual(400, exception.ErrorCode);
            }
        }

        [TestMethod]
        public void Should_Honor_Impersonation_For_Tokens_And_Transactions()
        {
            var amount = System.Math.Round(new System.Random().NextDouble() * 100, 2);
            var postTransactionRequestModel = new PostTransactionRequestModel
            {
                Payer = "John Smith",
                EmailAddress = "jsmith@example.com",
                Amount = amount,
                BankAccountInformation = new BankAccountInformationModel
                {
                    AccountHolder = "John Smith",
                    FirstName = "John",
                    LastName = "Smith",
                    AccountNumber = "4242424242424242",
                    RoutingNumber = "111000025",
                    AccountType = BankAccountInformationModel.AccountTypeEnum.Personalsavings
                },
                Comments = "Sample comments",
                InitiatingPartyFee = amount * .20
            };

            var response = _transactionsApi.TransactionsPost(postTransactionRequestModel, TestApiSettings.ImpersonationAccountKey);

            // Should return a valid Id.
            Assert.IsTrue(response.Id > 0);

            try
            {
                // Should not get the transaction when impersonation is off.
                _transactionsApi.TransactionsGet(response.Id, null);

                Assert.Fail();
            }
            catch (ApiException exception)
            {
                Assert.AreEqual(404, exception.ErrorCode);
            }

            // Should get the transaction when impersonation is on.
            Assert.IsNotNull(_transactionsApi.TransactionsGet(response.Id, TestApiSettings.ImpersonationAccountKey));

            try
            {
                // Should not be able to void with the impersonation key.
                _transactionsApi.TransactionsVoid(response.Id.Value, false, null);

                Assert.Fail();
            }
            catch (ApiException exception)
            {
                // 404 because without the permissions, this transaction isn't found.
                Assert.AreEqual(404, exception.ErrorCode);
            }

            // Should be able to void with the impersonation key.
            Assert.IsTrue(_transactionsApi.TransactionsVoid(response.Id.Value, false, TestApiSettings.ImpersonationAccountKey));
        }
    }
}
