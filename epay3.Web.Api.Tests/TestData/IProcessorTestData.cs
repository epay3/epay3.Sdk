using epay3.Web.Api.Sdk.Model;

namespace epay3.Web.Api.Tests.TestData
{
    public interface IProcessorTestData
    {
        BankAccountInformationModel Ach1 { get; }
        BankAccountInformationModel Ach2 { get; }
        CreditCardInformationModel Amex { get; }
        CreditCardInformationModel Mastercard { get; }
        CreditCardInformationModel Visa { get; }
    }
}