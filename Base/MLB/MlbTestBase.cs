using AutomationFramework.Constants;
using AutomationFramework.DefaultRequests;
using AutomationFramework.Helpers.DatabaseHelper;
using AutomationFramework.Helpers.RedisHelper;
using AutomationFramework.Model.Database.MLB;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace AutomationFramework.Base.MLB
{
    internal class MlbTestBase : TestBase
    {
        private static async Task<IEnumerable<BatterDetailsModel>> GetBattersDetails(string fixtureKey, string sport, string teamName, string lineupName)
        {
            var query = DbQueries.MlbBattersDetail.Replace("teamname", "/teams/"+teamName.ToUpper() + "/")
                .Replace("fixturename", fixtureKey).Replace("lineupname", lineupName.ToUpper()).Replace("sportname", KeywordMapping.GetSportMapping(sport)); 

            return await DatabaseHelper.ReadAllData<BatterDetailsModel>(sport, query,true);
        }

        private static async Task<IEnumerable<PitcherDetailsModel>> GetPitcherDetails( string sport, string teamName)
        {
            var query = DbQueries.MlbPitchersDetail.Replace("teamname", "/teams/" + teamName.ToUpper() + "/")
                .Replace("sportname", KeywordMapping.GetSportMapping(sport)); 

            return await DatabaseHelper.ReadAllData<PitcherDetailsModel>(sport, query,true);
        }

        protected static async Task<string?> SetupPreMatchFixture(string sport, DateTime timestamp, int numberOfSims,
            string? fixtureKeyOverride = null)
        {
            var fixture = await GetFixture(sport, fixtureKeyOverride);
            if (fixture == null) return null;
            var fixtureKey = fixture.FixtureKey;
            await MlbPreSimSetup(fixtureKey, sport, fixture.GMTStartDateTime, timestamp);

            var unPublishedResponse = await UnPublishFixture(fixtureKey, sport);
            if (!ValidateGuid(unPublishedResponse)) return null;

            var publishLinesResponse = await PublishMarketLines(fixtureKey, sport);
            if (!ValidateGuid(publishLinesResponse)) return null;

            var unPublishLinesResponse = await UnPublishMarketLines(fixtureKey, sport);
            if (unPublishLinesResponse == null) return null;

            var publishResponse = await PublishFixture(fixtureKey, sport);
            if (!ValidateGuid(publishResponse)) return null;

            var simResponse = SimulateFixture(fixtureKey, sport, numberOfSims);
            if (simResponse == null) return null;

            var isSimValid = ValidateGuid(simResponse);
            if (!isSimValid) return null;

            await MlbPostSimSetup(fixtureKey, sport,
                numberOfSims.ToString(),
                simResponse.Replace("\"", ""), timestamp);
            return fixtureKey;
        }

        internal static async ValueTask<T?> SetupFixture_GetDiffusionTopicDetails<T>(int numberOfSim,string market,string? fixtureKey=null)
        {
            var timestamp = DateTime.Now;
            T? data = default;

            fixtureKey =(fixtureKey==null) ?await SetupPreMatchFixture("mlb", timestamp, numberOfSim) :
                await SetupPreMatchFixture("mlb", timestamp, numberOfSim, fixtureKey);

            if (fixtureKey != null)
            {
                data= await GetDiffusionData<T>("mlb", fixtureKey, market, timestamp);
            }

            return data;
        }

        private static async Task<string?> PublishMarketLines(string fixtureKey, string sport)
        {
            var request = WorkbookWebApiRequests.MlbFixturePublishLineRequest(fixtureKey);
            return await PublishedMarketLines(request, sport);
        }

        private static async Task<string?> UnPublishMarketLines(string fixtureKey, string sport)
        {
            var request = WorkbookWebApiRequests.MlbFixturePublishLineRequest(fixtureKey);
            return await UnpublishedMarketLines(request, sport);
        }

        private static async Task MlbPreSimSetup(string fixtureKey, string sport, DateTime fixtureDateTime, DateTime timestamp)
        {
            sport = sport.ToUpper();
            await RedisReader.DeleteKeyValues(fixtureKey);
            await DeleteFixtureData(sport, fixtureKey, "tblSimulationFixtureBatterRatings");
            await DeleteFixtureData(sport, fixtureKey, "tblSimulationFixturePitcherRatings");
            await DeleteFixtureData(sport, fixtureKey, "tblSimulationFixtureDefenseRatings");
            await DeleteFixtureData(sport, fixtureKey, "tblSimulationFixtureInformation");
            await DeleteFixtureData(sport, fixtureKey, "tblSimulationFixtureTotalAdjustments");
            await DeleteFixtureData(sport, fixtureKey, "tblSimulationFixtureSupremacyAdjustments");
            await PreSimFixtureBatterAndPitcherRatingsSetup(fixtureKey, sport, timestamp);
            await PreSimFixtureTotalAdjustmentsSetup(fixtureKey, sport, timestamp);
            await PreSimFixtureTotalSupremacyAdjustmentsSetup(fixtureKey, sport, timestamp);
            await PreSimFixtureInformationSetup(fixtureKey, sport, fixtureDateTime, timestamp);
            await PreSimFixtureDefenseRatingsSetup(fixtureKey, sport, timestamp);
            await PreSimSimulationStateChangesSetup(fixtureKey, sport, timestamp);
        }

        private static async Task MlbPostSimSetup(string fixtureKey, string sport, string numberOfSims, string requestId, DateTime timestamp)
        {
            sport = sport.ToUpper();
            await PostSimSimulationFixtureSimmerId(fixtureKey, sport, timestamp);
            await PostSimSimulationFixtureRequests(fixtureKey, sport, numberOfSims, requestId, timestamp);
        }

        private static async Task PostSimSimulationFixtureRequests(string fixtureKey, string sport, string numberOfSims, string requestId, DateTime timestamp)
        {
            var request =
                SimulationFixtureRequests.SimulationFixtureDefaultRequests(fixtureKey, numberOfSims, requestId, timestamp);
            await InsertSimulationFixtureRequests(sport, request);

        }

        private static async Task PostSimSimulationFixtureSimmerId(string fixtureKey, string sport, DateTime timestamp)
        {
            var request =
                SimulationFixtureSimmerIdRequests.MlbSimulationFixtureSimmerIdDefaultRequests(fixtureKey, timestamp);
            await InsertSimulationFixtureSimmerId(sport, request);

        }

        private static async Task PreSimFixtureInformationSetup(string fixtureKey, string sport, DateTime fixtureDateTime, DateTime timestamp)
        {
            var request = SimulationFixtureInformationRequest.GetMlbSimulationFixtureInformationRequest(fixtureKey, fixtureDateTime, timestamp);
            await InsertSimulationFixtureInformation(sport, request);
        }

        private static async Task PreSimFixtureDefenseRatingsSetup(string fixtureKey, string sport,
            DateTime timestamp)
        {
            var request =
                SimulationFixtureDefenseRatingsRequest.GetSimulationFixtureDefenseRatings_Team1Request(fixtureKey,
                    timestamp);
            await InsertSimulationFixtureDefenseRatings(sport, request);

            request =
                SimulationFixtureDefenseRatingsRequest.GetSimulationFixtureDefenseRatings_Team2Request(fixtureKey,
                    timestamp);
            await InsertSimulationFixtureDefenseRatings(sport, request);
        }

        private static async Task PreSimFixtureBatterAndPitcherRatingsSetup(string fixtureKey, string sport, DateTime timestamp)
        {
            var teams = fixtureKey.Split(" ").Select(s => s.Trim()).ToArray();
            var teamNames = new List<string>() { teams[0], teams[2] };
            var count = 0;
            await Parallel.ForEachAsync(teamNames, async (team, _) =>
            {
                var teamNo = teamNames.IndexOf(team)+1;

                var battersLhp = (await GetBattersDetails(fixtureKey, sport, team, "LHP")).DistinctBy(i => i.BattingPosition).ToList();
                if (battersLhp.Count<13)
                    Assert.Fail("Team " + team + " for the fixture " + fixtureKey + " does not have batter data available");
                var battersRhp = (await GetBattersDetails(fixtureKey, sport, team, "RHP")).DistinctBy(i => i.BattingPosition).ToList();
                if (battersRhp.Count < 13)
                    Assert.Fail("Team " + team + " for the fixture " + fixtureKey + " does not have batter data available");
                var pitchers = (await GetPitcherDetails(sport, team)).DistinctBy(i=>i.PlayerId).ToList();
                if (pitchers.Count < 9)
                    Assert.Fail("Team " + team + " for the fixture " + fixtureKey + " does not have pitcher data available");
                
                for (var i = 0; i < 13; i++)
                {
                    var player = battersLhp[i];
                    var request = SimulationFixtureBatterRatingsRequest.GetSimulationFixturePitcherRatingsRequest(
                        fixtureKey, timestamp, teamNo,
                        (i + 1 < 10) ? "B" : "PH",
                        i + 1, player.PlayerID, player.Bats);

                    await InsertSimulationFixtureBatterRatings(sport, request);
                    count++;
                }

                
                for(var i=0;i<9;i++) 
                {
                    var pitcher = pitchers[i];
                    var position = (i == 0) ? "SP" : "RP";
                    var request = SimulationFixturePitcherRatingsRequest.GetSimulationFixturePitcherRatingsRequest(
                        fixtureKey, timestamp, teamNo, position,
                        i+1, pitcher.PlayerId, pitcher.Throws);
                    await InsertSimulationFixturePitcherRatings(sport, request);                    
                }
            });
        }

        private static async Task PreSimSimulationStateChangesSetup(string fixtureKey, string sport, DateTime timestamp)
        {
            var request = SimulationStateChanges.SimulationStateChangesRequests(fixtureKey, timestamp);
            await InsertSimulationStateChanges(sport, request);
        }

        private static async Task PreSimFixtureTotalAdjustmentsSetup(string fixtureKey, string sport, DateTime timestamp)
        {
            var stadiumFixFixtureTotalAdjustment =
                SimulationFixtureTotalAdjustmentsRequest.Get_Mlb_SimulationFixtureTotalAdjustments_Stadium_Request(fixtureKey, timestamp);
            await InsertSimulationFixtureTotalAdjustments(sport, stadiumFixFixtureTotalAdjustment);

            var stadiumFixtureTotalAdjustment =
                SimulationFixtureTotalAdjustmentsRequest.Get_Mlb_SimulationFixtureTotalAdjustments_StadiumFix_Request(fixtureKey, timestamp);
            await InsertSimulationFixtureTotalAdjustments(sport, stadiumFixtureTotalAdjustment);

            var umpireFixtureTotalAdjustment =
                SimulationFixtureTotalAdjustmentsRequest.Get_Mlb_SimulationFixtureTotalAdjustments_Umpire_Request(fixtureKey, timestamp);
            await InsertSimulationFixtureTotalAdjustments(sport, umpireFixtureTotalAdjustment);

            var weatherFixtureTotalAdjustment =
                SimulationFixtureTotalAdjustmentsRequest.Get_Mlb_SimulationFixtureTotalAdjustments_Weather_Request(fixtureKey, timestamp);
            await InsertSimulationFixtureTotalAdjustments(sport, weatherFixtureTotalAdjustment);

            var manAdjustFixtureTotalAdjustment =
                SimulationFixtureTotalAdjustmentsRequest.Get_Mlb_SimulationFixtureTotalAdjustments_Request(fixtureKey, "ManAdjust", timestamp);
            await InsertSimulationFixtureTotalAdjustments(sport, manAdjustFixtureTotalAdjustment);

            var overrideFixtureTotalAdjustment =
                SimulationFixtureTotalAdjustmentsRequest.Get_Mlb_SimulationFixtureTotalAdjustments_Request(fixtureKey, "Override", timestamp);
            await InsertSimulationFixtureTotalAdjustments(sport, overrideFixtureTotalAdjustment);

            var globalAdjustFixtureTotalAdjustment =
                SimulationFixtureTotalAdjustmentsRequest.Get_Mlb_SimulationFixtureTotalAdjustments_GlobalChange_Request(fixtureKey, timestamp);
            await InsertSimulationFixtureTotalAdjustments(sport, globalAdjustFixtureTotalAdjustment);
        }

        private static async Task PreSimFixtureTotalSupremacyAdjustmentsSetup(string fixtureKey, string sport, DateTime timestamp)
        {
            var hfaFixtureTotalAdjustment =
                SimulationFixtureSupremacyAdjustmentsRequest.Get_Mlb_SimulationFixtureSupremacyAdjustments_Hfa_Request(fixtureKey, timestamp);
            await InsertSimulationFixtureTotalSupremacyAdjustments(sport, hfaFixtureTotalAdjustment);

            var umpireFixtureTotalAdjustment =
                SimulationFixtureSupremacyAdjustmentsRequest.Get_Mlb_SimulationFixtureSupremacyAdjustments_Request(fixtureKey, "Umpire", timestamp);
            await InsertSimulationFixtureTotalSupremacyAdjustments(sport, umpireFixtureTotalAdjustment);

            var restAdjustFixtureTotalAdjustment =
                SimulationFixtureSupremacyAdjustmentsRequest.Get_Mlb_SimulationFixtureSupremacyAdjustments_Request(fixtureKey, "Rest", timestamp);
            await InsertSimulationFixtureTotalSupremacyAdjustments(sport, restAdjustFixtureTotalAdjustment);

            var travelFixtureTotalAdjustment =
                SimulationFixtureSupremacyAdjustmentsRequest.Get_Mlb_SimulationFixtureSupremacyAdjustments_Request(fixtureKey, "Travel", timestamp);
            await InsertSimulationFixtureTotalSupremacyAdjustments(sport, travelFixtureTotalAdjustment);

            var motivationFixtureTotalAdjustment =
                SimulationFixtureSupremacyAdjustmentsRequest.Get_Mlb_SimulationFixtureSupremacyAdjustments_Request(fixtureKey, "Motivation", timestamp);
            await InsertSimulationFixtureTotalSupremacyAdjustments(sport, motivationFixtureTotalAdjustment);

            var competitionAdjustFixtureTotalAdjustment =
                SimulationFixtureSupremacyAdjustmentsRequest.Get_Mlb_SimulationFixtureSupremacyAdjustments_Request(fixtureKey, "Competition", timestamp);
            await InsertSimulationFixtureTotalSupremacyAdjustments(sport, competitionAdjustFixtureTotalAdjustment);

            var manAdjustFixtureTotalAdjustment =
                SimulationFixtureSupremacyAdjustmentsRequest.Get_Mlb_SimulationFixtureSupremacyAdjustments_ManAdjust_Request(hfaFixtureTotalAdjustment);
            await InsertSimulationFixtureTotalSupremacyAdjustments(sport, manAdjustFixtureTotalAdjustment);

            var overrideFixtureTotalAdjustment =
                SimulationFixtureSupremacyAdjustmentsRequest.Get_Mlb_SimulationFixtureSupremacyAdjustments_Request(fixtureKey, "Override", timestamp);
            await InsertSimulationFixtureTotalSupremacyAdjustments(sport, overrideFixtureTotalAdjustment);
        }
    }
}
