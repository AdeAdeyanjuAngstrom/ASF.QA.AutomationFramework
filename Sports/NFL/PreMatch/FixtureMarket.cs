using ASF.Core.Common.Messages.Enums;
using ASF.Core.Common.NFL.Messages.MM.FixtureMarkets;
using AutomationFramework.Base.NFL;
using AutomationFramework.Constants;
using NUnit.Framework;

namespace AutomationFramework.Sports.NFL.PreMatch
{
    [Parallelizable(ParallelScope.All)]
    internal class FixtureMarket : NflTestBase
    {
        private readonly NflFixtureMarketHelper _nflFixtureMarketHelper = new();

        [Test]
        public async Task Sanity_Check()
        {
            string? errorMessage = null;
            var timestamp = DateTime.Now;
            var fixtureKey = await SetupPreMatchFixture("nfl", timestamp, 60000);

            if (fixtureKey != null)
            {
                var diffusionData = await GetDiffusionData<FixtureMarkets>("nfl", fixtureKey,
                    KeywordMapping.MarketTypes.FixtureMarkets.ToString(), timestamp);

                if (diffusionData != null)
                {
                    errorMessage= _nflFixtureMarketHelper.FixtureMarket_SanityChecks(diffusionData,"nfl",7,"pm");
                    
                    //Suspended State check
                    errorMessage += _nflFixtureMarketHelper.SuspendedStateCheck(diffusionData, SuspendState.Suspended);

                    //Suspended Reason check
                    errorMessage += _nflFixtureMarketHelper.SuspendedReasonCheck(diffusionData);
                    
                }
                else
                {
                    await Base.Base.TeamsAlert("NFL PreMatch Checks", "Issues found in Fixture Market checks - ",
                        "FixtureMarket data not pushed to diffusion for " + fixtureKey);
                    Assert.Fail("FixtureMarket data not pushed to diffusion");
                }
            }
            else
            {
                await Base.Base.TeamsAlert("NFL PreMatch Checks", "Issues found in Fixture Market checks - ",
                    "Fixture setup failed");
                Assert.Fail("Fixture setup failed");
            }

            if (!string.IsNullOrEmpty(errorMessage))
            {
                await Base.Base.TeamsAlert("NFL PreMatch Checks", "Issues found in Fixture Market checks - ", errorMessage);
                Assert.Fail(errorMessage);
            }
        }


        [Test]
        public async Task MarketSuspension_Check()
        {
            string? errorMessage = null;

            var dateTime = DateTime.Now;
            var fixtureKey = await SetupPreMatchFixture("nfl", dateTime, 60000);

            if (fixtureKey != null)
            {
                var diffusionData = await GetDiffusionData<FixtureMarkets>("nfl", fixtureKey,
                    KeywordMapping.MarketTypes.FixtureMarkets.ToString(), dateTime);

                if (diffusionData != null)
                {
                    var newDateTime = DateTime.Now;
                    var marketSuspensionResponse = await MarketSuspensionOverride(fixtureKey, "nfl",
                        KeywordMapping.MarketSuspension.UnSuspended);
                    if (marketSuspensionResponse != null)
                    {
                        if (ValidateGuid(SimulateFixture(fixtureKey, "nfl")))
                        {
                            var updatedDiffusionData = await GetDiffusionData<FixtureMarkets>("nfl", fixtureKey,
                                KeywordMapping.MarketTypes.FixtureMarkets.ToString(), newDateTime);
                            if (updatedDiffusionData != null)
                            {
                                errorMessage = _nflFixtureMarketHelper.FixtureMarket_SanityChecks(updatedDiffusionData, "nfl", 7, "pm");

                                //Suspended State check
                                errorMessage += _nflFixtureMarketHelper.SuspendedStateCheck(updatedDiffusionData, SuspendState.UnSuspended);

                                //Suspended Reason check
                                errorMessage += _nflFixtureMarketHelper.SuspendedReasonCheck(updatedDiffusionData, "0");
                            }
                            else
                            {
                                await Base.Base.TeamsAlert("NFL PreMatch Checks", "Issues found in Fixture Market checks - ",
                                    "FixtureMarket data not pushed to diffusion after Re-Sim for " + fixtureKey);
                                Assert.Fail("FixtureMarket data not pushed to diffusion after Re-Sim");
                            }
                        }
                    }
                    else
                    {
                        await Base.Base.TeamsAlert("NFL PreMatch Checks", "Issues found in Fixture Market checks - ",
                            "Market Suspension failed - " + fixtureKey);
                        Assert.Fail("Market Suspension failed");
                    }
                }
                else
                {
                    await Base.Base.TeamsAlert("NFL PreMatch Checks", "Issues found in Fixture Market checks - ",
                        "FixtureMarket data not pushed to diffusion for " + fixtureKey);
                    Assert.Fail("FixtureMarket data not pushed to diffusion");
                }
            }
            else
            {
                await Base.Base.TeamsAlert("NFL PreMatch Checks", "Issues found in Fixture Market checks - ",
                    "Fixture setup failed");
                Assert.Fail("Fixture setup failed");
            }

            if (!string.IsNullOrEmpty(errorMessage))
            {
                await Base.Base.TeamsAlert("NFL PreMatch All Market UnSuspension Checks",
                    "Issues found in Fixture Market checks - " + fixtureKey, errorMessage);
                Assert.Fail(errorMessage);
            }
        }

