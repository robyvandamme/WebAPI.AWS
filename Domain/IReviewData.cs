using System.Collections.Generic;

namespace Domain
{
    public interface IReviewData
    {
        IEnumerable<Review> GetReviews();
        Review GetReview();
    }
}