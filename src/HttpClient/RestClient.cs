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

using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace OpenPlzApi.AGVCH
{
    /// <summary>
    /// The implementation of the <see cref="IRestClient"/> interface.
    /// </summary>
    /// <param name="httpClient">An <see cref="HttpClient"/> instance</param>
    public class RestClient(HttpClient httpClient) : IRestClient
    {
        private readonly HttpClient _httpClient = httpClient;

        /// <summary>
        /// Requests an API endpoint and returns back a response stream
        /// </summary>
        /// <param name="requestUrl">The request url</param>
        /// <param name="mediaType">The request media type</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The value of the TResult parameter 
        /// contains a stream.</returns>
        public async Task<Stream> GetStreamAsync(Uri requestUrl, string mediaType, CancellationToken cancellationToken)
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);

            request.Headers.Accept.Clear();
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));

            var response = await _httpClient.SendAsync(request, cancellationToken);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStreamAsync(cancellationToken);
        }
    }
}