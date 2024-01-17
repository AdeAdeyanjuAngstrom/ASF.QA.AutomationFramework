using ASF.Core.Common.Messages.Enums;
using ASF.Core.Common.NFL.Messages.MM.FixtureMarkets;
using AutomationFramework.Constants;
using AutomationFramework.Model.WorkbookWebApi;

namespace AutomationFramework.Base.Markets
{
    public class FixtureMarketsTestBase
    {
        private static string? PeriodCountCheck(FixtureMarkets data, int count, string sport)
        {
            string? errorMessage = null;
            var periodCount = data.Periods.Count != count;

            if (periodCount)
            {
                errorMessage += "FixtureMarket has " +
                                data.Periods.Count + " periods but should have " + count + " - " +
                                string.Join(", ", data.Periods.Select(i => i.PeriodName).ToList()) +
                                "   " + Environment.NewLine;
            }

            foreach (var period in data.Periods)
            {
                if (!KeywordMapping.AllowedPeriodsPerSport[sport].Contains(period.PeriodName.ToLower()))
                {
                    errorMessage += "Fixture market has period " + period + " which is not present in the allowed list of periods." +
                                    "   " + Environment.NewLine;
                }
            }

            return errorMessage;
        }
        private static string? MarketCountCheck(FixtureMarkets data,string sport)
        {
            string? errorMessage = null;

            foreach (var period in data.Periods)
            {
                var markets = period.Markets.Select(i => i.MarketName).ToList();
                var allowedMarkets = KeywordMapping.FixtureMarketAllowedMarkets[sport][period.PeriodName.ToLower()];
                if (allowedMarkets.Count != period.Markets.Count)
                {
                    errorMessage += "Period " + period.PeriodName + " has the following markets " +
                                    string.Join(",", markets) + " but should have " + string.Join(",", allowedMarkets) +
                                    "   " + Environment.NewLine;
                }

                foreach (var market in markets)
                {
                    if (!KeywordMapping.FixtureMarketAllowedMarkets[sport][period.PeriodName.ToLower()].Contains(market.ToLower()))
                        errorMessage += "Period " + period.PeriodName + " has " +
                                        market + " which is not present in the allowed list of markets for the period " +
                                        "   " + Environment.NewLine;
                }
            }
            return errorMessage;
        }

        internal string? SuspendedStateCheck(FixtureMarkets data, SuspendState state)
        {
            string? errorMessage = null;
            var suspendedStateCount = data.Periods.Select(i =>
                i.Markets.Select(j => j.SuspendState != state));
            var periodCount = 0;

            foreach (var period in suspendedStateCount)
            {
                var marketCount = 0;
                foreach (var market in period)
                {
                    if (market)
                    {
                        errorMessage += "In period " + data.Periods[periodCount].PeriodName +
                                        " the market " +
                                        data.Periods[periodCount].Markets[marketCount].MarketName +
                                        " has Suspension state as " + data.Periods[periodCount]
                                            .Markets[marketCount].SuspendState +
                                        " instead of " + state + "   " + Environment.NewLine;
                    }
                    marketCount++;
                }

                periodCount++;
            }
            return errorMessage;
        }

        internal string? SpecificMarketSuspendedStateCheck(FixtureMarkets data, SuspendState state,
            MarketSuspensionSettings marketSuspensionResponse)
        {
            string? errorMessage = null;
            state = (state == SuspendState.Suspended) ? SuspendState.UnSuspended : SuspendState.Suspended;

            var suspendedStateCount = data.Periods.Select(i =>
                i.Markets.Select(j => j.SuspendState != state));
            var periodCount = 0;

            foreach (var period in suspendedStateCount)
            {
                var marketCount = 0;
                foreach (var market in period)
                {
                    if (market)
                    {
                        if(!(marketSuspensionResponse.marketPeriod.ToLower()== data.Periods[periodCount].PeriodName.ToLower()
                           && marketSuspensionResponse.marketAcronym.ToLower() == data.Periods[periodCount].Markets[marketCount].MarketName.ToLower()))
                            errorMessage += "In period " + data.Periods[periodCount].PeriodName +
                                            " the market " +
                                            data.Periods[periodCount].Markets[marketCount].MarketName +
                                            " has Suspension state as " + data.Periods[periodCount]
                                                .Markets[marketCount].SuspendState +
                                            " instead of " + state + "   " + Environment.NewLine;
                    }
                    marketCount++;
                }

                periodCount++;
            }
            return errorMessage;
        }

