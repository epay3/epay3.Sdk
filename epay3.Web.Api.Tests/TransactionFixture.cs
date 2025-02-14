using epay3.Web.Api.Sdk.Api;
using epay3.Web.Api.Sdk.Model;
using epay3.Web.Api.Tests.TestData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;

namespace epay3.Web.Api.Tests
{
    // Currently the Processor is using Processor12 setup in QA
    // But you may also use account 10056 to login and 10100 as impersonated account or any other similarly configured accounts preferred
    [TestClass]
    public class When_Posting_A_Transaction
    {
        private TokensApi _tokensApi;
        private TransactionsApi _transactionsApi;
        private ITestData _testData;

        [TestInitialize]
        public void Initialize()
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            _testData = new Processor();

            _transactionsApi = new TransactionsApi(_testData.Uri);
            _tokensApi = new TokensApi(_testData.Uri);

            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(_testData.Key + ":" + _testData.Secret);
            _tokensApi.Configuration.AddDefaultHeader("Authorization", "Basic " + Convert.ToBase64String(plainTextBytes));
            _transactionsApi.Configuration.AddDefaultHeader("Authorization", "Basic " + Convert.ToBase64String(plainTextBytes));
        }

        [TestMethod]
        public void Basic_Transaction_No_Impersonation_ACH()
        {
            var amount = Math.Round(new Random().NextDouble() * 100, 2);
            var postTransactionRequestModel = new PostTransactionRequestModel
            {
                Payer = "John Smith",
                EmailAddress = "jsmith@example.com",
                Amount = amount,
                BankAccountInformation = _testData.Ach1,
                Comments = "Test - Basic_Transaction_No_Impersonation_ACH",
                PayerFee = 0
            };

            var response = _transactionsApi.TransactionsPost(postTransactionRequestModel, null);

            // Should return a valid Id.
            Assert.AreEqual(PaymentResponseCode.Success, response.PaymentResponseCode);

            // Should get the transaction
            var transactionWithOutImpersonationKey = _transactionsApi.TransactionsGet(response.Id, null);
            Assert.IsNotNull(transactionWithOutImpersonationKey);

            // Confirm Expected Values
            Assert.AreEqual((double)postTransactionRequestModel.Amount + transactionWithOutImpersonationKey.Fee, transactionWithOutImpersonationKey.Amount);
            Assert.AreEqual(transactionWithOutImpersonationKey.Fee, transactionWithOutImpersonationKey.PayerFee);
        }

        [TestMethod]
        public void Basic_Transaction_No_Impersonation_CC()
        {
            var amount = Math.Round(new Random().NextDouble() * 100, 2);
            var postTransactionRequestModel = new PostTransactionRequestModel
            {
                Payer = "John Smith",
                EmailAddress = "jsmith@example.com",
                Amount = amount,
                CreditCardInformation = _testData.Visa,
                Comments = "Test - Basic_Transaction_No_Impersonation_CC",
                PayerFee = 0
            };

            var response = _transactionsApi.TransactionsPost(postTransactionRequestModel, null);

            // Should return a valid Id.
            Assert.AreEqual(PaymentResponseCode.Success, response.PaymentResponseCode);

            // Should get the transaction
            var transactionWithOutImpersonationKey = _transactionsApi.TransactionsGet(response.Id, null);
            Assert.IsNotNull(transactionWithOutImpersonationKey);

            // Confirm Expected Values
            Assert.AreEqual((double)postTransactionRequestModel.Amount + transactionWithOutImpersonationKey.Fee, transactionWithOutImpersonationKey.Amount);
            Assert.AreEqual(transactionWithOutImpersonationKey.Fee, transactionWithOutImpersonationKey.PayerFee);
        }

