using AutomationFramework.Model.Database.MLB;

namespace AutomationFramework.DefaultRequests
{
    internal static class SimulationFixtureBatterRatingsRequest
    {
        public static SimulationFixtureBatterRatingsModel GetSimulationFixturePitcherRatingsRequest(string fixtureKey, DateTime timestamp, int team, string type,
            int order, int playerId, string bats)
        {
            var rand = new Random();
            var request = new SimulationFixtureBatterRatingsModel()
            {
                FixtureKey = fixtureKey,
                Key = "Production",
                Inning = "PM",
                Timestamp = timestamp,
                Order = order,
                PlayerID = playerId,
                Type = type,
                Team = team,
                Bats = bats,

                Walk = (decimal)1061.15 + rand.NextInt64(-300, 300) + new decimal(rand.NextDouble()),
                Walk_Pltn = (decimal)35.986 + rand.NextInt64(-300, 300) + new decimal(rand.NextDouble()),
                nK = (decimal)1084.921 + rand.NextInt64(-300, 300) + new decimal(rand.NextDouble()),
                nK_Pltn = (decimal)7.277 + rand.NextInt64(-300, 300) + new decimal(rand.NextDouble()),
                FB = (decimal)1110.375 + rand.NextInt64(-300, 300) + new decimal(rand.NextDouble()),
                FB_Pltn = (decimal)53.698 + rand.NextInt64(-300, 300) + new decimal(rand.NextDouble()),
                nWGB = (decimal)1077.649 + rand.NextInt64(-300, 300) + new decimal(rand.NextDouble()),
                nWGB_Pltn = (decimal)31.418 + rand.NextInt64(-300, 300) + new decimal(rand.NextDouble()),
                GBH = (decimal)1109.406 + rand.NextInt64(-300, 300) + new decimal(rand.NextDouble()),
                GBH_Pltn = (decimal)-12.826 + rand.NextInt64(-300, 300) + new decimal(rand.NextDouble()),
                nWFB = (decimal)1022.117 + rand.NextInt64(-300, 300) + new decimal(rand.NextDouble()),
                nWFB_Pltn = (decimal)-18.035 + rand.NextInt64(-300, 300) + new decimal(rand.NextDouble()),
                FBH = (decimal)1059.789 + rand.NextInt64(-300, 300) + new decimal(rand.NextDouble()),
                FBH_Pltn = (decimal)-1.15 + rand.NextInt64(-300, 300) + new decimal(rand.NextDouble()),
                FBHR = (decimal)1120.755 + rand.NextInt64(-300, 300) + new decimal(rand.NextDouble()),
                FBHR_Pltn = (decimal)82.238 + rand.NextInt64(-300, 300) + new decimal(rand.NextDouble()),
                GBB = (decimal)1117.314 + rand.NextInt64(-300, 300) + new decimal(rand.NextDouble()),
                GBB_Pltn = (decimal)11.057 + rand.NextInt64(-300, 300) + new decimal(rand.NextDouble()),
                FBB = (decimal)1129.939 + rand.NextInt64(-300, 300) + new decimal(rand.NextDouble()),
                FBB_Pltn = (decimal)20.582 + rand.NextInt64(-300, 300) + new decimal(rand.NextDouble()),
                SA = (decimal)1212.248 + rand.NextInt64(-300, 300) + new decimal(rand.NextDouble()),
                SS = (decimal)1114.214 + rand.NextInt64(-300, 300) + new decimal(rand.NextDouble()),
                BaseAdv_R = (decimal)1140.484 + rand.NextInt64(-300, 300) + new decimal(rand.NextDouble()),
                BaseAdv_B = (decimal)1119.943 + rand.NextInt64(-300, 300) + new decimal(rand.NextDouble()),
                BPPA = (decimal)0.5,
                Pos = rand.NextInt64(9)
            };

            return request;
        }
    }
}
