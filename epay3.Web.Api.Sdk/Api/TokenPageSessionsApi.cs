using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using RestSharp;
using epay3.Web.Api.Sdk.Client;
using epay3.Web.Api.Sdk.Model;

namespace epay3.Web.Api.Sdk.Api
{
    
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface ITokenPageSessionsApi
    {
        /// <summary>
        /// Creates a temporary "session" with parameters so that the user can be forwarded to the token page with this context.
        /// </summary>
        /// <param name="postTokenPageSessionRequestModel">Contains the parameters for the "session".</param>
        /// <param name="impersonationAccountKey">The key that allows impersonation of another account for which the token is being created. Only specify a value if the account being impersonated is different from the account that is submitting this request.</param>
        string TokenPageSessionsPost (PostTokenPageSessionRequestModel postTokenPageSessionRequestModel, string impersonationAccountKey);
    }
  
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public class TokenPageSessionsApi : ITokenPageSessionsApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TokenPageSessionsApi"/> class.
        /// </summary>
        /// <returns></returns>
        public TokenPageSessionsApi(String basePath)
        {
            this.Configuration = new Configuration(new ApiClient(basePath));

            // ensure API client has configuration ready
            if (Configuration.ApiClient.Configuration == null)
            {
                this.Configuration.ApiClient.Configuration = this.Configuration;
            }
        }
    
        /// <summary>
        /// Initializes a new instance of the <see cref="TokenPageSessionsApi"/> class
        /// using Configuration object
        /// </summary>
        /// <param name="configuration">An instance of Configuration</param>
        /// <returns></returns>
        public TokenPageSessionsApi(Configuration configuration = null)
        {
            if (configuration == null) // use the default one in Configuration
                this.Configuration = Configuration.Default; 
            else
                this.Configuration = configuration;

            // ensure API client has configuration ready
            if (Configuration.ApiClient.Configuration == null)
            {
                this.Configuration.ApiClient.Configuration = this.Configuration;
            }
        }

        /// <summary>
        /// Gets the base path of the API client.
        /// </summary>
        /// <value>The base path</value>
        public String GetBasePath()
        {
            return this.Configuration.ApiClient.RestClient.BaseUrl.ToString();
        }

        /// <summary>
        /// Sets the base path of the API client.
        /// </summary>
        /// <value>The base path</value>
        [Obsolete("SetBasePath is deprecated, please do 'Configuraiton.ApiClient = new ApiClient(\"http://new-path\")' instead.")]
        public void SetBasePath(String basePath)
        {
            // do nothing
        }
    
        /// <summary>
        /// Gets or sets the configuration object
        /// </summary>
        /// <value>An instance of the Configuration</value>
        public Configuration Configuration {get; set;}

        /// <summary>
        /// Gets the default header.
        /// </summary>
        /// <returns>Dictionary of HTTP header</returns>
        [Obsolete("DefaultHeader is deprecated, please use Configuration.DefaultHeader instead.")]
        public Dictionary<String, String> DefaultHeader()
        {
            return this.Configuration.DefaultHeader;
        }

        /// <summary>
        /// Add default header.
        /// </summary>
        /// <param name="key">Header field name.</param>
        /// <param name="value">Header field value.</param>
        /// <returns></returns>
        [Obsolete("AddDefaultHeader is deprecated, please use Configuration.AddDefaultHeader instead.")]
        public void AddDefaultHeader(string key, string value)
        {
            this.Configuration.AddDefaultHeader(key, value);
        }

        /// <summary>
        /// Creates a temporary "session" with parameters so that the user can be forwarded to the token page with this context.
        /// </summary>
        /// <param name="postTokenPageSessionRequestModel">Contains the parameters for the "session".</param>
        /// <param name="impersonationAccountKey">The key that allows impersonation of another account for which the token is being created. Only specify a value if the account being impersonated is different from the account that is submitting this request.</param>
        public string TokenPageSessionsPost (PostTokenPageSessionRequestModel postTokenPageSessionRequestModel, string impersonationAccountKey)
        {
            // verify the required parameter 'postTokenPageSessionRequestModel' is set
            if (postTokenPageSessionRequestModel == null)
                throw new ApiException(400, "Missing required parameter 'postTokenPageSessionRequestModel' when calling TokenPageSessionsApi->TokenPageSessionsPost");

            var localVarPath = "/api/v1/TokenPageSessions";
            var localVarPathParams = new Dictionary<String, String>();
            var localVarQueryParams = new Dictionary<String, String>();
            var localVarHeaderParams = new Dictionary<String, String>(Configuration.DefaultHeader);
            var localVarFormParams = new Dictionary<String, String>();
            var localVarFileParams = new Dictionary<String, FileParameter>();
            Object localVarPostBody = null;

            // to determine the Content-Type header
            String[] localVarHttpContentTypes = new String[] {
                "application/json", "text/json", "application/xml", "text/xml", "application/x-www-form-urlencoded"
            };
            String localVarHttpContentType = Configuration.ApiClient.SelectHeaderContentType(localVarHttpContentTypes);

            // to determine the Accept header
            String[] localVarHttpHeaderAccepts = new String[] {
                "application/json", "text/json", "application/xml", "text/xml"
            };
            String localVarHttpHeaderAccept = Configuration.ApiClient.SelectHeaderAccept(localVarHttpHeaderAccepts);
            if (localVarHttpHeaderAccept != null)
                localVarHeaderParams.Add("Accept", localVarHttpHeaderAccept);

            // set "format" to json by default
            // e.g. /pet/{petId}.{format} becomes /pet/{petId}.json
            localVarPathParams.Add("format", "json");

            if (impersonationAccountKey != null) localVarHeaderParams.Add("impersonationAccountKey", Configuration.ApiClient.ParameterToString(impersonationAccountKey)); // header parameter

            if (postTokenPageSessionRequestModel.GetType() != typeof(byte[]))
            {
                localVarPostBody = Configuration.ApiClient.Serialize(postTokenPageSessionRequestModel); // http body (model) parameter
            }
            else
            {
                localVarPostBody = postTokenPageSessionRequestModel; // byte array
            }

            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse)Configuration.ApiClient.CallApi(localVarPath,
                Method.POST, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
                localVarPathParams, localVarHttpContentType);

            int localVarStatusCode = (int)localVarResponse.StatusCode;

            if (localVarStatusCode >= 400)
            {
                var errorResponseModel = Newtonsoft.Json.JsonConvert.DeserializeObject<ErrorResponseModel>(localVarResponse.Content);

                throw new ApiException(localVarStatusCode, errorResponseModel != null ? errorResponseModel.Message : null);
            }
            else if (localVarStatusCode == 0)
                throw new ApiException(localVarStatusCode, localVarResponse.ErrorMessage, localVarResponse.ErrorMessage);

            var id = localVarResponse.Headers.First(x => x.Name == "Location").Value.ToString().Split('/').Last();

            return id;
        }
    }
}
