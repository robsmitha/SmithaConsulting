using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace API.Utilities
{
    public static class ConfigurationManager
    {
        public static string ConfigurationsKey = "Configurations";
        public static IConfiguration AppSetting { get; }
        static ConfigurationManager()
        {
            AppSetting = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

        }
        public static string GetConfiguration(string key)
        {
            return !string.IsNullOrWhiteSpace(AppSetting[$"{ConfigurationsKey}:{key}"])
                    ? AppSetting[$"{ConfigurationsKey}:{key}"]
                    : string.Empty;
        }
    }
}
