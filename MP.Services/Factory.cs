using MP.Framework.Logging;

namespace MP.Services
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
