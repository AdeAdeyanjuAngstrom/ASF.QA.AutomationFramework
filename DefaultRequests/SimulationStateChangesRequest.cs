using AutomationFramework.Model.Database.MLB;

namespace AutomationFramework.DefaultRequests
{
    internal static class SimulationStateChanges
    {
        public static MlbSimulationStateChangesModel SimulationStateChangesRequests(string fixtureKey, DateTime timestamp)
        {
            var request = new MlbSimulationStateChangesModel()
            {
                FixtureKey = fixtureKey,
                SimulationKey = "Production",
                SimType = "Live",
                State = "PlayerMarketPlayer",
                GameState = "PM",
                Timestamp = timestamp
            };
            return request;
        }
    }
}
