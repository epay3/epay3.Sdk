using epay3.Web.Api.Sdk.Model;
using System;

namespace epay3.Web.Api.Tests.TestData
{
    /// <summary>
    /// Uses keys for Account #11 in the sandbox. Can process both credit cards and ACH transactions.
    /// </summary>
    public class Processor7 : TestApiSettings, ITestData
    {
        public  BankAccountInformationModel Ach1 => new BankAccountInformationModel
        {
            FirstName = "John",
            LastName = "Smith",
            RoutingNumber = "111000025",
            AccountNumber = "1234567890",
            AccountHolder = "ACME Corp",
            AccountType = AccountType.Corporatechecking
        };

        public  BankAccountInformationModel Ach2 => new BankAccountInformationModel
        {
            AccountHolder = "John Smith",
            FirstName = "John",
            LastName = "Smith",
            AccountNumber = "5454545454545454",
            RoutingNumber = "111000025",
            AccountType = AccountType.Personalsavings
        };

        public  CreditCardInformationModel Amex => new CreditCardInformationModel()
        {
            AccountHolder = "John Doe",
            CardNumber = "371449635398431",
            Cvc = "3714",
            Month = 12,
            Year = DateTime.Now.Year + 1,
            PostalCode = "54321"
        };

        public  CreditCardInformationModel Mastercard => new CreditCardInformationModel
        {
            AccountHolder = "John Doe",
            CardNumber = "5454545454545454",
            Cvc = "999",
            Month = 12,
            Year = DateTime.Now.Year + 1,
            PostalCode = "54321"
        };

        public  CreditCardInformationModel Visa => new CreditCardInformationModel
        {
            AccountHolder = "John Doe",
            CardNumber = "4457119922390123",
            Cvc = "123",
            Month = 12,
            Year = DateTime.Now.Year + 1,
            PostalCode = "54321"
        };
    }
}