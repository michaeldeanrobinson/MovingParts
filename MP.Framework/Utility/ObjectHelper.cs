using System;
using MP.Framework.Serialization;
using Newtonsoft.Json;

namespace MP.Framework.Utility
{
    public static class ObjectHelper
    {
        private static readonly JsonSerializerSettings SettingsWithoutType = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
        };
        private static IMessageSerializer _serializerWithoutType = new JsonMessageSerializer(SettingsWithoutType);
        private static IMessageSerializer _serializerWithType = new JsonMessageSerializer(true);

        public static string ToJson(object data, bool withType)
        {
            try
            {
                if (withType)
                {
                    return _serializerWithType.MessageToString(data);
                }
                else
                {
                    return _serializerWithoutType.MessageToString(data);
                }
            }
            catch (Exception ex)
            {
                Factory.LogManager.Logger.LogException(ex);

                return $"{{ \"error\": \"Could not serialize {data.GetType()} as JSON. - {ex.Message}\" }}";
            }
        }

        public static string ToJson(object data)
        {
            return ToJson(data, false);
        }

        public static T ToObject<T>(string json, bool withType)
        {
            try
            {
                if (withType)
                {
                    return _serializerWithType.StringToMessage<T>(json);
                }
                else
                {
                    return _serializerWithoutType.StringToMessage<T>(json);
                }
            }
            catch (Exception ex)
            {
                Factory.LogManager.Logger.LogException(ex);

                return default(T);
            }
        }

        public static T ToObject<T>(string json)
        {
            return ToObject<T>(json, false);
        }

        public static T CloneJson<T>(this T source)
        {
            try
            {
                // Don't serialize a null object, simply return the default for that object
                if (source == null)
                {
                    return default(T);
                }

                return _serializerWithoutType.StringToMessage<T>(_serializerWithoutType.MessageToString(source));
            }
            catch (Exception ex)
            {
                Factory.LogManager.Logger.LogException(ex);

                return default(T);
            }
        }
    }
}
