using System.Collections.Generic;
using API.Resources;

namespace API.Data
{
    public class ReviewData : IReviewData
    {
        public IEnumerable<Review> GetReviews()
        {
            var list = new List<Review>();
            var book = new Review
            {
                Id = 1,
                Subject = Subject.Book, // do we need the subject? if the review includes a book it is a book review, if it includes an app it is an app review...
                //Tenant = "Site1",
                Book = new Book()
                {
                    Author = "William Gibson",
                    Title = "Pattern Recognition",
                    Genre = "Science-Fiction?",
                    Url = "http://amazon.com",
                },
                Tags = new[] { "Science-Ficton,", "Branding" }

            };
            list.Add(book);

            var app = new Review()
            {
                Id = 2,
                Subject = Subject.App,
                //Tenant = "Site1",
                App = new App()
                {
                    Name = "Pluralsight",
                    Url = "http://pluralsight.com",

                },
                Tags = new[] { "Learning" }
            };
            list.Add(app);

            return list;
        }

        public Review GetReview(int id)
        {
            var review = new Review
            {
                Id = 1,
                Subject = Subject.Book,
                //Tenant = "Site1",
                Book = new Book()
                {
                    Author = "William Gibson",
                    Title = "Pattern Recognition",
                    Genre = "Science-Fiction?",
                    Url = "http://amazon.com",
                },
                Tags = new[] { "Science-Ficton,", "Branding" }

            };
            return review;
        }

        public int CreateOrUpdateReview(Review review)
        {
            return 1;
        }
    }
}