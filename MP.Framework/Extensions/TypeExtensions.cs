using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MP.Framework.Extensions
{
    public static class TypeExtensions
    {
        public static IEnumerable<PropertyInfo> GetProperties<TAttribute>(this Type entityType)
        {
            return entityType.GetProperties().Where(prop => prop.IsDefined(typeof(TAttribute), false));
        }
    }
}
