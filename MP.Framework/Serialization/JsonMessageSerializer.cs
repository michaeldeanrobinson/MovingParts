using MP.Framework.Serialization.Resolvers;
using Newtonsoft.Json;

namespace MP.Framework.Serialization
{
    public class JsonMessageSerializer : IMessageSerializer
    {
        private static readonly JsonSerializerSettings SettingsWithType =
            new JsonSerializerSettings
            {
                ContractResolver = new IgnoreDataContractContractResolver(),
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                TypeNameHandling = TypeNameHandling.Objects,
            };

        private static readonly JsonSerializerSettings SettingsWithoutType = new JsonSerializerSettings
        {
            ContractResolver = new IgnoreDataContractContractResolver(),
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
        };

        private readonly JsonSerializerSettings _settings;

        public JsonMessageSerializer(bool withType = true)
        {
            if (withType)
            {
                _settings = SettingsWithType;
            }
            else
            {
                _settings = SettingsWithoutType;
            }
        }

        public JsonMessageSerializer(JsonSerializerSettings settings)
        {
            _settings = settings;
        }

        public string MessageToString(object message)
        {
            return JsonConvert.SerializeObject(message, _settings);
        }

        public T StringToMessage<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json, _settings);
        }
    }
}
