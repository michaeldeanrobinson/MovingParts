using System;
using MP.Framework.Services.MessageHandlers;
using MP.Models;

namespace MP.Framework.Services.Decorators
{
    public class LoggingMessageHandlerDecorator : MessageHandlerDecorator
    {
        public LoggingMessageHandlerDecorator(IMessageHandler messageHandler)
            : base(messageHandler)
        {
        }

        public override IResponseModel Handle(IRequestModel requestModel)
        {
            string classMethodName = $"{requestModel.ModelType} LoggingMessageHandler Handle";

            Factory.LogManager.Logger.Entry(classMethodName);

            try
            {
                return MessageHandler.Handle(requestModel);
            }
            catch (Exception ex)
            {
                Factory.LogManager.Logger.LogError($"{classMethodName} errored.");
                Factory.LogManager.Logger.LogException(ex);

                throw;
            }
            finally
            {
                Factory.LogManager.Logger.Exit(classMethodName);
            }
        }
    }
}
