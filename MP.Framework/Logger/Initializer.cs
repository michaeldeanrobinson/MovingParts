using System.Configuration;

namespace MP.Framework.Logger
{
    public static class Initializer
    {
        private static bool _initialized = false;

        public static void Initialize()
        {
            if (_initialized)
            {
                return;
            }

            _initialized = true;

            // singleton settings no return value
            ConfigurationManager.GetSection("Framework/Logging");
            log4net.Config.XmlConfigurator.Configure();
        }
    }
}
