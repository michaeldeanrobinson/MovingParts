using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MP.Models.Rest
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ErrorLevel
    {
        API = 1,
        Data,
        Service,
        Database,
        Security,
    }
}
