using AutomationFramework.Model.Database.MLB;
using AutomationFramework.Model.Database.NFL;

namespace AutomationFramework.DefaultRequests
{
    internal static class SimulationFixtureSimmerIdRequests
    {
        public static NflSimulationFixtureSimmerIdModel NflSimulationFixtureSimmerIdDefaultRequests(string fixtureKey, string requestId)
        {
            var request = new NflSimulationFixtureSimmerIdModel()
            {
                FixtureKey = fixtureKey,
                Key = "Production",
                TruePrice = "1",
                UserID = Base.Base.Configuration.DatabaseUsername,
                RequestId = requestId
            };
            return request;
        }

        public static MlbSimulationFixtureSimmerIdModel MlbSimulationFixtureSimmerIdDefaultRequests(string fixtureKey, DateTime timestamp)
        {
            var request = new MlbSimulationFixtureSimmerIdModel()
            {
                FixtureKey = fixtureKey,
                Key = "Production",
                Timestamp = timestamp,
                UserID = Base.Base.Configuration.DatabaseUsername,
            };
            return request;
        }
    }
}
