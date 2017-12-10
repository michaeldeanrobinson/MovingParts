using MP.Framework.Services.Attributes;
using MP.Models;
using MP.Models.Authorization.Models;
using MP.Models.Enums;

namespace MP.Framework.Services.MessageHandlers
{
    [MessageHandlerRegistration(ModelTypes.AuthorizationToken, "Authorization Message Handler", "{2DBA46BF-5105-4D7A-91C2-82CDD045FD1E}")]
    public class AuthorizationMessageHandler : IMessageHandler
    {
        public IResponseModel Handle(IRequestModel requestModel)
        {
            // Call all Processors that need to know about this model
            AuthorizationTokenResponseModel responseModel = Factory.TokenProcessor.Execute(requestModel) as AuthorizationTokenResponseModel;

            return responseModel;
        }
    }
}
