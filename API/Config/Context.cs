using System.Configuration;

namespace API.Config
{
    public class Context : IContext
    {
        public string Environment => ConfigurationManager.AppSettings.Get("Context:Environment");
    }
}