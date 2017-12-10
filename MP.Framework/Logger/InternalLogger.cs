using System;
using System.Collections.Generic;

namespace MP.Framework.Logger
{
    internal class InternalLogger : ILogger
    {
        private readonly List<ILogger> _loggers = new List<ILogger>();
        private LoggerSeverity _threshold = LoggerSeverity.None;

        public bool Enabled
        {
            get { return (int)_threshold <= (int)LoggerSeverity.None; }
        }

        public bool IsDebugEnabled
        {
            get { return (int)_threshold <= (int)LoggerSeverity.Debug; }
        }

        public bool IsErrorEnabled
        {
            get { return (int)_threshold <= (int)LoggerSeverity.Error; }
        }

        public bool IsFatalEnabled
        {
            get { return (int)_threshold <= (int)LoggerSeverity.Fatal; }
        }

        public bool IsInfoEnabled
        {
            get { return (int)_threshold <= (int)LoggerSeverity.Info; }
        }

        public bool IsTraceEnabled
        {
            get { return (int)_threshold <= (int)LoggerSeverity.Trace; }
        }

        public bool IsWarnEnabled
        {
            get { return (int)_threshold <= (int)LoggerSeverity.Warn; }
        }

        public void Log(LoggerSeverity severity, object entry, params object[] formatParameters)
        {
            foreach (ILogger logger in _loggers)
            {
                logger.Log(severity, entry, formatParameters);
            }
        }

        public void LogDebug(object entry, params object[] formatParameters)
        {
            if (!IsDebugEnabled)
            {
                return;
            }

            Log(LoggerSeverity.Debug, entry, formatParameters);
        }

        public void Debug(object value, Exception ex)
        {
            if (!IsDebugEnabled)
            {
                return;
            }

            Log(LoggerSeverity.Debug, value, null);
            LogException(ex, LoggerSeverity.Debug);
        }

        public void Debug(object value)
        {
            if (!IsDebugEnabled)
            {
                return;
            }

            Log(LoggerSeverity.Debug, value, null);
        }

        public void LogError(object entry, params object[] formatParameters)
        {
            if (!IsErrorEnabled)
            {
                return;
            }

            Log(LoggerSeverity.Error, entry, formatParameters);
        }

        public void Error(object value, Exception ex)
        {
            if (!IsErrorEnabled)
            {
                return;
            }

            Log(LoggerSeverity.Error, value, null);
            LogException(ex, LoggerSeverity.Error);
        }

        public void Error(object value)
        {
            if (!IsErrorEnabled)
            {
                return;
            }

            Log(LoggerSeverity.Error, value, null);
        }

        public void LogFatal(object entry, params object[] formatParameters)
        {
            if (!IsFatalEnabled)
            {
                return;
            }

            Log(LoggerSeverity.Fatal, entry, formatParameters);
        }

        public void Fatal(object value, Exception ex)
        {
            if (!IsFatalEnabled)
            {
                return;
            }

            Log(LoggerSeverity.Fatal, value, null);
            LogException(ex, LoggerSeverity.Fatal);
        }

        public void Fatal(object value)
        {
            if (!IsFatalEnabled)
            {
                return;
            }

            Log(LoggerSeverity.Fatal, value, null);
        }

        public void LogInfo(object entry, params object[] formatParameters)
        {
            if (!IsInfoEnabled)
            {
                return;
            }

            Log(LoggerSeverity.Info, entry, formatParameters);
        }

        public void Info(object value, Exception ex)
        {
            if (!IsInfoEnabled)
            {
                return;
            }

            Log(LoggerSeverity.Info, value, null);
            LogException(ex, LoggerSeverity.Info);
        }

        public void Info(object value)
        {
            if (!IsInfoEnabled)
            {
                return;
            }

            Log(LoggerSeverity.Info, value, null);
        }

        public void LogWarn(object entry, params object[] formatParameters)
        {
            if (!IsWarnEnabled)
            {
                return;
            }

            Log(LoggerSeverity.Warn, entry, formatParameters);
        }

        public void Warn(object value, Exception ex)
        {
            if (!IsWarnEnabled)
            {
                return;
            }

            Log(LoggerSeverity.Warn, value, null);
            LogException(ex, LoggerSeverity.Warn);
        }

        public void Warn(object value)
        {
            if (!IsWarnEnabled)
            {
                return;
            }

            Log(LoggerSeverity.Warn, value, null);
        }

        public void LogException(Exception ex)
        {
            if (!IsErrorEnabled)
            {
                return;
            }

            foreach (ILogger logger in _loggers)
            {
                logger.LogException(ex);
            }
        }

        public void LogException(Exception ex, LoggerSeverity severity)
        {
            if ((int)severity < (int)_threshold)
            {
                return;
            }

            foreach (ILogger logger in _loggers)
            {
                logger.LogException(ex, severity);
            }
        }

        public void Entry(string methodName, params object[] args)
        {
            if (!IsTraceEnabled)
            {
                return;
            }

            foreach (ILogger logger in _loggers)
            {
                logger.Entry(methodName, args);
            }
        }

        public void Exit(string methodName, params object[] args)
        {
            if (!IsTraceEnabled)
            {
                return;
            }

            foreach (ILogger logger in _loggers)
            {
                logger.Exit(methodName, args);
            }
        }

        public void LogTrace(object entry, params object[] formatParameters)
        {
            if (!IsDebugEnabled)
            {
                return;
            }

            Log(LoggerSeverity.Trace, entry, formatParameters);
        }

        public void Trace(object value, Exception ex)
        {
            Log(LoggerSeverity.Trace, value, null);
            LogException(ex, LoggerSeverity.Trace);
        }

        public void Trace(object value)
        {
            Log(LoggerSeverity.Trace, value, null);
        }

        internal void AddLogger(ILogger logger)
        {
            _loggers.Add(logger);
        }

        internal void SetThreshold(LoggerSeverity threshold)
        {
            _threshold = threshold;
        }
    }
}
