﻿namespace epay3.Web.Api.Tests
{
    public static class TestApiSettings
    {
        public static string Uri
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["ApiUri"];
            }
        }

        public static string Key
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["ApiKey"];
            }
        }

        public static string Secret
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["ApiSecret"];
            }
        }

        public static string InvoiceKey
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["InvoiceApiKey"];
            }
        }

        public static string InvoiceSecret
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["InvoiceApiSecret"];
            }
        }

        public static string PublicKey
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["ApiPublicKey"];
            }
        }

        public static string ImpersonationAccountKey
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["ImpersonationAccountKey"];
            }
        }

        public static string InvoicesImpersonationAccountKey
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["InvoicesImpersonationAccountKey"];
            }
        }

        public static long ImpersonationOnlyBatchId => long.Parse(System.Configuration.ConfigurationManager.AppSettings["ImpersonationOnlyBatchId"]);
    }
}
