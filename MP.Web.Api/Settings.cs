using System;
using System.Configuration;

namespace MP.Web.Api
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

        public static string AllowedOrigin
        {
            get
            {
                if (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["AllowedOrigin"]))
                {
                    return String.Empty;
                }

                return ConfigurationManager.AppSettings["AllowedOrigin"];
            }
        }
    }
}