        internal string? SuspendedReasonCheck(FixtureMarkets data, string? suspendedReason = null)
        {
            string? errorMessage = null;
            var suspendedReasonCount = (suspendedReason == null) ? data.Periods.Select(i =>
                i.Markets.Select(j => !KeywordMapping.NflSuspensionReason.Contains(j.SuspensionReasons))) :
                data.Periods.Select(i =>
                    i.Markets.Select(j => j.SuspensionReasons != suspendedReason));
            var periodCount = 0;

            foreach (var period in suspendedReasonCount)
            {
                var marketCount = 0;
                foreach (var market in period)
                {
                    if (market)
                    {
                        errorMessage += "In period " + data.Periods[periodCount].PeriodName +
                                        " the market " +
                                        data.Periods[periodCount].Markets[marketCount].MarketName +
                                        " has Suspension Reason as " + data.Periods[periodCount]
                                            .Markets[marketCount].SuspensionReasons +
                                        " and is not in the allowed\\expected list of reasons." + "   " + Environment.NewLine;
                    }
                    marketCount++;
                }

                periodCount++;
            }
            return errorMessage;
        }

        internal string? SpecificMarketSuspendedReasonCheck(FixtureMarkets data,
            MarketSuspensionSettings marketSuspensionResponse,string? suspendedReason)
        {
            string? errorMessage = null;
            var suspendedReasonCount = data.Periods.Select(i =>
                    i.Markets.Select(j => j.SuspensionReasons != suspendedReason));
            var periodCount = 0;

            foreach (var period in suspendedReasonCount)
            {
                var marketCount = 0;
                foreach (var market in period)
                {
                    if (market)
                    {
                        if (!(marketSuspensionResponse.marketPeriod.ToLower() == data.Periods[periodCount].PeriodName.ToLower()
                              && marketSuspensionResponse.marketAcronym.ToLower() == data.Periods[periodCount].Markets[marketCount].MarketName.ToLower()))
                        {
                            errorMessage += "In period " + data.Periods[periodCount].PeriodName +
                                         " the market " +
                                         data.Periods[periodCount].Markets[marketCount].MarketName +
                                         " has Suspension Reason as " + data.Periods[periodCount]
                                             .Markets[marketCount].SuspensionReasons +
                                         " and is not in the allowed\\expected list of reasons." + "   " + Environment.NewLine;
                        }
                    }
                    marketCount++;
                }

                periodCount++;
            }
            return errorMessage;
        }

        private static string? LinesCountCheck(FixtureMarkets data, int count)
        {
            string? errorMessage = null;
            var linesCount = data.Periods.Select(i =>
                i.Markets.Select(j => j.Lines.Count <= count));
            var periodCount = 0;

            foreach (var period in linesCount)
            {
                var marketCount = 0;
                foreach (var market in period)
                {
                    if (market)
                    {
                        errorMessage += "In period " + data.Periods[periodCount].PeriodName +
                                        " the market " +
                                        data.Periods[periodCount].Markets[marketCount].MarketName +
                                        " has line count as " + data.Periods[periodCount]
                                            .Markets[marketCount].Lines.Count +
                                        " but should be >" + count + "   " + Environment.NewLine;
                    }
                    marketCount++;
                }
                periodCount++;
            }
            return errorMessage;
        }

