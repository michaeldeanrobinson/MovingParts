using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using MP.Models.Authorization.Models;
using MP.Models.Rest;

namespace MP.Web.Api.Controllers
{
    /// <summary>
    /// AuthorizationController
    /// </summary>
    [RoutePrefix("security/authorization")]
    public class AuthorizationController : BaseController
    {
        /// <summary>
        /// Generate an AuthenticationToken for use with all other endpoints
        /// </summary>
        /// <param name="requestModel">An instance of the login credentials</param>
        /// <returns>The Result that contains an AuthenticationToken</returns>
        /// <remarks>
        /// By default the token will expire in 24 hours
        /// </remarks>
        [HttpPost]
        [Route("tokenrequest")]
        [AllowAnonymous]
        public async Task<Result<TokenRequestAuthorizationResponseModel>> LoginRequest([FromBody]TokenRequestAuthorizationRequestModel requestModel)
        {
            Factory.LogManager.Logger.Entry($"AuthorizationController.LoginRequest");

            requestModel.UserAgent = Request.GetUserAgent();

            if (Request.Headers.Contains("X-Forwarded-For"))
            {
                requestModel.ClientIp = Request.Headers.GetValues("X-Forwarded-For")?.FirstOrDefault();
            }
            else
            {
                requestModel.ClientIp = Request.GetClientIp();
            }

            // process the request
            TokenRequestAuthorizationResponseModel responseModel = await ProcessRequest<TokenRequestAuthorizationRequestModel, TokenRequestAuthorizationResponseModel>(requestModel);

            // validate response
            if (String.IsNullOrWhiteSpace(responseModel?.AuthenticationToken))
            {
                return ResultHandler.CreateResultError<TokenRequestAuthorizationResponseModel>("Invalid login request, please use a valid username and password and try again.", 400, ErrorLevel.Data, ErrorType.Fatal);
            }

            Factory.LogManager.Logger.Exit($"AuthorizationController.LoginRequest");

            return new Result<TokenRequestAuthorizationResponseModel> { Value = responseModel };
        }
    }
}