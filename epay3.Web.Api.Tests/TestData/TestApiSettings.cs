using epay3.Web.Api.Tests.TestData;

namespace epay3.Web.Api.Tests
{
    public class TestApiSettings : IAccountConfig
    {
        public string Uri
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["ApiUri"];
            }
        }

        public virtual string Key
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["ApiKey"];
            }
        }

        public virtual string Secret
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["ApiSecret"];
            }
        }

        public virtual string PublicKey
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["ApiPublicKey"];
            }
        }

        public string InvoiceKey
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["InvoiceApiKey"];
            }
        }

        public string InvoiceSecret
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["InvoiceApiSecret"];
            }
        }

        public string ImpersonationAccountKey
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["ImpersonationAccountKey"];
            }
        }

        public string InvoicesImpersonationAccountKey
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["InvoicesImpersonationAccountKey"];
            }
        }

        public long ImpersonationOnlyBatchId => long.Parse(System.Configuration.ConfigurationManager.AppSettings["ImpersonationOnlyBatchId"]);
    }
}