        private static string? SettledStateCheck(FixtureMarkets data, SettledLineStateType settledState)
        {
            string? errorMessage = null;
            var settledLinesCount = data.Periods.Select(i =>
                i.Markets.Select(j => j.Lines.Select(k => k.SettledState != settledState)));
            var periodCount = 0;

            foreach (var period in settledLinesCount)
            {
                var marketCount = 0;
                foreach (var market in period)
                {
                    var lineCount = 0;
                    foreach (var line in market)
                    {
                        if (line)
                        {
                            errorMessage += "In period " + data.Periods[periodCount].PeriodName +
                                            " the market " +
                                            data.Periods[periodCount].Markets[marketCount].MarketName +
                                            " for line " + data.Periods[periodCount].Markets[marketCount]
                                                .Lines[lineCount] +
                                            " has SettledState as " + data.Periods[periodCount].Markets[marketCount]
                                                .Lines[lineCount].SettledState +
                                            " but should be " + settledState + "   " + Environment.NewLine;
                        }
                        lineCount++;
                    }
                    marketCount++;
                }
                periodCount++;
            }
            return errorMessage;
        }

        private static string? ResettlementVersionCheck(FixtureMarkets data, int resettlementVersion)
        {
            string? errorMessage = null;
            var settledValueCount = data.Periods.Select(i =>
                i.Markets.Select(j => j.Lines.Select(k => k.ResettlementVersion != resettlementVersion)));
            var periodCount = 0;

            foreach (var period in settledValueCount)
            {
                var marketCount = 0;
                foreach (var market in period)
                {
                    var lineCount = 0;
                    foreach (var line in market)
                    {
                        if (line)
                        {
                            errorMessage += "In period " + data.Periods[periodCount].PeriodName +
                                            " the market " +
                                            data.Periods[periodCount].Markets[marketCount].MarketName +
                                            " for line " + data.Periods[periodCount].Markets[marketCount]
                                                .Lines[lineCount] +
                                            " has ResettlementVersion as " + data.Periods[periodCount].Markets[marketCount]
                                                .Lines[lineCount].ResettlementVersion +
                                            " but should be " + resettlementVersion + "   " + Environment.NewLine;
                        }
                        lineCount++;
                    }
                    marketCount++;
                }
                periodCount++;
            }
            return errorMessage;
        }

        private static string? SettledValueCheck(FixtureMarkets data, float settValue)
        {
            string? errorMessage = null;
            var settledValueCount = data.Periods.Select(i =>
                i.Markets.Select(j => j.Lines.Select(k => k.SettledValue != settValue)));
            var periodCount = 0;

            foreach (var period in settledValueCount)
            {
                var marketCount = 0;
                foreach (var market in period)
                {
                    var lineCount = 0;
                    foreach (var line in market)
                    {
                        if (line)
                        {
                            errorMessage += "In period " + data.Periods[periodCount].PeriodName +
                                            " the market " +
                                            data.Periods[periodCount].Markets[marketCount].MarketName +
                                            " for line " + data.Periods[periodCount].Markets[marketCount]
                                                .Lines[lineCount] +
                                            " has SettledValue as " + data.Periods[periodCount].Markets[marketCount]
                                                .Lines[lineCount].SettledValue +
                                            " but should be " + settValue + "   " + Environment.NewLine;
                        }
                        lineCount++;
                    }
                    marketCount++;
                }
                periodCount++;
            }
            return errorMessage;
        }

        private static string? LineStateCheck(FixtureMarkets data, LineStateType lineState)
        {
            string? errorMessage = null;
            var lineStateCount = data.Periods.Select(i =>
                i.Markets.Select(j => j.Lines.Select(k => k.LineState != lineState)));
            var periodCount = 0;

            foreach (var period in lineStateCount)
            {
                var marketCount = 0;
                foreach (var market in period)
                {
                    var lineCount = 0;
                    foreach (var line in market)
                    {
                        if (line)
                        {
                            errorMessage += "In period " + data.Periods[periodCount].PeriodName +
                                            " the market " +
                                            data.Periods[periodCount].Markets[marketCount].MarketName +
                                            " for line " + data.Periods[periodCount].Markets[marketCount]
                                                .Lines[lineCount].Line +
                                            " has LineState as " + data.Periods[periodCount].Markets[marketCount]
                                                .Lines[lineCount].LineState +
                                            " but should be " + lineState + "   " + Environment.NewLine;
                        }
                        lineCount++;
                    }
                    marketCount++;
                }
                periodCount++;
            }
            return errorMessage;
        }

