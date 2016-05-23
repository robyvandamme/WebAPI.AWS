using System.Collections.Generic;
using Domain;

namespace API.Data
{
    public interface IReviewData
    {
        IEnumerable<Review> GetReviews();
        Review GetReview();
    }
}