using System;
using System.Collections.Generic;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Amazon.Util;
using API.Model;

namespace API.Data
{
    public class ReviewData : IReviewData
    {
        private static readonly AmazonDynamoDBClient DbClient;

        static ReviewData()
        {
            var config = new AmazonDynamoDBConfig();
            config.ServiceURL = "http://localhost:8000";
            DbClient = new AmazonDynamoDBClient(config);
        }

        public IEnumerable<Review> GetReviews()
        {
            DynamoDBContext context = new DynamoDBContext(DbClient);
            IEnumerable<Review> reviews = context.Scan<Review>();
            return reviews;
        }

        public IEnumerable<Review> GetReviewsByCategory(Category category)
        {
            DynamoDBContext context = new DynamoDBContext(DbClient);
            IEnumerable<Review> reviews =
              context.Query<Review>(category);

            return reviews;
        }

        public Review GetReview(Category category, Guid id)
        {
            DynamoDBContext context = new DynamoDBContext(DbClient);
            Review review = context.Load<Review>(category,id);
            return review;
        }

        public int CreateOrUpdateReview(Review review)
        {
            return 1;
        }

        public void CreateReview(Review review)
        {
            DynamoDBContext context = new DynamoDBContext(DbClient);
            context.Save(review);
        }

        public void DeleteReview(Review review)
        {
            DynamoDBContext context = new DynamoDBContext(DbClient);
            context.Delete(review);
        }

    }
}