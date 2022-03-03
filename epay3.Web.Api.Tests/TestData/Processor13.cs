using epay3.Web.Api.Sdk.Model;
using System;

namespace epay3.Web.Api.Tests.TestData
{
    /// <summary>
    /// Uses keys for Account #334 in the sandbox. Can only process credit cards.
    /// </summary>
    public class Processor13 : TestApiSettings, ITestData
    {
        public override string Key => System.Configuration.ConfigurationManager.AppSettings["ApiKey_Processor13"];

        public override string Secret => System.Configuration.ConfigurationManager.AppSettings["ApiSecret_Processor13"];

        public override string PublicKey => System.Configuration.ConfigurationManager.AppSettings["ApiPublicKey_Processor13"];

        public BankAccountInformationModel Ach1 => throw new NotImplementedException();

        public BankAccountInformationModel Ach2 => throw new NotImplementedException();

        public CreditCardInformationModel Amex => new CreditCardInformationModel()
        {
            AccountHolder = "John Doe",
            CardNumber = "370000000000002",
            Month = 3,
            Year = 2030,
            Cvc = "7373",
            PostalCode = "54321"
        };

        public CreditCardInformationModel Mastercard => new CreditCardInformationModel()
        {
            AccountHolder = "John Doe",
            CardNumber = "5555341244441115",
            Month = 3,
            Year = 2030,
            Cvc = "737",
            PostalCode = "54321"
        };

        public CreditCardInformationModel Visa => new CreditCardInformationModel
        {
            AccountHolder = "John Doe",
            CardNumber = "4111111145551142",
            Cvc = "737",
            Month = 3,
            Year = 2030,
            PostalCode = "54321"
        };
    }
}