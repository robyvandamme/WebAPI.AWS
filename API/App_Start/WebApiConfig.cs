using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using API.Config;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // configure CORS
            var corsPolicyAttribute = new CustomCorsPolicyAttribute();
            config.EnableCors(corsPolicyAttribute);
            // remove the XML formatter, just use JSON for now
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            // Use camel case for JSON data.
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver(); 
            // Enum conversion
            config.Formatters.JsonFormatter.SerializerSettings.Converters.Add
                (new Newtonsoft.Json.Converters.StringEnumConverter());
            // ignore null values in output
            config.Formatters.JsonFormatter.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
