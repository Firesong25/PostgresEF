using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PostgresEF
{
    public static class Configurations
    {
        public static string BlizzardClientId { get; private set; }
        public static string BlizzardClientPassword { get; private set; }

        public static string PostgresConnectionString { get; private set; }

        public static async Task Init()
        {
            //commented for Martijn
            string[] paths = { Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Data", "Configurations.txt" };
            string configurationFile = Path.Combine(paths);

            PostgresConnectionString = string.Empty;

            string serverName = "cleardragon";

            string[] configs = File.ReadAllLines(configurationFile);
            foreach (string config in configs)
            {
                if (PostgresConnectionString == string.Empty && config.Contains(serverName))
                {
                    PostgresConnectionString = config;
                }

                if (BlizzardClientId == null && config.Contains("clientId="))
                {
                    string[] configStrings = config.Split('=');
                    BlizzardClientId = configStrings[1];
                }

                if (BlizzardClientPassword == null && config.Contains("clientSecret="))
                {
                    string[] configStrings = config.Split('=');
                    BlizzardClientPassword = configStrings[1];
                }
            }
        }
    }
}