        [Test]
        public async Task RandomMarketUnSuspension_Check()
        {
            string? errorMessage = null;

            var dateTime = DateTime.Now;
            var fixtureKey = await SetupPreMatchFixture("nfl", dateTime, 60000);

            if (fixtureKey != null)
            {
                var diffusionData = await GetDiffusionData<FixtureMarkets>("nfl", fixtureKey,
                    KeywordMapping.MarketTypes.FixtureMarkets.ToString(), dateTime);

                if (diffusionData != null)
                {
                    var newDateTime = DateTime.Now;
                    var marketSuspensionResponse = await GetRandomMarketForMarketSuspension(fixtureKey, "nfl",
                        KeywordMapping.MarketSuspension.UnSuspended);

                    if (marketSuspensionResponse != null)
                    {
                        if (ValidateGuid(SimulateFixture(fixtureKey, "nfl")))
                        {
                            var updatedDiffusionData = await GetDiffusionData<FixtureMarkets>("nfl", fixtureKey,
                                KeywordMapping.MarketTypes.FixtureMarkets.ToString(), newDateTime);
                            if (updatedDiffusionData != null)
                            {
                                errorMessage =
                                    _nflFixtureMarketHelper.FixtureMarket_SanityChecks(updatedDiffusionData, "nfl", 2,
                                        "pm");

                                //Suspended State check
                                errorMessage +=
                                    _nflFixtureMarketHelper.SpecificMarketSuspendedStateCheck(updatedDiffusionData,
                                        SuspendState.UnSuspended, marketSuspensionResponse);

                                //Suspended Reason check
                                errorMessage += _nflFixtureMarketHelper.SpecificMarketSuspendedReasonCheck(updatedDiffusionData, marketSuspensionResponse, "0");
                            }
                            else
                            {
                                await Base.Base.TeamsAlert("NFL PreMatch Checks",
                                    "Issues found in Fixture Market checks - ",
                                    "FixtureMarket data not pushed to diffusion after Re-Sim for " + fixtureKey);
                                Assert.Fail("FixtureMarket data not pushed to diffusion after Re-Sim");
                            }
                        }
                    }
                    else
                    {
                        await Base.Base.TeamsAlert("NFL PreMatch Checks", "Issues found in Fixture Market checks - ",
                            "Market Suspension failed - " + fixtureKey);
                        Assert.Fail("Market Suspension failed");
                    }
                }
                else
                {
                    await Base.Base.TeamsAlert("NFL PreMatch Checks", "Issues found in Fixture Market checks - ",
                        "FixtureMarket data not pushed to diffusion for " + fixtureKey);
                    Assert.Fail("FixtureMarket data not pushed to diffusion");
                }
            }
            else
            {
                await Base.Base.TeamsAlert("NFL PreMatch Checks", "Issues found in Fixture Market checks - ",
                    "Fixture setup failed");
                Assert.Fail("Fixture setup failed");
            }

            if (!string.IsNullOrEmpty(errorMessage))
            {
                await Base.Base.TeamsAlert("NFL PreMatch Specific Market UnSuspension Checks",
                    "Issues found in Fixture Market checks - " + fixtureKey, errorMessage);
                Assert.Fail(errorMessage);
            }
        }
    }
}
