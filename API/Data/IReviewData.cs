using System;
using System.Collections.Generic;
using API.Model;

namespace API.Data
{
    public interface IReviewData
    {
        IEnumerable<Review> GetReviews();
        Review GetReview(Category category, Guid id);
        int CreateOrUpdateReview(Review review);
    }
}