using epay3.Web.Api.Sdk.Model;
using System;

namespace epay3.Web.Api.Tests.TestData
{
    /// <summary>
    /// Uses keys for Account #333 in the sandbox. Can only process AMEX credit cards.
    /// </summary>
    public class Processor12 : TestApiSettings, ITestData
    {
        public override string Key => System.Configuration.ConfigurationManager.AppSettings["ApiKey_Processor12"];

        public override string Secret => System.Configuration.ConfigurationManager.AppSettings["ApiSecret_Processor12"];

        public override string PublicKey => System.Configuration.ConfigurationManager.AppSettings["ApiPublicKey_Processor12"];

        public BankAccountInformationModel Ach1 => throw new NotImplementedException();

        public BankAccountInformationModel Ach2 => throw new NotImplementedException();

        public CreditCardInformationModel Amex => new CreditCardInformationModel()
        {
            AccountHolder = "John Doe",
            CardNumber = "373953192351004",
            Month = 12,
            Year = 2023,
            Cvc = "991",
            PostalCode = "54321"
        };

        public CreditCardInformationModel Mastercard => throw new NotImplementedException();

        public CreditCardInformationModel Visa => throw new NotImplementedException();
    }
}