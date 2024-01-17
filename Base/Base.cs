using AutomationFramework.Helpers.ApiHelper;
using AutomationFramework.Model.Others;
using FluentAssertions;
using StackExchange.Redis;

namespace AutomationFramework.Base
{
    public static class Base
    {
        public static readonly ConfigurationModel  Configuration=new();
        private static readonly Lazy<ConnectionMultiplexer> LazyConnection;
        public static ConnectionMultiplexer KafkaConnection => LazyConnection.Value;

        static Base()
        {
            AssertionOptions.FormattingOptions.MaxDepth = 1000;
            GetEnvironmentConfiguration();
            LazyConnection = new Lazy<ConnectionMultiplexer>(() =>
                ConnectionMultiplexer.Connect(Configuration.RedisDbUrl +
                                              ",abortConnect=false,connectTimeout=30000,responseTimeout=30000"));
        }

        private static void GetEnvironmentConfiguration()
        {
            Configuration.Environment = Startup.GetConfiguration("Environment").ToLower();
            Configuration.DiffusionUsername = Startup.GetConfiguration("DiffusionUsername");
            Configuration.DiffusionPassword = Startup.GetConfiguration("DiffusionPassword");
            Configuration.DiffusionUrl = (string.IsNullOrEmpty(Startup.GetConfiguration("DiffusionUrl")))
                ? "wss://" + GetEnvironmentPrefix(Configuration.Environment) +
                  "1-msa-angstrom.eu.diffusion.cloud/diffusion"
                : Startup.GetConfiguration("DiffusionUrl");
            Configuration.RedisDbUrl = (string.IsNullOrEmpty(Startup.GetConfiguration("RedisUrl")))
                ? GetEnvironmentPrefix(Configuration.Environment) +
                  "1-angstrom-redis.i3ry7r.0001.euw2.cache.amazonaws.com:6379"
                : Startup.GetConfiguration("RedisUrl");
            Configuration.DatabaseUrl = (string.IsNullOrEmpty(Startup.GetConfiguration("DatabaseUrl")))
                ? Configuration.Environment + ".cfafuksgxgck.eu-west-2.rds.amazonaws.com"
                : Startup.GetConfiguration("DatabaseUrl");
            Configuration.DatabaseUsername = Startup.GetConfiguration("DatabaseUsername");
            Configuration.DatabasePassword = Startup.GetConfiguration("DatabasePassword");
            Configuration.TeamsUrl = Startup.GetConfiguration("TeamsUrl");
        }

        private static string? GetEnvironmentPrefix(string environment)
        {
            return environment.ToLower() switch
            {
                "dev" => "d",
                "qa" => "q",
                "prod" => "p",
                _ => null
            };
        }

        internal static async Task TeamsAlert(string testTitle, string testDetails, string issue)
        {
            var request = new TeamsNotificationRequestModel()
            {
                type = "MessageCard",
                themeColor = "0076D7",
                summary = "Issues found while testing " + testTitle,
                sections = new Sections[]
                {
                    new()
                    {
                        activityTitle = "Issues found while testing " + testTitle,
                        activitySubtitle= "On "+Configuration.Environment.ToUpper()+" Environment",
                        activityImage= "https://adaptivecards.io/content/cats/3.png",
                        facts = new Facts[]
                        {
                            new()
                            {
                                name = testDetails,
                                value = issue
                            }
                        }
                    }
                }
            };

            await RestApiClient.SendPostRequestAsync(Base.Configuration.TeamsUrl, request);
        }
    }
}
