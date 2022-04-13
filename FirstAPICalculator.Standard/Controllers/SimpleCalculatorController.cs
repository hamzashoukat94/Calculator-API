// <copyright file="SimpleCalculatorController.cs" company="APIMatic">
// Copyright (c) APIMatic. All rights reserved.
// </copyright>
namespace FirstAPICalculator.Standard.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using FirstAPICalculator.Standard;
    using FirstAPICalculator.Standard.Authentication;
    using FirstAPICalculator.Standard.Http.Client;
    using FirstAPICalculator.Standard.Http.Request;
    using FirstAPICalculator.Standard.Http.Request.Configuration;
    using FirstAPICalculator.Standard.Http.Response;
    using FirstAPICalculator.Standard.Utilities;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// SimpleCalculatorController.
    /// </summary>
    public class SimpleCalculatorController : BaseController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleCalculatorController"/> class.
        /// </summary>
        /// <param name="config"> config instance. </param>
        /// <param name="httpClient"> httpClient. </param>
        /// <param name="authManagers"> authManager. </param>
        /// <param name="httpCallBack"> httpCallBack. </param>
        internal SimpleCalculatorController(IConfiguration config, IHttpClient httpClient, IDictionary<string, IAuthManager> authManagers, HttpCallBack httpCallBack = null)
            : base(config, httpClient, authManagers, httpCallBack)
        {
        }

        /// <summary>
        /// Calculate the expression using specified operation.
        /// </summary>
        /// <param name="operation">Required parameter: operation to perform.</param>
        /// <param name="x">Required parameter: First value.</param>
        /// <param name="y">Required parameter: second value.</param>
        /// <returns>Returns the double response from the API call.</returns>
        public double Calculate(
                Models.OperationTypeEnum operation,
                double x,
                double y)
        {
            Task<double> t = this.CalculateAsync(operation, x, y);
            ApiHelper.RunTaskSynchronously(t);
            return t.Result;
        }

        /// <summary>
        /// Calculate the expression using specified operation.
        /// </summary>
        /// <param name="operation">Required parameter: operation to perform.</param>
        /// <param name="x">Required parameter: First value.</param>
        /// <param name="y">Required parameter: second value.</param>
        /// <param name="cancellationToken"> cancellationToken. </param>
        /// <returns>Returns the double response from the API call.</returns>
        public async Task<double> CalculateAsync(
                Models.OperationTypeEnum operation,
                double x,
                double y,
                CancellationToken cancellationToken = default)
        {
            // the base uri for api requests.
            string baseUri = this.Config.GetBaseUri();

            // prepare query string for API call.
            StringBuilder queryBuilder = new StringBuilder(baseUri);
            queryBuilder.Append("/{operation}");

            // process optional template parameters.
            ApiHelper.AppendUrlWithTemplateParameters(queryBuilder, new Dictionary<string, object>()
            {
                { "operation", ApiHelper.JsonSerialize(operation).Trim('\"') },
            });

            // prepare specfied query parameters.
            var queryParams = new Dictionary<string, object>()
            {
                { "x", x },
                { "y", y },
            };

            // append request with appropriate headers and parameters
            var headers = new Dictionary<string, string>()
            {
                { "user-agent", this.UserAgent },
            };

            // prepare the API call request to fetch the response.
            HttpRequest httpRequest = this.GetClientInstance().Get(queryBuilder.ToString(), headers, queryParameters: queryParams);

            if (this.HttpCallBack != null)
            {
                this.HttpCallBack.OnBeforeHttpRequestEventHandler(this.GetClientInstance(), httpRequest);
            }

            // invoke request and get response.
            HttpStringResponse response = await this.GetClientInstance().ExecuteAsStringAsync(httpRequest, cancellationToken: cancellationToken).ConfigureAwait(false);
            HttpContext context = new HttpContext(httpRequest, response);
            if (this.HttpCallBack != null)
            {
                this.HttpCallBack.OnAfterHttpResponseEventHandler(this.GetClientInstance(), response);
            }

            // handle errors defined at the API level.
            this.ValidateResponse(response, context);

            return double.Parse(response.Body);
        }
    }
}