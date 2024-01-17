namespace AutomationFramework.Model.WorkbookWebApi
{
    internal class UserSimRequestModel
    {
        public string FixtureKey { get; set; }
        public string SimulationKey { get; set; }
        public int NumberOfSimsRequested { get; set; }
    }
}
