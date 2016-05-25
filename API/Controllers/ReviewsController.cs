using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using API.Data;
using API.Model;

namespace API.Controllers
{
    public class ReviewsController : ApiController
    {
        private readonly IReviewData _reviewData;

        public ReviewsController(IReviewData reviewData)
        {
            _reviewData = reviewData;
        }

        public IEnumerable<Review> Get()
        {
            var reviews = _reviewData.GetReviews();
            return reviews;
        }

        public Review Get(Guid id) // or do we need to use a string here and cast?
        {
            var review = _reviewData.GetReview(id);
            return review;
        }

        // POST: api/Reviews
        public HttpResponseMessage Post(Review review)
        {
            var id = _reviewData.CreateOrUpdateReview(review);
            //Debug.Write(review);
            //TODO: return something here, like the id or a link to the created/ updated resource.
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        // PUT: api/Reviews/5
        public void Put(int id, [FromBody]string value)
        {

        }

        // DELETE: api/Reviews/5
        public void Delete(int id)
        {

        }
    }
}
