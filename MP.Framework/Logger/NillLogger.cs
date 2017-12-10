using System;

namespace MP.Framework.Logger
{
    public class NillLogger : ILogger
    {
        public static readonly NillLogger Instance = new NillLogger();

        public bool Enabled
        {
            get
            {
                return true;
            }
        }

        public bool IsDebugEnabled
        {
            get
            {
                return true;
            }
        }

        public bool IsErrorEnabled
        {
            get
            {
                return true;
            }
        }

        public bool IsFatalEnabled
        {
            get
            {
                return true;
            }
        }

        public bool IsInfoEnabled
        {
            get
            {
                return true;
            }
        }

        public bool IsTraceEnabled
        {
            get
            {
                return true;
            }
        }

        public bool IsWarnEnabled
        {
            get
            {
                return true;
            }
        }

        public void Log(LoggerSeverity severity, object entry, params object[] formatParameters)
        {
            // Method intentionally left empty.
        }

        public void LogTrace(object entry, params object[] formatParameters)
        {
            // Method intentionally left empty.
        }

        public void LogDebug(object entry, params object[] formatParameters)
        {
            // Method intentionally left empty.
        }

        public void LogError(object entry, params object[] formatParameters)
        {
            // Method intentionally left empty.
        }

        public void LogFatal(object entry, params object[] formatParameters)
        {
            // Method intentionally left empty.
        }

        public void LogInfo(object entry, params object[] formatParameters)
        {
            // Method intentionally left empty.
        }

        public void LogWarn(object entry, params object[] formatParameters)
        {
            // Method intentionally left empty.
        }

        public void Entry(string methodName, params object[] args)
        {
            // Method intentionally left empty.
        }

        public void Exit(string methodName, params object[] args)
        {
            // Method intentionally left empty.
        }

        public void LogException(Exception ex)
        {
            // Method intentionally left empty.
        }

        public void LogException(Exception ex, LoggerSeverity severity)
        {
            // Method intentionally left empty.
        }

        public void Debug(object value, Exception ex)
        {
            // Method intentionally left empty.
        }

        public void Debug(object value)
        {
            // Method intentionally left empty.
        }

        public void Error(object value, Exception ex)
        {
            // Method intentionally left empty.
        }

        public void Error(object value)
        {
            // Method intentionally left empty.
        }

        public void Fatal(object value, Exception ex)
        {
            // Method intentionally left empty.
        }

        public void Fatal(object value)
        {
            // Method intentionally left empty.
        }

        public void Info(object value, Exception ex)
        {
            // Method intentionally left empty.
        }

        public void Info(object value)
        {
            // Method intentionally left empty.
        }

        public void Trace(object value, Exception ex)
        {
            // Method intentionally left empty.
        }

        public void Trace(object value)
        {
            // Method intentionally left empty.
        }

        public void Warn(object value, Exception ex)
        {
            // Method intentionally left empty.
        }

        public void Warn(object value)
        {
            // Method intentionally left empty.
        }
    }
}
