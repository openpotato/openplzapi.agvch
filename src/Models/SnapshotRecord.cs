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

namespace OpenPlzApi.AGVCH
{
    /// <summary>
    /// A representation of a AGVCH snaphot record (Snapshots der Gemeinden)
    /// </summary>
    public class SnapshotRecord: AGVCHRecord
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SnapshotRecord"/> class.
        /// </summary>
        /// <param name="csvReader">CSV reader as data source</param>
        public SnapshotRecord(CsvTableReader csvReader)
            : base(csvReader)
        {
            HistoricalCode = csvReader.GetValue<string>("HistoricalCode");
            BfsCode = csvReader.GetValue<string>("BfsCode");
            Name = csvReader.GetValue<string>("Name");
            ValidFrom = csvReader.GetValue<DateOnly?>("ValidFrom");
            ValidTo = csvReader.GetValue<DateOnly?>("ValidTo");
            ShortName = csvReader.GetValue<string>("ShortName");
            Parent = csvReader.GetValue<string>("Parent");
            Level = (SnapshotLevel)csvReader.GetValue<int>("Level");
            Comments.AddIfNotEmpty("de", csvReader.GetValue<string>("Rec_Type_de"));
            Comments.AddIfNotEmpty("fr", csvReader.GetValue<string>("Rec_Type_fr"));
        }

        /// <summary>
        /// Bfs Code (Bfs-Nummer des Eintrags)
        /// </summary>
        public string BfsCode { get; internal set; }

        /// <summary>
        /// Comments in German and French language
        /// </summary>
        public IDictionary<string, string> Comments { get; internal set; } = new Dictionary<string, string>();

        /// <summary>
        /// Historical code (Historisierte Nummer des Eintrags)
        /// </summary>
        public string HistoricalCode { get; internal set; }

        /// <summary>
        /// Level (Gemeinde, Bezirk oder Kanton?)
        /// </summary>
        public SnapshotLevel Level { get; internal set; }

        /// <summary>
        /// Name (Name des Eintrags)
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        /// Bfs Code of the parent record
        /// </summary>
        public string Parent { get; internal set; }

        /// <summary>
        /// Short name (Kurzname des Eintrags)
        /// </summary>
        public string ShortName { get; internal set; }

        /// <summary>
        /// Valid from
        /// </summary>
        public DateOnly? ValidFrom { get; internal set; }

        /// <summary>
        /// Valid to
        /// </summary>
        public DateOnly? ValidTo { get; internal set; }
    }
}
