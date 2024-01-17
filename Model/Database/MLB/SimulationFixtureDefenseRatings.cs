namespace AutomationFramework.Model.Database.MLB
{
    internal class SimulationFixtureDefenseRatingsModel
    {
        public string FixtureKey { get; set; }
        public string Key { get; set; }
        public string Inning { get; set; }
        public DateTime Timestamp { get; set; }
        public string Type { get; set; }
        public int Team { get; set; }
        public decimal GB { get; set; }
        public decimal FB { get; set; }
        public decimal Err { get; set; }
        public decimal SA { get; set; }
        public decimal SS { get; set; }
    }
}