        [TestMethod]
        public void Basic_Transaction_With_Impersonation_No_Initiating_Party_Fee_ACH()
        {
            var amount = Math.Round(new Random().NextDouble() * 100, 2);
            var postTransactionRequestModel = new PostTransactionRequestModel
            {
                Payer = "John Smith",
                EmailAddress = "jsmith@example.com",
                Amount = amount,
                BankAccountInformation = _testData.Ach2,
                Comments = "Test - Basic_Transaction_With_Impersonation_No_Initiating_Party_Fee_ACH",
                PayerFee = 0
            };

            var response = _transactionsApi.TransactionsPost(postTransactionRequestModel, _testData.ImpersonationAccountKey);

            // Should return a valid Id.
            Assert.AreEqual(PaymentResponseCode.Success, response.PaymentResponseCode);

            var transactionWithImpersonationKey = _transactionsApi.TransactionsGet(response.Id, _testData.ImpersonationAccountKey);
            Assert.IsNotNull(transactionWithImpersonationKey);

            // Compare transactions via impersonation
            Assert.AreEqual(postTransactionRequestModel.Amount, transactionWithImpersonationKey.Amount);
            Assert.AreEqual(postTransactionRequestModel.PayerFee, transactionWithImpersonationKey.PayerFee);
            Assert.IsTrue(transactionWithImpersonationKey.Fee > transactionWithImpersonationKey.PayerFee);
        }

        [TestMethod]
        public void Basic_Transaction_With_Impersonation_No_Initiating_Party_Fee_CC()
        {
            var amount = Math.Round(new Random().NextDouble() * 100, 2);
            var postTransactionRequestModel = new PostTransactionRequestModel
            {
                Payer = "John Smith",
                EmailAddress = "jsmith@example.com",
                Amount = amount,
                CreditCardInformation = _testData.Visa,
                Comments = "Test - Basic_Transaction_With_Impersonation_No_Initiating_Party_Fee_CC",
                PayerFee = 0,
                InitiatingPartyFee = 0
            };

            var response = _transactionsApi.TransactionsPost(postTransactionRequestModel, _testData.ImpersonationAccountKey);

            // Should return a valid Id.
            Assert.AreEqual(PaymentResponseCode.Success, response.PaymentResponseCode);

            var transactionWithImpersonationKey = _transactionsApi.TransactionsGet(response.Id, _testData.ImpersonationAccountKey);
            Assert.IsNotNull(transactionWithImpersonationKey);

            // Compare transactions via impersonation
            Assert.AreEqual(postTransactionRequestModel.Amount, transactionWithImpersonationKey.Amount);
            Assert.AreEqual(postTransactionRequestModel.PayerFee, transactionWithImpersonationKey.PayerFee);
            Assert.IsTrue(transactionWithImpersonationKey.Fee > transactionWithImpersonationKey.PayerFee);
        }

        [TestMethod]
        public void Basic_Transaction_With_Impersonation_With_Initiating_Party_Fee_ACH()
        {
            var amount = Math.Round(new Random().NextDouble() * 100, 2);
            var postTransactionRequestModel = new PostTransactionRequestModel
            {
                Payer = "John Smith",
                EmailAddress = "jsmith@example.com",
                Amount = amount,
                BankAccountInformation = _testData.Ach2,
                Comments = "Test - Basic_Transaction_With_Impersonation_With_Initiating_Party_Fee_ACH",
                InitiatingPartyFee = 1,
                PayerFee = 0
            };

            var response = _transactionsApi.TransactionsPost(postTransactionRequestModel, _testData.ImpersonationAccountKey);

            // Should return a valid Id.
            Assert.AreEqual(PaymentResponseCode.Success, response.PaymentResponseCode);

            var transactionWithImpersonationKey = _transactionsApi.TransactionsGet(response.Id, _testData.ImpersonationAccountKey);
            Assert.IsNotNull(transactionWithImpersonationKey);

            // Compare transactions via impersonation
            Assert.AreEqual(postTransactionRequestModel.Amount, transactionWithImpersonationKey.Amount);
            Assert.AreEqual(postTransactionRequestModel.PayerFee, transactionWithImpersonationKey.PayerFee);
            Assert.IsTrue(transactionWithImpersonationKey.Fee > transactionWithImpersonationKey.PayerFee);
            Assert.IsTrue(transactionWithImpersonationKey.Fee >= postTransactionRequestModel.InitiatingPartyFee);
        }

