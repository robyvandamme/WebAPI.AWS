using System;
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

        [Route("reviews")]
        public HttpResponseMessage Get()
        {
            var reviews = _reviewTable.GetReviews();
            return Request.CreateResponse(HttpStatusCode.OK, reviews);
        }

        [Route("reviews/{category}")]
        public HttpResponseMessage Get(string category)
        {
            Category categoryEnum;
            bool categoryExists = Enum.TryParse(category, true, out categoryEnum);
            if (categoryExists)
            {
                var reviews = _reviewTable.GetReviewsByCategory(categoryEnum);
                return Request.CreateResponse(HttpStatusCode.OK, reviews);
            }
            return new HttpResponseMessage(HttpStatusCode.NotFound);
        }

        [Route("reviews")]
        [Route("reviews/{category}/{id}")]
        public HttpResponseMessage Get(string category, Guid id)
        {
            Category categoryEnum;
            bool categoryExists = Enum.TryParse(category, true, out categoryEnum);
            if (categoryExists)
            {
                // todo: exception handling on GetReview
                var review = _reviewTable.GetReview(categoryEnum, id);
                return Request.CreateResponse(HttpStatusCode.OK, review);
            }
            return new HttpResponseMessage(HttpStatusCode.NotFound);
        }

        [Route("reviews")]
        [Route("reviews/{category}")]
        public HttpResponseMessage Post(string category, Review review)
        {
            // check if the category exists,
            // use that category, even if the category in the review is a different one? we overwrite it? NOPE, return a error: you cannot post app reviews to the books endpoint etc...
            // check if an id is present, 
            // if the id exists we refuse the creation? Nope, it's a post, we create a new review with a new Id
            Category categoryEnum;
            bool categoryExists = Enum.TryParse(category, true, out categoryEnum);
            if (categoryExists)
            {
                if (review.Category != categoryEnum)
                {
                    var response = new HttpResponseMessage();
                    response.StatusCode = (HttpStatusCode)422;
                    // TODO: add someting meaningfull as content here
                    return response;
                }
                review.Category = categoryEnum;
                review.Id = Guid.NewGuid();
                _reviewTable.SaveReview(review);
                return Request.CreateResponse(HttpStatusCode.OK, review);
            }
            return new HttpResponseMessage(HttpStatusCode.NotFound);
        }

        [Route("reviews/{category}/{id}")]
        public HttpResponseMessage Put(string category, Guid id, Review review)
        {
            Category categoryEnum;
            bool categoryExists = Enum.TryParse(category, true, out categoryEnum);
            if (categoryExists && id != Guid.Empty)
            {
                if (review.Category != categoryEnum)
                {
                    var response = new HttpResponseMessage();
                    response.StatusCode = (HttpStatusCode)422;
                    // TODO: add something meaningfull as content here
                    return response;
                }
                if (review.Id == Guid.Empty)
                {
                    review.Id = id;
                }
                if (!review.Id.Equals(id))
                {
                    return new HttpResponseMessage((HttpStatusCode)422);
                }
                _reviewTable.SaveReview(review);
                Request.CreateResponse(HttpStatusCode.OK, review);
            } 
            return new HttpResponseMessage(HttpStatusCode.NotFound);
        }

        [Route("reviews/{category}/{id}")]
        public HttpResponseMessage Delete(string category, Guid id)
        {
            Category categoryEnum;
            bool categoryExists = Enum.TryParse(category, true, out categoryEnum);
            if (categoryExists)
            {
                _reviewTable.DeleteReview(categoryEnum, id);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            return new HttpResponseMessage(HttpStatusCode.NotFound);

        }
    }
}
