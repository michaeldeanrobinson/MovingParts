using System.Web;
using System.Web.Http;
using System.Web.Http.Hosting;
using MP.Framework.Web;

namespace System.Net.Http
{
    public static class HttpRequestMessageExtensions
    {
        public static string GetClientIp(this HttpRequestMessage request)
        {
            return ((HttpContextWrapper)request.Properties["MS_HttpContext"]).Request.UserHostAddress;
        }

        public static string GetUserAgent(this HttpRequestMessage request)
        {
            return ((HttpContextWrapper)request.Properties["MS_HttpContext"]).Request.UserAgent;
        }

        public static string GetSitePath(this HttpRequestMessage request)
        {
            string protocol = Settings.RequireHttps ? "https://" : "http://";
            string path = ((HttpConfiguration)request.Properties[HttpPropertyKeys.HttpConfigurationKey]).VirtualPathRoot;

            return $"{protocol}{request.RequestUri.Authority}{path}/";
        }
    }
}
