using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using API.Logging;
using Autofac.Integration.WebApi;

namespace API.Filters
{
    public class GlobalExceptionFilter : IAutofacExceptionFilter
    {
        private readonly ILogger _logger;

        public GlobalExceptionFilter(ILogger logger)
        {
            _logger = logger;
        }

        public void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            _logger.LogUnhandledException(actionExecutedContext.Exception);
            actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
        }
    }
}