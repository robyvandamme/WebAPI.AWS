using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using Autofac.Integration.WebApi;
using Serilog;

namespace API.Filters
{
    public class GlobalExceptionFilter : IAutofacExceptionFilter
    {
        private readonly ILogger _logger;
        private static readonly string Machine = Environment.MachineName;

        public GlobalExceptionFilter(ILogger logger)
        {
            _logger = logger;
        }

        public void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var args = actionExecutedContext.ActionContext.ActionArguments;
            var controller = actionExecutedContext.ActionContext.ControllerContext.Controller.GetType().Name;
            var method = actionExecutedContext.ActionContext.ActionDescriptor.ActionName;
            _logger.Error(actionExecutedContext.Exception, "UnhandledException: {Machine} | {Controller} | {Method} | {Args}", Machine, controller, method, args);
            actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
        }
    }
}