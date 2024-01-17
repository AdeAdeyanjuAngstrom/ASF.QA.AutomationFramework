using AutomationFramework.Model.Database.MLB;

namespace AutomationFramework.DefaultRequests
{
    internal static class SimulationFixturePitcherRatingsRequest
    {
        public static SimulationFixturePitcherRatingsModel GetSimulationFixturePitcherRatingsRequest(string fixtureKey, DateTime timestamp, int team, string type,
            int order, int playerId, string throws)
        {
            var rand = new Random();
            var request = new SimulationFixturePitcherRatingsModel()
            {
                FixtureKey = fixtureKey,
                Key = "Production",
                Inning = "PM",
                Timestamp = timestamp,
                Order = order,
                PlayerID = playerId,
                Type = type,
                Team = team,
                Throws = throws,
                Walk = (decimal)1165.5659725 + rand.NextInt64(-300, 300) + new decimal(rand.NextDouble()),
                Walk_Pltn = (decimal)-22.887 + rand.NextInt64(-300, 300) + new decimal(rand.NextDouble()),
                nK = (decimal)1049.88165625 + rand.NextInt64(-300, 300) + new decimal(rand.NextDouble()),
                nK_Pltn = (decimal)77.158 + rand.NextInt64(-300, 300) + new decimal(rand.NextDouble()),
                FB = (decimal)1151.3942875 + rand.NextInt64(-300, 300) + new decimal(rand.NextDouble()),
                FB_Pltn = (decimal)-8.842 + rand.NextInt64(-300, 300) + new decimal(rand.NextDouble()),
                nWGB = (decimal)1115.47763 + rand.NextInt64(-300, 300) + new decimal(rand.NextDouble()),
                nWGB_Pltn = (decimal)1.402 + rand.NextInt64(-300, 300) + new decimal(rand.NextDouble()),
                GBH = (decimal)1072.37652 + rand.NextInt64(-300, 300) + new decimal(rand.NextDouble()),
                GBH_Pltn = (decimal)4.562 + rand.NextInt64(-300, 300) + new decimal(rand.NextDouble()),
                nWFB = (decimal)1097.175945 + rand.NextInt64(-300, 300) + new decimal(rand.NextDouble()),
                nWFB_Pltn = (decimal)26.481 + rand.NextInt64(-300, 300) + new decimal(rand.NextDouble()),
                FBH = (decimal)1085.08498625 + rand.NextInt64(-300, 300) + new decimal(rand.NextDouble()),
                FBH_Pltn = (decimal)-1.643 + rand.NextInt64(-300, 300) + new decimal(rand.NextDouble()),
                FBHR = (decimal)1093.48856125 + rand.NextInt64(-300, 300) + new decimal(rand.NextDouble()),
                FBHR_Pltn = (decimal)13.026 + rand.NextInt64(-300, 300) + new decimal(rand.NextDouble()),
                GBB = (decimal)1087.05067125 + rand.NextInt64(-300, 300) + new decimal(rand.NextDouble()),
                GBB_Pltn = (decimal)5.441 + rand.NextInt64(-300, 300) + new decimal(rand.NextDouble()),
                FBB = (decimal)1093.43274 + rand.NextInt64(-300, 300) + new decimal(rand.NextDouble()),
                FBB_Pltn = (decimal)7.66 + rand.NextInt64(-300, 300) + new decimal(rand.NextDouble()),
                WP = (decimal)1099.346 + rand.NextInt64(-300, 300) + new decimal(rand.NextDouble()),
                SA = (decimal)1147.368 + rand.NextInt64(-300, 300) + new decimal(rand.NextDouble()),
                SS = (decimal)1080.989 + rand.NextInt64(-300, 300) + new decimal(rand.NextDouble()),
                PitchLimit = (decimal)87 + rand.NextInt64(-300, 300) + new decimal(rand.NextDouble()),
                Loogy = 0,
                Days = 0,
                LastPitches = 28 + new Random().NextInt64(50),
                Unavailability = 0,
                AppearanceLength = 3,
                Selectability = 50,
                BaseAdv_P = (decimal)1108.915 + rand.NextInt64(-300, 300) + new decimal(rand.NextDouble()),
                FIP = (decimal)3.852903 + rand.NextInt64(-300, 300) + new decimal(rand.NextDouble())
            };

            return request;
        }
    }
}
