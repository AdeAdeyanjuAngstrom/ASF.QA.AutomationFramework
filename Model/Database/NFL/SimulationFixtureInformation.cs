namespace AutomationFramework.Model.Database.NFL
{
    internal class NflSimulationFixtureInformationModel
    {
        public string FixtureKey { get; set; }
        public string Key { get; set; }
        public string GameState { get; set; }
        public DateTime Timestamp { get; set; }
        public DateTime FixtureDateTime { get; set; }
        public string Roof { get; set; } = "0";
        public string Umpire { get; set; } = "0";
        public string HomeRestNext { get; set; } = "0";
        public string AwayRestNext { get; set; } = "0";
        public string HFA { get; set; } = "0";
        public string SupremacyAdjust { get; set; } = "0";
        public string TotalAdjust { get; set; } = "0";
        public string WindSpeed { get; set; } = "0";
        public string Temperature { get; set; } = "0";
        public string Precipitation { get; set; } = "0";
        public string Altitude { get; set; } = "0";
        public string KickOffTouchbackLocation { get; set; } = "0";
        public string ExtraPointLocation { get; set; } = "0";
        public string DefaultKickOffLocation { get; set; } = "0";
        public string AllowTies { get; set; } = "0";
        public string OvertimeRules { get; set; } = "0";
        public string OvertimeSeconds { get; set; } = "0";
        public string ClockRules { get; set; } = "0";
    }
}