        [TestMethod]
        public void Basic_Transaction_With_Impersonation_With_Initiating_Party_Fee_CC()
        {
            var amount = Math.Round(new Random().NextDouble() * 100, 2);
            var postTransactionRequestModel = new PostTransactionRequestModel
            {
                Payer = "John Smith",
                EmailAddress = "jsmith@example.com",
                Amount = amount,
                CreditCardInformation = _testData.Visa,
                Comments = "Test - Basic_Transaction_With_Impersonation_With_Initiating_Party_Fee_CC",
                InitiatingPartyFee = 1,
                PayerFee = 0
            };

            var response = _transactionsApi.TransactionsPost(postTransactionRequestModel, _testData.ImpersonationAccountKey);

            // Should return a valid Id.
            Assert.AreEqual(PaymentResponseCode.Success, response.PaymentResponseCode);

            var transactionWithImpersonationKey = _transactionsApi.TransactionsGet(response.Id, _testData.ImpersonationAccountKey);
            Assert.IsNotNull(transactionWithImpersonationKey);

            // Compare transactions via impersonation
            Assert.AreEqual(postTransactionRequestModel.Amount, transactionWithImpersonationKey.Amount);
            Assert.AreEqual(postTransactionRequestModel.PayerFee, transactionWithImpersonationKey.PayerFee);
            Assert.IsTrue(transactionWithImpersonationKey.Fee > transactionWithImpersonationKey.PayerFee);
            Assert.IsTrue(transactionWithImpersonationKey.Fee >= postTransactionRequestModel.InitiatingPartyFee);
        }

        [TestMethod]
        public void Payer_Fee_Greater_Than_Fee_No_Impersonation_ACH()
        {
            var amount = Math.Round(new Random().NextDouble() * 100, 2);
            var postTransactionRequestModel = new PostTransactionRequestModel
            {
                Payer = "John Smith",
                EmailAddress = "jsmith@example.com",
                Amount = amount,
                BankAccountInformation = _testData.Ach1,
                Comments = "Test - Payer_Fee_Greater_Than_Fee_No_Impersonation_ACH",
                PayerFee = 7
            };

            var response = _transactionsApi.TransactionsPost(postTransactionRequestModel, null);

            // Should return a valid Id.
            Assert.AreEqual(PaymentResponseCode.Success, response.PaymentResponseCode);

            // Should get the transaction
            var transactionWithOutImpersonationKey = _transactionsApi.TransactionsGet(response.Id, null);
            Assert.IsNotNull(transactionWithOutImpersonationKey);

            // Confirm Expected Values
            Assert.AreEqual(postTransactionRequestModel.Amount, transactionWithOutImpersonationKey.Amount);
            Assert.AreEqual(postTransactionRequestModel.PayerFee, transactionWithOutImpersonationKey.PayerFee);
            Assert.IsTrue(transactionWithOutImpersonationKey.PayerFee > transactionWithOutImpersonationKey.Fee);
        }

        [TestMethod]
        public void Payer_Fee_Greater_Than_Fee_No_Impersonation_CC()
        {
            var amount = Math.Round(new Random().NextDouble() * 100, 2);
            var postTransactionRequestModel = new PostTransactionRequestModel
            {
                Payer = "John Smith",
                EmailAddress = "jsmith@example.com",
                Amount = amount,
                CreditCardInformation = _testData.Visa,
                Comments = "Test - Payer_Fee_Greater_Than_Fee_No_Impersonation_CC",
                PayerFee = 7
            };

            var response = _transactionsApi.TransactionsPost(postTransactionRequestModel, null);

            // Should return a valid Id.
            Assert.AreEqual(PaymentResponseCode.Success, response.PaymentResponseCode);

            // Should get the transaction
            var transactionWithOutImpersonationKey = _transactionsApi.TransactionsGet(response.Id, null);
            Assert.IsNotNull(transactionWithOutImpersonationKey);

            // Confirm Expected Values
            Assert.AreEqual(postTransactionRequestModel.Amount, transactionWithOutImpersonationKey.Amount);
            Assert.AreEqual(postTransactionRequestModel.PayerFee, transactionWithOutImpersonationKey.PayerFee);
            Assert.IsTrue(transactionWithOutImpersonationKey.PayerFee > transactionWithOutImpersonationKey.Fee);
        }

