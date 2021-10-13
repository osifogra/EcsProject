using Microsoft.Extensions.Configuration;
using System.IO;

namespace EcsDigitalProject.UtilityHelpers
{
    public class ConfigManager
    {
        public static string BrowsersType => new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json").Build().GetSection("Browsers")["Browser"];

        public static string WebSiteUrl => new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json").Build().GetSection("WebSiteUrl")["url"];
    }
}