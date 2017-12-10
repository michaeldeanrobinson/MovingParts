using MP.Framework.Attributes;
using MP.Framework.Logger;

namespace MP.Framework.Logging
{
    [LogManagerRegistration("{DC521397-F390-487D-A0BF-DCEFEE44D477}", "LogManager")]
    public class LogManager : ILogManager
    {
        public ILogger Logger
        {
            get { return LoggerFactory.CreateLogger("DefaultLogger"); }
        }

        public ILogger ErrorLogger
        {
            get { return LoggerFactory.CreateLogger("ErrorLogger"); }
        }

        public ILogger DatabaseLogger
        {
            get { return LoggerFactory.CreateLogger("DatabaseLogger"); }
        }
    }
}
