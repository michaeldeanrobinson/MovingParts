using System;
using System.Configuration;

namespace MP.Models
{
    internal static class Settings
    {
        public static bool DebugMode
        {
            get
            {
                if (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["DebugMode"]))
                {
                    return false;
                }

                return bool.Parse(ConfigurationManager.AppSettings["DebugMode"]);
            }
        }

    }
}
