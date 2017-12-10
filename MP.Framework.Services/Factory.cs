using MP.Framework.Logging;
using MP.Framework.Services.Processors;

namespace MP.Framework.Services
{
    internal static class Factory
    {
        static Factory()
        {
            LogManager = LogManagerRepository.GetLogManager(Settings.LogManager);

            TokenProcessor = new TokenProcessor();
        }

        public static ILogManager LogManager { get; }

        public static IProcessor TokenProcessor { get; }
    }
}
