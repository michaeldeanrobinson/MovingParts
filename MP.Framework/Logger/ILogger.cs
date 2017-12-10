using System;

namespace MP.Framework.Logger
{
    public interface ILogger
    {
        bool Enabled { get; }
        bool IsDebugEnabled { get; }
        bool IsErrorEnabled { get; }
        bool IsFatalEnabled { get; }
        bool IsInfoEnabled { get; }
        bool IsTraceEnabled { get; }
        bool IsWarnEnabled { get; }

        /// <summary>
        /// Logs a message
        /// </summary>
        /// <param name="severity"></param>
        /// <param name="entry"></param>
        /// <param name="formatParameters"></param>
        void Log(LoggerSeverity severity, object entry, params object[] formatParameters);

        /// <summary>
        /// Logs a trace message
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="formatParameters"></param>
        void LogTrace(object entry, params object[] formatParameters);

        /// <summary>
        /// Logs a debug message
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="formatParameters"></param>
        void LogDebug(object entry, params object[] formatParameters);

        /// <summary>
        /// Logs an error message
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="formatParameters"></param>
        void LogError(object entry, params object[] formatParameters);

        /// <summary>
        /// Logs a fatal message
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="formatParameters"></param>
        void LogFatal(object entry, params object[] formatParameters);

        /// <summary>
        /// Logs an Information message
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="formatParameters"></param>
        void LogInfo(object entry, params object[] formatParameters);

        /// <summary>
        /// Logs a warning message
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="formatParameters"></param>
        void LogWarn(object entry, params object[] formatParameters);

        /// <summary>
        /// Dumps the entry.
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="args">The args.</param>
        void Entry(string methodName, params object[] args);

        /// <summary>
        /// Exits
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="args">The args.</param>
        void Exit(string methodName, params object[] args);

        /// <summary>
        /// Logs the exception.
        /// </summary>
        /// <param name="ex">The ex.</param>
        void LogException(Exception ex);

        /// <summary>
        /// Logs the exception.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <param name="severity">The severity.</param>
        void LogException(Exception ex, LoggerSeverity severity);

        void Debug(object value, Exception ex);
        void Debug(object value);
        void Error(object value, Exception ex);
        void Error(object value);
        void Fatal(object value, Exception ex);
        void Fatal(object value);
        void Info(object value, Exception ex);
        void Info(object value);
        void Trace(object value, Exception ex);
        void Trace(object value);
        void Warn(object value, Exception ex);
        void Warn(object value);
    }
}
