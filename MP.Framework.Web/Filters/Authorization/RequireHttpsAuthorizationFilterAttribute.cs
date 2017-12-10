using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace MP.Framework.Web.Filters.Authorization
{
    public class RequireHttpsAuthorizationFilterAttribute : AuthorizationFilterAttribute, IOrderedFilter
    {
        public RequireHttpsAuthorizationFilterAttribute(int order = 0)
        {
            Order = order;
        }

        public int Order { get; }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext != null)
            {
                string proto = actionContext.Request.RequestUri.Scheme;

                // This header will be added to requests coming thru AWS Elastic Load Balancer
                if (actionContext.Request.Headers.TryGetValues("X-Forwarded-Proto", out IEnumerable<string> forwardedProtos))
                {
                    proto = forwardedProtos.FirstOrDefault();
                }

                if (proto != Uri.UriSchemeHttps)
                {
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden)
                    {
                        Content = new StringContent("SSL Required"),
                        ReasonPhrase = "SSL Required"
                    };
                }
            }

            base.OnAuthorization(actionContext);
        }
    }
}
