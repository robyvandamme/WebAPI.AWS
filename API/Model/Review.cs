using System;
using Amazon.DynamoDBv2.DataModel;

namespace API.Model
{
    public class Review
    {
        // DynamoDB:
        // so, decided to pick subject as HashKey and Id as sortkey
        // reasons:
        // we need to be able to query by subject
        // we need to be able to query by id, but we can do that by using subject + id
        // for now no other search requirements, those can be added later as a secondary index
        // in a scenario where we would have millions of items this would not be a good solution...

        [DynamoDBHashKey]
        public Category Category { get; set; } // set the type, means you will include a book or an app...

        [DynamoDBRangeKey]
        public Guid Id { get; set; }

        // Do we store this in Dynamo or on S3 as Json? (figure out the size limitations and the impact on the read and write operations) 
        public string Text { get; set; }

        public string Author { get; set; }

        public string[] Tags { get; set; }

        // store this as a json document
        //[DynamoDBProperty(Converter = typeof(AddressConverter))]
        [DynamoDBIgnore]
        public Book Book { get; set; }

        // store this as a json document
        [DynamoDBIgnore]
        public App App { get; set; }

        //public string Tenant { get; set; } //  TODO: map this to some form of authentication, not important for now

        // TODO: autoincrement number (should be possible in dynamo)
        public int Likes { get; set; }

        // TODO: add an image for the review in S3
        //public S3Link ProfilePicture { get; set; }

    }
}