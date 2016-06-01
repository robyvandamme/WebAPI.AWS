using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace API.Controllers
{
    /// <summary>
    /// Health endpoint for the load balancer
    /// </summary>
    [ApiExplorerSettings(IgnoreApi= true)]
    public class HealthController : ApiController
    {
        public HttpStatusCode Get()
        {
            return HttpStatusCode.OK;
        }
    }
}
