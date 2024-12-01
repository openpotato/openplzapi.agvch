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
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace OpenPlzApi.AGVCH
{
    /// <summary>
    /// A typed HTTP client interface.
    /// </summary>
    public interface IRestClient
    {
        /// <summary>
        /// Request an API endpoint and return back the raw response stream
        /// </summary>
        /// <param name="requestUrl">The request url</param>
        /// <param name="mediaType">The request media type</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The value of the TResult parameter 
        /// contains the a stream.</returns>
        Task<Stream> GetStreamAsync(Uri requestUrl, string mediaType, CancellationToken cancellationToken);
    }
}
