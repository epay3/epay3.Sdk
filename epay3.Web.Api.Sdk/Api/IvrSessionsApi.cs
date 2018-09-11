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
    public interface IIvrSessionsApi
    {
        /// <summary>
        /// Creates a temporary "session" with parameters so that the caller can be forwarded to the IVR service with this context.
        /// </summary>
        /// <exception cref="epay3.Web.Api.Sdk.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="postIvrSessionRequestModel">Contains the parameters for the "session".</param>
        /// <param name="impersonationAccountKey">The key that allows impersonation of another account for which the token is being created. Only specify a value if the account being impersonated is different from the account that is submitting this request. (optional, default to )</param>
        /// <returns></returns>
        bool IvrSessionsPost (PostIvrSessionRequestModel postIvrSessionRequestModel, string impersonationAccountKey = null);
    }
  
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public class IvrSessionsApi : IIvrSessionsApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IvrSessionsApi"/> class.
        /// </summary>
        /// <returns></returns>
        public IvrSessionsApi(String basePath)
        {
            this.Configuration = new Configuration(new ApiClient(basePath));

            // ensure API client has configuration ready
            if (Configuration.ApiClient.Configuration == null)
            {
                this.Configuration.ApiClient.Configuration = this.Configuration;
            }
        }
    
        /// <summary>
        /// Initializes a new instance of the <see cref="IvrSessionsApi"/> class
        /// using Configuration object
        /// </summary>
        /// <param name="configuration">An instance of Configuration</param>
        /// <returns></returns>
        public IvrSessionsApi(Configuration configuration = null)
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
        /// Creates a temporary "session" with parameters so that the caller can be forwarded to the IVR service with this context. 
        /// </summary>
        /// <exception cref="epay3.Web.Api.Sdk.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="postIvrSessionRequestModel">Contains the parameters for the "session".</param> 
        /// <param name="impersonationAccountKey">The key that allows impersonation of another account for which the token is being created. Only specify a value if the account being impersonated is different from the account that is submitting this request. (optional, default to )</param> 
        public bool IvrSessionsPost (PostIvrSessionRequestModel postIvrSessionRequestModel, string impersonationAccountKey = null)
        {
            
            // verify the required parameter 'postIvrSessionRequestModel' is set
            if (postIvrSessionRequestModel == null)
                throw new ApiException(400, "Missing required parameter 'postIvrSessionRequestModel' when calling IvrSessionsApi->IvrSessionsPost");
            
    
            var localVarPath = "/api/v1/ivrSessions";
    
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
            
            
            if (postIvrSessionRequestModel.GetType() != typeof(byte[]))
            {
                localVarPostBody = Configuration.ApiClient.Serialize(postIvrSessionRequestModel); // http body (model) parameter
            }
            else
            {
                localVarPostBody = postIvrSessionRequestModel; // byte array
            }
            
    
            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse) Configuration.ApiClient.CallApi(localVarPath, 
                Method.POST, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
                localVarPathParams, localVarHttpContentType);

            int localVarStatusCode = (int) localVarResponse.StatusCode;
    
            if (localVarStatusCode >= 400)
                throw new ApiException (localVarStatusCode, "Error calling IvrSessionsPost: " + localVarResponse.Content, localVarResponse.Content);
            else if (localVarStatusCode == 0)
                throw new ApiException (localVarStatusCode, "Error calling IvrSessionsPost: " + localVarResponse.ErrorMessage, localVarResponse.ErrorMessage);


            return true;
        }
    }
}