        [TestMethod]
        public void Payer_Fee_Greater_Than_Fee_With_Impersonation_No_Initiating_Party_Fee_ACH()
        {
            var amount = Math.Round(new Random().NextDouble() * 100, 2);
            var postTransactionRequestModel = new PostTransactionRequestModel
            {
                Payer = "John Smith",
                EmailAddress = "jsmith@example.com",
                Amount = amount,
                BankAccountInformation = _testData.Ach1,
                Comments = "Test - Payer_Fee_Greater_Than_Fee_With_Impersonation_No_Initiating_Party_Fee_ACH",
                PayerFee = 7
            };

            var response = _transactionsApi.TransactionsPost(postTransactionRequestModel, _testData.ImpersonationAccountKey);

            // Should return a valid Id.
            Assert.AreEqual(PaymentResponseCode.Success, response.PaymentResponseCode);

            var transactionWithImpersonationKey = _transactionsApi.TransactionsGet(response.Id, _testData.ImpersonationAccountKey);
            Assert.IsNotNull(transactionWithImpersonationKey);

            // Compare transactions via impersonation
            Assert.AreEqual(postTransactionRequestModel.Amount, transactionWithImpersonationKey.Amount);
            Assert.AreEqual(postTransactionRequestModel.PayerFee, transactionWithImpersonationKey.PayerFee);
            Assert.IsTrue(transactionWithImpersonationKey.PayerFee > transactionWithImpersonationKey.Fee);
        }

        [TestMethod]
        public void Payer_Fee_Greater_Than_Fee_With_Impersonation_No_Initiating_Party_Fee_CC()
        {
            var amount = Math.Round(new Random().NextDouble() * 100, 2);
            var postTransactionRequestModel = new PostTransactionRequestModel
            {
                Payer = "John Smith",
                EmailAddress = "jsmith@example.com",
                Amount = amount,
                CreditCardInformation = _testData.Visa,
                Comments = "Test - Payer_Fee_Greater_Than_Fee_With_Impersonation_No_Initiating_Party_Fee_CC",
                PayerFee = 7
            };

            var response = _transactionsApi.TransactionsPost(postTransactionRequestModel, _testData.ImpersonationAccountKey);

            // Should return a valid Id.
            Assert.AreEqual(PaymentResponseCode.Success, response.PaymentResponseCode);

            var transactionWithImpersonationKey = _transactionsApi.TransactionsGet(response.Id, _testData.ImpersonationAccountKey);
            Assert.IsNotNull(transactionWithImpersonationKey);

            // Compare transactions via impersonation
            Assert.AreEqual(postTransactionRequestModel.Amount, transactionWithImpersonationKey.Amount);
            Assert.AreEqual(postTransactionRequestModel.PayerFee, transactionWithImpersonationKey.PayerFee);
            Assert.IsTrue(transactionWithImpersonationKey.PayerFee > transactionWithImpersonationKey.Fee);
        }

