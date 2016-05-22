using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Domain;

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
                Site = "Site1",
                Url = "http://amazon.com",
                Author = "William Gibson",
                Title = "Pattern Recognition"
            };
            list.Add(book);
            return list;
        }

        public Review Get(int id)
        {
            var book = new BookReview
            {
                Id = 1,
                Type = "Book",
                Site = "Site1",
                Url = "http://amazon.com",
                Author = "William Gibson",
                Title = "Pattern Recognition"
            };

            return book;
        }

        // POST: api/Reviews
        public void Post([FromBody]string value)
        {
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
