using System.Collections.Concurrent;
using AutomationFramework.Constants;
using AutomationFramework.Helpers.ApiHelper;
using AutomationFramework.Helpers.DatabaseHelper;
using AutomationFramework.Helpers.DiffusionHelper;
using Newtonsoft.Json;
using AutomationFramework.Model.WorkbookWebApi;
using AutomationFramework.Model.Database.NFL;
using AutomationFramework.DefaultRequests;
using AutomationFramework.Model.Database.MLB;

namespace AutomationFramework.Base
{
    public class TestBase
    {
        private static readonly ConcurrentDictionary<string, List<string>> SelectedFixtures = new();
        private static readonly object LockObject = new();

        #region DB Methods

        private static async Task<IEnumerable<PendingFixturesModel>> GetPreMatchFixture(string sport)
        {
            return await DatabaseHelper.ReadAllData<PendingFixturesModel>(sport, "tblPendingFixtures");
        }

        internal static async Task DeleteFixtureData(string sport, string fixtureKey, string tableName)
        {
            var query = tableName + " where FixtureKey = " + "'" + fixtureKey + "'";
            await DatabaseHelper.DeleteData(sport, query);
        }

        internal static async Task InsertSimulationFixtureTotalAdjustments(string sport, NflSimulationFixtureTotalAdjustmentsModel dataObject)
        {
            await DatabaseHelper.InsertData(sport, "tblSimulationFixtureTotalAdjustments", dataObject);
        }

        internal static async Task InsertSimulationFixtureRatings(string sport, SimulationFixtureRatingsModel dataObject)
        {
            await DatabaseHelper.InsertData(sport, "tblSimulationFixtureRatings", dataObject);
        }

        internal static async Task InsertSimulationFixtureRequests(string sport, SimulationFixtureRequestsModel dataObject)
        {
            await DatabaseHelper.InsertData(sport, "tblSimulationFixtureRequests", dataObject);
        }

        internal static async Task InsertSimulationFixtureSimmerId(string sport, NflSimulationFixtureSimmerIdModel dataObject)
        {
            await DatabaseHelper.InsertData(sport, "tblSimulationFixtureSimmerID", dataObject);
        }

        internal static async Task InsertSimulationFixtureInformation(string sport, NflSimulationFixtureInformationModel dataObject)
        {
            await DatabaseHelper.InsertData(sport, "tblSimulationFixtureInformation", dataObject);
        }

        internal static async Task InsertSimulationFixtureTotalSupremacyAdjustments(string sport, MlbSimulationFixtureSupremacyAdjustmentsModel dataObject)
        {
            await DatabaseHelper.InsertData(sport, "tblSimulationFixtureSupremacyAdjustments", dataObject);
        }

        internal static async Task InsertSimulationFixtureTotalSupremacyAdjustments(string sport, NflSimulationFixtureSupremacyAdjustmentsModel dataObject)
        {
            await DatabaseHelper.InsertData(sport, "tblSimulationFixtureSupremacyAdjustments", dataObject);
        }

        internal static async Task InsertSimulationFixtureTotalAdjustments(string sport, MlbSimulationFixtureTotalAdjustmentsModel dataObject)
        {
            await DatabaseHelper.InsertData(sport, "tblSimulationFixtureTotalAdjustments", dataObject);
        }

        internal static async Task InsertSimulationStateChanges(string sport, MlbSimulationStateChangesModel dataObject)
        {
            await DatabaseHelper.InsertData(sport, "tblSimulationStateChanges", dataObject, "BaseballPublishing");
        }

        internal static async Task InsertSimulationFixtureBatterRatings(string sport, SimulationFixtureBatterRatingsModel dataObject)
        {
            await DatabaseHelper.InsertData(sport, "tblSimulationFixtureBatterRatings", dataObject);
        }

        internal static async Task InsertSimulationFixtureDefenseRatings(string sport, SimulationFixtureDefenseRatingsModel dataObject)
        {
            await DatabaseHelper.InsertData(sport, "tblSimulationFixtureDefenseRatings", dataObject);
        }

        internal static async Task InsertSimulationFixturePitcherRatings(string sport, SimulationFixturePitcherRatingsModel dataObject)
        {
            await DatabaseHelper.InsertData(sport, "tblSimulationFixturePitcherRatings", dataObject);
        }

        internal static async Task InsertSimulationFixtureSimmerId(string sport, MlbSimulationFixtureSimmerIdModel dataObject)
        {
            await DatabaseHelper.InsertData(sport, "tblSimulationFixtureSimmerID", dataObject);
        }

        internal static async Task InsertSimulationFixtureInformation(string sport, MlbSimulationFixtureInformationModel dataObject)
        {
            await DatabaseHelper.InsertData(sport, "tblSimulationFixtureInformation", dataObject);
        }

        #endregion

        #region API Methods

        internal static async Task<string?> UnpublishedMarketLines(object request, string sport)
        {
            return await RestApiClient.SendPostRequestAsync(
                KeywordMapping.GetWebApiUrl(sport) + "/api/publishing-manager/" + sport + "-unpublishedmarketlines-update",
                request);
        }

        internal static async Task<string?> PublishedMarketLines(object request, string sport)
        {
            return await RestApiClient.SendPostRequestAsync(
                KeywordMapping.GetWebApiUrl(sport) + "/api/publishing-manager/" + sport + "-publishedmarketlines-update",
                request);
        }

        private static async Task<string?> Published(FixturePublishRequestModel request, string sport)
        {
            return await RestApiClient.SendPostRequestAsync(
                KeywordMapping.GetWebApiUrl(sport) + "/api/publishing-manager/" + sport + "-publishing-update", request);
        }

