using Microsoft.Extensions.Configuration;

namespace AutomationFramework
{
    public class Startup
    {
        private static readonly IConfiguration Configuration;

        static Startup()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appSettings.json", optional: false, reloadOnChange: true);
                
            Configuration = builder.Build();
        }

        public static string GetConfiguration(string name)
        {
            return Configuration.GetSection(name).Value!;
        }
    }
}
