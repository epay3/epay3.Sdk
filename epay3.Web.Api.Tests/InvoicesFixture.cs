using epay3.Web.Api.Sdk.Api;
using epay3.Web.Api.Sdk.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace epay3.Web.Api.Tests
{
    [TestClass]
    public class InvoicesFixture
    {
        [TestInitialize]
        public void Initialize()
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
        }

        private InvoicesApi GetApi(bool useImpersonation)
        {
            var invoicesApi = new InvoicesApi(TestApiSettings.Uri);
            var plainTextBytes = new byte[0];
            if (useImpersonation)
            {
                plainTextBytes = System.Text.Encoding.UTF8.GetBytes(TestApiSettings.Key + ":" + TestApiSettings.Secret);
                invoicesApi.Configuration.AddDefaultHeader("Authorization", "Basic " + System.Convert.ToBase64String(plainTextBytes));
                return invoicesApi;
            }

            plainTextBytes = System.Text.Encoding.UTF8.GetBytes(TestApiSettings.InvoiceKey + ":" + TestApiSettings.InvoiceSecret);
            invoicesApi.Configuration.AddDefaultHeader("Authorization", "Basic " + System.Convert.ToBase64String(plainTextBytes));
            return invoicesApi;
            
        }

        [TestMethod]
        public void Should_Get_Successfully_With_Impersonation_Key()
        {
            var invoicesApi = GetApi(true);
            var result = invoicesApi.InvoicesGet(new Dictionary<string, string>() { ["accountCode"] = "123", ["postalCode"] = "78701" }, TestApiSettings.InvoicesImpersonationAccountKey);

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
            bool success = invoicesApi.InvoicesUpdate(updateInvoicesRequestModel, TestApiSettings.InvoicesImpersonationAccountKey);

            // Should post successfully.
            Assert.IsTrue(success);
        }
    }
}