        private static string? LineSelectionsCountCheck(FixtureMarkets data, int selectionsCount)
        {
            string? errorMessage = null;
            var lineStateCount = data.Periods.Select(i =>
                i.Markets.Select(j => j.Lines.Select(k => k.Selections.Count <= selectionsCount)));
            var periodCount = 0;

            foreach (var period in lineStateCount)
            {
                var marketCount = 0;
                foreach (var market in period)
                {
                    var lineCount = 0;
                    foreach (var line in market)
                    {
                        if (line)
                        {
                            errorMessage += "In period " + data.Periods[periodCount].PeriodName +
                                            " the market " +
                                            data.Periods[periodCount].Markets[marketCount].MarketName +
                                            " for line " + data.Periods[periodCount].Markets[marketCount]
                                                .Lines[lineCount].Line +
                                            " has Selections Count as " + data.Periods[periodCount].Markets[marketCount]
                                                .Lines[lineCount].Selections.Count +
                                            " but should be greater than " + selectionsCount + "   " + Environment.NewLine;
                        }
                        lineCount++;
                    }
                    marketCount++;
                }
                periodCount++;
            }
            return errorMessage;
        }

        private static string? LineTruePriceCheck(FixtureMarkets data, int truePrice)
        {
            string? errorMessage = null;
            var lineStateCount = data.Periods.Select(i =>
                i.Markets.Select(j => j.Lines.Select(k => k.Selections.Select(l => decimal.Parse(l.TruePrice) <= truePrice))));
            var periodCount = 0;

            foreach (var period in lineStateCount)
            {
                var marketCount = 0;
                foreach (var market in period)
                {
                    var lineCount = 0;
                    foreach (var line in market)
                    {
                        var selectionsCount = 0;
                        foreach (var selection in line)
                        {
                            if (selection)
                            {
                                errorMessage += "In period " + data.Periods[periodCount].PeriodName +
                                                " the market " +
                                                data.Periods[periodCount].Markets[marketCount].MarketName +
                                                " for line " + data.Periods[periodCount].Markets[marketCount]
                                                    .Lines[lineCount].Line +
                                                " for selection " + data.Periods[periodCount].Markets[marketCount]
                                                    .Lines[lineCount].Selections[selectionsCount].Selection +
                                                " the true price is " + data.Periods[periodCount].Markets[marketCount]
                                                    .Lines[lineCount].Selections[selectionsCount].TruePrice +
                                                " but should be greater than " + truePrice + "   " +
                                                Environment.NewLine;
                            }
                            selectionsCount++;
                        }
                        lineCount++;
                    }
                    marketCount++;
                }
                periodCount++;
            }
            return errorMessage;
        }

