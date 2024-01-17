namespace AutomationFramework.Model.Database.MLB
{
    internal class MlbSimulationStateChangesModel
    {
        public string FixtureKey { get; set; }
        public string SimulationKey { get; set; }
        public string SimType { get; set; }
        public string State { get; set; }
        public string GameState { get; set; }
        public DateTime Timestamp { get; set; }

    }
}
