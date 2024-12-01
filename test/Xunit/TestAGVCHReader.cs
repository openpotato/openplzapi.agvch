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
using System.Threading.Tasks;
using Xunit;

namespace OpenPlzApi.AGVCH.Tests
{
    /// <summary>
    /// Unit tests for <see cref="AGVCHReader<T>"/>.
    /// </summary>
    public class TestAGVCHReader
    {
        [Fact]
        public async Task TestSnapshotRecord()
        {
            var csvContent =
                """
                HistoricalCode,BfsCode,ValidFrom,ValidTo,Level,Parent,Name,ShortName,Inscription,Radiation,Rec_Type_fr,Rec_Type_de
                10053,101,12.09.1848,,2,1,Bezirk Affoltern,Affoltern,100,,,
                """;

            using var strReader = new StringReader(csvContent);

            IAsyncEnumerator<SnapshotRecord> enumerator = AGVCHReader.ReadAsync<SnapshotRecord>(strReader).GetAsyncEnumerator();

            Assert.True(await enumerator.MoveNextAsync());
            Assert.Equal(new DateOnly(1848, 9, 12), (enumerator.Current).ValidFrom);
            Assert.Equal(SnapshotLevel.District, (enumerator.Current).Level);
            Assert.Equal("101", (enumerator.Current).BfsCode);
            Assert.Equal("10053", (enumerator.Current ).HistoricalCode);
            Assert.Equal("Bezirk Affoltern", (enumerator.Current).Name);
            Assert.Equal("Affoltern", (enumerator.Current).ShortName);
            Assert.Equal("1", (enumerator.Current).Parent);
            Assert.False(await enumerator.MoveNextAsync());
        }
    }
}
