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

using System.Net.Http;

namespace OpenPlzApi.AGVCH
{
    /// <summary>
    /// An API client factory for <see cref="AGVCHApiClient" />
    /// </summary>
    public static class AGVCHApiClientFactory
    {
        /// <summary>
        /// The offical base url of the AGVCH API
        /// </summary>
        public const string AGVCHApiBaseUrl = "https://www.agvchapp.bfs.admin.ch/api/";

        /// <summary>
        /// Creates and returns a new instance of <see cref="AGVCHApiClient" />
        /// </summary>
        /// <param name="baseUrl">The base url of the AGVCH API</param>
        /// <returns>Returns a new <see cref="AGVCHApiClient" /> derived API client</returns>
        public static AGVCHApiClient CreateClient(string baseUrl = AGVCHApiBaseUrl)
        {
            return new AGVCHApiClient(baseUrl);
        }

        /// <summary>
        /// Creates and returns a new instance of <see cref="AGVCHApiClient" />
        /// </summary>
        /// <param name="httpClient">A <see cref="HttpClient"/> instance</param>
        /// <param name="baseUrl">The base url of the AGVCH API</param>
        /// <returns>Returns a new <see cref="AGVCHApiClient" /> derived API client</returns>
        public static AGVCHApiClient CreateClient(HttpClient httpClient, string baseUrl = AGVCHApiBaseUrl)
        {
            return new AGVCHApiClient(httpClient, baseUrl);
        }
    }
}
