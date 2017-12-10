using System;
using System.Collections.Generic;
using MP.Framework.Reflection;
using MP.Framework.Services.Attributes;
using MP.Framework.Services.Decorators;
using MP.Models.Enums;

namespace MP.Framework.Services.MessageHandlers
{
    public class MessageHandlerRepository : IMessageHandlerRepository
    {
        private readonly Dictionary<ModelTypes, IMessageHandler> _repository = new Dictionary<ModelTypes, IMessageHandler>();

        public MessageHandlerRepository(List<Type> types)
        {
            foreach (Type type in types)
            {
                MessageHandlerRegistrationAttribute attrib = AttributeUtilities.GetAttribute<MessageHandlerRegistrationAttribute>(type);

                if (attrib == null)
                {
                    continue;
                }

                IMessageHandler messageHandler = Activator.CreateInstance(type, new object[] { }) as IMessageHandler;

                messageHandler = new LoggingMessageHandlerDecorator(messageHandler);

                _repository.Add(attrib.ModelType, messageHandler);

                // TODO: Add to MessageHandler meta data to database
            }
        }

        public IMessageHandler this[ModelTypes modelType]
        {
            get
            {
                if (!_repository.ContainsKey(modelType))
                {
                    return NillMessageHandler.Instance;
                }

                return _repository[modelType];
            }
        }
    }
}
