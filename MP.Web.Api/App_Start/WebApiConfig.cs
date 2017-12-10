using System.Collections.Generic;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Filters;
using MP.Framework.Web.Filters;
using MP.Framework.Web.Filters.Action;
using MP.Framework.Web.Filters.Authorization;
using MP.Framework.Web.Handlers;
using MP.Framework.Web.Security;
using Newtonsoft.Json;

namespace MP.Web.Api
{
    internal static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // enable cors
            config.EnableCors(new EnableCorsAttribute(Settings.AllowedOrigin, "*", "*"));

            // apply Filters
            if (Settings.RequireHttps)
            {
                config.Filters.Add(new RequireHttpsAuthorizationFilterAttribute());
            }

            // These filters will fire in this order. Do not change the order.
            ISecurityManager securityManager = AutofacIoC.Resolve<ISecurityManager>();
            config.Filters.Add(new AuthenticationTokenRequiredActionFilterAttribute(securityManager));
            config.Filters.Add(new LoggingActionFilterAttribute());
            config.Filters.Add(new SafeMethodActionFilterAttribute());

            // register message handlers
            config.MessageHandlers.Insert(0, new ResponseCompressionHandler());
            config.MessageHandlers.Add(new CompressionAcceptEncodingHeaderHandler(new Dictionary<string, double>
            {
                { "br", 1.0 },
                { "gzip", 0.7 },
                { "deflate", 0.5 },
            }));
            config.MessageHandlers.Add(new GZipDecompressionHandler());

            // order filters
            // Start clean by replacing with filter provider for global configuration.
            // For these globally added filters we need not do any ordering as filters are
            // executed in the order they are added to the filter collection
            config.Services.Replace(typeof(IFilterProvider), new ConfigurationFilterProvider());
            // Custom action filter provider which does ordering for Controllers and Actions
            config.Services.Add(typeof(IFilterProvider), new OrderedFilterProvider());

            // Web API routes
            config.MapHttpAttributeRoutes();

            // Serialization settings
            config.Formatters.Clear();

            // add JSON formatter
            config.Formatters.Add(new JsonMediaTypeFormatter());
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new JsonContractResolver(new JsonMediaTypeFormatter());
            // this will default the response type to json when the request is from a browser
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

            // Add custom media types as supported to their default formatters
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeWithQualityHeaderValue("application/gzip"));

            // add XML formatter
            config.Formatters.Add(new XmlMediaTypeFormatter());
            config.Formatters.XmlFormatter.UseXmlSerializer = false;
        }
    }
}
