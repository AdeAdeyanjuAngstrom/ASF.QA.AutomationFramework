using AutomationFramework.Helpers.RedisHelper;
using AutomationFramework.Model.Database.NFL;
using AutomationFramework.DefaultRequests;
using AutomationFramework.Constants;
using AutomationFramework.Helpers.DatabaseHelper;

namespace AutomationFramework.Base.NFL
{
    public class NflTestBase : TestBase
    {
        private static async Task<IEnumerable<PlayerDetailsModel>> GetPlayerDetails(string sport, string teamName, string offenseOrDefense)
        {
            var position = offenseOrDefense.ToLower() == "offense" ? "'Coach','QB','RB','FB','WR','TE','OLINE','K'" : "'P' , 'DLINE', 'LB', 'DB', 'P'";

            var query = DbQueries.NflPlayerDetails.Replace("teamname", teamName.ToUpper())
                .Replace("positioncondition", position).Replace("sportname", sport.ToUpper());

            return await DatabaseHelper.ReadAllData<PlayerDetailsModel>(sport, query);
        }

        protected static async Task<string?> SetupPreMatchFixture(string sport, DateTime timestamp, int numberOfSims,
            string? fixtureKeyOverride = null)
        {
            var fixture = await GetFixture(sport, fixtureKeyOverride);
            if (fixture == null) return null;
            var fixtureKey = fixture.FixtureKey;
            await NflPreSimSetup(fixtureKey, sport, fixture.UkFixtureDateTime, timestamp);

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

            await NflPostSimSetup(fixtureKey, sport,
                numberOfSims.ToString(),
                simResponse.Replace("\"", ""), timestamp);
            return fixtureKey;
        }

        private static async Task<string?> PublishMarketLines(string fixtureKey, string sport)
        {
            var request = WorkbookWebApiRequests.NflFixturePublishLineRequest(fixtureKey);
            return await PublishedMarketLines(request, sport);
        }

        private static async Task<string?> UnPublishMarketLines(string fixtureKey, string sport)
        {
            var request = WorkbookWebApiRequests.NflFixturePublishLineRequest(fixtureKey);
            return await UnpublishedMarketLines(request, sport);
        }
        private static async Task NflPreSimSetup(string fixtureKey, string sport, DateTime fixtureDateTime, DateTime timestamp)
        {
            sport = sport.ToUpper();
            await RedisReader.DeleteKeyValues(fixtureKey);
            await DeleteFixtureData(sport, fixtureKey, "tblSimulationFixtureRatings");
            await DeleteFixtureData(sport, fixtureKey, "tblSimulationFixtureInformation");
            await DeleteFixtureData(sport, fixtureKey, "tblSimulationFixtureTotalAdjustments");
            await DeleteFixtureData(sport, fixtureKey, "tblSimulationFixtureSupremacyAdjustments");
            await NflPreSimFixtureTotalAdjustmentsSetup(fixtureKey, sport, timestamp);
            await NflPreSimFixtureTotalSupremacyAdjustmentsSetup(fixtureKey, sport, timestamp);
            await NflPreSimFixtureInformationSetup(fixtureKey, sport, fixtureDateTime, timestamp);
            await NflPreSimFixtureRatingSetup(fixtureKey, sport, timestamp);
        }

        private static async Task NflPostSimSetup(string fixtureKey, string sport, string numberOfSims, string requestId, DateTime timestamp)
        {
            sport = sport.ToUpper();
            await NflPostSimSimulationFixtureSimmerId(fixtureKey, sport, requestId);
            await NflPostSimSimulationFixtureRequests(fixtureKey, sport, numberOfSims, requestId, timestamp);
        }

        private static async Task NflPostSimSimulationFixtureRequests(string fixtureKey, string sport, string numberOfSims, string requestId, DateTime timestamp)
        {
            var request =
                SimulationFixtureRequests.SimulationFixtureDefaultRequests(fixtureKey, numberOfSims, requestId, timestamp);
            await InsertSimulationFixtureRequests(sport, request);

        }

        private static async Task NflPostSimSimulationFixtureSimmerId(string fixtureKey, string sport, string requestId)
        {
            var request =
                SimulationFixtureSimmerIdRequests.NflSimulationFixtureSimmerIdDefaultRequests(fixtureKey, requestId);
            await InsertSimulationFixtureSimmerId(sport, request);

        }

        private static async Task NflPreSimFixtureInformationSetup(string fixtureKey, string sport, DateTime fixtureDateTime, DateTime timestamp)
        {
            var request = SimulationFixtureInformationRequest.GetNflSimulationFixtureInformationRequest(fixtureKey, fixtureDateTime, timestamp);
            await InsertSimulationFixtureInformation(sport, request);
        }

