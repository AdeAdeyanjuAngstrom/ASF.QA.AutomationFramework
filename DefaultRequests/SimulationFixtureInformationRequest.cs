using AutomationFramework.Model.Database.MLB;
using AutomationFramework.Model.Database.NFL;

namespace AutomationFramework.DefaultRequests
{
    internal static class SimulationFixtureInformationRequest
    {
        public static NflSimulationFixtureInformationModel GetNflSimulationFixtureInformationRequest(string fixtureKey,
            DateTime fixtureDateTime, DateTime timestamp)
        {
            var request = new NflSimulationFixtureInformationModel()
            {
                FixtureKey = fixtureKey,
                Key = "Production",
                GameState = "PM",
                FixtureDateTime = fixtureDateTime,
                Timestamp = timestamp,
                Umpire = "0",
                HFA = "1",
                Temperature = "70",
                KickOffTouchbackLocation = "25",
                ExtraPointLocation = "15",
                DefaultKickOffLocation = "65",
                AllowTies = "1",
                OvertimeRules = "1",
                OvertimeSeconds = "600",
                ClockRules = "1"
            };

            return request;
        }

        public static MlbSimulationFixtureInformationModel GetMlbSimulationFixtureInformationRequest(string fixtureKey,
            DateTime fixtureDateTime, DateTime timestamp)
        {
            var request = new MlbSimulationFixtureInformationModel()
            {
                FixtureKey = fixtureKey,
                Key = "Production",
                Inning = "PM",
                FixtureDateTime = fixtureDateTime,
                Timestamp = timestamp,
                Umpire = "0",
                HFA = "0",
                LeagueRules = "DH",
                MatchLength = "9",
                Roof = "Closed",
                HomeRestNext = "1",
                AwayRestNext = "1",
                RunsAdjust = "0",
                DesignatedHitter = "1",
                GhostRunner = "1",
                RPswing = "0.5"
            };

            return request;
        }
    }
}
