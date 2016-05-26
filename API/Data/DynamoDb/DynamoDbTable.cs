using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using API.Config;

namespace API.Data.DynamoDb
{
    public abstract class DynamoDbTable
    { 
        protected static readonly AmazonDynamoDBClient DbClient = new AmazonDynamoDBClient();

        protected DynamoDbTable(IContext context)
        {
            // TODO: this is only necessary for dev.. need to set this somewhere else 
            // http://docs.aws.amazon.com/amazondynamodb/latest/developerguide/DynamoDBLocal.html
            // we probably need to do the same thing for every environment we have....    
            if (context.Environment.Equals(Constants.DevEnvironment, StringComparison.OrdinalIgnoreCase))
            {
                DbClient.Config.RegionEndpoint = RegionEndpoint.APSoutheast1;
                 DbClient.Config.ServiceURL = "http://localhost:8000";
            }
        }
    }

    public class DynamoDbTable<T> : DynamoDbTable
    {
        public string TableName { get;}

        public DynamoDbTable(IContext context) : base(context)
        {
            // we also need an environment suffix for the tablename, except locally...
            TableName = GetTableName(context);
        }
 
        private string GetTableName(IContext context)
        { 
            var suffix = "-" + CultureInfo.InvariantCulture.TextInfo.ToTitleCase(context.Environment); ;

            var attribute = typeof(T).GetCustomAttributes(typeof(DynamoDBTableAttribute), true)
               .FirstOrDefault() as DynamoDBTableAttribute;
     
            return attribute != null ? attribute.TableName + suffix : typeof(T).Name + suffix;
        }
    }
}