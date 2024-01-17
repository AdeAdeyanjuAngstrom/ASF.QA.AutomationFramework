using ASF.Core.Common.External.Helpers;
//using ASF.Core.Common.SecretsManager;
using Newtonsoft.Json;


namespace AutomationFramework.Helpers.RedisHelper
{
    public static class RedisHelper
    {
        private static readonly IByteSerializer ByteSerializer;
        //private static SecretsManagerClient _secretsManager;
        private static readonly Dictionary<string, Func<byte[], string>> SupportedRedisKeys;
        

        static RedisHelper()
        {
            ByteSerializer = new ProtoBufSerializer();
            //_secretsManager = new SecretsManagerClient();
            //_memcache = new ASF.Core.Common.Cache.MemoryCache(_secretsManager.GetRedisConnectionString(environment));
            SupportedRedisKeys = new();

            InitialiseNflRedisKeys();
            InitialiseMlbRedisKeys();
            InitialiseNbaRedisKeys();
        }
        private static Func<byte[], string> NewFunc<T>()
        {
            return DeserializeBytes<T>;
        }
        public static string? GetJsonFromRedisBytes(string searchKey,string fixture, byte[] bytes)
        {
            var supportedRedisKeys = ReplaceKeyForFixture(SupportedRedisKeys, fixture);

            return supportedRedisKeys.ContainsKey(searchKey) ? supportedRedisKeys[searchKey].Invoke(bytes) : null;
        }

        private static Dictionary<string, Func<byte[], string>> ReplaceKeyForFixture(Dictionary<string, Func<byte[], string>> mapoing, string fixture)
        {
            var temp = new Dictionary<string, Func<byte[], string>>();
            foreach (var(key,value) in mapoing)
            {
                temp.Add(key.Replace("{FixtureKey}",fixture,StringComparison.CurrentCultureIgnoreCase), value);
            }

            return temp;
        }

        private static string DeserializeBytes<T>(byte[] data)
        {
            try
            {
                var deserializedObject = ByteSerializer.DeserializeFromBytes<T>(data);
                return JsonConvert.SerializeObject(deserializedObject);
            }
            catch (Exception ex)
            {
                return $"Failed to DeserializeBytes: \n\n{ex}";
            }
        }

