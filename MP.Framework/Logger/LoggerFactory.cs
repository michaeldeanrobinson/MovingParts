using System;
using System.Collections.Generic;
using System.Reflection;

namespace MP.Framework.Logger
{
    public static class LoggerFactory
    {
        private static Dictionary<string, ILogger> _namedLoggers = new Dictionary<string, ILogger>();
        private static Dictionary<Type, ILogger> _typedLoggers = new Dictionary<Type, ILogger>();
        private static Dictionary<Assembly, ILogger> _assemblyLoggers = new Dictionary<Assembly, ILogger>();
        private static ILogger _defaultLogger = NillLogger.Instance;

        public static ILogger DefaultLogger
        {
            get
            {
                Initializer.Initialize();

                return _defaultLogger;
            }
        }

        public static ILogger CreateLogger(string name)
        {
            Initializer.Initialize();

            if (_namedLoggers.ContainsKey(name))
            {
                return _namedLoggers[name];
            }

            return _defaultLogger;
        }

        public static ILogger CreateLogger(Type type, string name)
        {
            Initializer.Initialize();

            if (_typedLoggers.ContainsKey(type))
            {
                return _typedLoggers[type];
            }

            if (_assemblyLoggers.ContainsKey(type.Assembly))
            {
                return _assemblyLoggers[type.Assembly];
            }

            return CreateLogger(name);
        }

        public static ILogger CreateLogger(Type type)
        {
            Initializer.Initialize();

            if (_typedLoggers.ContainsKey(type))
            {
                return _typedLoggers[type];
            }

            if (_assemblyLoggers.ContainsKey(type.Assembly))
            {
                return _assemblyLoggers[type.Assembly];
            }

            return _defaultLogger;
        }

        public static ILogger CreateLogger(Assembly assembly, string name)
        {
            Initializer.Initialize();

            if (_assemblyLoggers.ContainsKey(assembly))
            {
                return _assemblyLoggers[assembly];
            }

            return CreateLogger(name);
        }

        public static ILogger CreateLogger(Assembly assembly)
        {
            Initializer.Initialize();

            if (_assemblyLoggers.ContainsKey(assembly))
            {
                return _assemblyLoggers[assembly];
            }

            return _defaultLogger;
        }

        internal static void AddLogger(string name, ILogger logger)
        {
            if (name == null)
            {
                return;
            }

            if (_namedLoggers.ContainsKey(name))
            {
                return;
            }

            if (_namedLoggers.Count == 0)
            {
                _defaultLogger = logger;
            }

            _namedLoggers.Add(name, logger);
        }

        internal static void AddLogger(Assembly name, ILogger logger)
        {
            if (name == null)
            {
                return;
            }

            if (_assemblyLoggers.ContainsKey(name))
            {
                return;
            }

            _assemblyLoggers.Add(name, logger);
        }

        internal static void AddLogger(Type name, ILogger logger)
        {
            if (name == null)
            {
                return;
            }

            if (_typedLoggers.ContainsKey(name))
            {
                return;
            }

            _typedLoggers.Add(name, logger);
        }

        internal static void SetDefault(string name)
        {
            if (name != null && _namedLoggers.ContainsKey(name))
            {
                _defaultLogger = _namedLoggers[name];
            }
        }
    }
}
