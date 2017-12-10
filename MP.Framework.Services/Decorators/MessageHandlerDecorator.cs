using MP.Framework.Services.MessageHandlers;
using MP.Models;

namespace MP.Framework.Services.Decorators
{
    public abstract class MessageHandlerDecorator : IMessageHandler
    {
        protected MessageHandlerDecorator(IMessageHandler messageHandler)
        {
            MessageHandler = messageHandler;
        }

        protected IMessageHandler MessageHandler { get; }

        public abstract IResponseModel Handle(IRequestModel requestModel);
    }
}
