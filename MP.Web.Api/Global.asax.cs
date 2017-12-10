using System.Web;
using System.Web.Http;
using FluentValidation.WebApi;
using MP.Framework.Reflection;

namespace MP.Web.Api
{
    /// <summary>
    /// WebApiApplication
    /// </summary>
    public class WebApiApplication : HttpApplication
    {
        /// <summary>
        /// Application_Start
        /// </summary>
        protected void Application_Start()
        {
            AssemblyUtilities.LoadAssemblies(HttpRuntime.BinDirectory, "MP.*.dll");

            AutofacIoC.Initialize();

            GlobalConfiguration.Configure(WebApiConfig.Register);

            FluentValidationModelValidatorProvider.Configure(GlobalConfiguration.Configuration);

            Factory.LogManager.Logger.LogInfo("Web.Service.Api started...");
        }
    }
}
