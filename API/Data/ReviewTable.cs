using System;
using System.Collections.Generic;
using System.Globalization;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using API.Config;
using API.Model;

namespace API.Data
{
    public class ReviewTable
    {
        private readonly AmazonDynamoDBClient _dbClient;
        private readonly DynamoDBOperationConfig _dbOperationConfig;
        private readonly string _tableName;

        public ReviewTable(AmazonDynamoDBClient dbClient, IContext context)
        {
            _dbClient = dbClient;
            _dbOperationConfig = new DynamoDBOperationConfig();
            _dbOperationConfig.TableNamePrefix =
                CultureInfo.InvariantCulture.TextInfo.ToTitleCase(context.Environment.ToLowerInvariant()) + "-";

            // for the lower level table operations we will need to set the full table name ourselves it seems
            _tableName = _dbOperationConfig.TableNamePrefix + "Review";
        }

        public IEnumerable<Review> GetReviews()
        {
            IEnumerable<Review> reviews;
            using (var context = new DynamoDBContext(_dbClient, _dbOperationConfig))
            {
                //try
                //{
                    reviews = context.Scan<Review>();
                    //var query = new QueryOperationConfig();
                    //query.
                    //reviews = context.FromQuery<Review>()

                //}
                //catch (AmazonDynamoDBException dbException)
                //{

                //}   
            }
            return reviews;
        }

        public IEnumerable<Review> GetReviewsByCategory(Category category)
        {
            IEnumerable<Review> reviews;
            using (var context = new DynamoDBContext(_dbClient, _dbOperationConfig))
            {
                reviews = context.Query<Review>(category);
            }
            return reviews;
        }

        public Review GetReview(Category category, Guid id)
        {
            Review review;
            using (var context = new DynamoDBContext(_dbClient, _dbOperationConfig))
            {
                review = context.Load<Review>(category, id);
            }
            return review;
        }

        public void SaveReview(Review review)
        {
            using (var context = new DynamoDBContext(_dbClient, _dbOperationConfig))
            {
                context.Save(review);
            }
        }

        public void DeleteReview(Review review)
        {
            using (var context = new DynamoDBContext(_dbClient, _dbOperationConfig))
            {
                context.Delete(review);
            }
        }

        public void LikeReview(Category category, Guid id)
        {
            var keys = new Dictionary<string, AttributeValue>();
            keys.Add("Category", new AttributeValue { N = ((int)category).ToString() });
            keys.Add("Id", new AttributeValue { S = id.ToString() });

            var updateExpression = "ADD #a :increment";

            var expressionAttributeNames = new Dictionary<string, string>();
            expressionAttributeNames.Add("#a", "Likes");

            var expressionAttributeValues = new Dictionary<string, AttributeValue>();
            expressionAttributeValues.Add(":increment", new AttributeValue { N = "1" });

            var updateItemRequest = new UpdateItemRequest();
            updateItemRequest.TableName = _tableName;
            updateItemRequest.Key = keys;
            updateItemRequest.UpdateExpression = updateExpression;
            updateItemRequest.ExpressionAttributeNames = expressionAttributeNames;
            updateItemRequest.ExpressionAttributeValues = expressionAttributeValues;

            // do we need something from the response? maybe the new amount of likes?
            var response = _dbClient.UpdateItem(updateItemRequest);

        }

        public void DeleteReview(Category category, Guid id)
        {
            using (var context = new DynamoDBContext(_dbClient, _dbOperationConfig))
            {
                context.Delete<Review>(category, id);
            }
        }
    }
}