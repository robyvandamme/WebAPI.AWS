using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Autofac.Integration.WebApi;

namespace API.Filters
{
    /// <summary>
    /// Attribute to mark endpoints used only for integration test purposes
    /// </summary>
    public class IntegrationTestFilter: IAutofacActionFilter
    {
        private static IEnumerable<string> AllowedIpAddresses
        { get { return Convert.ToString(ConfigurationManager.AppSettings["IntegrationTest:AllowedIpAddresses"]).Split(';'); } }

        public void OnActionExecuting(HttpActionContext actionContext)
        {
            string ipAddress = HttpContext.Current.Request.UserHostAddress;
            if (!string.IsNullOrWhiteSpace(ipAddress))
            {
                if (!IsIpAddressAllowed(ipAddress.Trim()))
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden);
                }
            }
        }

        public void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            // do we need to implement anything here?
        }

        private bool IsIpAddressAllowed(string ipAddress)
        {
            if (!string.IsNullOrWhiteSpace(ipAddress))
            {
                return AllowedIpAddresses.Any(a => a.Trim().Equals(ipAddress, StringComparison.InvariantCultureIgnoreCase));
            }
            return false;
        }
    }
}