using System.Collections.Generic;

namespace Domain
{
    public interface IReviewData
    {
        IEnumerable<Review> GetReviews();
        Review GetReview();
    }

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