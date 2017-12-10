using System;
using MP.Models;

namespace MP.Framework.Services.MessageHandlers
{
    public class NillMessageHandler : IMessageHandler
    {
        public static readonly NillMessageHandler Instance = new NillMessageHandler();

        public IResponseModel Handle(IRequestModel requestModel)
        {
            throw new ArgumentException("MessageHandler Received an unrecognized model.");
        }
    }
}