        [TestMethod]
        public void Payer_Fee_Greater_Than_Fee_With_Impersonation_With_Initiating_Party_Fee_ACH()
        {
            var amount = Math.Round(new Random().NextDouble() * 100, 2);
            var postTransactionRequestModel = new PostTransactionRequestModel
            {
                Payer = "John Smith",
                EmailAddress = "jsmith@example.com",
                Amount = amount,
                BankAccountInformation = _testData.Ach1,
                Comments = "Test - Payer_Fee_Greater_Than_Fee_With_Impersonation_With_Initiating_Party_Fee_ACH",
                PayerFee = 7,
                InitiatingPartyFee = 1
            };

            var response = _transactionsApi.TransactionsPost(postTransactionRequestModel, _testData.ImpersonationAccountKey);

            // Should return a valid Id.
            Assert.AreEqual(PaymentResponseCode.Success, response.PaymentResponseCode);

            var transactionWithImpersonationKey = _transactionsApi.TransactionsGet(response.Id, _testData.ImpersonationAccountKey);
            Assert.IsNotNull(transactionWithImpersonationKey);

            // Compare transactions via impersonation
            Assert.AreEqual(postTransactionRequestModel.Amount, transactionWithImpersonationKey.Amount);
            Assert.AreEqual(postTransactionRequestModel.PayerFee, transactionWithImpersonationKey.PayerFee);
            Assert.IsTrue(transactionWithImpersonationKey.PayerFee > transactionWithImpersonationKey.Fee);
            Assert.IsTrue(transactionWithImpersonationKey.Fee >= postTransactionRequestModel.InitiatingPartyFee);
        }

        [TestMethod]
        public void Payer_Fee_Greater_Than_Fee_With_Impersonation_With_Initiating_Party_Fee_CC()
        {
            var amount = Math.Round(new Random().NextDouble() * 100, 2);
            var postTransactionRequestModel = new PostTransactionRequestModel
            {
                Payer = "John Smith",
                EmailAddress = "jsmith@example.com",
                Amount = amount,
                CreditCardInformation = _testData.Visa,
                Comments = "Test - Payer_Fee_Greater_Than_Fee_With_Impersonation_With_Initiating_Party_Fee_CC",
                PayerFee = 7,
                InitiatingPartyFee = 1
            };

            var response = _transactionsApi.TransactionsPost(postTransactionRequestModel, _testData.ImpersonationAccountKey);

            // Should return a valid Id.
            Assert.AreEqual(PaymentResponseCode.Success, response.PaymentResponseCode);

            var transactionWithImpersonationKey = _transactionsApi.TransactionsGet(response.Id, _testData.ImpersonationAccountKey);
            Assert.IsNotNull(transactionWithImpersonationKey);

            // Compare transactions via impersonation
            Assert.AreEqual(postTransactionRequestModel.Amount, transactionWithImpersonationKey.Amount);
            Assert.AreEqual(postTransactionRequestModel.PayerFee, transactionWithImpersonationKey.PayerFee);
            Assert.IsTrue(transactionWithImpersonationKey.PayerFee > transactionWithImpersonationKey.Fee);
            Assert.IsTrue(transactionWithImpersonationKey.Fee >= postTransactionRequestModel.InitiatingPartyFee);
        }

        [TestMethod]
        public void Fee_Greater_Than_Payer_Fee_No_Impersonation_ACH()
        {
            var amount = Math.Round(new Random().NextDouble() * 100, 2);
            var postTransactionRequestModel = new PostTransactionRequestModel
            {
                Payer = "John Smith",
                EmailAddress = "jsmith@example.com",
                Amount = amount,
                BankAccountInformation = _testData.Ach2,
                Comments = "Test - Fee_Greater_Than_Payer_Fee_No_Impersonation_ACH",
                PayerFee = 2
            };

            var response = _transactionsApi.TransactionsPost(postTransactionRequestModel, null);

            // Should return a valid Id.
            Assert.AreEqual(PaymentResponseCode.Success, response.PaymentResponseCode);

            // Should get the transaction
            var transactionWithOutImpersonationKey = _transactionsApi.TransactionsGet(response.Id, null);
            Assert.IsNotNull(transactionWithOutImpersonationKey);

            // Confirm Expected Values
            Assert.AreEqual(postTransactionRequestModel.Amount, transactionWithOutImpersonationKey.Amount);
            Assert.AreEqual(postTransactionRequestModel.PayerFee, transactionWithOutImpersonationKey.PayerFee);
            Assert.IsTrue(transactionWithOutImpersonationKey.Fee > transactionWithOutImpersonationKey.PayerFee);
        }