        private static void InitialiseNflRedisKeys()
        {
            // NFL Market Maker Keys
            SupportedRedisKeys["mkt-nfl-{FixtureKey}-Prod-PlayerMatchupMarkets"] = NewFunc<ASF.Core.Common.NFL.Messages.MM.PlayerMatchupMarkets.PlayerMatchupMarkets>();
            SupportedRedisKeys["mkt-nfl-{FixtureKey}-Prod-PlayerMarkets"] = NewFunc<ASF.Core.Common.NFL.Messages.MM.PlayerMarkets.PlayerMarkets>();
            SupportedRedisKeys["mkt-nfl-{FixtureKey}-Prod-FixtureMarkets"] = NewFunc<ASF.Core.Common.NFL.Messages.MM.FixtureMarkets.FixtureMarkets>();
            SupportedRedisKeys["mkt-nfl-{FixtureKey}-Prod-PlayerCombinedMarkets"] = NewFunc<ASF.Core.Common.NFL.Messages.MM.PlayerCombinedMarkets.PlayerCombinedMarkets>();
            SupportedRedisKeys["mkt-nfl-{FixtureKey}-Prod-PlayerMatchupHandicapMarkets"] = NewFunc<ASF.Core.Common.NFL.Messages.MM.PlayerMatchupHandicapMarkets.PlayerMatchupHandicapMarkets>();
            SupportedRedisKeys["mkt-nfl-{FixtureKey}-Prod-PlayerMilestoneMarkets"] = NewFunc<ASF.Core.Common.NFL.Messages.MM.PlayerMilestoneMarkets.PlayerMilestoneMarkets>();
            SupportedRedisKeys["mkt-nfl-{FixtureKey}-Prod-PlayerMostMarkets"] = NewFunc<ASF.Core.Common.NFL.Messages.MM.PlayerMostMarkets.PlayerMostMarkets>();
            SupportedRedisKeys["mkt-nfl-{FixtureKey}-Prod-PlayerRaceToMarkets"] = NewFunc<ASF.Core.Common.NFL.Messages.MM.PlayerRaceToMarkets.PlayerRaceToMarkets>();
            SupportedRedisKeys["mkt-nfl-{FixtureKey}-Prod-CurrentDriveMarkets"] = NewFunc<ASF.Core.Common.NFL.Messages.MM.MicroMarkets.NFL.CurrentDrive.CurrentDriveMarkets>();
            SupportedRedisKeys["mkt-nfl-{FixtureKey}-Prod-FixtureCorrectScoreMarkets"] = NewFunc<ASF.Core.Common.NFL.Messages.MM.FixtureCorrectScoreMarkets.FixtureCorrectScoreMarkets>();
            SupportedRedisKeys["mkt-nfl-{FixtureKey}-Prod-FirstLastNextMarkets"] = NewFunc<ASF.Core.Common.NFL.Messages.MM.PlayerFirstLastNextMarkets.FirstLastNextMarkets>();
            
            // NFL Settlement Keys
            SupportedRedisKeys["stl-nfl-{fixtureKey}-FixtureMarketSettlements"] = NewFunc<ASF.Core.Common.Messages.Cache.Settlements.FixtureMarketSettlements>();
            SupportedRedisKeys["stl-nfl-{fixtureKey}-PlayerMarketSettlements"] = NewFunc<ASF.Core.Common.Messages.Cache.Settlements.PlayerMarketSettlements>();
            SupportedRedisKeys["stl-nfl-{fixtureKey}-PlayerMatchupMarketSettlements"] = NewFunc<ASF.Core.Common.Messages.Cache.Settlements.PlayerMatchupMarketSettlements>();
            SupportedRedisKeys["stl-nfl-{fixtureKey}-DriveMarketSettlements"] = NewFunc<ASF.Core.Common.Messages.Cache.Settlements.CurrentDriveMarketSettlements>();
            SupportedRedisKeys["stl-nfl-{fixtureKey}-FixtureCorrectScoreMarketSettlements"] = NewFunc<ASF.Core.Common.Messages.Cache.Settlements.FixtureCorrectScoreMarketSettlements>();
            SupportedRedisKeys["stl-nfl-{fixtureKey}-PlayerCombinedMarketSettlements"] = NewFunc<ASF.Core.Common.Messages.Cache.Settlements.PlayerCombinedMarketSettlements>();
            SupportedRedisKeys["stl-nfl-{fixtureKey}-PlayerMatchupHandicapMarketSettlements"] = NewFunc<ASF.Core.Common.Messages.Cache.Settlements.PlayerMatchupHandicapMarketSettlements>();
            SupportedRedisKeys["stl-nfl-{fixtureKey}-PlayerMilestoneMarketSettlements"] = NewFunc<ASF.Core.Common.Messages.Cache.Settlements.PlayerMilestoneMarketSettlements>();
            SupportedRedisKeys["stl-nfl-{fixtureKey}-PlayerMostMarketSettlements"] = NewFunc<ASF.Core.Common.Messages.Cache.Settlements.PlayerMostMarketSettlements>();
            SupportedRedisKeys["stl-nfl-{fixtureKey}-PlayerRaceToMarketSettlements"] = NewFunc<ASF.Core.Common.Messages.Cache.Settlements.PlayerRaceToMarketSettlements>();

            // NFL Suspension Keys
            SupportedRedisKeys["sus-nfl-{fixtureKey}-FixtureMarketSuspensions"] = NewFunc<ASF.Core.Common.NFL.Messages.Cache.Suspensions.FixtureMarketSuspensions>();
            SupportedRedisKeys["sus-nfl-{fixtureKey}-PlayerMarketSuspensions"] = NewFunc<ASF.Core.Common.NFL.Messages.Cache.Suspensions.PlayerMarketSuspensions>();
            SupportedRedisKeys["sus-nfl-{fixtureKey}-PlayerMatchupMarketAutoSuspensions"] = NewFunc<ASF.Core.Common.NFL.Messages.Cache.Suspensions.PlayerMatchupMarketSuspensions>();
            SupportedRedisKeys["sus-nfl-{fixtureKey}-DriveMarketSuspensions"] = NewFunc<ASF.Core.Common.NFL.Messages.Cache.Suspensions.CurrentDriveMarketSuspensions>();
            SupportedRedisKeys["sus-nfl-{fixtureKey}-FirstLastNextMarketSuspensions"] = NewFunc<ASF.Core.Common.NFL.Messages.Cache.Suspensions.FirstLastNextMarketSuspensions>();
            SupportedRedisKeys["sus-nfl-{fixtureKey}-PlayerCombinedMarketsSuspensions"] = NewFunc<ASF.Core.Common.NFL.Messages.Cache.Suspensions.PlayerCombinedMarketSuspensions>();
            SupportedRedisKeys["sus-nfl-{fixtureKey}-PlayerMatchupHandicapMarketsSuspensions"] = NewFunc<ASF.Core.Common.NFL.Messages.Cache.Suspensions.PlayerMatchupHandicapMarketSuspensions>();
            SupportedRedisKeys["sus-nfl-{fixtureKey}-PlayerMilestoneMarketsSuspensions"] = NewFunc<ASF.Core.Common.NFL.Messages.Cache.Suspensions.PlayerMilestoneMarketSuspensions>();
            SupportedRedisKeys["sus-nfl-{fixtureKey}-PlayerMostMarketsSuspensions"] = NewFunc<ASF.Core.Common.NFL.Messages.Cache.Suspensions.PlayerMostMarketSuspensions>();
            SupportedRedisKeys["sus-nfl-{fixtureKey}-PlayerRaceToMarketsSuspensions"] = NewFunc<ASF.Core.Common.NFL.Messages.Cache.Suspensions.PlayerRaceToMarketSuspensions>();
            SupportedRedisKeys["sus-nfl-{fixtureKey}-FixtureCorrectScoreMarketsSuspensions"] = NewFunc<ASF.Core.Common.NFL.Messages.Cache.Suspensions.FixtureCorrectScoreMarketSuspensions>();
            SupportedRedisKeys["sus-nfl-{fixtureKey}-SuspensionFlagStateLookup"] = NewFunc<ASF.Core.Common.NFL.Messages.Attributes.SuspensionManagerStateLookup>();

            SupportedRedisKeys["rat-nfl-proto-{FixtureKey}"] = NewFunc<ASF.Core.Common.Messages.Attributes.NFL.Rating>();
            SupportedRedisKeys["nfl-agg-aggregate-data-{FixtureKey}"] = NewFunc<ASF.Core.Common.Messages.Messages.AggregateDataMessageNFL>();
        }