        private static async Task NflPreSimFixtureRatingSetup(string fixtureKey, string sport, DateTime timestamp)
        {
            var teams = fixtureKey.Split(" ").Select(s => s.Trim()).ToArray();
            var teamNames = new List<string>() { teams[0], teams[2] };
            await Parallel.ForEachAsync(teamNames, async (team, _) =>
            {
                var playerDetailsTeamOffense = await GetPlayerDetails(sport, team, "offense");

                foreach (var player in playerDetailsTeamOffense)
                {
                    SimulationFixtureRatingsModel request;
                    if (player.Position == "Coach")
                        request = SimulationFixtureRatingsRequests.GetSimulationFixtureRatingCoachRequest(fixtureKey,
                            player.PlayerId.ToString(), player.FirstName + " " + player.Surname, player.Position, team,
                            player.StatType, player.DOB, player.Starter, player.OnField, timestamp);
                    else
                        request = SimulationFixtureRatingsRequests.GetSimulationFixtureRatingPlayerRequest(fixtureKey,
                            player.PlayerId.ToString(), player.FirstName + " " + player.Surname, player.Position, team,
                            player.StatType, player.DOB, player.Starter, player.OnField, timestamp);

                    await InsertSimulationFixtureRatings(sport, request);
                }

                var playerDetailsTeamDefense = await GetPlayerDetails(sport, team, "defense");
                foreach (var player in playerDetailsTeamDefense)
                {
                    SimulationFixtureRatingsModel request;
                    if (player.Position == "Coach")
                        request = SimulationFixtureRatingsRequests.GetSimulationFixtureRatingCoachRequest(fixtureKey,
                            player.PlayerId.ToString(), player.FirstName + " " + player.Surname, player.Position, team,
                            player.StatType, player.DOB, player.Starter, player.OnField, timestamp);
                    else
                        request = SimulationFixtureRatingsRequests.GetSimulationFixtureRatingPlayerRequest(fixtureKey,
                            player.PlayerId.ToString(), player.FirstName + " " + player.Surname, player.Position, team,
                            player.StatType, player.DOB, player.Starter, player.OnField, timestamp);

                    await InsertSimulationFixtureRatings(sport, request);
                }
            });
        }

        private static async Task NflPreSimFixtureTotalAdjustmentsSetup(string fixtureKey, string sport, DateTime timestamp)
        {
            var fixtureFudgeFixtureTotalAdjustment =
                SimulationFixtureTotalAdjustmentsRequest.GetNflSimulationFixtureTotalAdjustmentsRequest(fixtureKey, "FixtureFudge", timestamp);
            await InsertSimulationFixtureTotalAdjustments(sport, fixtureFudgeFixtureTotalAdjustment);

            var stadiumFixtureTotalAdjustment =
                SimulationFixtureTotalAdjustmentsRequest.GetNflSimulationFixtureTotalAdjustmentsRequest(fixtureKey, "Stadium", timestamp);
            await InsertSimulationFixtureTotalAdjustments(sport, stadiumFixtureTotalAdjustment);

            var umpireFixtureTotalAdjustment =
                SimulationFixtureTotalAdjustmentsRequest.GetNflSimulationFixtureTotalAdjustmentsUmpireRequest(fixtureKey, "Umpire", timestamp);
            await InsertSimulationFixtureTotalAdjustments(sport, umpireFixtureTotalAdjustment);

            var weatherFixtureTotalAdjustment =
                SimulationFixtureTotalAdjustmentsRequest.GetNflSimulationFixtureTotalAdjustmentsRequest(fixtureKey, "Weather", timestamp);
            await InsertSimulationFixtureTotalAdjustments(sport, weatherFixtureTotalAdjustment);

            var fixtureAdjustFixtureTotalAdjustment =
                SimulationFixtureTotalAdjustmentsRequest.GetNflSimulationFixtureTotalAdjustmentsRequest(fixtureKey, "FixtureAdjust", timestamp);
            await InsertSimulationFixtureTotalAdjustments(sport, fixtureAdjustFixtureTotalAdjustment);

            var traderFixtureAdjustFixtureTotalAdjustment =
                SimulationFixtureTotalAdjustmentsRequest.GetNflSimulationFixtureTotalAdjustmentsRequest(fixtureKey, "TraderFixtureAdjust", timestamp);
            traderFixtureAdjustFixtureTotalAdjustment.qbPace = (decimal)-13.25;
            await InsertSimulationFixtureTotalAdjustments(sport, traderFixtureAdjustFixtureTotalAdjustment);

            var globalAdjustFixtureTotalAdjustment =
                SimulationFixtureTotalAdjustmentsRequest.GetNflSimulationFixtureTotalAdjustmentsGlobalAdjustRequest(fixtureKey, "GlobalAdjust", timestamp);
            await InsertSimulationFixtureTotalAdjustments(sport, globalAdjustFixtureTotalAdjustment);
        }

