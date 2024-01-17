
namespace AutomationFramework.Model.WorkbookWebApi
{
    internal class MarketSuspensionRequestModel
    {
        public string FixtureKey { get; set; }
        public MarketSuspensionSettings[] marketSuspensionSettings { get; set; }
    }

    internal class MarketPlayerSuspensionRequestModel
    {
        public string FixtureKey { get; set; }
    }

    internal class PlayerSuspensionRequestModel
    {
        public string FixtureKey { get; set; }
    }

    public class MarketSuspensionSettings
    {
        public int value { get; set; }
        public string marketAcronym { get; set; }
        public string marketPeriod { get; set; }
        public int marketType { get; set; }
    }
}
