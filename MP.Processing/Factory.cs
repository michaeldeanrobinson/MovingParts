using AutoMapper;
using MP.Framework.Logging;
using MP.Framework.Services.Processors;
using MP.Processing.AutoMapper;
using MP.Services;

namespace MP.Framework.Services
{
    internal static class Factory
    {
        static Factory()
        {
            LogManager = LogManagerRepository.GetLogManager(Settings.LogManager);
            Mapper = AutoMapperConfiguration.Configure().CreateMapper();

            // Processors
            TokenProcessor = new TokenProcessor();

            // Services
            UserService = new UserService();
        }

        public static ILogManager LogManager { get; }
        public static IMapper Mapper { get; internal set; }

        public static IProcessor TokenProcessor { get; }
        public static UserService UserService { get; }
    }
}
