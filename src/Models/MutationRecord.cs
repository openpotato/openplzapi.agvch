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

namespace OpenPlzApi.AGVCH
{
    /// <summary>
    /// A representation of a AGVCH mutation record (Mutationen der Gemeinden)
    /// </summary>
    public class MutationRecord : AGVCHRecord
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MutationRecord"/> class.
        /// </summary>
        /// <param name="csvReader">CSV reader as data source</param>
        public MutationRecord(CsvTableReader csvReader)
            : base(csvReader)
        {
            MutationNumber = csvReader.GetValue<string>("MutationNumber");
            MutationDate = csvReader.GetValue<DateOnly>("MutationDate");
            InitialHistoricalCode = csvReader.GetValue<string>("InitialHistoricalCode");
            InitialCode = csvReader.GetValue<string>("InitialCode");
            InitialName = csvReader.GetValue<string>("InitialName");
            InitialParentHistoricalCode = csvReader.GetValue<string>("InitialParentHistoricalCode");
            InitialParentName = csvReader.GetValue<string>("InitialParentName");
            InitialStep = csvReader.GetValue<string>("InitialStep");
            TerminalHistoricalCode = csvReader.GetValue<string>("TerminalHistoricalCode");
            TerminalCode = csvReader.GetValue<string>("TerminalCode");
            TerminalName = csvReader.GetValue<string>("TerminalName");
            TerminalParentHistoricalCode = csvReader.GetValue<string>("TerminalParentHistoricalCode");
            TerminalParentName = csvReader.GetValue<string>("TerminalParentName");
            TerminalStep = csvReader.GetValue<string>("TerminalStep");
        }

        /// <summary>
        /// InitialCode
        /// </summary>
        public string InitialCode { get; internal set; }

        /// <summary>
        /// InitialHistoricalCode
        /// </summary>
        public string InitialHistoricalCode { get; internal set; }

        /// <summary>
        /// InitialName
        /// </summary>
        public string InitialName { get; internal set; }

        /// <summary>
        /// Bezeichnung (EF5)
        /// </summary>
        public string InitialParentHistoricalCode { get; internal set; }

        /// <summary>
        /// InitialParentName
        /// </summary>
        public string InitialParentName { get; internal set; }

        /// <summary>
        /// InitialStep
        /// </summary>
        public string InitialStep { get; internal set; }

        /// <summary>
        /// MutationDate
        /// </summary>
        public DateOnly MutationDate { get; internal set; }

        /// <summary>
        /// MutationNumber
        /// </summary>
        public string MutationNumber { get; internal set; }

        /// <summary>
        /// TerminalCode
        /// </summary>
        public string TerminalCode { get; internal set; }

        /// <summary>
        /// TerminalHistoricalCode
        /// </summary>
        public string TerminalHistoricalCode { get; internal set; }
        
        /// <summary>
        /// TerminalName
        /// </summary>
        public string TerminalName { get; internal set; }

        /// <summary>
        /// TerminalParentHistoricalCode
        /// </summary>
        public string TerminalParentHistoricalCode { get; internal set; }

        /// <summary>
        /// TerminalParentName
        /// </summary>
        public string TerminalParentName { get; internal set; }

        /// <summary>
        /// TerminalStep
        /// </summary>
        public string TerminalStep { get; internal set; }
    }
}
