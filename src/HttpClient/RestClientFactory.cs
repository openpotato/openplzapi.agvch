#region OpenPlzApi.AGVCH - Copyright (c) STÜBER SYSTEMS GmbH
/*    
 *    OpenPlzApi.AGVCH 
 *    
 *    Copyright (c) STÜBER SYSTEMS GmbH
 *
 *    Licensed under the MIT License, Version 2.0. 
 * 
 */
#endregion

using Microsoft.Extensions.DependencyInjection;
using Polly;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace OpenPlzApi.AGVCH
{
    /// <summary>
    /// A factory for <see cref="IRestClient"/> implementations.
    /// </summary>
    public static class RestClientFactory
    {
        /// <summary>
        /// Creates a new instance of an <see cref="IRestClient"/> implementation.
        /// </summary>
        /// <returns>The new instance</returns>
        public static IRestClient CreateRestClient()
        {
            // Create dependency injection container
            var serviceCollection = new ServiceCollection();

            // Register HTTP Client
            serviceCollection
                .AddHttpClient<IRestClient, RestClient>(client =>
                {
                    client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue(AssemblyInfo.GetAgentName(), AssemblyInfo.GetVersion()));
                })
                .AddPolicyHandler(Policy<HttpResponseMessage>.Handle<HttpRequestException>()
                    .OrResult(msg => msg.StatusCode == HttpStatusCode.RequestTimeout)
                    .OrResult(msg => msg.StatusCode == HttpStatusCode.ServiceUnavailable)
                    .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))));

            // Create IRestHttpClient implementation
            var services = serviceCollection.BuildServiceProvider();

            // Return back IRestHttpClient implementation
            return services.GetRequiredService<IRestClient>();
        }
    }
}
