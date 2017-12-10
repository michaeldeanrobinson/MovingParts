using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using MP.Framework.Web.Security;

namespace MP.Web.Api
{
    internal static class AutofacIoC
    {
        public static IContainer Container { get; set; }

        public static void Initialize()
        {
            // Create the container builder.
            ContainerBuilder builder = new ContainerBuilder();

            // Register the Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // single instances, they must not access the DB to qualify for SingleInstance
            builder.RegisterType<SecurityManager>().As<ISecurityManager>().SingleInstance();

            // Build the container.
            Container = builder.Build();

            // Create the depenedency resolver.
            AutofacWebApiDependencyResolver resolver = new AutofacWebApiDependencyResolver(Container);

            // Configure Web API with the dependency resolver.
            GlobalConfiguration.Configuration.DependencyResolver = resolver;
        }

        public static T Resolve<T>()
        {
            using (ILifetimeScope scope = AutofacIoC.Container.BeginLifetimeScope("AutofacWebRequest"))
            {
                return scope.Resolve<T>();
            }
        }
    }
}