using System;
using System.Collections.Generic;

namespace MP.Framework.Utility
{
    public static class Check
    {
        public static T NotNull<T>(T value, string parameterName)
            where T : class
        {
            if (value == null)
            {
                throw new ArgumentNullException(parameterName);
            }

            return value;
        }

        public static T? NotNull<T>(T? value, string parameterName)
            where T : struct
        {
            if (value == null)
            {
                throw new ArgumentNullException(parameterName);
            }

            return value;
        }

        public static string NotEmpty(string value, string parameterName)
        {
            if (String.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException($"A string like \"The argument '{parameterName}' cannot be null, empty or contain only white space.\"");
            }

            return value;
        }

        public static bool NotNullOrEmpty<T>(T? value)
            where T : struct
        {
            if (value == null || EqualityComparer<T>.Default.Equals(value.Value, default(T)))
            {
                return false;
            }

            return true;
        }
    }
}
