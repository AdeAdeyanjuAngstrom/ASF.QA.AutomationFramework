using AutomationFramework.Model.Database.MLB;
using AutomationFramework.Model.Database.NFL;

namespace AutomationFramework.DefaultRequests
{
    internal static class SimulationFixtureTotalAdjustmentsRequest
    {
        public static NflSimulationFixtureTotalAdjustmentsModel GetNflSimulationFixtureTotalAdjustmentsRequest(string fixtureKey, string type, DateTime timestamp)
        {
            var request = new NflSimulationFixtureTotalAdjustmentsModel()
            {
                FixtureKey = fixtureKey,
                Key = "Production",
                GameState = "PM",
                Type = type,
                Timestamp = timestamp
            };

            return request;
        }

        public static NflSimulationFixtureTotalAdjustmentsModel GetNflSimulationFixtureTotalAdjustmentsUmpireRequest(string fixtureKey, string type, DateTime timestamp)
        {
            var request = new NflSimulationFixtureTotalAdjustmentsModel()
            {
                FixtureKey = fixtureKey,
                Key = "Production",
                GameState = "PM",
                Type = type,
                Timestamp = timestamp,
                cPenaltyPreSnapOffByOpponent = (decimal)-26.5309999999999,
                cPenaltyPreSnapDefByOpponent = (decimal)51.3520000000001,
                cPenaltyOff10PassByOpponent = (decimal)143.784,
                cPenaltyOff10RushByOpponent = (decimal)11.4190000000001,
                cPenaltyOff15BySelf = (decimal)-194.4741176,
                cPenaltyDef15BySelf = (decimal)-10.8209412000001,
                cPenaltyDef5Auto1stByOpponent = (decimal)61.04,
                rPassInterference = (decimal)115.906
            };

            return request;
        }

        public static NflSimulationFixtureTotalAdjustmentsModel GetNflSimulationFixtureTotalAdjustmentsGlobalAdjustRequest(string fixtureKey, string type, DateTime timestamp)
        {
            var request = new NflSimulationFixtureTotalAdjustmentsModel()
            {
                FixtureKey = fixtureKey,
                Key = "Production",
                GameState = "PM",
                Type = type,
                qbPace = 44,
                qbRushPass = -2,
                rPassInterference = 5,
                oRushPass3rdAndShort = -20,
                qbSack = 24,
                qbScramble = -30,
                qbScrambleYards = -25,
                qbPassType = -32,
                qbShortPassAttempt = -1,
                qbMediumPassComplete = -13,
                qbLongPassComplete = -5,
                qbShortPassYAC = -24,
                qbMediumPassYAC = -19,
                kFieldGoalMake = 110,
                Timestamp = timestamp
            };

            return request;
        }

        public static MlbSimulationFixtureTotalAdjustmentsModel Get_Mlb_SimulationFixtureTotalAdjustments_Request(string fixtureKey, string type, DateTime timestamp)
        {
            var request = new MlbSimulationFixtureTotalAdjustmentsModel()
            {
                FixtureKey = fixtureKey,
                Key = "Production",
                Inning = "PM",
                Type = type,
                Timestamp = timestamp
            };
            return request;
        }

        public static MlbSimulationFixtureTotalAdjustmentsModel Get_Mlb_SimulationFixtureTotalAdjustments_Stadium_Request(string fixtureKey, DateTime timestamp)
        {
            var rand = new Random();
            var request = Get_Mlb_SimulationFixtureTotalAdjustments_Request(fixtureKey, "Stadium", timestamp);
            request.Walk = (decimal)-267.092 + new decimal(rand.NextDouble());
            request.nK = (decimal)198.55 + new decimal(rand.NextDouble());
            request.FB = (decimal)50.711 + new decimal(rand.NextDouble());
            request.nWGB = (decimal)309.162 + new decimal(rand.NextDouble());
            request.GBH = (decimal)26.149 + new decimal(rand.NextDouble());
            request.nWFB = (decimal)198.036 + new decimal(rand.NextDouble());
            request.FBH = (decimal)-61.159 + new decimal(rand.NextDouble());
            request.FBHR = (decimal)-284.467 + new decimal(rand.NextDouble());
            request.GBB = (decimal)-254.451 + new decimal(rand.NextDouble());
            request.FBB = (decimal)-51.507 + new decimal(rand.NextDouble());
            request.BaseAdvGB = (decimal)2.646 + new decimal(rand.NextDouble());
            request.BaseAdvFB = (decimal)27.273 + new decimal(rand.NextDouble());

            return request;
        }

