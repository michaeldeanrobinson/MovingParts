using MP.Framework.Logger;

namespace MP.Framework.Logging
{
    public class NillLogManager : ILogManager
    {
        internal static readonly NillLogManager Instance = new NillLogManager();

        public ILogger Logger
        {
            get { return NillLogger.Instance; }
        }

        public ILogger ErrorLogger
        {
            get { return NillLogger.Instance; }
        }

        public ILogger DatabaseLogger
        {
            get { return NillLogger.Instance; }
        }
    }
}
