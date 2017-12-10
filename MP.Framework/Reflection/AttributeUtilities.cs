using System;
using System.Reflection;
using MP.Framework.Extensions;

namespace MP.Framework.Reflection
{
    public static class AttributeUtilities
    {
        public static T GetAttribute<T>(ICustomAttributeProvider attributeProvider)
            where T : Attribute
        {
            return GetAttribute<T>(attributeProvider, true);
        }

        public static T GetAttribute<T>(ICustomAttributeProvider attributeProvider, bool inherit)
            where T : Attribute
        {
            T[] attributes = GetAttributes<T>(attributeProvider, inherit);

            return attributes.GetSingleItem(true);
        }

        public static T[] GetAttributes<T>(ICustomAttributeProvider attributeProvider, bool inherit)
            where T : Attribute
        {
            Utility.Check.NotNull(attributeProvider, "attributeProvider");

            return (T[])attributeProvider.GetCustomAttributes(typeof(T), inherit);
        }
    }
}
