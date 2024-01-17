namespace AutomationFramework.Model.Database.NFL
{
    internal class PendingFixturesModel
    {
        public int Week { get; set; }
        public DateTime UkFixtureDateTime { get; set; }
        public DateTime GMTStartDateTime { get; set; }
        public DateTime LocalStartDateTime { get; set; }
        public string FixtureKey { get; set; }
        public string Home { get; set; }
        public string Away { get; set; }
        public string Stadium { get; set; }

    }
}
