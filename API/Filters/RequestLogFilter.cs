using System;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Autofac.Integration.WebApi;
using Serilog;

namespace API.Filters
{
    public class RequestLogFilter: IAutofacActionFilter
{
        private readonly ILogger _logger;
        private static readonly string Machine = Environment.MachineName;

        public RequestLogFilter(ILogger logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(HttpActionContext actionContext)
        {
            // TODO: enable/diable this using a flag somewhere?(global or local config)
            var args = actionContext.ActionArguments;
            var controller = actionContext.ControllerContext.Controller.GetType().Name;
            var method = actionContext.ActionDescriptor.ActionName;
            _logger.Information("ActionExecuting: {Machine} | {Controller} | {Method} | {Args} ", Machine, controller, method, args);
        }

        public void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            var args = actionExecutedContext.ActionContext.ActionArguments;
            var controller = actionExecutedContext.ActionContext.ControllerContext.Controller.GetType().Name;
            var method = actionExecutedContext.ActionContext.ActionDescriptor.ActionName;
            _logger.Information("ActionExecuted: {Machine} | {Controller} | {Method} | {Args} ", Machine, controller, method, args);
        }
}
}