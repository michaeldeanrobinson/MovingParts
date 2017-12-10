using System;

namespace MP.Framework.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class LogManagerRegistrationAttribute : Attribute
    {
        public LogManagerRegistrationAttribute(string id, string name)
        {
            Id = new Guid(id);
            Name = name;
        }

        public Guid Id { get; }
        public string Name { get; }
    }
}
