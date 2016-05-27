using System;
using System.Collections.Generic;
using System.Globalization;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using API.Config;
using API.Model;

namespace API.Data
{
    public class ReviewTable
    {
        private readonly AmazonDynamoDBClient _dbClient;
        private readonly DynamoDBOperationConfig _dbOperationConfig;

        public ReviewTable(AmazonDynamoDBClient dbClient, IContext context)
        {
            _dbClient = dbClient;
            _dbOperationConfig = new DynamoDBOperationConfig();
            _dbOperationConfig.TableNamePrefix =
                CultureInfo.InvariantCulture.TextInfo.ToTitleCase(context.Environment.ToLowerInvariant()) + "-";
        }

        public IEnumerable<Review> GetReviews()
        {
            IEnumerable<Review> reviews;
            using (var context = new DynamoDBContext(_dbClient, _dbOperationConfig))
            {
                reviews = context.Scan<Review>();
            } 
            return reviews;
        }

        public IEnumerable<Review> GetReviewsByCategory(Category category)
        {
            IEnumerable<Review> reviews;
            using (var context = new DynamoDBContext(_dbClient, _dbOperationConfig))
            {
                reviews = context.Scan<Review>();
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
 
        public void CreateReview(Review review)
        {
            using (var context =new DynamoDBContext(_dbClient, _dbOperationConfig))
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

    }
}