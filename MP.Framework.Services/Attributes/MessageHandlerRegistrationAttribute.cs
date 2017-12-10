using System;
using MP.Models.Enums;

namespace MP.Framework.Services.Attributes
{
    public class MessageHandlerRegistrationAttribute : Attribute
    {
        public MessageHandlerRegistrationAttribute(ModelTypes modelType, string messageHandlerName, string messageHandlerId)
        {
            ModelType = modelType;
            MessageHandlerName = messageHandlerName;
            MessageHandlerId = new Guid(messageHandlerId);
        }

        public ModelTypes ModelType { get; }
        public string MessageHandlerName { get; }
        public Guid MessageHandlerId { get; }
    }
}
