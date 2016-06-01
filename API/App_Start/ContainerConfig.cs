using System.Reflection;
using System.Web.Http;
using Amazon.DynamoDBv2;
using API.Config;
using API.Controllers;
using API.Data;
using API.Filters;
using Autofac;
using Autofac.Integration.WebApi;
using AutofacSerilogIntegration;
using Serilog;

namespace API
{
    public static class ContainerConfig
    {
        internal static void Configure()
        {
            var builder = new ContainerBuilder();

            // Get your HttpConfiguration.
            var config = GlobalConfiguration.Configuration;

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterWebApiFilterProvider(config);

            builder.RegisterType<Context>().As<IContext>().SingleInstance();

            builder.Register(c => new AmazonDynamoDBClient(DynamoDbHelper.ConfigureDynamoDb())).SingleInstance();

            builder.RegisterType<ReviewTable>().AsSelf();

            // TODO: configure different sink for deployment
            Log.Logger = new LoggerConfiguration().WriteTo.Trace().CreateLogger();

            builder.Register(c => new GlobalExceptionFilter(c.Resolve<ILogger>()))
                .AsWebApiExceptionFilterFor<ApiController>().SingleInstance(); // should this be single instance, that is the default in web api...?

            builder.Register(c => new IntegrationTestFilter())
               .AsWebApiActionFilterFor<TestController>().SingleInstance();

            builder.RegisterLogger();

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
 
    }
}