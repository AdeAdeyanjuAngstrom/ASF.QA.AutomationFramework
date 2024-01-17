using AutomationFramework.Model.Database.MLB;

namespace AutomationFramework.DefaultRequests
{
    internal static class SimulationFixtureDefenseRatingsRequest
    {
        public static SimulationFixtureDefenseRatingsModel GetSimulationFixtureDefenseRatings_Team1Request(string fixtureKey, DateTime timestamp)
        {
            var rand = new Random();
            var request = new SimulationFixtureDefenseRatingsModel()
            {
                FixtureKey = fixtureKey,
                Key = "Production",
                Inning = "PM",
                Timestamp = timestamp,
                Type = "Field",
                Team = 1,
                GB = (decimal)10.273042 + new decimal(rand.NextDouble()),
                FB = (decimal)-0.304234700000002 + new decimal(rand.NextDouble()),
                Err = (decimal)25.946 + new decimal(rand.NextDouble()),
                SA = (decimal)3.73399999999992 + new decimal(rand.NextDouble()),
                SS = (decimal)-1.33799999999997 + new decimal(rand.NextDouble())
            };

            return request;
        }

        public static SimulationFixtureDefenseRatingsModel GetSimulationFixtureDefenseRatings_Team2Request(string fixtureKey, DateTime timestamp)
        {
            var rand = new Random();
            var request = new SimulationFixtureDefenseRatingsModel()
            {
                FixtureKey = fixtureKey,
                Key = "Production",
                Inning = "PM",
                Timestamp = timestamp,
                Type = "Field",
                Team = 2,
                GB = (decimal)14.134135 + new decimal(rand.NextDouble()),
                FB = (decimal)0.94866 + new decimal(rand.NextDouble()),
                Err = (decimal)35.364 + new decimal(rand.NextDouble()),
                SA = (decimal)12.5519999999999 + new decimal(rand.NextDouble()),
                SS = (decimal)-11.0340000000001 + new decimal(rand.NextDouble())
            };

            return request;
        }
    }
}
