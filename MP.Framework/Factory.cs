﻿using MP.Framework.Logging;

namespace MP.Framework
{
    internal static class Factory
    {
        static Factory()
        {
            LogManager = LogManagerRepository.GetLogManager(Settings.LogManager);
        }

        public static ILogManager LogManager { get; }
    }
}
