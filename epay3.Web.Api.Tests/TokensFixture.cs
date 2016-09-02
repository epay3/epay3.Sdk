using Microsoft.VisualStudio.TestTools.UnitTesting;
using epay3.Web.Api.Sdk.Api;
using epay3.Web.Api.Sdk.Model;
using epay3.Web.Api.Sdk.Client;
using System.Net;

namespace epay3.Web.Api.Tests
{
    [TestClass]
    public class When_Handling_Tokens
    {
        private TokensApi _tokensApi;
        private TransactionsApi _transactionsApi;

        private string Uri
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["ApiUri"];
            }
        }

        private string Key
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["ApiKey"];
            }
        }

        private string Secret
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["ApiSecret"];
            }
        }

        [TestInitialize]
        public void Initialize()
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            _tokensApi = new TokensApi(Uri);
            _transactionsApi = new TransactionsApi(Uri);
        }

        [TestMethod]
        public void Should_Fail_To_Authorize_With_Invalid_Credentials()
        {
            try
            {
                var postTokenRequestModel = new PostTokenRequestModel
                {
                    BankAccountInformation = new BankAccountInformationModel
                    {
                        RoutingNumber = "111000025",
                        AccountNumber = "1234567890"
                    }
                };

                _tokensApi.TokensPost(postTokenRequestModel);

                Assert.Fail();
            }
            catch (ApiException apiException)
            {
                Assert.AreEqual(401, apiException.ErrorCode);
            }
        }

        [TestMethod]
        public void Should_Not_Store_Invalid_Credit_Card_Token()
        {
            var postTokenRequestModel = new PostTokenRequestModel
            {
                CreditCardInformation = new CreditCardInformationModel
                {
                    AccountHolder = "John Doe",
                    CardNumber = "4457119922390",
                    Cvc = "123"
                }
            };

            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(Key + ":" + Secret);

            _tokensApi.Configuration.AddDefaultHeader("Authorization", "Basic " + System.Convert.ToBase64String(plainTextBytes));

            try
            {
                _tokensApi.TokensPost(postTokenRequestModel);

                Assert.Fail();
            }
            catch (ApiException)
            {

            }
        }

        [TestMethod]
        public void Should_Successfully_Store_Credit_Card_Token()
        {
            var postTokenRequestModel = new PostTokenRequestModel
            {
                CreditCardInformation = new CreditCardInformationModel
                {
                    AccountHolder = "John Doe",
                    CardNumber = "4457119922390123",
                    Cvc = "123"
                }
            };

            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(Key + ":" + Secret);

            _tokensApi.Configuration.AddDefaultHeader("Authorization", "Basic " + System.Convert.ToBase64String(plainTextBytes));

            var id = _tokensApi.TokensPost(postTokenRequestModel);

            // Should return a valid Id.
            Assert.IsTrue(!string.IsNullOrWhiteSpace(id));

            // Should successfully delete a token.
            Assert.IsTrue(_tokensApi.TokensDelete(id));
        }

        [TestMethod]
        public void Should_Successfully_Store_And_Delete_An_Ach_Token()
        {
            var postTokenRequestModel = new PostTokenRequestModel
            {
                BankAccountInformation = new BankAccountInformationModel
                {
                    FirstName = "John",
                    LastName = "Smith",
                    RoutingNumber = "111000025",
                    AccountNumber = "1234567890",
                    AccountType = BankAccountInformationModel.AccountTypeEnum.Corporatechecking
                }
            };

            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(Key + ":" + Secret);

            _tokensApi.Configuration.AddDefaultHeader("Authorization", "Basic " + System.Convert.ToBase64String(plainTextBytes));

            var id = _tokensApi.TokensPost(postTokenRequestModel);

            // Should return a valid Id.
            Assert.IsTrue(!string.IsNullOrWhiteSpace(id));

            // Should successfully delete a token.
            Assert.IsTrue(_tokensApi.TokensDelete(id));
        }

        [TestMethod]
        public void Should_Fail_To_Use_An_Invalid_Token_In_Credit_Card_Transaction()
        {
            var postTransactionRequestModel = new PostTransactionRequestModel
            {
                Payer = "John Smith",
                EmailAddress = "jsmith@example.com",
                Amount = System.Math.Round(new System.Random().NextDouble() * 1000, 2),
                TokenId = "INVALID_TOKEN",
                Comments = "Sample comments"
            };

            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(Key + ":" + Secret);

            _transactionsApi.Configuration.AddDefaultHeader("Authorization", "Basic " + System.Convert.ToBase64String(plainTextBytes));

            try
            {
                _transactionsApi.TransactionsPost(postTransactionRequestModel);

                Assert.Fail();
            }
            catch(ApiException)
            {

            }
        }

        [TestMethod]
        public void Should_Successfully_Use_A_Token_In_Credit_Card_Transaction()
        {
            var postTokenRequestModel = new PostTokenRequestModel
            {
                CreditCardInformation = new CreditCardInformationModel
                {
                    AccountHolder = "John Doe",
                    CardNumber = "4457119922390123",
                    Cvc = "123"
                }
            };

            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(Key + ":" + Secret);

            _tokensApi.Configuration.AddDefaultHeader("Authorization", "Basic " + System.Convert.ToBase64String(plainTextBytes));

            var tokenId = _tokensApi.TokensPost(postTokenRequestModel);

            // Should return a valid Id.
            Assert.IsTrue(!string.IsNullOrWhiteSpace(tokenId));

            var postTransactionRequestModel = new PostTransactionRequestModel
            {
                Payer = "John Smith",
                EmailAddress = "jsmith@example.com",
                Amount = System.Math.Round(new System.Random().NextDouble() * 1000, 2),
                TokenId = tokenId,
                Comments = "Sample comments",
                SendReceipt = false
            };

            _transactionsApi.Configuration.AddDefaultHeader("Authorization", "Basic " + System.Convert.ToBase64String(plainTextBytes));

            var transactionId = _transactionsApi.TransactionsPost(postTransactionRequestModel);

            // Should return a valid Id.
            Assert.IsTrue(transactionId > 0);
        }

        [TestMethod]
        public void Should_Use_A_Different_Public_Id_For_Duplicate_Tokens()
        {
            var postTokenRequestModel = new PostTokenRequestModel
            {
                CreditCardInformation = new CreditCardInformationModel
                {
                    AccountHolder = "John Doe",
                    CardNumber = "4457119922390123",
                    Cvc = "123"
                }
            };

            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(Key + ":" + Secret);

            _tokensApi.Configuration.AddDefaultHeader("Authorization", "Basic " + System.Convert.ToBase64String(plainTextBytes));

            var firstTokenId = _tokensApi.TokensPost(postTokenRequestModel);
            var secondTokenId = _tokensApi.TokensPost(postTokenRequestModel);

            // Should return a different Id.
            Assert.AreNotEqual(firstTokenId, secondTokenId);
        }

        [TestMethod]
        public void Should_Successfully_Use_A_Token_In_An_Ach_Transaction()
        {
            var postTokenRequestModel = new PostTokenRequestModel
            {
                BankAccountInformation = new BankAccountInformationModel
                {
                    RoutingNumber = "111000025",
                    AccountNumber = "1234567890",
                    FirstName = "John",
                    LastName  = "Smith",
                    AccountHolder = "ACME Corp",
                    AccountType = BankAccountInformationModel.AccountTypeEnum.Corporatechecking
                }
            };

            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(Key + ":" + Secret);

            _tokensApi.Configuration.AddDefaultHeader("Authorization", "Basic " + System.Convert.ToBase64String(plainTextBytes));

            var tokenId = _tokensApi.TokensPost(postTokenRequestModel);

            // Should return a valid Id.
            Assert.IsTrue(!string.IsNullOrWhiteSpace(tokenId));

            var postTransactionRequestModel = new PostTransactionRequestModel
            {
                Payer = "John Smith",
                EmailAddress = "jsmith@example.com",
                Amount = System.Math.Round(new System.Random().NextDouble() * 1000, 2),
                TokenId = tokenId,
                Comments = "Sample comments",
                SendReceipt = false
            };

            _transactionsApi.Configuration.AddDefaultHeader("Authorization", "Basic " + System.Convert.ToBase64String(plainTextBytes));

            var transactionId = _transactionsApi.TransactionsPost(postTransactionRequestModel);

            // Should return a valid Id.
            Assert.IsTrue(transactionId > 0);
        }
    }
}
