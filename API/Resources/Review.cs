using Amazon.DynamoDBv2.DataModel;

namespace API.Resources
{
    public abstract class Review
    {
        public int Id { get; set; }
        public string Tenant { get; set; } //  TODO: map this to some form of authentication
        public abstract string Type { get; set; }
        public string Url { get; set; }
        public string[] Tags { get; set; }
        public string ReviewText { get; set; }
        public string ReviewAuthor { get; set; } //
    }

    [DynamoDBTable("Review")]
    public class BookReview : Review
    {
        public override string Type { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
    }

    [DynamoDBTable("Review")]
    public class ApplicationReview : Review
    {
        // we should be able to query on multiple types...? Or not?
        public override string Type { get; set; }
        public string Name { get; set; }
    }
}