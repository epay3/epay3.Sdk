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
    public class IvrSessionsFixture
    {
        private IvrSessionsApi _ivrSessionsApi;

        [TestInitialize]
        public void Initialize()
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            _ivrSessionsApi = new IvrSessionsApi(TestApiSettings.Uri);

            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(TestApiSettings.Key + ":" + TestApiSettings.Secret);

            _ivrSessionsApi.Configuration.AddDefaultHeader("Authorization", "Basic " + System.Convert.ToBase64String(plainTextBytes));
        }

        [TestMethod]
        public void Should_Return_Successfully_With_No_Impersonation_Key()
        {
            var postTokenPageSessionRequestModel = new PostIvrSessionRequestModel
            {
                AttributeValues = new System.Collections.Generic.Dictionary<string, string>
                {
                    { "parameter 1", "value 1" },
                    { "parameter 2", "value 2" }
                },
                PhoneNumber = "512-555-5555",
                Expiration = 10
            };

            bool success = _ivrSessionsApi.IvrSessionsPost(postTokenPageSessionRequestModel, null);

            // Should post successfully.
            Assert.IsTrue(success);
        }

        [TestMethod]
        public void Should_Return_Successfully_With_An_Impersonation_Key()
        {
            var postTokenPageSessionRequestModel = new PostIvrSessionRequestModel
            {
                AttributeValues = new System.Collections.Generic.Dictionary<string, string>
                {
                    { "param1", "parameter value 1" },
                    { "param2", "parameter value 2" }
                },
                PhoneNumber = "512-555-5555",
                Expiration = 10
            };

            bool success = _ivrSessionsApi.IvrSessionsPost(postTokenPageSessionRequestModel, TestApiSettings.ImpersonationAccountKey);

            // Should post successfully.
            Assert.IsTrue(success);
        }
    }
}
