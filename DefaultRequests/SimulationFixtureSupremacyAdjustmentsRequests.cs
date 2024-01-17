using AutomationFramework.Model.Database.MLB;
using AutomationFramework.Model.Database.NFL;

namespace AutomationFramework.DefaultRequests
{
    internal static class SimulationFixtureSupremacyAdjustmentsRequest
    {
        public static NflSimulationFixtureSupremacyAdjustmentsModel GetSimulationFixtureSupremacyAdjustmentsRequest(string fixtureKey, string type, DateTime timestamp)
        {
            var request = new NflSimulationFixtureSupremacyAdjustmentsModel()
            {
                FixtureKey = fixtureKey,
                Key = "Production",
                GameState = "PM",
                Type = type,
                Timestamp = timestamp
            };

            return request;
        }

        public static NflSimulationFixtureSupremacyAdjustmentsModel GetSimulationFixtureSupremacyAdjustmentsHfaRequest(string fixtureKey, string type, DateTime timestamp)
        {
            var request = new NflSimulationFixtureSupremacyAdjustmentsModel()
            {
                FixtureKey = fixtureKey,
                Key = "Production",
                GameState = "PM",
                Type = type,
                cPenaltyPreSnapOffBySelf = "1.9747",
                cPenaltyPreSnapDefByOpponent = "20.1565",
                cPenaltyOff10PassBySelf = "3.1941",
                cPenaltyDef5Auto1stByOpponent = "12.194",
                dRushing = "1.70625",
                qbPressure = "2.63445",
                qbSack = "0.8008",
                qbPassType = "4.54545",
                qbShortPassAttempt = "-2.17945",
                qbLongPassAttempt = "0.84175",
                qbPassIntercept = "6.825",
                qbShortPassComplete = "3.2487",
                qbMediumPassComplete = "3.5399",
                qbLongPassComplete = "4.9595",
                qbShortPassYAC = "2.4479",
                kFieldGoalMake = "5.278",
                Timestamp = timestamp
            };

            return request;
        }

        public static NflSimulationFixtureSupremacyAdjustmentsModel GetSimulationFixtureSupremacyAdjustmentsGlobalAdjustRequest(string fixtureKey, string type, DateTime timestamp)
        {
            var request = new NflSimulationFixtureSupremacyAdjustmentsModel()
            {
                FixtureKey = fixtureKey,
                Key = "Production",
                GameState = "PM",
                Type = type,
                qbPace = "44",
                qbRushPass = "-2",
                rPassInterference = "5",
                oRushPass3rdAndShort = "-20",
                qbSack = "24",
                qbScramble = "-30",
                qbScrambleYards = "-25",
                qbPassType = "-32",
                qbShortPassAttempt = "-1",
                qbMediumPassComplete = "-13",
                qbLongPassComplete = "-5",
                qbShortPassYAC = "-24",
                qbMediumPassYAC = "-19",
                kFieldGoalMake = "110",
                Timestamp = timestamp
            };

            return request;
        }

        public static MlbSimulationFixtureSupremacyAdjustmentsModel Get_Mlb_SimulationFixtureSupremacyAdjustments_Request(string fixtureKey, string type, DateTime timestamp)
        {
            var request = new MlbSimulationFixtureSupremacyAdjustmentsModel()
            {
                FixtureKey = fixtureKey,
                Key = "Production",
                Inning = "PM",
                Type = type,
                Timestamp = timestamp
            };

            return request;
        }

        public static MlbSimulationFixtureSupremacyAdjustmentsModel Get_Mlb_SimulationFixtureSupremacyAdjustments_Hfa_Request(string fixtureKey, DateTime timestamp)
        {
            var rand = new Random();
            var request = Get_Mlb_SimulationFixtureSupremacyAdjustments_Request(fixtureKey, "HFA", timestamp);
            request.Walk = (decimal)6.6915 + new decimal(rand.NextDouble());
            request.nK = (decimal)4.137 + new decimal(rand.NextDouble());
            request.FB = (decimal)1.549 + new decimal(rand.NextDouble());
            request.nWGB = (decimal)0.795 + new decimal(rand.NextDouble());
            request.GBH = (decimal)6.4395 + new decimal(rand.NextDouble());
            request.nWFB = (decimal)-0.8275 - new decimal(rand.NextDouble());
            request.FBH = (decimal)0.289 + new decimal(rand.NextDouble());
            request.FBHR = (decimal)3.5635 + new decimal(rand.NextDouble());
            request.GBB = (decimal)5.323 + new decimal(rand.NextDouble());
            request.FBB = (decimal)4.9205 + new decimal(rand.NextDouble());

            return request;
        }

        public static MlbSimulationFixtureSupremacyAdjustmentsModel Get_Mlb_SimulationFixtureSupremacyAdjustments_ManAdjust_Request(MlbSimulationFixtureSupremacyAdjustmentsModel hfaRequest)
        {
            hfaRequest.Walk *= -1;
            hfaRequest.nK *= -1;
            hfaRequest.FB *= -1;
            hfaRequest.nWGB *= -1;
            hfaRequest.GBH *= -1;
            hfaRequest.nWFB *= -1;
            hfaRequest.FBH *= -1;
            hfaRequest.FBHR *= -1;
            hfaRequest.GBB *= -1;
            hfaRequest.FBB *= -1;
            hfaRequest.Type = "ManAdjust";

            return hfaRequest;
        }
    }
}
