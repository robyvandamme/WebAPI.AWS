using System;
using Amazon.DynamoDBv2.DataModel;

namespace API.Data.DynamoDb.Tables
{
    //public class ReviewTable
    //{
    //    // so, decide to pick subject ad HashKey and Id as sortkey
    //    // reasons:
    //    // we need to be able to query by subject
    //    // we need to be able to query by id, but we can do that by using subject + id
    //    // in a scenario where we would have millions of items this would not be a good solution

    //    [DynamoDBHashKey]
    //    public string Subject { get; set; } // this comes from an enum, so could be int or string..., but i assume it will be saved as an int?

    //    [DynamoDBRangeKey]
    //    public Guid Id { get; set; }

    //    public DateTime DateCreated { get; set; }

    //    public DateTime DateUpdated { get; set; }

    //    public string Author { get; set; }

    //    // so, do we store this using dynamo or as a json in S3? Figure out limitiations to size and impact on read/write operations.
    //    public string Text { get; set; }

    //    // TODO: add an image for the review in S3
    //    //public S3Link ProfilePicture { get; set; }

    //    [DynamoDBProperty(AttributeName = "Tags")]
    //    public IEquatable<string> Tags { get; set; }



    //}
}