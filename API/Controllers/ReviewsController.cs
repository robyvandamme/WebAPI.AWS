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
        private readonly ReviewTable _reviewTable;

        public ReviewsController(ReviewTable reviewTable)
        {
            _reviewTable = reviewTable;
        }

        public IEnumerable<Review> Get()
        {
            var reviews = _reviewTable.GetReviews();
            return reviews;
        }

        public Review Get(Category category, Guid id) // or do we need to use a string here and cast?
        {
            var review = _reviewTable.GetReview(category, id);
            return review;
        }

        // POST: api/Reviews
        public HttpResponseMessage Post(Review review)
        {
            _reviewTable.SaveReview(review);
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
