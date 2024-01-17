using ASF.Core.Common.Messages.Enums;

namespace AutomationFramework.Model.WorkbookWebApi
{
    public class FixturePublishRequestModel
    {
        public string FixtureKey { get; set; }
        public string SimulationKey { get; set; }
        public string ClientKey { get; set; }
        public PublishType Published { get; set; } //Fixture Publish State
    }
}