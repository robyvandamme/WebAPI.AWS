using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using API.Resources;

namespace API.Controllers
{
    public class ReviewsController : ApiController
    {
        public IEnumerable<Review> Get()
        {
            var list = new List<Review>();
            var book = new BookReview
            {
                Id = 1,
                Type = "Book",
                Tenant = "Site1",
                Url = "http://amazon.com",
                Author = "William Gibson",
                Title = "Pattern Recognition",
                Genre = "Science-Fiction?",
                Tags = new []{"Science-Ficton,", "Branding"}
            };
            list.Add(book);

            var app = new ApplicationReview()
            {
                Id = 2,
                Type = "Application",
                Tenant = "Site1",
                Url = "http://pluralsight.com",
                Name = "Pluralsight",
                Tags = new []{"Learning"}
            };
            list.Add(app);

            return list;
        }

        public Review Get(int id)
        {
            var book = new BookReview
            {
                Id = 1,
                Type = "Book",
                Tenant = "Site1",
                Url = "http://amazon.com",
                Author = "William Gibson",
                Title = "Pattern Recognition"
            };

            return book;
        }

        // POST: api/Reviews
        public HttpResponseMessage Post(Review review) // what happens if we send in a bookreview here, will it automatically map all the properties?
        {
            Console.Write(review);
            //TODO: return something here, like the id or a link to the created/ updated resource.
            return new HttpResponseMessage(HttpStatusCode.Accepted);
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
