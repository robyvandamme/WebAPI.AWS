using System.Net;
using System.Web.Http;

namespace API.Controllers
{
    /// <summary>
    /// Health endpoint for the load balancer
    /// </summary>
    public class HealthController : ApiController
    {
        public HttpStatusCode Get()
        {
            return HttpStatusCode.OK;
        }
    }
}
