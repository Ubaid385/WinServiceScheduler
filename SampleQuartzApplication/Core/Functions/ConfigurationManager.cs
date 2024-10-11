using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Functions
{
    public static class ConfigurationManager
    {
        private static IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false, false).Build();

        public static string getConfig(string config)
        {
            return configuration[config];
        }

        public static T getConfig<T>(string config)
        {
            var value = configuration[config];
            return (T)Convert.ChangeType(value, typeof(T));
        }
    }
}
