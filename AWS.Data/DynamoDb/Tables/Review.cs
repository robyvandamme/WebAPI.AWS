using Amazon.DynamoDBv2.DataModel;

namespace AWS.Data.DynamoDb.Tables
{
    public class ReviewTable
    {
        [DynamoDBHashKey]
        public int Id { get; set; }

        //public S3Link ProfilePicture { get; set; }

        //public int Age { get; set; }

        //[DynamoDBProperty(AttributeName = "Interests")]
        //public List<string> Likes { get; set; }
    }
}