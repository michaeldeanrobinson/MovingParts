using MP.Framework.Logging;
using MP.Framework.Services.Processors;
using MP.Services;

namespace MP.Framework.Services
{
    internal static class Factory
    {
        static Factory()
        {
            LogManager = LogManagerRepository.GetLogManager(Settings.LogManager);

            // Processors
            TokenProcessor = new TokenProcessor();

            // Services
            UserService = new UserService();
        }

        public static ILogManager LogManager { get; }

        public static IProcessor TokenProcessor { get; }
        public static UserService UserService { get; }
    }
}
