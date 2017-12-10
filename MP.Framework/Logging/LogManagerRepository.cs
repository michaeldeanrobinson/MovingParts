using System;
using System.Collections.Generic;
using MP.Framework.Attributes;
using MP.Framework.Reflection;

namespace MP.Framework.Logging
{
    public static class LogManagerRepository
    {
        private static Dictionary<string, ILogManager> _repository = new Dictionary<string, ILogManager>(StringComparer.CurrentCultureIgnoreCase);

        static LogManagerRepository()
        {
            Type[] types = typeof(LogManagerRepository).Assembly.GetTypes();

            foreach (Type type in types)
            {
                LogManagerRegistrationAttribute attrib = AttributeUtilities.GetAttribute<LogManagerRegistrationAttribute>(type);
                if (attrib == null)
                {
                    continue;
                }

                _repository.Add(attrib.Name, Activator.CreateInstance(type, null) as ILogManager);
            }
        }

        public static ILogManager GetLogManager(string name)
        {
            if (String.IsNullOrWhiteSpace(name) || !_repository.ContainsKey(name))
            {
                return NillLogManager.Instance;
            }

            return _repository[name];
        }
    }
}
