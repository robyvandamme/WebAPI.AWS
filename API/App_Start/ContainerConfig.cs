using System.Reflection;
using System.Web.Http;
using API.Data;
using Autofac;
using Autofac.Integration.WebApi;

namespace API
{
    public static class ContainerConfig
    {
        internal static void Configure()
        {
            var builder = new ContainerBuilder();

            // Get your HttpConfiguration.
            var config = GlobalConfiguration.Configuration;

            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            // OPTIONAL: Register the Autofac filter provider
            builder.RegisterWebApiFilterProvider(config);

            builder.RegisterType<ReviewData>().As<IReviewData>();

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}