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
    public interface ITransactionsApi
    {
        /// <summary>
        /// Retrieves the details of a transaction.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="epay3.Web.Api.Sdk.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">The unique identifier of the transaction.</param>
        /// <param name="impersonationAccountKey">The key that allows impersonation of another account for which the transaction is being processed. Only specify a value if the account being impersonated is different from the account that is submitting this request.</param>
        /// <returns>GetTransactionResponseModel</returns>
        GetTransactionResponseModel TransactionsGet(long? id, string impersonationAccountKey = null);

        /// <summary>
        /// Submit a sale transaction for either ACH or credit card.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="epay3.Web.Api.Sdk.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="postTransactionRequestModel">The details of the transaction to be processed.</param>
        /// <param name="impersonationAccountKey">The key that allows impersonation of another account for which the transaction is being processed. Only specify a value if the account being impersonated is different from the account that is submitting this request.</param>
        /// <returns>PostTransactionResponseModel</returns>
        PostTransactionResponseModel TransactionsPost (PostTransactionRequestModel postTransactionRequestModel, string impersonationAccountKey = null);

        /// <summary>
        /// Submits a request to void a transaction.
        /// </summary>
        /// <param name="id">The Id of the transaction.</param>
        /// <param name="sendReceipt">Set to true if a receipt should be sent to all parties upon a successful void.</param>
        /// <param name="impersonationAccountKey">The key that allows impersonation of another account for which the transaction is being processed. Only specify a value if the account being impersonated is different from the account that is submitting this request.</param>
        /// <returns>PostVoidTransactionResponseModel</returns>
        PostVoidTransactionResponseModel TransactionsVoid(long id, bool sendReceipt, string impersonationAccountKey = null);
    }
  
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public class TransactionsApi : ITransactionsApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionsApi"/> class.
        /// </summary>
        /// <returns></returns>
        public TransactionsApi(String basePath)
        {
            this.Configuration = new Configuration(new ApiClient(basePath));

            // ensure API client has configuration ready
            if (Configuration.ApiClient.Configuration == null)
            {
                this.Configuration.ApiClient.Configuration = this.Configuration;
            }
        }
    
        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionsApi"/> class
        /// using Configuration object
        /// </summary>
        /// <param name="configuration">An instance of Configuration</param>
        /// <returns></returns>
        public TransactionsApi(Configuration configuration = null)
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
        /// Retrieves the details of a transaction. 
        /// </summary>
        /// <exception cref="epay3.Web.Api.Sdk.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">The unique identifier of the transaction.</param> 
        /// <param name="impersonationAccountKey">The key that allows impersonation of another account for which the transaction is being processed. Only specify a value if the account being impersonated is different from the account that is submitting this request.</param>
        /// <returns>GetTransactionResponseModel</returns>
        public GetTransactionResponseModel TransactionsGet(long? id, string impersonationAccountKey = null)
        {
            // verify the required parameter 'id' is set
            if (id == null)
                throw new ApiException(400, "Missing required parameter 'id' when calling TransactionsApi->TransactionsGet");

            var localVarPath = "/api/v1/transactions/{id}";

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

            if (impersonationAccountKey != null) localVarHeaderParams.Add("impersonationAccountKey", Configuration.ApiClient.ParameterToString(impersonationAccountKey)); // header parameter

            // set "format" to json by default
            // e.g. /pet/{petId}.{format} becomes /pet/{petId}.json
            localVarPathParams.Add("format", "json");
            if (id != null) localVarPathParams.Add("id", Configuration.ApiClient.ParameterToString(id)); // path parameter

            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse)Configuration.ApiClient.CallApi(localVarPath,
                Method.GET, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
                localVarPathParams, localVarHttpContentType);

            int localVarStatusCode = (int)localVarResponse.StatusCode;

            if (localVarStatusCode >= 400)
                throw new ApiException(localVarStatusCode, "Error calling TransactionsGet: " + localVarResponse.Content, localVarResponse.Content);
            else if (localVarStatusCode == 0)
                throw new ApiException(localVarStatusCode, "Error calling TransactionsGet: " + localVarResponse.ErrorMessage, localVarResponse.ErrorMessage);

            var response = new ApiResponse<GetTransactionResponseModel>(localVarStatusCode,
                localVarResponse.Headers.ToDictionary(x => x.Name, x => x.Value.ToString()),
                (GetTransactionResponseModel)Configuration.ApiClient.Deserialize(localVarResponse, typeof(GetTransactionResponseModel)));

            return response.Data;
        }

        /// <summary>
        /// Submit a sale transaction for either ACH or credit card. 
        /// </summary>
        /// <exception cref="epay3.Web.Api.Sdk.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="postTransactionRequestModel">The details of the transaction to be processed.</param> 
        /// <param name="impersonationAccountKey">The key that allows impersonation of another account for which the transaction is being processed. Only specify a value if the account being impersonated is different from the account that is submitting this request.</param>
        /// <returns>PostTransactionResponseModel</returns>
        public PostTransactionResponseModel TransactionsPost (PostTransactionRequestModel postTransactionRequestModel, string impersonationAccountKey = null)
        {
            // verify the required parameter 'postTransactionRequestModel' is set
            if (postTransactionRequestModel == null)
                throw new ApiException(400, "Missing required parameter 'postTransactionRequestModel' when calling TransactionsApi->TransactionsPost");

            var localVarPath = "/api/v1/Transactions";
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

            if (postTransactionRequestModel.GetType() != typeof(byte[]))
            {
                localVarPostBody = Configuration.ApiClient.Serialize(postTransactionRequestModel); // http body (model) parameter
            }
            else
            {
                localVarPostBody = postTransactionRequestModel; // byte array
            }

            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse) Configuration.ApiClient.CallApi(localVarPath, 
                Method.POST, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
                localVarPathParams, localVarHttpContentType);

            int localVarStatusCode = (int) localVarResponse.StatusCode;

            if (localVarStatusCode == 400)
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<PostTransactionResponseModel>(localVarResponse.Content);                
            }
            else if (localVarStatusCode >= 400)
            {
                var errorResponseModel = Newtonsoft.Json.JsonConvert.DeserializeObject<ErrorResponseModel>(localVarResponse.Content);

                throw new ApiException(localVarStatusCode, errorResponseModel != null ? errorResponseModel.Message : null);
            }
            else if (localVarStatusCode == 0)
                throw new ApiException(localVarStatusCode, localVarResponse.ErrorMessage, localVarResponse.ErrorMessage);

            var id = localVarResponse.Headers.First(x => x.Name == "Location").Value.ToString().Split('/').Last();

            return new PostTransactionResponseModel
            {
                Id = long.Parse(id),
                PaymentResponseCode = PaymentResponseCode.Success
            };
        }

        /// <summary>
        /// Submits a request to void a transaction.
        /// </summary>
        /// <param name="id">The Id of the transaction.</param>
        /// <param name="sendReceipt">Set to true if a receipt should be sent to all parties upon a successful void.</param>
        /// <param name="impersonationAccountKey">The key that allows impersonation of another account for which the transaction is being processed. Only specify a value if the account being impersonated is different from the account that is submitting this request.</param>
        /// <returns>PostVoidTransactionResponseModel</returns>
        public PostVoidTransactionResponseModel TransactionsVoid(long id, bool sendReceipt, string impersonationAccountKey = null)
        {
            var localVarPath = string.Format("/api/v1/Transactions/{0}/void", id);
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

            localVarQueryParams.Add("sendReceipt", sendReceipt.ToString());

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
                return Newtonsoft.Json.JsonConvert.DeserializeObject<PostVoidTransactionResponseModel>(localVarResponse.Content);
            }
            else if (localVarStatusCode >= 400)
            {
                var errorResponseModel = Newtonsoft.Json.JsonConvert.DeserializeObject<ErrorResponseModel>(localVarResponse.Content);

                throw new ApiException(localVarStatusCode, errorResponseModel != null ? errorResponseModel.Message : null);
            }

            return new PostVoidTransactionResponseModel
            {
                ReversalResponseCode = ReversalResponseCode.Success
            };
        }
    }
}
