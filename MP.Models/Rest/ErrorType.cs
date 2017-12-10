using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MP.Models.Rest
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ErrorType
    {
        Info = 1,
        Warning,
        Error,
        Fatal,
    }
}
