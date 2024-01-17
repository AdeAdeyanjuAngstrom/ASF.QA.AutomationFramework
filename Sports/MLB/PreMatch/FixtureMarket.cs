using ASF.Core.Common.Messages.Enums;
using ASF.Core.Common.NFL.Messages.MM.FixtureMarkets;
using AutomationFramework.Base.MLB;
using AutomationFramework.Constants;
using NUnit.Framework;

namespace AutomationFramework.Sports.MLB.PreMatch
{
    [Parallelizable(ParallelScope.All)]
    internal class FixtureMarket : MlbTestBase
    {
        private readonly MlbFixtureMarketHelper _mlbFixtureMarketHelper = new();

        [Test]
        public async Task Sanity_Check()
        {
            string? errorMessage = null;
            var timestamp = DateTime.Now;

            var fixtureKey = await SetupPreMatchFixture("mlb", timestamp, 60000);

            if (fixtureKey != null)
            {
                var diffusionData = await GetDiffusionData<FixtureMarkets>("mlb", fixtureKey,
                    KeywordMapping.MarketTypes.FixtureMarkets.ToString(), timestamp);

                if (diffusionData != null)
                {
                    errorMessage = _mlbFixtureMarketHelper.FixtureMarket_SanityChecks(diffusionData, "mlb", 2, "pm");

                    //Suspended State check
                    errorMessage += _mlbFixtureMarketHelper.SuspendedStateCheck(diffusionData, SuspendState.Suspended);

                    //Suspended Reason check
                    errorMessage += _mlbFixtureMarketHelper.SuspendedReasonCheck(diffusionData);

                }
                else
                {
                    await Base.Base.TeamsAlert("MLB PreMatch Checks", "Issues found in Fixture Market checks - ",
                        "FixtureMarket data not pushed to diffusion for " + fixtureKey);
                    Assert.Fail("FixtureMarket data not pushed to diffusion");
                }
            }
            else
            {
                await Base.Base.TeamsAlert("MLB PreMatch Checks", "Issues found in Fixture Market checks - ",
                    "Fixture setup failed");
                Assert.Fail("Fixture setup failed");
            }

            if (!string.IsNullOrEmpty(errorMessage))
            {
                await Base.Base.TeamsAlert("MLB PreMatch Checks", "Issues found in Fixture Market checks - ",
                    errorMessage);
                Assert.Fail(errorMessage);
            }
        }


        [Test]
        public async Task AllMarketUnSuspension_Check()
        {
            string? errorMessage = null;

            var dateTime = DateTime.Now;
            var fixtureKey = await SetupPreMatchFixture("mlb", dateTime, 60000);

            if (fixtureKey != null)
            {
                var diffusionData = await GetDiffusionData<FixtureMarkets>("mlb", fixtureKey,
                    KeywordMapping.MarketTypes.FixtureMarkets.ToString(), dateTime);

                if (diffusionData != null)
                {
                    var newDateTime = DateTime.Now;
                    var marketSuspensionResponse = await MarketSuspensionOverride(fixtureKey, "mlb",
                        KeywordMapping.MarketSuspension.UnSuspended);
                    if (marketSuspensionResponse != null)
                    {
                        if (ValidateGuid(SimulateFixture(fixtureKey, "mlb")))
                        {
                            var updatedDiffusionData = await GetDiffusionData<FixtureMarkets>("mlb", fixtureKey,
                                KeywordMapping.MarketTypes.FixtureMarkets.ToString(), newDateTime);
                            if (updatedDiffusionData != null)
                            {
                                errorMessage =
                                    _mlbFixtureMarketHelper.FixtureMarket_SanityChecks(updatedDiffusionData, "mlb", 2,
                                        "pm");

                                //Suspended State check
                                errorMessage +=
                                    _mlbFixtureMarketHelper.SuspendedStateCheck(updatedDiffusionData,
                                        SuspendState.UnSuspended);

                                //Suspended Reason check
                                errorMessage += _mlbFixtureMarketHelper.SuspendedReasonCheck(updatedDiffusionData, "0");
                            }
                            else
                            {
                                await Base.Base.TeamsAlert("MLB PreMatch Checks",
                                    "Issues found in Fixture Market checks - ",
                                    "FixtureMarket data not pushed to diffusion after Re-Sim for " + fixtureKey);
                                Assert.Fail("FixtureMarket data not pushed to diffusion after Re-Sim");
                            }
                        }
                    }
                    else
                    {
                        await Base.Base.TeamsAlert("MLB PreMatch Checks", "Issues found in Fixture Market checks - ",
                            "Market Suspension failed - " + fixtureKey);
                        Assert.Fail("Market Suspension failed");
                    }
                }
                else
                {
                    await Base.Base.TeamsAlert("MLB PreMatch Checks", "Issues found in Fixture Market checks - ",
                        "FixtureMarket data not pushed to diffusion for " + fixtureKey);
                    Assert.Fail("FixtureMarket data not pushed to diffusion");
                }
            }
            else
            {
                await Base.Base.TeamsAlert("MLB PreMatch Checks", "Issues found in Fixture Market checks - ",
                    "Fixture setup failed");
                Assert.Fail("Fixture setup failed");
            }

            if (!string.IsNullOrEmpty(errorMessage))
            {
                await Base.Base.TeamsAlert("MLB PreMatch All Market UnSuspension Checks",
                    "Issues found in Fixture Market checks - " + fixtureKey, errorMessage);
                Assert.Fail(errorMessage);
            }
        }


