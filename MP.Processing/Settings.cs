﻿using System;
using System.Configuration;

namespace MP.Framework.Services
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
    }
}
