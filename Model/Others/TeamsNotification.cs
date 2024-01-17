using Newtonsoft.Json;

namespace AutomationFramework.Model.Others
{
    internal class TeamsNotificationRequestModel
    {
        [JsonProperty(PropertyName="@type")]
        public string type { get; set; }
        public string themeColor { get; set; }
        public string summary { get; set; }
        public Sections[] sections { get; set; }
    }

    public class Sections
    {
        public string activityTitle { get; set; }
        public string activitySubtitle { get; set; }
        public string activityImage { get; set; }
        public Facts[] facts { get; set; }
    }

    public class Facts
    {
        public string name { get; set; }
        public string value { get; set; }
    }
}
