namespace MP.Framework.Serialization
{
    public interface IMessageSerializer
    {
        string MessageToString(object message);
        T StringToMessage<T>(string json);
    }
}
