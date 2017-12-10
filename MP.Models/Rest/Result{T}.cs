using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace MP.Models.Rest
{
    [DataContract(Namespace = "", Name = nameof(Result))]
    [KnownType("GetKnownTypes")] // for serialization
    public class Result<T> : Result
    {
        static Result()
        {
            KnownTypes = new List<Type>
            {
                typeof(Result<T>)
            };

            try
            {
                foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
                {
                    foreach (Type type in assembly.GetTypes())
                    {
                        if (!type.IsAbstract && type.GetInterfaces().Contains(typeof(IResponseModel)))
                        {
                            KnownTypes.Add(type);
                        }
                    }
                }
            }
            catch
            {
                Console.WriteLine("Fatal error!");
            }
        }

        [DataMember(EmitDefaultValue = false, Order = 0)]
        public T Value { get; set; }

        public static implicit operator T(Result<T> v)
        {
            return v.Value;
        }

        public static List<Type> GetKnownTypes()
        {
            return KnownTypes;
        }

        public override bool SetValue(object value)
        {
            try
            {
                Value = (T)value;

                return true;
            }
            catch
            {
                return false;
            }
        }

        public override object GetValue()
        {
            return Value;
        }
    }
}
