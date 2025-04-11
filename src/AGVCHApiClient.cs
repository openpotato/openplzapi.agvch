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
using System.Net.Http;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace OpenPlzApi.AGVCH
{
    /// <summary>
    /// API client for the REST services of the application of the Swiss communes (AGVCH)
    /// </summary>
    public class AGVCHApiClient
    {
        private readonly Uri _baseUrl;
        private readonly IRestClient _restClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="AGVCHApiClient"/> class.
        /// </summary>
        /// <param name="httpClient">A <see cref="HttpClient"/> instance</param>
        /// <param name="baseUrl">The base url of the OpenPLZ API</param>
        public AGVCHApiClient(HttpClient httpClient, string baseUrl)
        {
            _restClient = new RestClient(httpClient);
            _baseUrl = new Uri(baseUrl);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IRestClient"/> class.
        /// </summary>
        /// <param name="restClient">An implementation of <see cref="IRestClient"/></param>
        /// <param name="baseUrl">The base url of the OpenPLZ API</param>
        public AGVCHApiClient(IRestClient restClient, string baseUrl)
        {
            _restClient = restClient;
            _baseUrl = new Uri(baseUrl);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AGVCHApiClient"/> class.
        /// </summary>
        /// <param name="baseUrl">The base url of the OpenPLZ API</param>
        public AGVCHApiClient(string baseUrl)
            : this(RestClientFactory.CreateRestClient(), baseUrl)
        {
        }

        /// <summary>
        /// Downloads the raw CSV file with the list of communes with different classifications
        /// to a local file.
        /// </summary>
        /// <param name="targetFile">The local file</param>
        /// <param name="referenceDate">The reference date</param>
        /// <param name="labelLanguages">Languages of the labels as comma-separated ISO 639-1 codes</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task DownloadLevelsDocumentAsync(
            FileInfo targetFile,
            DateOnly referenceDate,
            string labelLanguages = null,
            CancellationToken cancellationToken = default)
        {
            using var file = await GetLevelsDocumentAsync(referenceDate, labelLanguages, cancellationToken).ConfigureAwait(false);
            using var fileStream = new FileStream(targetFile.FullName, FileMode.Create, FileAccess.ReadWrite, FileShare.None);
            await file.CopyToAsync(fileStream, cancellationToken);
        }

        /// <summary>
        /// Downloads the raw CSV file with the list of all community-related changes that have taken place during a specified period
        /// to a local file.
        /// </summary>
        /// <param name="targetFile">The local file</param>
        /// <param name="startPeriod">Start of the period</param>
        /// <param name="endPeriod">End of the period</param>
        /// <param name="includeTerritoryExchange">Inclusion of data records relating only to territorial changes</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task DownloadMutationsDocumentAsync(
            FileInfo targetFile,
            DateOnly startPeriod,
            DateOnly endPeriod,
            bool includeTerritoryExchange = false,
            CancellationToken cancellationToken = default)
        {
            using var file = await GetMutationsDocumentAsync(startPeriod, endPeriod, includeTerritoryExchange, cancellationToken).ConfigureAwait(false);
            using var fileStream = new FileStream(targetFile.FullName, FileMode.Create, FileAccess.ReadWrite, FileShare.None);
            await file.CopyToAsync(fileStream, cancellationToken);
        }

        /// <summary>
        /// Downloads the raw CSV file with the list of the communes that exist on a specified day to
        /// a local file.
        /// </summary>
        /// <param name="targetFile">The local file</param>
        /// <param name="referenceDate">The reference date</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task DownloadSnapshotDocumentAsync(
            FileInfo targetFile,
            DateOnly referenceDate,
            CancellationToken cancellationToken = default)
        {
            using var file = await GetSnapshotDocumentAsync(referenceDate, cancellationToken).ConfigureAwait(false);
            using var fileStream = new FileStream(targetFile.FullName, FileMode.Create, FileAccess.ReadWrite, FileShare.None);
            await file.CopyToAsync(fileStream, cancellationToken);
        }

        /// <summary>
        /// Returns a list of communes with different classifications
        /// </summary>
        /// <param name="referenceDate">The reference date</param>
        /// <param name="labelLanguages">Languages of the labels as comma-separated ISO 639-1 codes</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The value of the TResult parameter 
        /// contains <see cref="LevelRecord"/> instance.</returns>
        public async IAsyncEnumerable<LevelRecord> GetLevelsAsync(
            DateOnly referenceDate,
            string labelLanguages = null,
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            using var stream =
                await GetRestClient().GetStreamAsync(
                    CreateUriBuilder()
                        .WithRelativePath("communes/levels")
                        .WithParameter("date", referenceDate.ToString("dd-MM-yyyy"))
                        .WithParameter("labelLanguages", labelLanguages)
                        .Uri,
                    MediaTypeNames.Text.Csv,
                    cancellationToken);

            await foreach (var record in AGVCHReader.ReadAsync<LevelRecord>(stream))
            {
                yield return record;
            }
        }

        /// <summary>
        /// Downloads the raw CSV file with the list of communes with different classifications
        /// </summary>
        /// <param name="referenceDate">The reference date</param>
        /// <param name="labelLanguages">Languages of the labels as comma-separated ISO 639-1 codes</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The value of the TResult parameter 
        /// contains a <see cref="Stream"/> instance.</returns>
        public async Task<Stream> GetLevelsDocumentAsync(
            DateOnly referenceDate,
            string labelLanguages = null,
            CancellationToken cancellationToken = default)
        {
            return await GetRestClient().GetStreamAsync(
                CreateUriBuilder()
                    .WithRelativePath("communes/levels")
                    .WithParameter("date", referenceDate.ToString("dd-MM-yyyy"))
                    .WithParameter("labelLanguages", labelLanguages)
                    .Uri,
                MediaTypeNames.Text.Csv,
                cancellationToken);
        }

        /// <summary>
        /// Returns a list of all community-related changes that have taken place during a specified period.
        /// </summary>
        /// <param name="startPeriod">Start of the period</param>
        /// <param name="endPeriod">End of the period</param>
        /// <param name="includeTerritoryExchange">Inclusion of data records relating only to territorial changes</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The value of the TResult parameter 
        /// contains <see cref="MutationRecord"/> instance.</returns>
        public async IAsyncEnumerable<MutationRecord> GetMutationsAsync(
            DateOnly startPeriod,
            DateOnly endPeriod,
            bool includeTerritoryExchange = false,
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            using var stream =
                await GetRestClient().GetStreamAsync(
                    CreateUriBuilder()
                        .WithRelativePath("communes/mutations")
                        .WithParameter("startPeriod", startPeriod.ToString("dd-MM-yyyy"))
                        .WithParameter("endPeriod", endPeriod.ToString("dd-MM-yyyy"))
                        .WithParameter("includeTerritoryExchange", includeTerritoryExchange)
                        .Uri,
                    MediaTypeNames.Text.Csv,
                    cancellationToken);

            await foreach (var record in AGVCHReader.ReadAsync<MutationRecord>(stream))
            {
                yield return record;
            }
        }

        /// <summary>
        /// Downloads the raw CSV file with the list of all commune-related changes that have taken place during a specified period.
        /// </summary>
        /// <param name="startPeriod">Start of the period</param>
        /// <param name="endPeriod">End of the period</param>
        /// <param name="includeTerritoryExchange">Inclusion of data records relating only to territorial changes</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The value of the TResult parameter 
        /// contains a <see cref="Stream"/> instance.</returns>
        public async Task<Stream> GetMutationsDocumentAsync(
            DateOnly startPeriod,
            DateOnly endPeriod,
            bool includeTerritoryExchange = false,
            CancellationToken cancellationToken = default)
        {
            return await GetRestClient().GetStreamAsync(
                CreateUriBuilder()
                    .WithRelativePath("communes/mutations")
                    .WithParameter("startPeriod", startPeriod.ToString("dd-MM-yyyy"))
                    .WithParameter("endPeriod", endPeriod.ToString("dd-MM-yyyy"))
                    .WithParameter("includeTerritoryExchange", includeTerritoryExchange)
                    .Uri,
                MediaTypeNames.Text.Csv,
                cancellationToken);
        }

        /// <summary>
        /// Returns a list of the communes that exist on a specified day.
        /// </summary>
        /// <param name="referenceDate">The reference date</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The value of the TResult parameter 
        /// contains <see cref="SnapshotRecord"/> instance.</returns>
        public async IAsyncEnumerable<SnapshotRecord> GetSnapshotAsync(
            DateOnly referenceDate,
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            using var stream =
                await GetRestClient().GetStreamAsync(
                    CreateUriBuilder()
                        .WithRelativePath("communes/snapshot")
                        .WithParameter("date", referenceDate.ToString("dd-MM-yyyy"))
                        .Uri,
                    MediaTypeNames.Text.Csv,
                    cancellationToken);

            await foreach (var record in AGVCHReader.ReadAsync<SnapshotRecord>(stream))
            {
                yield return record;
            }
        }

        /// <summary>
        /// Downloads the raw CSV file with the list of the communes that exist on a specified day.
        /// </summary>
        /// <param name="referenceDate">The reference date</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The value of the TResult parameter 
        /// contains a <see cref="Stream"/> instance.</returns>
        public async Task<Stream> GetSnapshotDocumentAsync(
            DateOnly referenceDate,
            CancellationToken cancellationToken = default)
        {
            return await GetRestClient().GetStreamAsync(
                CreateUriBuilder()
                    .WithRelativePath("communes/snapshot")
                    .WithParameter("date", referenceDate.ToString("dd-MM-yyyy"))
                    .Uri,
                MediaTypeNames.Text.Csv,
                cancellationToken);
        }
        
        /// <summary>
        /// Creates an uri builder with the internal base url as starting point
        /// </summary>
        /// <returns>A new <see cref="UriBuilder"/> instance</returns>
        private UriBuilder CreateUriBuilder()
        {
            return new UriBuilder(_baseUrl);
        }

        /// <summary>
        /// Gives back the interanal instance of the rest client
        /// </summary>
        /// <returns>A new <see cref="IRestClient"/> implmentation</returns>
        private IRestClient GetRestClient()
        {
            return _restClient;
        }
    }
}