        public static MlbSimulationFixtureTotalAdjustmentsModel Get_Mlb_SimulationFixtureTotalAdjustments_StadiumFix_Request(string fixtureKey, DateTime timestamp)
        {
            var rand = new Random();
            var request = Get_Mlb_SimulationFixtureTotalAdjustments_Request(fixtureKey, "StadiumFix", timestamp);
            request.Walk = (decimal)-1.50310556 - new decimal(rand.NextDouble());
            request.nK = (decimal)-0.64596272 - new decimal(rand.NextDouble());
            request.FB = (decimal)-0.4658385 - new decimal(rand.NextDouble());
            request.nWGB = (decimal)-0.63354036 - new decimal(rand.NextDouble());
            request.GBH = (decimal)-0.37888198 - new decimal(rand.NextDouble());
            request.nWFB = (decimal)-0.14285714 - new decimal(rand.NextDouble());
            request.FBH = (decimal)-0.0931677 - new decimal(rand.NextDouble());
            request.FBHR = (decimal)-0.9937888 - new decimal(rand.NextDouble());
            request.GBB = (decimal)-0.310559 - new decimal(rand.NextDouble());
            request.FBB = (decimal)-0.1552795 - new decimal(rand.NextDouble());

            return request;
        }

        public static MlbSimulationFixtureTotalAdjustmentsModel Get_Mlb_SimulationFixtureTotalAdjustments_Umpire_Request(string fixtureKey, DateTime timestamp)
        {
            var rand = new Random();
            var request = Get_Mlb_SimulationFixtureTotalAdjustments_Request(fixtureKey, "Umpire", timestamp);
            request.Walk = (decimal)-4.79 - new decimal(rand.NextDouble());
            request.nK = (decimal)-3.2 - new decimal(rand.NextDouble());

            return request;
        }

        public static MlbSimulationFixtureTotalAdjustmentsModel Get_Mlb_SimulationFixtureTotalAdjustments_Weather_Request(string fixtureKey, DateTime timestamp)
        {
            var rand = new Random();
            var request = Get_Mlb_SimulationFixtureTotalAdjustments_Request(fixtureKey, "Weather", timestamp);
            request.Walk = (decimal)3.55 + new decimal(rand.NextDouble());
            request.nK = (decimal)-2.458375 + new decimal(rand.NextDouble());
            request.FB = (decimal)-2.121125 + new decimal(rand.NextDouble());
            request.nWGB = (decimal)-0.576875 - new decimal(rand.NextDouble());
            request.GBH = (decimal)-0.843124 - new decimal(rand.NextDouble());
            request.nWFB = (decimal)-1.411125 - new decimal(rand.NextDouble());
            request.FBH = (decimal)-1.499875 + new decimal(rand.NextDouble());
            request.FBHR = (decimal)-8.25374 + new decimal(rand.NextDouble());
            request.GBB = (decimal)-3.63875 + new decimal(rand.NextDouble());
            request.FBB = (decimal)0.497 + new decimal(rand.NextDouble());

            return request;
        }

        public static MlbSimulationFixtureTotalAdjustmentsModel Get_Mlb_SimulationFixtureTotalAdjustments_GlobalChange_Request(string fixtureKey, DateTime timestamp)
        {
            var rand = new Random();
            var request = Get_Mlb_SimulationFixtureTotalAdjustments_Request(fixtureKey, "GlobalChange", timestamp);
            request.nK = (decimal)6.3 + new decimal(rand.NextDouble());
            request.FB = (decimal)1.5 + new decimal(rand.NextDouble());
            request.GBH = -2 - new decimal(rand.NextDouble());
            request.FBH = -2 - new decimal(rand.NextDouble());
            request.FBHR = -25 + new decimal(rand.NextDouble());

            return request;
        }
    }
}
