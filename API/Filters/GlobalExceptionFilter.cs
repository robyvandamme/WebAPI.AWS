using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using Autofac.Integration.WebApi;

namespace API.Filters
{
    public class GlobalExceptionFilter : IAutofacExceptionFilter
    {
        public void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
        }
    }
}