using System;

namespace MP.Framework.Logger
{
    public enum LoggerSeverity
    {
        None = 0,
        Trace = 10,
        Debug = 20,
        Info = 30,
        Warn = 40,
        Error = 50,
        Fatal = 60,
        Infinity = Int32.MaxValue,
    }
}
