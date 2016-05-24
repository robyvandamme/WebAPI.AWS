using System.Collections.Generic;
using API.Resources;

namespace API.Data
{
    public interface IReviewData
    {
        IEnumerable<Review> GetReviews();
        Review GetReview(int id);
        int CreateOrUpdateReview(Review review);

    }
}