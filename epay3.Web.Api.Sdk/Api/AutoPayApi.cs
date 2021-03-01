using epay3.Web.Api.Sdk.Client;
using epay3.Web.Api.Sdk.Model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace epay3.Web.Api.Sdk.Api
{
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IAutoPayApi
    {
        /// <summary>
        /// Retrieves the details of an AutoPay.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="epay3.Web.Api.Sdk.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">The unique identifier of the AutoPay.</param>
        /// <param name="impersonationAccountKey">The key that allows impersonation of another account for which the transaction(s) will be processed. Only specify a value if the account being impersonated is different from the account that is submitting this request. (optional, default to )</param> 
        /// <returns>GetAutoPayResponseModel</returns>
        GetAutoPayResponseModel AutoPayGet(long id, string impersonationAccountKey = null);

        /// <summary>
        /// Creates an auto pay specified Invoice. 
        /// </summary>
        /// <exception cref="epay3.Web.Api.Sdk.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="postAutoPayRequestModel">Contains the parameters for the auto pay.</param> 
        /// <param name="impersonationAccountKey">The key that allows impersonation of another account for which the transaction(s) will be processed. Only specify a value if the account being impersonated is different from the account that is submitting this request. (optional, default to )</param> 
        /// <returns></returns>
        long? AutoPayPost(PostAutoPayRequestModel postAutoPayRequestModel, string impersonationAccountKey = null);
    }
    public class AutoPayApi : IAutoPayApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AutoPayApi"/> class.
        /// </summary>
        /// <returns></returns>
        public AutoPayApi(String basePath)
        {
            this.Configuration = new Configuration(new ApiClient(basePath));

            // ensure API client has configuration ready
            if (Configuration.ApiClient.Configuration == null)
            {
                this.Configuration.ApiClient.Configuration = this.Configuration;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoPayApi"/> class
        /// using Configuration object
        /// </summary>
        /// <param name="configuration">An instance of Configuration</param>
        /// <returns></returns>
        public AutoPayApi(Configuration configuration = null)
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
        public Configuration Configuration { get; set; }

        /// <summary>
        /// Cancels an active auto pay. 
        /// </summary>
        /// <exception cref="epay3.Web.Api.Sdk.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">The public Id of the auto pay.</param> 
        /// <param name="impersonationAccountKey">The key that allows impersonation of another account for which the transaction(s) will be processed. Only specify a value if the account being impersonated is different from the account that is submitting this request. (optional, default to )</param> 
        /// <returns></returns>
        public bool AutoPayCancel(string id, string impersonationAccountKey = null)
        {
            if (id == null)
                throw new ApiException(400, "Missing required parameter 'id' when calling AutoPayApi->AutoPayCancel");

            var localVarPath = string.Format("/api/v1/autoPay/{0}/cancel", id);
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
                Method.POST, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
                localVarPathParams, localVarHttpContentType);

            int localVarStatusCode = (int)localVarResponse.StatusCode;

            if (localVarStatusCode == 400)
            {
                var errorResponseModel = Newtonsoft.Json.JsonConvert.DeserializeObject<ErrorResponseModel>(localVarResponse.Content);

                throw new ApiException(localVarStatusCode, errorResponseModel != null ? errorResponseModel.Message : null);
            }
            else if (localVarStatusCode >= 400)
            {
                var errorResponseModel = Newtonsoft.Json.JsonConvert.DeserializeObject<ErrorResponseModel>(localVarResponse.Content);

                throw new ApiException(localVarStatusCode, errorResponseModel != null ? errorResponseModel.Message : null);
            }
            else if (localVarStatusCode == 0)
                throw new ApiException(localVarStatusCode, localVarResponse.ErrorMessage, localVarResponse.ErrorMessage);

            return true;
        }

        /// <summary>
        /// Retrieves the details of a Auto Pay. 
        /// </summary>
        /// <exception cref="epay3.Web.Api.Sdk.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">The unique identifier of the auto pay.</param> 
        /// <param name="impersonationAccountKey">The key that allows impersonation of another account for which the transaction(s) will be processed. Only specify a value if the account being impersonated is different from the account that is submitting this request. (optional, default to )</param> 
        /// <returns>GetAutoPayResponseModel</returns>
        public GetAutoPayResponseModel AutoPayGet(long id, string impersonationAccountKey = null)
        {
            // verify the required parameter 'id' is set
            if (id == null)
                throw new ApiException(400, "Missing required parameter 'id' when calling AutoPayApi->AutoPayGet");

            var localVarPath = "/api/v1/autoPay/{id}";

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
            localVarPathParams.Add("id", Configuration.ApiClient.ParameterToString(id)); // path parameter

            if (impersonationAccountKey != null) localVarHeaderParams.Add("impersonationAccountKey", Configuration.ApiClient.ParameterToString(impersonationAccountKey)); // header parameter

            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse)Configuration.ApiClient.CallApi(localVarPath,
                Method.GET, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
                localVarPathParams, localVarHttpContentType);

            int localVarStatusCode = (int)localVarResponse.StatusCode;

            if (localVarStatusCode >= 400)
                throw new ApiException(localVarStatusCode, "Error calling AutoPayGet: " + localVarResponse.Content, localVarResponse.Content);
            else if (localVarStatusCode == 0)
                throw new ApiException(localVarStatusCode, "Error calling AutoPayGet: " + localVarResponse.ErrorMessage, localVarResponse.ErrorMessage);

            var response = new ApiResponse<GetAutoPayResponseModel>(localVarStatusCode,
                localVarResponse.Headers.ToDictionary(x => x.Name, x => x.Value.ToString()),
                (GetAutoPayResponseModel)Configuration.ApiClient.Deserialize(localVarResponse, typeof(GetAutoPayResponseModel)));

            return response.Data;
        }

        /// <summary>
        /// Creates an auto pay specified Invoice. 
        /// </summary>
        /// <exception cref="epay3.Web.Api.Sdk.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="postAutoPayRequestModel">Contains the parameters for the auto pay.</param> 
        /// <param name="impersonationAccountKey">The key that allows impersonation of another account for which the transaction(s) will be processed. Only specify a value if the account being impersonated is different from the account that is submitting this request. (optional, default to )</param> 
        /// <returns></returns>
        public long? AutoPayPost(PostAutoPayRequestModel postAutoPayRequestModel, string impersonationAccountKey = null)
        {
            if (postAutoPayRequestModel == null)
                throw new ApiException(400, "Missing required parameter 'postAutoPayRequestModel' when calling AutoPay Api->AutoPayPost");

            var localVarPath = "/api/v1/autoPay";
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

            if (postAutoPayRequestModel.GetType() != typeof(byte[]))
            {
                localVarPostBody = Configuration.ApiClient.Serialize(postAutoPayRequestModel); // http body (model) parameter
            }
            else
            {
                localVarPostBody = postAutoPayRequestModel; // byte array
            }

            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse)Configuration.ApiClient.CallApi(localVarPath,
                Method.POST, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
                localVarPathParams, localVarHttpContentType);

            int localVarStatusCode = (int)localVarResponse.StatusCode;

            if (localVarStatusCode == 400)
            {
                var errorResponseModel = Newtonsoft.Json.JsonConvert.DeserializeObject<ErrorResponseModel>(localVarResponse.Content);

                throw new ApiException(localVarStatusCode, errorResponseModel != null ? errorResponseModel.Message : null);
            }
            else if (localVarStatusCode >= 400)
            {
                var errorResponseModel = Newtonsoft.Json.JsonConvert.DeserializeObject<ErrorResponseModel>(localVarResponse.Content);

                throw new ApiException(localVarStatusCode, errorResponseModel != null ? errorResponseModel.Message : null);
            }
            else if (localVarStatusCode == 0)
                throw new ApiException(localVarStatusCode, localVarResponse.ErrorMessage, localVarResponse.ErrorMessage);

            var id = localVarResponse.Headers.First(x => x.Name == "Location").Value.ToString().Split('/').Last();

            return long.Parse(id);
                
        }
    }
}