        private static void InitialiseMlbRedisKeys()
        {
            // MLB Market Maker Keys
            SupportedRedisKeys["mkt-mlb-{FixtureKey}-Prod-PlayerMatchupMarkets"] = NewFunc<ASF.Core.Common.NFL.Messages.MM.PlayerMatchupMarkets.PlayerMatchupMarkets>();
            SupportedRedisKeys["mkt-mlb-{FixtureKey}-Prod-PlayerMarkets"] = NewFunc<ASF.Core.Common.NFL.Messages.MM.PlayerMarkets.PlayerMarkets>();
            SupportedRedisKeys["mkt-mlb-{FixtureKey}-Prod-FixtureMarkets"] = NewFunc<ASF.Core.Common.NFL.Messages.MM.FixtureMarkets.FixtureMarkets>();
            SupportedRedisKeys["mkt-mlb-{FixtureKey}-Prod-BBPlayerMarkets"] = NewFunc<ASF.Core.Common.NFL.Messages.MM.BetBuilderMarkets.BBPlayerMarkets>();
            SupportedRedisKeys["mkt-mlb-{FixtureKey}-Prod-MlbHalfInningMarkets"] = NewFunc<ASF.Core.Common.NFL.Messages.Publishing.Payloads.MM.MicroMarkets.Baseball.HalfInningMarkets.MlbHalfInningMarkets>();
            SupportedRedisKeys["mkt-mlb-{FixtureKey}-Prod-MlbCurrentAtBatMarkets"] = NewFunc<ASF.Core.Common.NFL.Messages.Publishing.Payloads.MM.MicroMarkets.Baseball.AtBatMarkets.MlbCurrentAtBatMarkets>();

            // MLB Settlement Keys
            SupportedRedisKeys["stl-mlb-{FixtureKey}-FixtureMarketSettlements"] = NewFunc<ASF.Core.Common.Messages.Cache.Settlements.FixtureMarketSettlements>();
            SupportedRedisKeys["stl-mlb-{FixtureKey}-PlayerMarketSettlements"] = NewFunc<ASF.Core.Common.Messages.Cache.Settlements.PlayerMarketSettlements>();
            SupportedRedisKeys["stl-mlb-{FixtureKey}-PlayerMatchupMarketSettlements"] = NewFunc<ASF.Core.Common.Messages.Cache.Settlements.PlayerMatchupMarketSettlements>();
            SupportedRedisKeys["stl-mlb-{FixtureKey}-BBPlayerMarketSettlements"] = NewFunc<ASF.Core.Common.Messages.Cache.Settlements.BBPlayerMarketSettlements>();
            SupportedRedisKeys["stl-mlb-{FixtureKey}-MlbHalfInningMarketSettlements"] = NewFunc<ASF.Core.Common.Messages.Cache.Settlements.MLB.MlbHalfInningMarketSettlements>();
            SupportedRedisKeys["stl-mlb-{FixtureKey}-MlbCurrentAtBatMarketSettlements"] = NewFunc<ASF.Core.Common.Messages.Cache.Settlements.MLB.MlbCurrentAtBatMarketSettlements>();

            // MLB Suspension Keys
            SupportedRedisKeys["sus-mlb-{FixtureKey}-FixtureMarketSuspensions"] = NewFunc<ASF.Core.Common.NFL.Messages.Cache.Suspensions.FixtureMarketSuspensions>();
            SupportedRedisKeys["sus-mlb-{FixtureKey}-PlayerMarketSuspensions"] = NewFunc<ASF.Core.Common.NFL.Messages.Cache.Suspensions.PlayerMarketSuspensions>();
            SupportedRedisKeys["sus-mlb-{FixtureKey}-PlayerMatchupMarketAutoSuspensions"] = NewFunc<ASF.Core.Common.NFL.Messages.Cache.Suspensions.PlayerMatchupMarketSuspensions>();
            SupportedRedisKeys["sus-mlb-{FixtureKey}-MlbHalfInningMarketSuspensions"] = NewFunc<ASF.Core.Common.Messages.Cache.Suspensions.MLB.MlbHalfInningMarketSuspensions>();
            SupportedRedisKeys["sus-mlb-{FixtureKey}-BBPlayerMarketSuspensions"] = NewFunc<ASF.Core.Common.NFL.Messages.Cache.Suspensions.FirstLastNextMarketSuspensions>();
            SupportedRedisKeys["sus-mlb-{FixtureKey}-MlbCurrentAtBatMarketSuspensions"] = NewFunc<ASF.Core.Common.NFL.Messages.Cache.Suspensions.BBPlayerMarketSuspensions>();
            SupportedRedisKeys["sus-mlb-{FixtureKey}-SuspensionFlagStateLookup"] = NewFunc<ASF.Core.Common.Messages.Attributes.MLB.SuspensionManagerStateLookup>();

            SupportedRedisKeys["rat-mlb-proto-{FixtureKey}"] = NewFunc<ASF.Core.Common.Messages.Attributes.MLB.Ratings.Rating>();
            SupportedRedisKeys["mlb-agg-aggregate-data-{FixtureKey}"] = NewFunc<ASF.Core.Common.Messages.MessageBus.Flat.MLB.AggregateDataMessageMLB>();
        }

