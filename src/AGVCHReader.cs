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

using Enbrea.Csv;
using System;
using System.Collections.Generic;
using System.IO;

namespace OpenPlzApi.AGVCH
{
    /// <summary>
    /// A static reader class for CSV files from AGVCH. 
    /// </summary>
    public static class AGVCHReader
    {
        /// <summary>
        /// Iterates over the internal AGVCH CSV stream and gives back AGVCH records
        /// </summary>
        /// <returns>An enumerator of <see cref="AGVCHRecord"/> based instances</returns>
        public static IEnumerable<T> Read<T>(Stream stream) where T : AGVCHRecord
        {
            using var strReader = new StreamReader(stream);

            foreach (var record in Read<T>(strReader))
            {
                yield return record;
            };
        }

        /// <summary>
        /// Iterates over the internal AGVCH CSV stream and gives back AGVCH records
        /// </summary>
        /// <returns>An enumerator of <see cref="AGVCHRecord"/> based instances</returns>
        public static IEnumerable<T> Read<T>(TextReader textReader) where T : AGVCHRecord
        {
            var csvReader = new CsvTableReader(textReader, new CsvConfiguration { Separator = ',' });

            csvReader.SetFormats<DateOnly>("dd.MM.yyyy");
            csvReader.ReadHeaders();
            while (csvReader.Read() > 1)
            {
                yield return CreateRecord<T>(csvReader);
            };
        }

        /// <summary>
        /// Iterates over the AGVCH CSV stream and gives back AGVCH records
        /// </summary>
        /// <returns>An async enumerator of <see cref="AGVCHRecord"/> based instances</returns>
        public static async IAsyncEnumerable<T> ReadAsync<T>(Stream stream) where T: AGVCHRecord
        {
            using var strReader = new StreamReader(stream);

            await foreach (var record in ReadAsync<T>(strReader))
            {
                yield return record;
            };
        }

        /// <summary>
        /// Iterates over the internal AGVCH CSV stream and gives back AGVCH records
        /// </summary>
        /// <returns>An async enumerator of <see cref="AGVCHRecord"/> based instances</returns>
        public static async IAsyncEnumerable<T> ReadAsync<T>(TextReader textReader) where T : AGVCHRecord
        {
            var csvReader = new CsvTableReader(textReader, new CsvConfiguration { Separator = ',' });

            csvReader.SetFormats<DateOnly>("dd.MM.yyyy");
            await csvReader.ReadHeadersAsync();

            while (await csvReader.ReadAsync() > 1)
            {
                yield return CreateRecord<T>(csvReader);
            };
        }

        /// <summary>
        /// Creates a new <see cref="AGVCHRecord"/> based instance
        /// </summary>
        /// <typeparam name="T">The type of record</typeparam>
        /// <param name="csvReader">A CSV reader as data source</param>
        /// <returns>The new <see cref="AGVCHRecord"/> based instance</returns>
        public static T CreateRecord<T>(CsvTableReader csvReader) where T : AGVCHRecord
        {
            return (T)Activator.CreateInstance(typeof(T), [csvReader]);
        }
    }
}
