using System;
using System.Configuration;

namespace MP.Framework.Web
{
    internal static class Settings
    {
        public static string LogManager
        {
            get
            {
                if (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["LogManager"]))
                {
                    return null;
                }

                return ConfigurationManager.AppSettings["LogManager"];
            }
        }

        public static bool RequireHttps
        {
            get
            {
                if (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["RequireHttps"]))
                {
                    return false;
                }

                return bool.Parse(ConfigurationManager.AppSettings["RequireHttps"]);
            }
        }

        public static int TokenValidTimeSpan
        {
            get
            {
                if (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["TokenValidTimeSpan"]))
                {
                    return 1;
                }

                return Int32.Parse(ConfigurationManager.AppSettings["TokenValidTimeSpan"]);
            }
        }
    }
}