        private static async Task NflPreSimFixtureTotalSupremacyAdjustmentsSetup(string fixtureKey, string sport, DateTime timestamp)
        {
            var hfaFixtureTotalAdjustment =
                SimulationFixtureSupremacyAdjustmentsRequest.GetSimulationFixtureSupremacyAdjustmentsHfaRequest(fixtureKey, "HFA", timestamp);
            await InsertSimulationFixtureTotalSupremacyAdjustments(sport, hfaFixtureTotalAdjustment);

            var hfaAdjustFixtureTotalAdjustment =
                SimulationFixtureSupremacyAdjustmentsRequest.GetSimulationFixtureSupremacyAdjustmentsRequest(fixtureKey, "HFAAdjust", timestamp);
            await InsertSimulationFixtureTotalSupremacyAdjustments(sport, hfaAdjustFixtureTotalAdjustment);

            var fixtureFudgeFixtureTotalAdjustment =
                SimulationFixtureSupremacyAdjustmentsRequest.GetSimulationFixtureSupremacyAdjustmentsRequest(fixtureKey, "FixtureFudge", timestamp);
            await InsertSimulationFixtureTotalSupremacyAdjustments(sport, fixtureFudgeFixtureTotalAdjustment);

            var umpireFixtureTotalAdjustment =
                SimulationFixtureSupremacyAdjustmentsRequest.GetSimulationFixtureSupremacyAdjustmentsRequest(fixtureKey, "Umpire", timestamp);
            await InsertSimulationFixtureTotalSupremacyAdjustments(sport, umpireFixtureTotalAdjustment);

            var restAdjustFixtureTotalAdjustment =
                SimulationFixtureSupremacyAdjustmentsRequest.GetSimulationFixtureSupremacyAdjustmentsRequest(fixtureKey, "Rest", timestamp);
            await InsertSimulationFixtureTotalSupremacyAdjustments(sport, restAdjustFixtureTotalAdjustment);

            var travelFixtureTotalAdjustment =
                SimulationFixtureSupremacyAdjustmentsRequest.GetSimulationFixtureSupremacyAdjustmentsRequest(fixtureKey, "Travel", timestamp);
            await InsertSimulationFixtureTotalSupremacyAdjustments(sport, travelFixtureTotalAdjustment);

            var motivationFixtureTotalAdjustment =
                SimulationFixtureSupremacyAdjustmentsRequest.GetSimulationFixtureSupremacyAdjustmentsRequest(fixtureKey, "Motivation", timestamp);
            await InsertSimulationFixtureTotalSupremacyAdjustments(sport, motivationFixtureTotalAdjustment);

            var competitionAdjustFixtureTotalAdjustment =
                SimulationFixtureSupremacyAdjustmentsRequest.GetSimulationFixtureSupremacyAdjustmentsRequest(fixtureKey, "Competition", timestamp);
            await InsertSimulationFixtureTotalSupremacyAdjustments(sport, competitionAdjustFixtureTotalAdjustment);

            var fixtureAdjustAdjustFixtureTotalAdjustment =
                SimulationFixtureSupremacyAdjustmentsRequest.GetSimulationFixtureSupremacyAdjustmentsRequest(fixtureKey, "FixtureAdjust", timestamp);
            await InsertSimulationFixtureTotalSupremacyAdjustments(sport, fixtureAdjustAdjustFixtureTotalAdjustment);

            var traderFixtureAdjustFixtureTotalAdjustment =
                SimulationFixtureSupremacyAdjustmentsRequest.GetSimulationFixtureSupremacyAdjustmentsRequest(fixtureKey, "TraderFixtureAdjust", timestamp);
            traderFixtureAdjustFixtureTotalAdjustment.qbPace = "13.25";
            await InsertSimulationFixtureTotalSupremacyAdjustments(sport, traderFixtureAdjustFixtureTotalAdjustment);

            var globalAdjustFixtureTotalAdjustment =
                SimulationFixtureSupremacyAdjustmentsRequest.GetSimulationFixtureSupremacyAdjustmentsGlobalAdjustRequest(fixtureKey, "GlobalAdjust", timestamp);
            await InsertSimulationFixtureTotalSupremacyAdjustments(sport, globalAdjustFixtureTotalAdjustment);
        }
    }
}