        private static async Task<string?> Simulate(UserSimRequestModel request, string sport)
        {
            return await RestApiClient.SendPostRequestAsync(
                KeywordMapping.GetWebApiUrl(sport) + "/api/sim-request-manager/" + sport + "-request-user-sim", request);
        }

        private static async Task<string?> GetManualSuspension(MarketSuspensionRequestModel request, string sport)
        {
            return await RestApiClient.SendPostRequestAsync(
                KeywordMapping.GetWebApiUrl(sport) + "/api/manual-suspension-manager/" + sport +
                "-get-market-suspension-overrides", request);
        }

        private static async Task<string?> SetManualSuspension(MarketSuspensionRequestModel request, string sport)
        {
            return await RestApiClient.SendPostRequestAsync(
                KeywordMapping.GetWebApiUrl(sport) + "/api/manual-suspension-manager/" + sport +
                "-set-market-suspension-overrides", request);
        }

        #endregion


        internal static async Task<PendingFixturesModel?> GetFixture(string sport, string? fixtureKey)
        {
            PendingFixturesModel? fixture;
            do
            {
                var preMatchFixtures = (await GetPreMatchFixture(sport)).ToList();
                var randomIndex = new Random().Next(preMatchFixtures.Count);
                fixture = (fixtureKey == null)
                    ? preMatchFixtures[randomIndex]
                    : preMatchFixtures.Find(i =>
                        string.Equals(i.FixtureKey, fixtureKey, StringComparison.CurrentCultureIgnoreCase));
            } while (SelectedFixtures.ContainsKey(sport) && SelectedFixtures[sport].Contains(fixture!.FixtureKey));

            if (!SelectedFixtures.ContainsKey(sport))
                SelectedFixtures.TryAdd(sport, new List<string>());
            SelectedFixtures[sport].Add(fixture!.FixtureKey);
            return fixture;
        }

        internal static async Task<string?> UnPublishFixture(string fixtureKey, string sport)
        {
            var unPublishedRequest = WorkbookWebApiRequests.FixtureUnPublishRequest(fixtureKey);
            return await Published(unPublishedRequest, sport);
        }

        internal static async Task<string?> PublishFixture(string fixtureKey, string sport)
        {
            var publishedRequest = WorkbookWebApiRequests.FixturePublishRequest(fixtureKey);
            return await Published(publishedRequest, sport);
        }

        internal static string? SimulateFixture(string fixtureKey, string sport, int numberOfSims = 60000)
        {

            lock (LockObject)
            {
                Thread.Sleep(TimeSpan.FromSeconds(KeywordMapping.SimDelay[Base.Configuration.Environment]));
                var simRequest = WorkbookWebApiRequests.UserSimRequest(fixtureKey, numberOfSims);
                var response= Simulate(simRequest, sport).GetAwaiter().GetResult();
                Console.WriteLine("Simulate requestId- " + response);
                return response;
            }
        }

        internal static async Task<string?> MarketSuspensionOverride(string fixtureKey, string sport,
            KeywordMapping.MarketSuspension marketSuspension)
        {

            var request = new MarketSuspensionRequestModel() { FixtureKey = fixtureKey };
            var response =
                JsonConvert.DeserializeObject<MarketSuspensionRequestModel>((await GetManualSuspension(request, sport))!);
            request.marketSuspensionSettings = new MarketSuspensionSettings[response!.marketSuspensionSettings.Length];
            request.marketSuspensionSettings = response.marketSuspensionSettings;
            foreach (var t in request.marketSuspensionSettings)
            {
                t.value = (int)marketSuspension;
            }

            var setMarketSuspensionResponse = await SetManualSuspension(request, sport);
            return (ValidateGuid(setMarketSuspensionResponse)) ? setMarketSuspensionResponse : null;
        }

        internal static async Task<MarketSuspensionSettings?> GetRandomMarketForMarketSuspension(string fixtureKey, string sport,
            KeywordMapping.MarketSuspension marketSuspension)
        {

            var request = new MarketSuspensionRequestModel() { FixtureKey = fixtureKey };
            var response =
                JsonConvert.DeserializeObject<MarketSuspensionRequestModel>((await GetManualSuspension(request, sport))!);

            request.marketSuspensionSettings = new MarketSuspensionSettings[1];
            request.marketSuspensionSettings[0] =
                response!.marketSuspensionSettings[new Random().NextInt64(0, response.marketSuspensionSettings.Length)];
            request.marketSuspensionSettings[0].value = (int)marketSuspension;

            var setMarketSuspensionResponse = await SetManualSuspension(request, sport);
            return (ValidateGuid(setMarketSuspensionResponse)) ? request.marketSuspensionSettings[0] : null;
        }

        internal static bool ValidateGuid(string? guid)
        {
            return guid != null && Guid.TryParse(guid.Replace("\"", ""), out _);
        }

        protected static async Task<T?> GetDiffusionData<T>(string sport, string fixture, string marketType,
            DateTime timestamp)
        {

            T? data = default;
            fixture = (fixture.Contains(" ")) ? fixture.Replace(" ", "_") : fixture;
            Console.WriteLine(fixture + " time - " + timestamp);
            var response = await DiffusionData.GetTopicLatestData(
                "External/MarketMaker/Markets/" + KeywordMapping.GetSportMapping(sport) + "/" + fixture + "/Production/" + marketType,
                timestamp);

            return response != null ? JsonConvert.DeserializeObject<T>(response) : data;
        }
    }
}
