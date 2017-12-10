using System;

namespace MP.Framework.Web.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class IgnoreActionFiltersAttribute : Attribute
    {
    }
}
