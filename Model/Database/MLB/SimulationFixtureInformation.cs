namespace AutomationFramework.Model.Database.MLB
{
    internal class MlbSimulationFixtureInformationModel
    {
        public string FixtureKey { get; set; }
        public string Key { get; set; }
        public string Inning { get; set; }
        public DateTime Timestamp { get; set; }
        public DateTime FixtureDateTime { get; set; }
        public string LeagueRules { get; set; } = "0";
        public string MatchLength { get; set; } = "0";
        public string Roof { get; set; } = "0";
        public string Umpire { get; set; } = "0";
        public string HomeRestNext { get; set; } = "0";
        public string AwayRestNext { get; set; } = "0";
        public string HFA { get; set; } = "0";
        public string RunsAdjust { get; set; } = "0";
        public string DesignatedHitter { get; set; } = "0";
        public string GhostRunner { get; set; } = "0";
        public string RPswing { get; set; } = "0";
    }
}
