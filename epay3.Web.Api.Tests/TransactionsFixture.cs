using Microsoft.VisualStudio.TestTools.UnitTesting;
using epay3.Web.Api.Sdk.Api;
using epay3.Web.Api.Sdk.Model;
using epay3.Web.Api.Sdk.Client;
using System.Net;
using System.Linq;
using System;

namespace epay3.Web.Api.Tests
{
    [TestClass]
    public class When_Posting_A_Transaction
    {
        private TokensApi _tokensApi;
        private TransactionsApi _transactionsApi;

        [TestInitialize]
        public void Initialize()
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            _transactionsApi = new TransactionsApi(TestApiSettings.Uri);
            _tokensApi = new TokensApi(TestApiSettings.Uri);

            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(TestApiSettings.Key + ":" + TestApiSettings.Secret);

            _tokensApi.Configuration.AddDefaultHeader("Authorization", "Basic " + System.Convert.ToBase64String(plainTextBytes));
            _transactionsApi.Configuration.AddDefaultHeader("Authorization", "Basic " + System.Convert.ToBase64String(plainTextBytes));
        }

        [TestMethod]
        public void Should_Successfully_Process_And_Void_Credit_Card()
        {
            var amount = System.Math.Round(new System.Random().NextDouble() * 100, 2);
            var postTransactionRequestModel = new PostTransactionRequestModel
            {
                Payer = "John Smith",
                EmailAddress = "jsmith@example.com",
                Amount = amount,
                CreditCardInformation = new CreditCardInformationModel
                {
                    AccountHolder = "John Smith",
                    CardNumber = "4242424242424242",
                    Cvc = "123",
                    Month = 12,
                    Year = System.DateTime.Now.Year + 2,
                    PostalCode = "54321"
                },
                AttributeValues = new System.Collections.Generic.Dictionary<string, string> { { "phoneNumber", "512-234-1233" }, { "agentCode", "213498" } },
                Comments = "Sample comments",
                PayerFee = amount * .10
            };

            var response = _transactionsApi.TransactionsPost(postTransactionRequestModel, null);

            // Should return a valid Id.
            Assert.IsTrue(response.Id > 0);

            // Should successfully void a transaction.
            Assert.AreEqual(ReversalResponseCode.Success, _transactionsApi.TransactionsVoid(response.Id.Value, new PostVoidTransactionRequestModel { SendReceipt = false }).ReversalResponseCode);

            var getTransactionResponseModel = _transactionsApi.TransactionsGet(response.Id.Value);

            Assert.IsNotNull(getTransactionResponseModel);
            Assert.AreEqual("512-234-1233", getTransactionResponseModel.AttributeValues.Single(x=>x.ParameterName=="phoneNumber").Value);
            Assert.IsNotNull(getTransactionResponseModel.Events.SingleOrDefault(x => x.EventType == EventType.Sale));
            Assert.IsNotNull(getTransactionResponseModel.Events.SingleOrDefault(x => x.EventType == EventType.Void));

            // Should not be able to void the transaction more than once.
            Assert.AreEqual(ReversalResponseCode.PreviouslyVoided, _transactionsApi.TransactionsVoid(response.Id.Value, new PostVoidTransactionRequestModel { SendReceipt = false }).ReversalResponseCode);
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
                    AccountType = AccountType.Personalsavings
                },
                AttributeValues = new System.Collections.Generic.Dictionary<string, string> { { "phoneNumber", "512-234-1233" }, { "agentCode", "213498" } },
                Comments = "Sample comments"
            };

            var response = _transactionsApi.TransactionsPost(postTransactionRequestModel, null);

            // Should return a valid Id.
            Assert.IsTrue(response.Id > 0);

            // Should successfully void a transaction.
            Assert.AreEqual(ReversalResponseCode.Success, _transactionsApi.TransactionsVoid(response.Id.Value, new PostVoidTransactionRequestModel { SendReceipt = false }).ReversalResponseCode);

            var getTransactionResponseModel = _transactionsApi.TransactionsGet(response.Id.Value);

            Assert.IsNotNull(getTransactionResponseModel);
            Assert.AreEqual("512-234-1233", getTransactionResponseModel.AttributeValues.Single(x => x.ParameterName == "phoneNumber").Value);
            Assert.IsNotNull(getTransactionResponseModel.Events.SingleOrDefault(x => x.EventType == EventType.Sale));
            Assert.IsNotNull(getTransactionResponseModel.Events.SingleOrDefault(x => x.EventType == EventType.Void));

            // Should not be able to void the transaction more than once.
            Assert.AreEqual(ReversalResponseCode.PreviouslyVoided, _transactionsApi.TransactionsVoid(response.Id.Value, new PostVoidTransactionRequestModel { SendReceipt = false }).ReversalResponseCode);
        }

        [TestMethod]
        public void Should_Honor_Impersonation_For_Transactions()
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
                    AccountType = AccountType.Personalsavings
                },
                Comments = "Sample comments",
                InitiatingPartyFee = amount * .20
            };

            var response = _transactionsApi.TransactionsPost(postTransactionRequestModel, TestApiSettings.ImpersonationAccountKey);

            // Should return a valid Id.
            Assert.IsTrue(response.Id > 0);
            Assert.AreEqual(PaymentResponseCode.Success, response.PaymentResponseCode);

            // Should get the transaction even when impersonation is off.
            Assert.IsNotNull(_transactionsApi.TransactionsGet(response.Id, null));
            // Should get the transaction when impersonation is on.
            Assert.IsNotNull(_transactionsApi.TransactionsGet(response.Id, TestApiSettings.ImpersonationAccountKey));

            // Should not be able to void with the impersonation key.
            Assert.AreNotEqual(ReversalResponseCode.Success, _transactionsApi.TransactionsVoid(response.Id.Value, new PostVoidTransactionRequestModel { SendReceipt = false }, null));

            // Should be able to void with the impersonation key.
            Assert.AreEqual(ReversalResponseCode.Success, _transactionsApi.TransactionsVoid(response.Id.Value, new PostVoidTransactionRequestModel { SendReceipt = false }, TestApiSettings.ImpersonationAccountKey).ReversalResponseCode);
        }

        /// <summary>
        /// This is just an example showing how to refund a transaction. Refunds are not allowed until
        /// the transaction is settled and batched.
        /// </summary>
        [TestMethod]
        [Ignore]
        public void Should_Successfully_Issue_Full_Refund()
        {
            var amount = new System.Random().Next(10, 1000);
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
                    AccountType = AccountType.Personalsavings
                },
                Comments = "Sample comments"
            };

            var response = _transactionsApi.TransactionsPost(postTransactionRequestModel);

            // Should return a valid Id.
            Assert.IsTrue(response.Id > 0);

            var refundResponse = _transactionsApi.TransactionsRefund(response.Id.Value, new PostRefundTransactionRequestModel { SendReceipt = false });
            var refundTransaction = _transactionsApi.TransactionsGet(refundResponse.Id);

            Assert.IsNotNull(refundTransaction);
            Assert.AreEqual(refundTransaction.Amount * -1, postTransactionRequestModel.Amount + 3);
        }

        /// <summary>
        /// This is just an example showing how to refund a transaction. Refunds are not allowed until
        /// the transaction is settled and batched.
        /// </summary>
        [TestMethod]
        [Ignore]
        public void Should_Successfully_Issue_Partial_Refunds()
        {
            var amount = new System.Random().Next(100, 1000);
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
                    AccountType = AccountType.Personalsavings
                },
                Comments = "Sample comments"
            };

            var response = _transactionsApi.TransactionsPost(postTransactionRequestModel);

            // Should return a valid Id.
            Assert.IsTrue(response.Id > 0);

            var refundResponse = _transactionsApi.TransactionsRefund(response.Id.Value, new PostRefundTransactionRequestModel { SendReceipt = false, Amount = 5 });
            var refundTransaction = _transactionsApi.TransactionsGet(refundResponse.Id);

            Assert.IsNotNull(refundTransaction);
            Assert.AreEqual(refundTransaction.Amount * -1, 5);

            refundResponse = _transactionsApi.TransactionsRefund(response.Id.Value, new PostRefundTransactionRequestModel { SendReceipt = false, Amount = 6 });
            refundTransaction = _transactionsApi.TransactionsGet(refundResponse.Id);

            Assert.IsNotNull(refundTransaction);
            Assert.AreEqual(refundTransaction.Amount * -1, 6);

            refundResponse = _transactionsApi.TransactionsRefund(response.Id.Value, new PostRefundTransactionRequestModel { SendReceipt = false, Amount = postTransactionRequestModel.Amount });

            Assert.AreEqual(ReversalResponseCode.GenericDecline, refundResponse.ReversalResponseCode);
            Assert.IsNull(refundResponse.Id);
        }

        [TestMethod]
        public void Should_Fail_With_Invalid_Authorization_Id()
        {
            var amount = System.Math.Round(new System.Random().NextDouble() * 100, 2);
            var postTransactionRequestModel = new PostTransactionRequestModel
            {
                Payer = "John Smith",
                EmailAddress = "jsmith@example.com",
                Amount = amount,
                AuthorizationId = "INVALID_ID"
            };

            var response = _transactionsApi.TransactionsPost(postTransactionRequestModel, null);

            Assert.AreEqual(PaymentResponseCode.InvalidAuthorization, response.PaymentResponseCode);
        }

        //[TestMethod]
        //public void Should_Fail_With_Inaccessible_Authorization_Id()
        //{
        //
        //
        //}

        [TestMethod]
        public void Should_Successfully_Use_Authorization_Id()
        {
            var postTokenRequestModel = new PostTokenRequestModel
            {
                Payer = "John Doe",
                EmailAddress = "jdoe@example.com",
                CreditCardInformation = new CreditCardInformationModel
                {
                    AccountHolder = "John Doe",
                    CardNumber = "4242424242424242",
                    Cvc = "123",
                    Month = 12,
                    Year = 2020,
                    PostalCode = "54321"
                }
            };

            var amount = System.Math.Round(new System.Random().NextDouble() * 100, 2);
            var tokenId = _tokensApi.TokensPost(postTokenRequestModel);

            var authorizationId = _transactionsApi.TransactionsAuthorize(new PostAuthorizeTransactionRequestModel
            {
                Amount = amount,
                TokenId = tokenId
            });

            Assert.IsNotNull(authorizationId);

            var postTransactionRequestModel = new PostTransactionRequestModel
            {
                Payer = "John Smith",
                EmailAddress = "jsmith@example.com",
                Amount = amount,
                AuthorizationId = authorizationId
            };

            var transactionResponse = _transactionsApi.TransactionsPost(postTransactionRequestModel, null);

            Assert.AreEqual(PaymentResponseCode.Success, transactionResponse.PaymentResponseCode);
        }

        [TestMethod]
        public void Should_Successfully_Use_Authorization_Id_With_Impersonation()
        {
            var postTokenRequestModel = new PostTokenRequestModel
            {
                Payer = "John Doe",
                EmailAddress = "jdoe@example.com",
                CreditCardInformation = new CreditCardInformationModel
                {
                    AccountHolder = "John Doe",
                    CardNumber = "4242424242424242",
                    Cvc = "123",
                    Month = 12,
                    Year = 2020,
                    PostalCode = "54321"
                }
            };

            var amount = System.Math.Round(new System.Random().NextDouble() * 100, 2);
            var tokenId = _tokensApi.TokensPost(postTokenRequestModel, TestApiSettings.ImpersonationAccountKey);

            var authorizationId = _transactionsApi.TransactionsAuthorize(new PostAuthorizeTransactionRequestModel
            {
                Amount = amount,
                TokenId = tokenId
            }, TestApiSettings.ImpersonationAccountKey);

            Assert.IsNotNull(authorizationId, TestApiSettings.ImpersonationAccountKey);

            var postTransactionRequestModel = new PostTransactionRequestModel
            {
                Payer = "John Smith",
                EmailAddress = "jsmith@example.com",
                Amount = amount,
                AuthorizationId = authorizationId
            };

            // This attempt should fail without the same impersonation key.
            var transactionResponse = _transactionsApi.TransactionsPost(postTransactionRequestModel);

            Assert.AreEqual(PaymentResponseCode.InvalidAuthorization, transactionResponse.PaymentResponseCode);

            // This attempt should succeed with the the correct impersonation key.
            transactionResponse = _transactionsApi.TransactionsPost(postTransactionRequestModel, TestApiSettings.ImpersonationAccountKey);

            Assert.AreEqual(PaymentResponseCode.Success, transactionResponse.PaymentResponseCode);
        }
    }
    [TestClass]
    public class When_Searching_Transactions
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
        public void Should_Successfully_Find_Transactions()
        {
            // Search results can be iterated through, with each returned transaction coming back in the form of a GetTransactionResponseModel. 
            var searchResults = _transactionsApi.TransactionsSearch(accountId: 3, beginDate: DateTime.Parse("1/1/2017"), endDate: DateTime.UtcNow, transactionSearchTypeId: TransactionSearchType.Processed, minAmount: -200m, maxAmount: 1000m, pageSize: 5, page: 2);
            Assert.IsNotNull(searchResults);

            // Additionally, every parameter when searching for transactions is optional.
            var searchAllResults = _transactionsApi.TransactionsSearch();
            Assert.IsTrue(searchAllResults.TotalRecords > 0);
            Assert.IsNotNull(searchAllResults);
        }
    }
}
