using epay3.Web.Api.Sdk.Api;
using epay3.Web.Api.Sdk.Model;
using epay3.Web.Api.Tests.TestData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Net;

namespace epay3.Web.Api.Tests
{
    [TestClass]
    public class InvoicesFixture
    {
        private TransactionsApi _transactionsApi;
        private ITestData _testData;

        [TestInitialize]
        public void Initialize()
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            _testData = new TestData.Processor7();

            _transactionsApi = new TransactionsApi(_testData.Uri);

            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(_testData.Key + ":" + _testData.Secret);
            _transactionsApi.Configuration.AddDefaultHeader("Authorization", "Basic " + System.Convert.ToBase64String(plainTextBytes));

            _testData = new TestData.Processor7();
        }

        private InvoicesApi GetApi(bool useImpersonation)
        {
            var invoicesApi = new InvoicesApi(_testData.Uri);
            var plainTextBytes = new byte[0];
            if (useImpersonation)
            {
                plainTextBytes = System.Text.Encoding.UTF8.GetBytes(_testData.Key + ":" + _testData.Secret);
                invoicesApi.Configuration.AddDefaultHeader("Authorization", "Basic " + System.Convert.ToBase64String(plainTextBytes));
                return invoicesApi;
            }

            plainTextBytes = System.Text.Encoding.UTF8.GetBytes(_testData.InvoiceKey + ":" + _testData.InvoiceSecret);
            invoicesApi.Configuration.AddDefaultHeader("Authorization", "Basic " + System.Convert.ToBase64String(plainTextBytes));
            return invoicesApi;
        }

        [TestMethod]
        public void Should_Get_Successfully_With_Impersonation_Key()
        {
            var invoicesApi = GetApi(true);
            var result = invoicesApi.InvoicesGet(new Dictionary<string, string>() { ["accountCode"] = "123", ["postalCode"] = "78701" }, _testData.InvoicesImpersonationAccountKey);

            // Should post successfully.
            Assert.IsTrue(result.Status == InvoiceStatus.Success);
        }

        [TestMethod]
        public void Should_Get_Successfully()
        {
            var invoicesApi = GetApi(false);

            var result = invoicesApi.InvoicesGet(new Dictionary<string, string>() { ["accountCode"] = "123", ["postalCode"] = "78701" });

            // Should post successfully.
            Assert.IsTrue(result.Status == InvoiceStatus.Success);
        }

        [TestMethod]
        public void Should_Update_Successfully_With_Impersonation_Key()
        {
            var updateInvoicesRequestModel = new UpdateInvoicesRequestModel()
            {
                AttributeValues = new Dictionary<string, string>()
                {
                    ["accountCode"] = "123",
                    ["postalCode"] = "78701"
                },
                PaidInvoices = new List<PaidInvoiceModel>()
                {
                    new PaidInvoiceModel()
                    {
                        Id = "112120",
                        PaidAmount = 4874.91d
                    }
                }
            };

            var invoicesApi = GetApi(true);
            bool success = invoicesApi.InvoicesUpdate(updateInvoicesRequestModel, _testData.InvoicesImpersonationAccountKey);

            // Should post successfully.
            Assert.IsTrue(success);
        }

        [TestMethod]
        public void Should_Create_Transaction_Then_Update_Existing_Transaction_Successfully_With_Impersonation_Key()
        {
            var amount = new System.Random().Next(10, 1000);
            var postTransactionRequestModel = new PostTransactionRequestModel
            {
                Payer = "John Smith",
                EmailAddress = "jsmith@example.com",
                Amount = amount,
                BankAccountInformation = _testData.Ach2,
                Comments = "Sample comments"
            };

            var response = _transactionsApi.TransactionsPost(postTransactionRequestModel, _testData.InvoicesImpersonationAccountKey);

            // Should return a valid Id.
            Assert.IsTrue(response.Id > 0);

            var updateInvoicesRequestModel = new UpdateInvoicesRequestModel()
            {
                Id = response.Id.Value,
                AttributeValues = new Dictionary<string, string>()
                {
                    ["accountCode"] = "123",
                    ["postalCode"] = "78701"
                },
                PaidInvoices = new List<PaidInvoiceModel>()
                {
                    new PaidInvoiceModel()
                    {
                        Id = "112121",
                        PaidAmount = postTransactionRequestModel.Amount.Value / 2
                    },
                    new PaidInvoiceModel()
                    {
                        Id = "112122",
                        PaidAmount = postTransactionRequestModel.Amount - (postTransactionRequestModel.Amount.Value / 2)
                    }
                }
            };

            bool success = GetApi(true).InvoicesUpdate(updateInvoicesRequestModel, _testData.InvoicesImpersonationAccountKey);

            // Should post successfully.
            Assert.IsTrue(success);
        }
    }
}