using epay3.Web.Api.Sdk.Client;
using epay3.Web.Api.Sdk.Model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace epay3.Web.Api.Sdk.Api
{

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface ITokensApi
    {
        /// <summary>
        /// Removes a stored token.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="epay3.Web.Api.Sdk.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">The Id of the token to be deleted.</param>
        /// <param name="impersonationAccountKey">The key that allows impersonation of another account for which the token is being processed. Only specify a value if the account being impersonated is different from the account that is submitting this request.</param>
        /// <returns></returns>
        bool TokensDelete(string id, string impersonationAccountKey = null);

        /// <summary>
        /// Retrieves the details of a token.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="epay3.Web.Api.Sdk.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">The unique identifier of the token.</param>
        /// <param name="impersonationAccountKey">The key that allows impersonation of another account for which the token is being processed. Only specify a value if the account being impersonated is different from the account that is submitting this request.</param>
        /// <returns>GetTokenResponseModel</returns>
        GetTokenResponseModel TokensGet(string id, string impersonationAccountKey = null);

        /// <summary>
        /// Stores a token for either ACH or credit card payments.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="epay3.Web.Api.Sdk.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="postTokenRequestModel">The details of the token to be created.</param>
        /// <param name="impersonationAccountKey">The key that allows impersonation of another account for which the token is being processed. Only specify a value if the account being impersonated is different from the account that is submitting this request.</param>
        /// <returns></returns>
        string TokensPost (PostTokenRequestModel postTokenRequestModel, string impersonationAccountKey = null);
    }
  
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public class TokensApi : ITokensApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TokensApi"/> class.
        /// </summary>
        /// <returns></returns>
        public TokensApi(String basePath)
        {
            this.Configuration = new Configuration(new ApiClient(basePath));

            // ensure API client has configuration ready
            if (Configuration.ApiClient.Configuration == null)
            {
                this.Configuration.ApiClient.Configuration = this.Configuration;
            }
        }
    
        /// <summary>
        /// Initializes a new instance of the <see cref="TokensApi"/> class
        /// using Configuration object
        /// </summary>
        /// <param name="configuration">An instance of Configuration</param>
        /// <returns></returns>
        public TokensApi(Configuration configuration = null)
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
        /// Gets or sets the configuration object
        /// </summary>
        /// <value>An instance of the Configuration</value>
        public Configuration Configuration {get; set;}

        /// <summary>
        /// Removes a stored token. 
        /// </summary>
        /// <exception cref="epay3.Web.Api.Sdk.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">The Id of the token to be deleted.</param> 
        /// <param name="impersonationAccountKey">The key that allows impersonation of another account for which the token is being processed. Only specify a value if the account being impersonated is different from the account that is submitting this request.</param>
        /// <returns></returns>
        public bool TokensDelete (string id, string impersonationAccountKey = null)
        {
            var localVarPath = string.Format("/api/v1/Tokens/{0}", id);
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

            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse)Configuration.ApiClient.CallApi(localVarPath,
                Method.DELETE, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
                localVarPathParams, localVarHttpContentType);

            int localVarStatusCode = (int)localVarResponse.StatusCode;

            if (localVarStatusCode == 200)
                return true;
            else if (localVarStatusCode >= 400)
            {
                var errorResponseModel = Newtonsoft.Json.JsonConvert.DeserializeObject<ErrorResponseModel>(localVarResponse.Content);

                throw new ApiException(localVarStatusCode, errorResponseModel != null ? errorResponseModel.Message : null);
            }
            else
                throw new ApiException(localVarStatusCode, localVarResponse.ErrorMessage, localVarResponse.ErrorMessage);
        }

        /// <summary>
        /// Retrieves the details of a token.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="epay3.Web.Api.Sdk.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">The unique identifier of the token.</param>
        /// <param name="impersonationAccountKey">The key that allows impersonation of another account for which the token is being processed. Only specify a value if the account being impersonated is different from the account that is submitting this request.</param>
        /// <returns>GetTokenResponseModel</returns>
        public GetTokenResponseModel TokensGet(string id, string impersonationAccountKey = null)
        {
            // verify the required parameter 'id' is set
            if (id == null)
                throw new ApiException(400, "Missing required parameter 'id' when calling TokensApi->TokensGet");

            var localVarPath = "/api/v1/tokens/{id}";

            var localVarPathParams = new Dictionary<String, String>();
            var localVarQueryParams = new Dictionary<String, String>();
            var localVarHeaderParams = new Dictionary<String, String>(Configuration.DefaultHeader);
            var localVarFormParams = new Dictionary<String, String>();
            var localVarFileParams = new Dictionary<String, FileParameter>();
            Object localVarPostBody = null;

            // to determine the Content-Type header
            String[] localVarHttpContentTypes = new String[] {

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
            if (id != null) localVarPathParams.Add("id", Configuration.ApiClient.ParameterToString(id)); // path parameter

            if (impersonationAccountKey != null) localVarHeaderParams.Add("impersonationAccountKey", Configuration.ApiClient.ParameterToString(impersonationAccountKey)); // header parameter

            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse)Configuration.ApiClient.CallApi(localVarPath,
                Method.GET, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
                localVarPathParams, localVarHttpContentType);

            int localVarStatusCode = (int)localVarResponse.StatusCode;

            if (localVarStatusCode >= 400)
                throw new ApiException(localVarStatusCode, "Error calling TokensGet: " + localVarResponse.Content, localVarResponse.Content);
            else if (localVarStatusCode == 0)
                throw new ApiException(localVarStatusCode, "Error calling TokensGet: " + localVarResponse.ErrorMessage, localVarResponse.ErrorMessage);

            var response = new ApiResponse<GetTokenResponseModel>(localVarStatusCode,
                localVarResponse.Headers.ToDictionary(x => x.Name, x => x.Value.ToString()),
                (GetTokenResponseModel)Configuration.ApiClient.Deserialize(localVarResponse, typeof(GetTokenResponseModel)));

            return response.Data;
        }

        /// <summary>
        /// Stores a token for either ACH or credit card payments. 
        /// </summary>
        /// <exception cref="epay3.Web.Api.Sdk.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="postTokenRequestModel">The details of the token to be created.</param> 
        /// <param name="impersonationAccountKey">The key that allows impersonation of another account for which the token is being processed. Only specify a value if the account being impersonated is different from the account that is submitting this request.</param>
        /// <returns></returns>
        public string TokensPost (PostTokenRequestModel postTokenRequestModel, string impersonationAccountKey = null)
        {
            // verify the required parameter 'postTransactionRequestModel' is set
            if (postTokenRequestModel == null)
                throw new ApiException(400, "Missing required parameter 'postTokenRequestModel' when calling TokensApi->TokensPost");

            var localVarPath = "/api/v1/Tokens";
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

            if (postTokenRequestModel.GetType() != typeof(byte[]))
            {
                localVarPostBody = Configuration.ApiClient.Serialize(postTokenRequestModel); // http body (model) parameter
            }
            else
            {
                localVarPostBody = postTokenRequestModel; // byte array
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

            return localVarResponse.Headers.First(x => x.Name == "Location").Value.ToString().Split('/').Last();
        }
    }
}
