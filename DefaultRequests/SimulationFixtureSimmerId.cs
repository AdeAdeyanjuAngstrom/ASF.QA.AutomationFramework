using AutomationFramework.Model.Database.NFL;

namespace AutomationFramework.DefaultRequests
{
    internal static class SimulationFixtureRequests
    {
        public static SimulationFixtureRequestsModel SimulationFixtureDefaultRequests(string fixtureKey,
            string numberOfSims, string requestId, DateTime timestamp)
        {
            var request = new SimulationFixtureRequestsModel()
            {
                FixtureKey = fixtureKey,
                Key = "Production",
                Timestamp = timestamp,
                NumberOfSims = numberOfSims,
                RequestId = requestId
            };
            return request;
        }
    }
}
