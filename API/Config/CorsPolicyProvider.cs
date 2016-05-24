using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Cors;
using System.Web.Http.Cors;

namespace API.Config
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class CustomCorsPolicyAttribute : Attribute, ICorsPolicyProvider
    {
        private readonly CorsPolicy _policy;

        private static IEnumerable<string> AllowedOrigins
            => Convert.ToString(ConfigurationManager.AppSettings["Cors:AllowedOrigins"]).Split(';');

        public CustomCorsPolicyAttribute()
        {
            _policy = new CorsPolicy
            {
                AllowAnyMethod = true,
                AllowAnyHeader = true,
                SupportsCredentials = true,
            };

            foreach (var allowedOrigin in AllowedOrigins)
            {
                _policy.Origins.Add(allowedOrigin);
            }
        }

        public Task<CorsPolicy> GetCorsPolicyAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_policy);
        }
    }
}