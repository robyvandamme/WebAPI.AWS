using System.Collections.Generic;
using Domain;

namespace API.Data
{
    public class ReviewData : IReviewData
    {
        public IEnumerable<Review> GetReviews()
        {
            return new List<Review>();
        }

        public Review GetReview()
        {
            return new BookReview();
        }
    }
}