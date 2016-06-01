using System;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace API.Controllers
{
    [ApiExplorerSettings(IgnoreApi= true)]
    public class TestController : ApiController
    {
        public HttpResponseMessage Get()
        {
            // we use a global exception filter, so this should always return a 500
            throw new Exception("**INTEGRATION TEST**");
        }

    }
}