        private static void InitialiseNbaRedisKeys()
        {
            // NBA Market Maker Keys
            SupportedRedisKeys["mkt-nba-{FixtureKey}-Prod-PlayerMatchupMarkets"] = NewFunc<ASF.Core.Common.NFL.Messages.MM.PlayerMatchupMarkets.PlayerMatchupMarkets>();
            SupportedRedisKeys["mkt-nba-{FixtureKey}-Prod-PlayerMarkets"] = NewFunc<ASF.Core.Common.NFL.Messages.MM.PlayerMarkets.PlayerMarkets>();
            SupportedRedisKeys["mkt-nba-{FixtureKey}-Prod-FixtureMarkets"] = NewFunc<ASF.Core.Common.NFL.Messages.MM.FixtureMarkets.FixtureMarkets>();
            SupportedRedisKeys["mkt-nba-{FixtureKey}-Prod-PlayerCombinedMarkets"] = NewFunc<ASF.Core.Common.NFL.Messages.MM.PlayerCombinedMarkets.PlayerCombinedMarkets>();
            SupportedRedisKeys["mkt-nba-{FixtureKey}-Prod-PlayerMatchupHandicapMarkets"] = NewFunc<ASF.Core.Common.NFL.Messages.MM.PlayerMatchupHandicapMarkets.PlayerMatchupHandicapMarkets>();
            SupportedRedisKeys["mkt-nba-{FixtureKey}-Prod-PlayerMilestoneMarkets"] = NewFunc<ASF.Core.Common.NFL.Messages.MM.PlayerMilestoneMarkets.PlayerMilestoneMarkets>();
            SupportedRedisKeys["mkt-nba-{FixtureKey}-Prod-PlayerMostMarkets"] = NewFunc<ASF.Core.Common.NFL.Messages.MM.PlayerMostMarkets.PlayerMostMarkets>();
            SupportedRedisKeys["mkt-nba-{FixtureKey}-Prod-PlayerRaceToMarkets"] = NewFunc<ASF.Core.Common.NFL.Messages.MM.PlayerRaceToMarkets.PlayerRaceToMarkets>();
            SupportedRedisKeys["mkt-nba-{FixtureKey}-Prod-FirstLastNextMarkets"] = NewFunc<ASF.Core.Common.NFL.Messages.MM.PlayerFirstLastNextMarkets.FirstLastNextMarkets>();

            // NBA Settlement Keys
            SupportedRedisKeys["stl-nba-{FixtureKey}-FixtureMarketSettlements"] = NewFunc<ASF.Core.Common.Messages.Cache.Settlements.FixtureMarketSettlements>();
            SupportedRedisKeys["stl-nba-{FixtureKey}-PlayerMarketSettlements"] = NewFunc<ASF.Core.Common.Messages.Cache.Settlements.PlayerMarketSettlements>();
            SupportedRedisKeys["stl-nba-{FixtureKey}-PlayerMatchupMarketSettlements"] = NewFunc<ASF.Core.Common.Messages.Cache.Settlements.PlayerMatchupMarketSettlements>();
            SupportedRedisKeys["stl-nba-{FixtureKey}-DriveMarketSettlements"] = NewFunc<ASF.Core.Common.Messages.Cache.Settlements.CurrentDriveMarketSettlements>();
            SupportedRedisKeys["stl-nba-{FixtureKey}-FixtureCorrectScoreMarketSettlements"] = NewFunc<ASF.Core.Common.Messages.Cache.Settlements.FixtureCorrectScoreMarketSettlements>();
            SupportedRedisKeys["stl-nba-{FixtureKey}-PlayerCombinedMarketSettlements"] = NewFunc<ASF.Core.Common.Messages.Cache.Settlements.PlayerCombinedMarketSettlements>();
            SupportedRedisKeys["stl-nba-{FixtureKey}-PlayerMatchupHandicapMarketSettlements"] = NewFunc<ASF.Core.Common.Messages.Cache.Settlements.PlayerMatchupHandicapMarketSettlements>();
            SupportedRedisKeys["stl-nba-{FixtureKey}-PlayerMilestoneMarketSettlements"] = NewFunc<ASF.Core.Common.Messages.Cache.Settlements.PlayerMilestoneMarketSettlements>();
            SupportedRedisKeys["stl-nba-{FixtureKey}-PlayerMostMarketSettlements"] = NewFunc<ASF.Core.Common.Messages.Cache.Settlements.PlayerMostMarketSettlements>();
            SupportedRedisKeys["stl-nba-{FixtureKey}-PlayerRaceToMarketSettlements"] = NewFunc<ASF.Core.Common.Messages.Cache.Settlements.PlayerRaceToMarketSettlements>();

            // NBA Suspension Keys
            SupportedRedisKeys["sus-nba-{FixtureKey}-FixtureMarketSuspensions"] = NewFunc<ASF.Core.Common.NBA.Messages.Cache.Suspensions.FixtureMarketSuspensions>();
            SupportedRedisKeys["sus-nba-{FixtureKey}-PlayerMarketSuspensions"] = NewFunc<ASF.Core.Common.NBA.Messages.Cache.Suspensions.PlayerMarketSuspensions>();
            SupportedRedisKeys["sus-nba-{FixtureKey}-PlayerMatchupMarketAutoSuspensions"] = NewFunc<ASF.Core.Common.NBA.Messages.Cache.Suspensions.PlayerMatchupMarketSuspensions>();
            SupportedRedisKeys["sus-nba-{FixtureKey}-FirstLastNextMarketSuspensions"] = NewFunc<ASF.Core.Common.NBA.Messages.Cache.Suspensions.FirstLastNextMarketSuspensions>();
            SupportedRedisKeys["sus-nba-{FixtureKey}-PlayerCombinedMarketsSuspensions"] = NewFunc<ASF.Core.Common.NBA.Messages.Cache.Suspensions.PlayerCombinedMarketSuspensions>();
            SupportedRedisKeys["sus-nba-{FixtureKey}-PlayerMatchupHandicapMarketsSuspensions"] = NewFunc<ASF.Core.Common.NBA.Messages.Cache.Suspensions.PlayerMatchupHandicapMarketSuspensions>();
            SupportedRedisKeys["sus-nba-{FixtureKey}-PlayerMilestoneMarketsSuspensions"] = NewFunc<ASF.Core.Common.NBA.Messages.Cache.Suspensions.PlayerMilestoneMarketSuspensions>();
            SupportedRedisKeys["sus-nba-{FixtureKey}-PlayerMostMarketsSuspensions"] = NewFunc<ASF.Core.Common.NBA.Messages.Cache.Suspensions.PlayerMostMarketSuspensions>();
            SupportedRedisKeys["sus-nba-{FixtureKey}-PlayerRaceToMarketsSuspensions"] = NewFunc<ASF.Core.Common.NBA.Messages.Cache.Suspensions.PlayerRaceToMarketSuspensions>();
            SupportedRedisKeys["sus-nba-{FixtureKey}-SuspensionFlagStateLookup"] = NewFunc<ASF.Core.Common.NBA.Messages.Attributes.SuspensionManagerStateLookup>();

            SupportedRedisKeys["rat-nba-proto-{FixtureKey}"] = NewFunc<ASF.Core.Common.Messages.Attributes.NBA.Ratings.Rating>();
            SupportedRedisKeys["nba-agg-aggregate-data-{FixtureKey}"] = NewFunc<ASF.Core.Common.Messages.Messages.AggregateDataMessageNBA>();
        }
    }
}
