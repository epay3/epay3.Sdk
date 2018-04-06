namespace epay3.Web.Api.Tests
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
    }
}