        [TestMethod]
        public void Fee_Greater_Than_Payer_Fee_No_Impersonation_CC()
        {
            var amount = Math.Round(new Random().NextDouble() * 100, 2);
            var postTransactionRequestModel = new PostTransactionRequestModel
            {
                Payer = "John Smith",
                EmailAddress = "jsmith@example.com",
                Amount = amount,
                CreditCardInformation = _testData.Visa,
                Comments = "Test - Fee_Greater_Than_Payer_Fee_No_Impersonation_CC",
                PayerFee = 1
            };

            var response = _transactionsApi.TransactionsPost(postTransactionRequestModel, null);

            // Should return a valid Id.
            Assert.AreEqual(PaymentResponseCode.Success, response.PaymentResponseCode);

            // Should get the transaction
            var transactionWithOutImpersonationKey = _transactionsApi.TransactionsGet(response.Id, null);
            Assert.IsNotNull(transactionWithOutImpersonationKey);

            // Confirm Expected Values
            Assert.AreEqual(postTransactionRequestModel.Amount, transactionWithOutImpersonationKey.Amount);
            Assert.AreEqual(postTransactionRequestModel.PayerFee, transactionWithOutImpersonationKey.PayerFee);
            Assert.IsTrue(transactionWithOutImpersonationKey.Fee > transactionWithOutImpersonationKey.PayerFee);
        }

        [TestMethod]
        public void Fee_Greater_Than_Payer_Fee_With_Impersonation_No_Initiating_Party_Fee_ACH()
        {
            var amount = Math.Round(new Random().NextDouble() * 100, 2);
            var postTransactionRequestModel = new PostTransactionRequestModel
            {
                Payer = "John Smith",
                EmailAddress = "jsmith@example.com",
                Amount = amount,
                BankAccountInformation = _testData.Ach1,
                Comments = "Test - Fee_Greater_Than_Payer_Fee_With_Impersonation_No_Initiating_Party_Fee_ACH",
                PayerFee = .01
            };

            var response = _transactionsApi.TransactionsPost(postTransactionRequestModel, _testData.ImpersonationAccountKey);

            // Should return a valid Id.
            Assert.AreEqual(PaymentResponseCode.Success, response.PaymentResponseCode);

            var transactionWithImpersonationKey = _transactionsApi.TransactionsGet(response.Id, _testData.ImpersonationAccountKey);
            Assert.IsNotNull(transactionWithImpersonationKey);

            // Compare transactions via impersonation
            Assert.AreEqual(postTransactionRequestModel.Amount, transactionWithImpersonationKey.Amount);
            Assert.AreEqual(postTransactionRequestModel.PayerFee, transactionWithImpersonationKey.PayerFee);
            Assert.IsTrue(transactionWithImpersonationKey.Fee > transactionWithImpersonationKey.PayerFee);
        }

        [TestMethod]
        public void Fee_Greater_Than_Payer_Fee_With_Impersonation_No_Initiating_Party_Fee_CC()
        {
            var amount = Math.Round(new Random().NextDouble() * 100, 2);
            var postTransactionRequestModel = new PostTransactionRequestModel
            {
                Payer = "John Smith",
                EmailAddress = "jsmith@example.com",
                Amount = amount,
                CreditCardInformation = _testData.Visa,
                Comments = "Test - Fee_Greater_Than_Payer_Fee_With_Impersonation_No_Initiating_Party_Fee_CC",
                PayerFee = .01
            };

            var response = _transactionsApi.TransactionsPost(postTransactionRequestModel, _testData.ImpersonationAccountKey);

            // Should return a valid Id.
            Assert.AreEqual(PaymentResponseCode.Success, response.PaymentResponseCode);

            var transactionWithImpersonationKey = _transactionsApi.TransactionsGet(response.Id, _testData.ImpersonationAccountKey);
            Assert.IsNotNull(transactionWithImpersonationKey);

            // Compare transactions via impersonation
            Assert.AreEqual(postTransactionRequestModel.Amount, transactionWithImpersonationKey.Amount);
            Assert.AreEqual(postTransactionRequestModel.PayerFee, transactionWithImpersonationKey.PayerFee);
            Assert.IsTrue(transactionWithImpersonationKey.Fee > transactionWithImpersonationKey.PayerFee);
        }

