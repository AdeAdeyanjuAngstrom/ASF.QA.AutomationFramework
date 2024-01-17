namespace AutomationFramework.Model.Database.MLB
{
    internal class MlbSimulationFixtureSupremacyAdjustmentsModel
    {
        public string FixtureKey { get; set; }
        public string Key { get; set; }
        public string Inning { get; set; }
        public DateTime Timestamp { get; set; }
        public string Type { get; set; }
        public decimal Walk { get; set; } = 0;
        public decimal nK { get; set; } = 0;
        public decimal FB { get; set; } = 0;
        public decimal nWGB { get; set; } = 0;
        public decimal GBH { get; set; } = 0;
        public decimal nWFB { get; set; } = 0;
        public decimal FBH { get; set; } = 0;
        public decimal FBHR { get; set; } = 0;
        public decimal GBB { get; set; } = 0;
        public decimal FBB { get; set; } = 0;
    }
}