        [Test]
        public async Task RandomMarketUnSuspension_Check()
        {
            string? errorMessage = null;

            var dateTime = DateTime.Now;
            var fixtureKey = await SetupPreMatchFixture("mlb", dateTime, 60000);

            if (fixtureKey != null)
            {
                var diffusionData = await GetDiffusionData<FixtureMarkets>("mlb", fixtureKey,
                    KeywordMapping.MarketTypes.FixtureMarkets.ToString(), dateTime);

                if (diffusionData != null)
                {
                    var newDateTime = DateTime.Now;
                    var marketSuspensionResponse = await GetRandomMarketForMarketSuspension(fixtureKey, "mlb",
                        KeywordMapping.MarketSuspension.UnSuspended);

                    if (marketSuspensionResponse != null)
                    {
                        if (ValidateGuid(SimulateFixture(fixtureKey, "mlb")))
                        {
                            var updatedDiffusionData = await GetDiffusionData<FixtureMarkets>("mlb", fixtureKey,
                                KeywordMapping.MarketTypes.FixtureMarkets.ToString(), newDateTime);
                            if (updatedDiffusionData != null)
                            {
                                errorMessage =
                                    _mlbFixtureMarketHelper.FixtureMarket_SanityChecks(updatedDiffusionData, "mlb", 2,
                                        "pm");

                                //Suspended State check
                                errorMessage +=
                                    _mlbFixtureMarketHelper.SpecificMarketSuspendedStateCheck(updatedDiffusionData,
                                        SuspendState.UnSuspended,marketSuspensionResponse);

                                //Suspended Reason check
                                errorMessage += _mlbFixtureMarketHelper.SpecificMarketSuspendedReasonCheck(updatedDiffusionData, marketSuspensionResponse,"0");
                            }
                            else
                            {
                                await Base.Base.TeamsAlert("MLB PreMatch Checks",
                                    "Issues found in Fixture Market checks - ",
                                    "FixtureMarket data not pushed to diffusion after Re-Sim for " + fixtureKey);
                                Assert.Fail("FixtureMarket data not pushed to diffusion after Re-Sim");
                            }
                        }
                    }
                    else
                    {
                        await Base.Base.TeamsAlert("MLB PreMatch Checks", "Issues found in Fixture Market checks - ",
                            "Market Suspension failed - " + fixtureKey);
                        Assert.Fail("Market Suspension failed");
                    }
                }
                else
                {
                    await Base.Base.TeamsAlert("MLB PreMatch Checks", "Issues found in Fixture Market checks - ",
                        "FixtureMarket data not pushed to diffusion for " + fixtureKey);
                    Assert.Fail("FixtureMarket data not pushed to diffusion");
                }
            }
            else
            {
                await Base.Base.TeamsAlert("MLB PreMatch Checks", "Issues found in Fixture Market checks - ",
                    "Fixture setup failed");
                Assert.Fail("Fixture setup failed");
            }

            if (!string.IsNullOrEmpty(errorMessage))
            {
                await Base.Base.TeamsAlert("MLB PreMatch Specific Market UnSuspension Checks",
                    "Issues found in Fixture Market checks - " + fixtureKey, errorMessage);
                Assert.Fail(errorMessage);
            }
        }
    }
}
