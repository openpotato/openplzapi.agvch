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
using System.Linq;

namespace OpenPlzApi.AGVCH
{
    /// <summary>
    /// A representation of a AGVCH level record (Raumgliederungen der Gemeinden)
    /// </summary>
    public class LevelRecord : AGVCHRecord
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LevelRecord"/> class.
        /// </summary>
        /// <param name="csvReader">CSV reader as data source</param>
        public LevelRecord(CsvTableReader csvReader)
            : base(csvReader)
        {
            HistoricalCode = csvReader.GetValue<string>("HistoricalCode");
            BfsCode = csvReader.GetValue<string>("BfsCode");
            Name = csvReader.GetValue<string>("Name");
            District = csvReader.GetValue<string>("District");
            DistrictId = csvReader.GetValue<string>("DistrictId");
            Canton = csvReader.GetValue<string>("Canton");
            CantonId = csvReader.GetValue<string>("CantonId");
            foreach (var header in csvReader.Headers)
            {
                if (header.All(char.IsUpper))
                {
                    Levels.AddIfNotEmpty(header, csvReader.GetValue<string>(header));
                }
            }
        }

        /// <summary>
        /// Code (Gemeindenummer)
        /// </summary>
        public string BfsCode { get; internal set; }

        /// <summary>
        /// Name of canton (Kantonsname)
        /// </summary>
        public string Canton { get; internal set; }

        /// <summary>
        /// Id of canton
        /// </summary>
        public string CantonId { get; internal set; }

        /// <summary>
        /// Name of district (Bezirksname)
        /// </summary>
        public string District { get; internal set; }

        /// <summary>
        /// Id of district
        /// </summary>
        public string DistrictId { get; internal set; }

        /// <summary>
        /// Historical code (Historisierte Nummer der Gemeinde)
        /// </summary>
        public string HistoricalCode { get; internal set; }

        /// <summary>
        /// Dictionary of level information
        /// </summary>
        public IDictionary<string, string> Levels { get; internal set; } = new Dictionary<string, string>();
        
        /// <summary>
        /// Name (Name der Gemeinde)
        /// </summary>
        public string Name { get; internal set; }
    }
}
