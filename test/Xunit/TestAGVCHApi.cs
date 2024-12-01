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
using System.Threading.Tasks;
using Xunit;

namespace OpenPlzApi.AGVCH.Tests
{
    /// <summary>
    /// Unit tests for <see cref="AGVCHApiClient"/>.
    /// </summary>
    public class TestAGVCHApi
    {
        [Fact]
        public async Task TestLevelsApi()
        {
            var client = AGVCHApiClientFactory.CreateClient();

            var records = client.GetLevelsAsync(new DateOnly(2024, 11, 1));

            var recordCount = 0;
            var existsBern = false;

            await foreach (var record in records)
            {
                if (record.HistoricalCode == "15029") existsBern = true;
                recordCount++;
            }

            Assert.Equal(2131, recordCount);
            Assert.True(existsBern);
        }

        [Fact]
        public async Task TestMutationsApi()
        {
            var client = AGVCHApiClientFactory.CreateClient();

            var records = client.GetMutationsAsync(new DateOnly(2023, 1, 1), new DateOnly(2024, 1, 1), false);

            var recordCount = 0;
            var existsDiemerswil = false;

            await foreach (var record in records)
            {
                if (record.InitialHistoricalCode == "15116") existsDiemerswil = true;
                recordCount++;
            }

            Assert.Equal(28, recordCount);
            Assert.True(existsDiemerswil);
        }

        [Fact]
        public async Task TestSnapshotApi()
        {
            var client = AGVCHApiClientFactory.CreateClient();

            var records = client.GetSnapshotAsync(new DateOnly(2024, 11, 1));

            var existsBern = false;

            await foreach (var record in records)
            {
                if (record.ShortName == "BE") existsBern = true;
            }

            Assert.True(existsBern);
        }
    }
}
