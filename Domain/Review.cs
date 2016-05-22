namespace Domain
{
    public abstract class Review
    {
        public int Id { get; set; }
        public string Site { get; set; } // can be multiple sites... how do we model that?
        public abstract string Type { get; set; }
        public string Url { get; set; }
    }

    public class BookReview : Review
    {
        public override string Type { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string Review { get; set; }
    }

    public class ApplicationReview : Review
    {
        // we should be able to query on multiple types...? Or not?
        public override string Type { get; set; }
        public string Name { get; set; }
        public string Review { get; set; }

    }
}