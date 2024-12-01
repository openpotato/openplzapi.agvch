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

namespace OpenPlzApi.AGVCH
{
    /// <summary>
    /// An abstract representation of a AGVCH record
    /// </summary>
    public abstract class AGVCHRecord
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AGVCHRecord"/> class.
        /// </summary>
        /// <param name="csvReader">CSV reader as data source</param>
        public AGVCHRecord(CsvTableReader csvReader)
        {
        }
    }
}
