using Amazon.DynamoDBv2.DataModel;

namespace API.Resources
{
    public class Review
    {
        public int Id { get; set; }
        //public string Tenant { get; set; } //  TODO: map this to some form of authentication, not important for now
        public Subject Subject { get; set; } // set the type, means you will include a book or an app...
 
        public string Text { get; set; }
        public string Author { get; set; }

        public string Url { get; set; }
        public string[] Tags { get; set; }

        public Book Book { get; set; } // so, different approach.... instead of inheritance, simply add the type to the review, depending on what is sent we know the review type
        public Application Application { get; set; }
        
    }

    public enum Subject
    {
        Book,
        Application
    }

    //[DynamoDBTable("Review")]
    public class Book
    {
        public string Author { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
    }

    //[DynamoDBTable("Review")]
    public class Application
    {
        public string Name { get; set; }
    }
}