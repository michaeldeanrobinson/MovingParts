using System;
using System.Diagnostics;
using MP.Framework.Security.Encryption;
using MP.Framework.Utility;
using MP.Framework.Web.Security;
using MP.Models;
using MP.Models.Authorization.Models;
using MP.Models.Rest;
using MP.Models.Security;
using MP.Services.Entities;

namespace MP.Framework.Services.Processors
{
    public class TokenProcessor : IProcessor
    {
        private static readonly AesHelper _aes = new AesHelper();
        private static readonly Guid _debugUserId = new Guid("261e32f5-b253-4291-8301-0f43683c282f");

        public IResponseModel Execute(IRequestModel requestModel)
        {
            AuthorizationTokenRequestModel model = requestModel as AuthorizationTokenRequestModel;

            if (Debugger.IsAttached && model.Username.Equals("debugUsername") && model.Password.Equals("debugPassword"))
            {
                return GenerateAuthTokenResponse(model, _debugUserId);
            }

            // Look up username and password in database
            UserEntity userEntityResponse = Factory.UserService.FindByUsernameAndPassword(model.Username, model.Password);

            if (userEntityResponse != null && userEntityResponse.UserId != Guid.Empty)
            {
                return GenerateAuthTokenResponse(model, userEntityResponse.UserId);
            }

            return CreateErrorResponse(new Exception("Invalid login request, please use a valid username and password and try again."));

        }

        private AuthorizationTokenResponseModel GenerateAuthTokenResponse(AuthorizationTokenRequestModel requestModel, Guid userId)
        {
            DateTime timestamp = DateTime.UtcNow;

            AuthenticationToken token = new AuthenticationToken
            {
                UTCTimestamp = timestamp.ToString("O"),
                UserId = userId, // the UserId on the Model can be the anonymous UserId, not the one found in the DB, so this must be passed in to get the one from the DB
                Username = requestModel.Username,
                Password = requestModel.Password,
                UserAgent = requestModel.UserAgent,
                IPAddress = requestModel.ClientIp,
            };

            string json = ObjectHelper.ToJson(token);
            Factory.LogManager.Logger.LogInfo("Serialized: {0}", json);

            return new AuthorizationTokenResponseModel()
            {
                AuthenticationToken = _aes.Encrypt(json),
                Expiration = timestamp.Add(SecurityManager.TokenValidTimeSpan),
            };
        }

        private AuthorizationTokenResponseModel CreateErrorResponse(Exception exception)
        {
            return new AuthorizationTokenResponseModel()
            {
                Error = ResultHandler.CreateError(exception.Message, 400, ErrorLevel.Data, ErrorType.Fatal),
            };
        }
    }
}