        [TestMethod]
        public void Fee_Greater_Than_Payer_Fee_With_Impersonation_With_Initiating_Party_Fee_ACH()
        {
            var amount = Math.Round(new Random().NextDouble() * 100, 2);
            var postTransactionRequestModel = new PostTransactionRequestModel
            {
                Payer = "John Smith",
                EmailAddress = "jsmith@example.com",
                Amount = amount,
                BankAccountInformation = _testData.Ach1,
                Comments = "Test - Fee_Greater_Than_Payer_Fee_With_Impersonation_With_Initiating_Party_Fee_ACH",
                PayerFee = 2,
                InitiatingPartyFee = 1
            };

            var response = _transactionsApi.TransactionsPost(postTransactionRequestModel, _testData.ImpersonationAccountKey);

            // Should return a valid Id.
            Assert.AreEqual(PaymentResponseCode.Success, response.PaymentResponseCode);

            var transactionWithImpersonationKey = _transactionsApi.TransactionsGet(response.Id, _testData.ImpersonationAccountKey);
            Assert.IsNotNull(transactionWithImpersonationKey);

            // Compare transactions via impersonation
            Assert.AreEqual(postTransactionRequestModel.Amount, transactionWithImpersonationKey.Amount);
            Assert.AreEqual(postTransactionRequestModel.PayerFee, transactionWithImpersonationKey.PayerFee);
            Assert.IsTrue(transactionWithImpersonationKey.Fee > transactionWithImpersonationKey.PayerFee);
            Assert.IsTrue(transactionWithImpersonationKey.Fee >= postTransactionRequestModel.InitiatingPartyFee);
        }

        [TestMethod]
        public void Fee_Greater_Than_Payer_Fee_With_Impersonation_With_Initiating_Party_Fee_CC()
        {
            var amount = Math.Round(new Random().NextDouble() * 100, 2);
            var postTransactionRequestModel = new PostTransactionRequestModel
            {
                Payer = "John Smith",
                EmailAddress = "jsmith@example.com",
                Amount = amount,
                CreditCardInformation = _testData.Visa,
                Comments = "Test - Fee_Greater_Than_Payer_Fee_With_Impersonation_With_Initiating_Party_Fee_CC",
                PayerFee = 2,
                InitiatingPartyFee = 1
            };

            var response = _transactionsApi.TransactionsPost(postTransactionRequestModel, _testData.ImpersonationAccountKey);

            // Should return a valid Id.
            Assert.AreEqual(PaymentResponseCode.Success, response.PaymentResponseCode);

            var transactionWithImpersonationKey = _transactionsApi.TransactionsGet(response.Id, _testData.ImpersonationAccountKey);
            Assert.IsNotNull(transactionWithImpersonationKey);

            // Compare transactions via impersonation
            Assert.AreEqual(postTransactionRequestModel.Amount, transactionWithImpersonationKey.Amount);
            Assert.AreEqual(postTransactionRequestModel.PayerFee, transactionWithImpersonationKey.PayerFee);
            Assert.IsTrue(transactionWithImpersonationKey.Fee > transactionWithImpersonationKey.PayerFee);
            Assert.IsTrue(transactionWithImpersonationKey.Fee >= postTransactionRequestModel.InitiatingPartyFee);
        }
    }
}