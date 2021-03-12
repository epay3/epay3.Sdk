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
    public interface IInvoicesApi
    {
        /// <summary>
        /// Gets a collection of invoices given lookup attributes. Add these attributes as individual parameters.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="epay3.Web.Api.Sdk.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="impersonationAccountKey"> (optional, default to )</param>
        /// <returns>The list of gotten invoices.</returns>
        InvoicesResponseModel InvoicesGet(Dictionary<string, string> attributeValues, string impersonationAccountKey);

        /// <summary>
        /// Updates a collection of invoices in the management system.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="epay3.Web.Api.Sdk.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="updateInvoicesRequestModel">The paid invoices and lookup attributes.</param>
        /// <param name="impersonationAccountKey"> (optional, default to )</param>
        /// <returns></returns>
        bool InvoicesUpdate(UpdateInvoicesRequestModel updateInvoicesRequestModel, string impersonationAccountKey);
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public class InvoicesApi : IInvoicesApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvoicesApi"/> class.
        /// </summary>
        /// <returns></returns>
        public InvoicesApi(String basePath)
        {
            this.Configuration = new Configuration(new ApiClient(basePath));

            // ensure API client has configuration ready
            if (Configuration.ApiClient.Configuration == null)
            {
                this.Configuration.ApiClient.Configuration = this.Configuration;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvoicesApi"/> class
        /// using Configuration object
        /// </summary>
        /// <param name="configuration">An instance of Configuration</param>
        /// <returns></returns>
        public InvoicesApi(Configuration configuration = null)
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
        /// Gets a collection of invoices given lookup attributes. Add these attributes as individual parameters. 
        /// </summary>
        /// <exception cref="epay3.Web.Api.Sdk.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="impersonationAccountKey"> (optional, default to )</param> 
        /// <returns></returns>
        public InvoicesResponseModel InvoicesGet(Dictionary<string,string> attributes, string impersonationAccountKey)
        { 
            var localVarPath = "/api/v1/Invoices";

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


            if (impersonationAccountKey != null) localVarHeaderParams.Add("impersonationAccountKey", Configuration.ApiClient.ParameterToString(impersonationAccountKey)); // header parameter

            foreach (var attribute in attributes)
            {
                localVarQueryParams.Add(attribute.Key, attribute.Value);
            }

            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse)Configuration.ApiClient.CallApi(localVarPath,
                Method.GET, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
                localVarPathParams, localVarHttpContentType);

            int localVarStatusCode = (int)localVarResponse.StatusCode;

            if (localVarStatusCode >= 400)
            {
                if (localVarStatusCode == 401)
                    throw new ApiException(localVarStatusCode, "Error calling InvoicesGet: " + localVarResponse.StatusDescription);
                else
                    throw new ApiException(localVarStatusCode, "Error calling InvoicesGet: " + localVarResponse.Content, localVarResponse.Content);
            }
            else if (localVarStatusCode == 0)
                throw new ApiException(localVarStatusCode, "Error calling InvoicesGet: " + localVarResponse.ErrorMessage, localVarResponse.ErrorMessage);


            var response = new ApiResponse<InvoicesResponseModel>(localVarStatusCode,
                localVarResponse.Headers.ToDictionary(x => x.Name, x => x.Value.ToString()),
                (InvoicesResponseModel)Configuration.ApiClient.Deserialize(localVarResponse, typeof(InvoicesResponseModel)));

            return response.Data;
        }

        /// <summary>
        /// Gets a collection of invoices given lookup attributes. Add these attributes as individual parameters. 
        /// </summary>
        /// <exception cref="epay3.Web.Api.Sdk.Client.ApiException">Thrown when fails to make API call</exception>
        /// <returns></returns>
        public InvoicesResponseModel InvoicesGet(Dictionary<string, string> attributes)
        {
            var localVarPath = "/api/v1/Invoices";

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

            foreach (var attribute in attributes)
            {
                localVarQueryParams.Add(attribute.Key, attribute.Value);
            }

            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse)Configuration.ApiClient.CallApi(localVarPath,
                Method.GET, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
                localVarPathParams, localVarHttpContentType);

            int localVarStatusCode = (int)localVarResponse.StatusCode;

            if (localVarStatusCode >= 400)
            {
                if (localVarStatusCode == 401)
                    throw new ApiException(localVarStatusCode, "Error calling InvoicesGet: " + localVarResponse.StatusDescription);
                else
                    throw new ApiException(localVarStatusCode, "Error calling InvoicesGet: " + localVarResponse.Content, localVarResponse.Content);
            }
            else if (localVarStatusCode == 0)
                throw new ApiException(localVarStatusCode, "Error calling InvoicesGet: " + localVarResponse.ErrorMessage, localVarResponse.ErrorMessage);


            var response = new ApiResponse<InvoicesResponseModel>(localVarStatusCode,
                localVarResponse.Headers.ToDictionary(x => x.Name, x => x.Value.ToString()),
                (InvoicesResponseModel)Configuration.ApiClient.Deserialize(localVarResponse, typeof(InvoicesResponseModel)));

            return response.Data;
        }

        /// <summary>
        /// Updates a collection of invoices in the management system. 
        /// </summary>
        /// <exception cref="epay3.Web.Api.Sdk.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="updateInvoicesRequestModel">The paid invoices and lookup attributes.</param> 
        /// <param name="impersonationAccountKey"> (optional, default to )</param> 
        /// <returns></returns>
        public bool InvoicesUpdate(UpdateInvoicesRequestModel updateInvoicesRequestModel, string impersonationAccountKey)
        {

            // verify the required parameter 'updateInvoicesRequestModel' is set
            if (updateInvoicesRequestModel == null)
                throw new ApiException(400, "Missing required parameter 'updateInvoicesRequestModel' when calling InvoicesApi->InvoicesUpdate");


            var localVarPath = "/api/v1/Invoices";

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


            if (updateInvoicesRequestModel.GetType() != typeof(byte[]))
            {
                localVarPostBody = Configuration.ApiClient.Serialize(updateInvoicesRequestModel); // http body (model) parameter
            }
            else
            {
                localVarPostBody = updateInvoicesRequestModel; // byte array
            }

            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse)Configuration.ApiClient.CallApi(localVarPath,
                Method.POST, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
                localVarPathParams, localVarHttpContentType);

            int localVarStatusCode = (int)localVarResponse.StatusCode;

            if (localVarStatusCode >= 400)
            {
                if (localVarStatusCode == 401)
                    throw new ApiException(localVarStatusCode, "Error calling InvoicesGet: " + localVarResponse.StatusDescription);
                else
                    throw new ApiException(localVarStatusCode, "Error calling InvoicesGet: " + localVarResponse.Content, localVarResponse.Content);
            }
            else if (localVarStatusCode == 0)
                throw new ApiException(localVarStatusCode, "Error calling InvoicesUpdate: " + localVarResponse.ErrorMessage, localVarResponse.ErrorMessage);


            return localVarStatusCode == 200;
        }
    }

}
