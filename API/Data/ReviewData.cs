using System.Collections.Generic;
using API.Resources;

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