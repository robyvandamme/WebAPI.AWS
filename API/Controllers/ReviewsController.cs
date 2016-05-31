using System;
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

        [Route("reviews")]
        public HttpResponseMessage Get()
        {
            // TODO: we should probably try and filter at the query
            var reviews = _reviewTable.GetReviews().Where(o => o.Category.IsSpecified());
            return Request.CreateResponse(HttpStatusCode.OK, reviews);
        }

        [Route("reviews/{category}")]
        public HttpResponseMessage Get(string category)
        {
            var theCategory = ParseCategory(category);
            if (theCategory.IsSpecified())
            {
                var reviews = _reviewTable.GetReviewsByCategory(theCategory);
                return Request.CreateResponse(HttpStatusCode.OK, reviews);
            }
            return new HttpResponseMessage(HttpStatusCode.NotFound);
        }

        [Route("reviews/{category}/{id}")]
        public HttpResponseMessage Get(string category, Guid id)
        {
            var theCategory = ParseCategory(category);
            if (theCategory.IsSpecified())
            {
                // todo: exception handling on GetReview
                var review = _reviewTable.GetReview(theCategory, id);
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

            var theCategory = ParseCategory(category);
            if(theCategory.IsSpecified())
            {
                // we do want to accept unspecified categories of course
                if (review.Category != theCategory && review.Category.IsSpecified())
                {
                    return CreateValidationErrorResponse();
                }
                review.Category = theCategory;
                review.Id = Guid.NewGuid();
                _reviewTable.SaveReview(review);
                return Request.CreateResponse(HttpStatusCode.OK, review);
            }
            return new HttpResponseMessage(HttpStatusCode.NotFound);
        }

        [Route("reviews/{category}/{id}")]
        public HttpResponseMessage Put(string category, Guid id, Review review)
        {
            var theCategory = ParseCategory(category);
            if (theCategory.IsSpecified() && id != Guid.Empty)
            {
                UpateEmptyReviewParameters(review, theCategory, id);

                if (review.Category != theCategory || review.Id != id)
                {
                    return CreateValidationErrorResponse();
                }

                _reviewTable.SaveReview(review);
                Request.CreateResponse(HttpStatusCode.OK, review);
            } 
            return new HttpResponseMessage(HttpStatusCode.NotFound);
        }

        [Route("reviews/{category}/{id}")]
        public HttpResponseMessage Delete(string category, Guid id)
        {
            var theCategory = ParseCategory(category);
            if (theCategory.IsSpecified())
            {
                _reviewTable.DeleteReview(theCategory, id);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            return new HttpResponseMessage(HttpStatusCode.NotFound);
        }

        private static void UpateEmptyReviewParameters(Review review, Category category, Guid id)
        {
            if (review.Id == Guid.Empty)
            {
                review.Id = id;
            }
            if (review.Category == Category.Unspecified)
            {
                review.Category = category;
            }
        }

        // needs an input param for the content..., turn into an extension method?
        private HttpResponseMessage CreateValidationErrorResponse()
        {
            // TODO: add someting meaningful as content 
            return Request.CreateErrorResponse((HttpStatusCode) 422, String.Empty);
        }

        private static Category ParseCategory(string category)
        {
            Category categoryEnum;
            Enum.TryParse(category, true, out categoryEnum);
            return categoryEnum;
        }
    }
}
