using epay3.Web.Api.Sdk.Model;
using System;

namespace epay3.Web.Api.Tests.TestData
{
    // Make a note here about which account we use and why!
    public class Processor : TestApiSettings, ITestData
    {
        public override string Key => System.Configuration.ConfigurationManager.AppSettings["ApiKey_Processor"];

        public override string Secret => System.Configuration.ConfigurationManager.AppSettings["ApiSecret_Processor"];

        public override string PublicKey => System.Configuration.ConfigurationManager.AppSettings["ApiPublicKey_Processor"];

        public BankAccountInformationModel Ach1 => new BankAccountInformationModel
        {
            FirstName = "John",
            LastName = "Smith",
            RoutingNumber = "111000025",
            AccountNumber = "1234567890",
            AccountHolder = "ACME Corp",
            AccountType = AccountType.Corporatechecking
        };

        public BankAccountInformationModel Ach2 => new BankAccountInformationModel
        {
            AccountHolder = "John Smith",
            FirstName = "John",
            LastName = "Smith",
            AccountNumber = "5454545454545454",
            RoutingNumber = "111000025",
            AccountType = AccountType.Personalsavings
        };

        public CreditCardInformationModel Amex => throw new NotImplementedException();

        public CreditCardInformationModel Mastercard => throw new NotImplementedException();

        public CreditCardInformationModel Visa => new CreditCardInformationModel
        {
            AccountHolder = "John Doe",
            CardNumber = "4444333322221111",
            Cvc = "737",
            Month = 03,
            Year = 2030,
            PostalCode = "54321"
        };
    }
}