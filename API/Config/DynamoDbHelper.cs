using System;
using System.Configuration;
using Amazon;
using Amazon.DynamoDBv2;

namespace API.Config
{
    public class DynamoDbHelper
    {
        public static AmazonDynamoDBConfig ConfigureDynamoDb()
        {
            var config = new AmazonDynamoDBConfig();
            config.RegionEndpoint = RegionEndpoint.GetBySystemName(ConfigurationManager.AppSettings.Get("AWSRegion"));
            var environment = ConfigurationManager.AppSettings.Get("Context:Environment");
            if (environment.Equals(Constants.Environments.Local, StringComparison.OrdinalIgnoreCase))
            {
                config.ServiceURL = "http://localhost:8000";
            }
            return config;
        }
    }
}