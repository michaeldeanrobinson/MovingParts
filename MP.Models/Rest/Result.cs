using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using MP.Framework.Utility;
using Newtonsoft.Json;

namespace MP.Models.Rest
{
    [DataContract(Namespace = "")]
    public class Result : IResult
    {
        internal static List<Type> KnownTypes { get; set; }

        public TimeSpan ExecutionTime { get; set; }

        [DataMember(Name = nameof(ExecutionTime), Order = 900000002)]
        public string ExecutionTimeString
        {
            get
            {
                return ExecutionTime.ToString();
            }

            set { /* This property is readonly. Empty braces are needed for Protobuffer support */ }
        }

        [DataMember(EmitDefaultValue = false, Order = 900000001)]
        public Error Error { get; set; }

        [DataMember(Order = 900000000)]
        public virtual bool Success
        {
            get
            {
                return this.Error == null;
            }

            set { /* This property is readonly. Empty braces are needed for Protobuffer support */ }
        }

        [JsonProperty(Order = 900000003)]
        [IgnoreDataMember]
        public Guid ProcessTag { get; set; }

        public virtual bool SetValue(object value)
        {
            return false;
        }

        public virtual object GetValue()
        {
            return null;
        }

        public override string ToString()
        {
            return ObjectHelper.ToJson(this);
        }
    }
}
