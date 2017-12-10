using MP.Framework.Services.Mediator;
using MP.Models;
using MP.Models.Enums;

namespace MP.Framework.Services
{
    public static class ServiceContext
    {
        private static readonly ModelTypes _modelTypes = ModelTypes.Authorization;

        static ServiceContext()
        {
            ModelMediator = new ModelMediator();
        }

        public static IModelMediator ModelMediator { get; }

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
