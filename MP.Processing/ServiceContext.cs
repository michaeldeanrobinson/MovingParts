using System;
using System.Collections.Generic;
using MP.Framework.Reflection;
using MP.Framework.Services.Mediator;
using MP.Framework.Services.MessageHandlers;
using MP.Models;
using MP.Models.Enums;

namespace MP.Framework.Services
{
    public static class ServiceContext
    {
        private static readonly ModelTypes _modelTypes = ModelTypes.AuthorizationToken;

        static ServiceContext()
        {
            List<Type> types = AssemblyUtilities.GetTypes("MP.*.dll", "MP.");

            MessageHandlerRepository = new MessageHandlerRepository(types);
            ModelMediator = new ModelMediator();
        }

        public static IModelMediator ModelMediator { get; }
        public static IMessageHandlerRepository MessageHandlerRepository { get; }

        public static bool ShouldBeLogged(IRequestModel requestModel)
        {
            if (requestModel != null)
            {
                return !_modelTypes.HasFlag(requestModel.ModelType);
            }

            return false;
        }

        public static bool ShouldBeLogged(IResponseModel responseModel)
        {
            if (responseModel != null)
            {
                return !_modelTypes.HasFlag(responseModel.ModelType);
            }

            return false;
        }
    }
}
