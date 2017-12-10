using System;
using System.Text;
using log4net;

namespace MP.Framework.Logger
{
    internal class Log4NetLogger : ILogger
    {
        private readonly ILog _logger = null;

        internal Log4NetLogger(string name)
        {
            _logger = log4net.LogManager.GetLogger(name);
        }

        public bool IsDebugEnabled
        {
            get { return _logger.IsDebugEnabled; }
        }

        public bool IsErrorEnabled
        {
            get { return _logger.IsErrorEnabled; }
        }

        public bool IsFatalEnabled
        {
            get { return _logger.IsFatalEnabled; }
        }

        public bool IsInfoEnabled
        {
            get { return _logger.IsInfoEnabled; }
        }

        public bool IsTraceEnabled
        {
            get { return _logger.IsDebugEnabled; }
        }

        public bool IsWarnEnabled
        {
            get { return _logger.IsWarnEnabled; }
        }

        public bool Enabled
        {
            get { return true; }
        }

        public void Log(LoggerSeverity severity, object entry, params object[] formatParameters)
        {
            switch (severity)
            {
                case LoggerSeverity.Debug:
                    LogDebug(entry, formatParameters);
                    break;
                case LoggerSeverity.Error:
                    LogError(entry, formatParameters);
                    break;
                case LoggerSeverity.Fatal:
                    LogFatal(entry, formatParameters);
                    break;
                case LoggerSeverity.Infinity:
                    LogFatal(entry, formatParameters);
                    break;
                case LoggerSeverity.Info:
                    LogInfo(entry, formatParameters);
                    break;
                case LoggerSeverity.None:
                    break;
                case LoggerSeverity.Trace:
                    LogDebug(entry, formatParameters);
                    break;
                case LoggerSeverity.Warn:
                    LogWarn(entry, formatParameters);
                    break;
                default:
                    break;
            }
        }

        public void LogTrace(object entry, params object[] formatParameters)
        {
            if (formatParameters == null)
            {
                _logger.Debug(entry);
            }
            else
            {
                _logger.DebugFormat(entry.ToString(), formatParameters);
            }
        }

        public void LogDebug(object entry, params object[] formatParameters)
        {
            if (formatParameters == null)
            {
                _logger.Debug(entry);
            }
            else
            {
                _logger.DebugFormat(entry.ToString(), formatParameters);
            }
        }

        public void LogError(object entry, params object[] formatParameters)
        {
            if (formatParameters == null)
            {
                _logger.Error(entry);
            }
            else
            {
                _logger.ErrorFormat(entry.ToString(), formatParameters);
            }
        }

        public void LogFatal(object entry, params object[] formatParameters)
        {
            if (formatParameters == null)
            {
                _logger.Fatal(entry);
            }
            else
            {
                _logger.FatalFormat(entry.ToString(), formatParameters);
            }
        }

        public void LogInfo(object entry, params object[] formatParameters)
        {
            if (formatParameters == null)
            {
                _logger.Info(entry);
            }
            else
            {
                _logger.InfoFormat(entry.ToString(), formatParameters);
            }
        }

        public void LogWarn(object entry, params object[] formatParameters)
        {
            if (formatParameters == null)
            {
                _logger.Warn(entry);
            }
            else
            {
                _logger.WarnFormat(entry.ToString(), formatParameters);
            }
        }

        public void Entry(string methodName, params object[] args)
        {
            StringBuilder sb = new StringBuilder(32);
            sb.Append("> ");
            sb.Append(methodName);
            if (args != null && args.Length > 0)
            {
                sb.Append(": ");
                BuildArgList(sb, args);
            }

            _logger.Debug(sb.ToString());
        }

        public void Exit(string methodName, params object[] args)
        {
            StringBuilder sb = new StringBuilder(32);
            sb.Append("< ");
            sb.Append(methodName);
            _logger.Debug(sb.ToString());
        }

        public void LogException(Exception ex)
        {
            LogException(ex, LoggerSeverity.Error);
        }

        public void LogException(Exception ex, LoggerSeverity severity)
        {
            switch (severity)
            {
                case LoggerSeverity.Debug:
                    _logger.Debug(ex);
                    break;
                case LoggerSeverity.Error:
                    _logger.Error(ex);
                    break;
                case LoggerSeverity.Fatal:
                    _logger.Fatal(ex);
                    break;
                case LoggerSeverity.Infinity:
                    _logger.Fatal(ex);
                    break;
                case LoggerSeverity.Info:
                    _logger.Info(ex);
                    break;
                case LoggerSeverity.None:
                    break;
                case LoggerSeverity.Trace:
                    _logger.Debug(ex);
                    break;
                case LoggerSeverity.Warn:
                    _logger.Warn(ex);
                    break;
                default:
                    break;
            }
        }

        public void Debug(object value, Exception ex)
        {
            _logger.Debug(value, ex);
        }

        public void Debug(object value)
        {
            _logger.Debug(value);
        }

        public void Error(object value, Exception ex)
        {
            _logger.Error(value, ex);
        }

        public void Error(object value)
        {
            _logger.Error(value);
        }

        public void Fatal(object value, Exception ex)
        {
            _logger.Fatal(value, ex);
        }

        public void Fatal(object value)
        {
            _logger.Fatal(value);
        }

        public void Info(object value, Exception ex)
        {
            _logger.Info(value, ex);
        }

        public void Info(object value)
        {
            _logger.Info(value);
        }

        public void Trace(object value, Exception ex)
        {
            _logger.Debug(value, ex);
        }

        public void Trace(object value)
        {
            _logger.Debug(value);
        }

        public void Warn(object value, Exception ex)
        {
            _logger.Warn(value, ex);
        }

        public void Warn(object value)
        {
            _logger.Warn(value);
        }

        private static void BuildArgList(StringBuilder sb, object[] args)
        {
            if (args != null)
            {
                sb.Append(" (");
                for (int i = 0; i < args.Length; i++)
                {
                    if (i > 0)
                    {
                        sb.Append(", ");
                    }

                    if (args[i] == null)
                    {
                        sb.Append("[null]");
                    }
                    else
                    {
                        sb.Append(args[i]);
                    }
                }

                sb.Append(")");
            }
        }
    }
}
