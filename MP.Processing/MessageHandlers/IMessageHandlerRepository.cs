using MP.Models.Enums;

namespace MP.Framework.Services.MessageHandlers
{
    public interface IMessageHandlerRepository
    {
        IMessageHandler this[ModelTypes modelType] { get; }
    }
}