using Microsoft.VisualStudio.TestTools.UnitTesting;
using epay3.Web.Api.Sdk.Api;
using epay3.Web.Api.Sdk.Model;
using epay3.Web.Api.Sdk.Client;
using System.Net;
using System.Linq;

namespace epay3.Web.Api.Tests
{
    [TestClass]
    public class When_Handling_Tokens
    {
        private TokensApi _tokensApi;
        private TransactionsApi _transactionsApi;

        [TestInitialize]
        public void Initialize()
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            _tokensApi = new TokensApi(TestApiSettings.Uri);
            _transactionsApi = new TransactionsApi(TestApiSettings.Uri);

            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(TestApiSettings.Key + ":" + TestApiSettings.Secret);

            _tokensApi.Configuration.AddDefaultHeader("Authorization", "Basic " + System.Convert.ToBase64String(plainTextBytes));
            _transactionsApi.Configuration.AddDefaultHeader("Authorization", "Basic " + System.Convert.ToBase64String(plainTextBytes));
        }

        [TestMethod]
        public void Should_Fail_To_Authorize_With_Invalid_Credentials()
        {
            try
            {
                var postTokenRequestModel = new PostTokenRequestModel
                {
                    Payer = "John Doe",
                    EmailAddress = "jdoe@example.com",
                    BankAccountInformation = new BankAccountInformationModel
                    {
                        RoutingNumber = "111000025",
                        AccountNumber = "1234567890"
                    }
                };

                _tokensApi.Configuration.DefaultHeader["Authorization"] = null;

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
                Payer = "John Doe",
                EmailAddress = "jdoe@example.com",
                CreditCardInformation = new CreditCardInformationModel
                {
                    AccountHolder = "John Doe",
                    CardNumber = "4457119922390",
                    Cvc = "123"
                }
            };

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
                Payer = "John Doe",
                EmailAddress = "jdoe@example.com",
                CreditCardInformation = new CreditCardInformationModel
                {
                    AccountHolder = "John Doe",
                    CardNumber = "4457119922390123",
                    Cvc = "123",
                    Month = 12,
                    Year = System.DateTime.Now.Year,
                    PostalCode = "54321"
                }
            };

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
                Payer = "John Doe",
                EmailAddress = "jdoe@example.com",
                BankAccountInformation = new BankAccountInformationModel
                {
                    FirstName = "John",
                    LastName = "Smith",
                    RoutingNumber = "111000025",
                    AccountNumber = "1234567890",
                    AccountType = AccountType.Corporatechecking
                }
            };

            var id = _tokensApi.TokensPost(postTokenRequestModel);

            // Should return a valid Id.
            Assert.IsTrue(!string.IsNullOrWhiteSpace(id));

            // Should successfully delete a token.
            Assert.IsTrue(_tokensApi.TokensDelete(id));
        }

        [TestMethod]
        public void Should_Get_A_404_For_An_Invalid_Token_Id()
        {
            try
            {
                _tokensApi.TokensGet("INVALID ID");

                Assert.Fail();
            }
            catch(ApiException exception)
            {
                Assert.AreEqual(404, exception.ErrorCode);
            }
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

            var response = _transactionsApi.TransactionsPost(postTransactionRequestModel, null);

            Assert.AreEqual(PaymentResponseCode.InvalidToken, response.PaymentResponseCode);
            Assert.IsNotNull(response.Message);
        }

        [TestMethod]
        public void Should_Successfully_Use_A_Token_In_Credit_Card_Transaction()
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
                    Year = System.DateTime.Now.Year,
                    PostalCode = "54321"
                },
                AttributeValues = new System.Collections.Generic.Dictionary<string, string> { { "parameter1", "parameter value 1" }, { "parameter2", "parameter value 2" } }
            };

            var tokenId = _tokensApi.TokensPost(postTokenRequestModel);
            var getTokenResponseModel = _tokensApi.TokensGet(tokenId);

            Assert.IsNotNull(getTokenResponseModel);
            Assert.AreEqual(2, getTokenResponseModel.AttributeValues.Count);
            Assert.AreEqual("parameter value 1", getTokenResponseModel.AttributeValues.Single(x => x.ParameterName == "parameter1").Value);
            Assert.AreEqual("parameter value 2", getTokenResponseModel.AttributeValues.Single(x => x.ParameterName == "parameter2").Value);

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

            var response = _transactionsApi.TransactionsPost(postTransactionRequestModel, null);

            // Should return a valid Id.
            Assert.IsTrue(response.Id > 0);
            Assert.AreEqual(PaymentResponseCode.Success, response.PaymentResponseCode);
        }

        [TestMethod]
        public void Should_Use_A_Different_Public_Id_For_Duplicate_Tokens()
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
                    Year = System.DateTime.Now.Year + 1,
                    PostalCode = "54321"
                }
            };

            var firstTokenId = _tokensApi.TokensPost(postTokenRequestModel);
            var secondTokenId = _tokensApi.TokensPost(postTokenRequestModel);

            // Should return a different Id.
            Assert.AreNotEqual(firstTokenId, secondTokenId);
        }

        [TestMethod]
        [ExpectedException(typeof(ApiException))]
        public void Should_Not_Successfully_Store_An_Invalid_Bank_Account_Token()
        {
            var postTokenRequestModel = new PostTokenRequestModel
            {
                Payer = "John Doe",
                EmailAddress = "jdoe@example.com",
                CreditCardInformation = new CreditCardInformationModel
                {
                    AccountHolder = "John Doe",
                    CardNumber = "4457119922F90123",
                    Cvc = "123",
                    Month = 12,
                    Year = System.DateTime.Now.Year,
                    PostalCode = "54321"
                }
            };

            var tokenId = _tokensApi.TokensPost(postTokenRequestModel);
        }

        [TestMethod]
        [ExpectedException(typeof(ApiException))]
        public void Should_Not_Successfully_Store_An_Invalid_Credit_Card_Token()
        {
            var postTokenRequestModel = new PostTokenRequestModel
            {
                Payer = "John Doe",
                EmailAddress = "jdoe@example.com",
                BankAccountInformation = new BankAccountInformationModel
                {
                    RoutingNumber = "111000025",
                    AccountNumber = "12345XX67890",
                    FirstName = "John",
                    LastName = "Smith",
                    AccountHolder = "ACME Corp",
                    AccountType = AccountType.Corporatesavings
                }
            };

            var tokenId = _tokensApi.TokensPost(postTokenRequestModel);
        }

        [TestMethod]
        public void Should_Successfully_Use_A_Token_In_An_Corporate_Savings_Transaction()
        {
            var postTokenRequestModel = new PostTokenRequestModel
            {
                Payer = "John Doe",
                EmailAddress = "jdoe@example.com",
                BankAccountInformation = new BankAccountInformationModel
                {
                    RoutingNumber = "111000025",
                    AccountNumber = "1234567890",
                    FirstName = "John",
                    LastName = "Smith",
                    AccountHolder = "ACME Corp",
                    AccountType = AccountType.Corporatesavings
                }
            };

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

            var response = _transactionsApi.TransactionsPost(postTransactionRequestModel, null);

            // Should return a valid Id.
            Assert.IsTrue(response.Id > 0);
            Assert.AreEqual(PaymentResponseCode.Success, response.PaymentResponseCode);
        }

        [TestMethod]
        public void Should_Successfully_Use_A_Token_In_An_Corporate_Checking_Transaction()
        {
            var postTokenRequestModel = new PostTokenRequestModel
            {
                Payer = "John Doe",
                EmailAddress = "jdoe@example.com",
                BankAccountInformation = new BankAccountInformationModel
                {
                    RoutingNumber = "111000025",
                    AccountNumber = "1234567890",
                    FirstName = "John",
                    LastName = "Smith",
                    AccountHolder = "ACME Corp",
                    AccountType = AccountType.Corporatechecking
                }
            };

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

            var response = _transactionsApi.TransactionsPost(postTransactionRequestModel, null);

            // Should return a valid Id.
            Assert.IsTrue(response.Id > 0);
            Assert.AreEqual(PaymentResponseCode.Success, response.PaymentResponseCode);
        }

        [TestMethod]
        public void Should_Successfully_Create_Token_With_Impersonation()
        {
            var postTokenRequestModel = new PostTokenRequestModel
            {
                Payer = "John Doe",
                EmailAddress = "jdoe@example.com",
                BankAccountInformation = new BankAccountInformationModel
                {
                    RoutingNumber = "111000025",
                    AccountNumber = "1234567890",
                    FirstName = "John",
                    LastName = "Smith",
                    AccountHolder = "ACME Corp",
                    AccountType = AccountType.Corporatechecking
                }
            };

            var tokenId = _tokensApi.TokensPost(postTokenRequestModel, TestApiSettings.ImpersonationAccountKey);

            // Should return a valid Id.
            Assert.IsTrue(!string.IsNullOrWhiteSpace(tokenId));

            try
            {
                // Should not get the token when impersonation is off.
                Assert.IsNull(_tokensApi.TokensGet(tokenId, null));

                Assert.Fail();
            }
            catch (ApiException exception)
            {
                Assert.AreEqual(404, exception.ErrorCode);
            }

            // Should get the token when impersonation is on.
            Assert.IsNotNull(_tokensApi.TokensGet(tokenId, TestApiSettings.ImpersonationAccountKey));
        }
    }
}