        private static string? LineSettledStateCheck(FixtureMarkets data, SettledSelectionStateType settledState)
        {
            string? errorMessage = null;
            var lineStateCount = data.Periods.Select(i =>
                i.Markets.Select(j => j.Lines.Select(k => k.Selections.Select(l => l.SettledState != settledState))));
            var periodCount = 0;

            foreach (var period in lineStateCount)
            {
                var marketCount = 0;
                foreach (var market in period)
                {
                    var lineCount = 0;
                    foreach (var line in market)
                    {
                        var selectionsCount = 0;
                        foreach (var selection in line)
                        {
                            if (selection)
                            {
                                errorMessage += "In period " + data.Periods[periodCount].PeriodName +
                                                " the market " +
                                                data.Periods[periodCount].Markets[marketCount].MarketName +
                                                " for line " + data.Periods[periodCount].Markets[marketCount]
                                                    .Lines[lineCount].Line +
                                                " for selection " + data.Periods[periodCount].Markets[marketCount]
                                                    .Lines[lineCount].Selections[selectionsCount].Selection +
                                                " the settled state is " + data.Periods[periodCount].Markets[marketCount]
                                                    .Lines[lineCount].Selections[selectionsCount].SettledState +
                                                " but should be " + settledState + "   " +
                                                Environment.NewLine;
                            }
                            selectionsCount++;
                        }
                        lineCount++;
                    }
                    marketCount++;
                }
                periodCount++;
            }
            return errorMessage;
        }

        private static string? LineOddsCheck(FixtureMarkets data)
        {
            string? errorMessage = null;
            var lineStateCount = data.Periods.Select(i =>
                i.Markets.Select(j => j.Lines.Select(k => k.Selections.Select(l => decimal.Parse(l.Odds) < (decimal)1.001 && decimal.Parse(l.Odds) > 1001))));
            var periodCount = 0;

            foreach (var period in lineStateCount)
            {
                var marketCount = 0;
                foreach (var market in period)
                {
                    var lineCount = 0;
                    foreach (var line in market)
                    {
                        var selectionsCount = 0;
                        foreach (var selection in line)
                        {
                            if (selection)
                            {
                                errorMessage += "In period " + data.Periods[periodCount].PeriodName +
                                                " the market " +
                                                data.Periods[periodCount].Markets[marketCount].MarketName +
                                                " for line " + data.Periods[periodCount].Markets[marketCount]
                                                    .Lines[lineCount].Line +
                                                " for selection " + data.Periods[periodCount].Markets[marketCount]
                                                    .Lines[lineCount].Selections[selectionsCount].Selection +
                                                " the settled state is " + data.Periods[periodCount].Markets[marketCount]
                                                    .Lines[lineCount].Selections[selectionsCount].Odds +
                                                " but should be between 1.001 and 1001" + "   " +
                                                Environment.NewLine;
                            }
                            selectionsCount++;
                        }
                        lineCount++;
                    }
                    marketCount++;
                }
                periodCount++;
            }
            return errorMessage;
        }

        internal string FixtureMarket_SanityChecks(FixtureMarkets diffusionData, string sport,int periodCount,string gameState)
        {
            string? errorMessage = null;
            //Period count check
            errorMessage += PeriodCountCheck(diffusionData, periodCount, sport);

            //market count check
            errorMessage += MarketCountCheck(diffusionData,sport);
            //GameState Check
            if (diffusionData.GameState.ToLower() != gameState)
                errorMessage += "GameState should be PM but is " + diffusionData.GameState +
                                Environment.NewLine;
            //InPlayState Check
            if (diffusionData.InPlayState != 0)
                errorMessage += "InPlayState should be 0 but is " + diffusionData.InPlayState +
                                Environment.NewLine;

            //Lines Count check
            errorMessage += LinesCountCheck(diffusionData, 0);

            //SettledState check
            errorMessage += SettledStateCheck(diffusionData, SettledLineStateType.Open);

            //SettledValue check
            errorMessage += SettledValueCheck(diffusionData, 0);

            //ResettlementVersion check
            errorMessage += ResettlementVersionCheck(diffusionData, 0);

            //LineState check
            errorMessage += LineStateCheck(diffusionData, LineStateType.Open);

            //Selections check
            errorMessage += LineSelectionsCountCheck(diffusionData, 0);

            //TruePrice check
            errorMessage += LineTruePriceCheck(diffusionData, 0);

            //SettledState check
            errorMessage += LineSettledStateCheck(diffusionData, SettledSelectionStateType.Open);

            //Odds check
            errorMessage += LineOddsCheck(diffusionData);
            return errorMessage;
        }
    }
}
