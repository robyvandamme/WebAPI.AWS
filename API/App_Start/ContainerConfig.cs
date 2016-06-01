﻿using System;
using System.Configuration;
using System.Reflection;
using System.Web.Http;
using Amazon;
using Amazon.DynamoDBv2;
using API.Config;
using API.Controllers;
using API.Data;
using API.Filters;
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

            builder.RegisterType<Context>().As<IContext>().SingleInstance();

            builder.Register(c => new AmazonDynamoDBClient(DynamoDbHelper.ConfigureDynamoDb())).SingleInstance();

            builder.RegisterType<ReviewTable>().AsSelf();

            builder.Register(c => new GlobalExceptionFilter())
                .AsWebApiExceptionFilterFor<ApiController>().SingleInstance(); // should this be single instance, that is the default in web api...?

            builder.Register(c => new IntegrationTestFilter())
               .AsWebApiActionFilterFor<TestController>().SingleInstance(); 

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
 
    }
}