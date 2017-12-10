using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using MP.Models.Rest;
using MP.Models.Security;
using MP.Framework.Web.Security;

namespace MP.Framework.Web.Filters.Action
{
    public class AuthenticationTokenRequiredActionFilterAttribute : ActionFilterAttribute, IOrderedFilter
    {
        private readonly ISecurityManager _securityManager;

        public AuthenticationTokenRequiredActionFilterAttribute(ISecurityManager securityManager, int order = 0)
        {
            _securityManager = securityManager;
            Order = order;
        }

        public int Order { get; }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            ICollection<AllowAnonymousAttribute> attributes = actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>();
            AuthenticationHeaderValue authHeader = actionContext.Request.Headers.Authorization;

            if (actionContext.Request.Method == HttpMethod.Options || attributes.Count > 0)
            {
                actionContext.RequestContext.Principal = SetCurrentPrincipal("00000000-0000-0000-0000-000000000000");

                base.OnActionExecuting(actionContext);

                return;
            }

            if (authHeader != null)
            {
                AuthenticationToken authToken = _securityManager.DecryptAuthenticationToken(authHeader.Parameter);

                if (authToken == null)
                {
                    actionContext.Response = GetBadRequestResultError();
                    return;
                }

                string forwardedIp = String.Empty;

                if (actionContext.Request.Headers.Contains("X-Forwarded-For"))
                {
                    forwardedIp = actionContext.Request.Headers.GetValues("X-Forwarded-For")?.FirstOrDefault();
                }
                else
                {
                    forwardedIp = actionContext.Request.GetClientIp();
                }

                if (_securityManager.IsValidToken(authToken, actionContext.Request.GetUserAgent(), forwardedIp))
                {
                    string userId = _securityManager.GetUserIdFromToken(authToken).ToString();
                    actionContext.RequestContext.Principal = SetCurrentPrincipal(userId);

                    base.OnActionExecuting(actionContext);

                    return;
                }
            }

            actionContext.Response = GetBadRequestResultError();
        }

        private HttpResponseMessage GetBadRequestResultError()
        {
            return new HttpResponseMessage(HttpStatusCode.Unauthorized)
            {
                Content = new StringContent(ResultHandler.CreateResultError("Authentication Token is bad, please request a new Authentication Token", 401, ErrorLevel.Security, ErrorType.Fatal).ToString()),
            };
        }

        private IPrincipal SetCurrentPrincipal(string userId)
        {
            GenericPrincipal currentPrincipal = new GenericPrincipal(new GenericIdentity(userId), null);
            return currentPrincipal;
        }
    }
}
