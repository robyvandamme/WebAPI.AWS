using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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
                 DbClient.Config.ServiceURL = "http://localhost:8000";
            }
        }
    }

    public class DynamoDbTable<T> : DynamoDbTable
    {
        public string TableName { get;}

        // do we need to inject something here? Maybe the client, if we decide not to make this one private static but have the container take care of that...
        public DynamoDbTable(IContext context) : base(context)
        {
            // we also need a constant for the environment to append to the tablename
            TableName = GetTableName(context);
        }

        // we need to create a table for each environment that we have...
        public void CreateTable()
        {
            List<string> currentTables = DbClient.ListTables().TableNames;

            if (!currentTables.Contains(TableName))
            {
                DbClient.CreateTable(new CreateTableRequest
                {
                    TableName = TableName, // TODO: what happens if I don't specify throughput?
                                            //ProvisionedThroughput =
                                            //    new ProvisionedThroughput { ReadCapacityUnits = 3, WriteCapacityUnits = 1 },

                    // this also needs to come from the table definition
                    KeySchema = new List<KeySchemaElement>
                        {
                            new KeySchemaElement
                            {
                                AttributeName = "Name",
                                KeyType = KeyType.HASH
                            }
                        },
                    AttributeDefinitions = new List<AttributeDefinition>
                        {
                            new AttributeDefinition {AttributeName = "Name", AttributeType = ScalarAttributeType.S}
                        }
                });
            }
        }

        public void DeleteTable()
        {
            // add some exepctionhandling and stuff....
            var response = DbClient.DeleteTable(new DeleteTableRequest { TableName = TableName });
            // do something with the response...
        }

        private static void WaitUntilTableReady(string tableName)
        {
            string status = null;
            // Let us wait until table is created. Call DescribeTable.
            do
            {
                System.Threading.Thread.Sleep(5000); // Wait 5 seconds.
                try
                {
                    var res = DbClient.DescribeTable(new DescribeTableRequest
                    {
                        TableName = tableName
                    });
                    status = res.Table.TableStatus;
                }
                catch (ResourceNotFoundException)
                {
                    // DescribeTable is eventually consistent. So you might
                    // get resource not found. So we handle the potential exception.
                }
            } while (status != "ACTIVE");
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