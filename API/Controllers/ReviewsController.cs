using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            var book = new Review
            {
                Id = 1,
                Subject = Subject.Book, // do we need the subject? if the review includes a book it is a book review, if it includes an app it is an app review...
                //Tenant = "Site1",
                Url = "http://amazon.com", 
                Book = new Book()
                {
                    Author = "William Gibson",
                    Title = "Pattern Recognition",
                    Genre = "Science-Fiction?"
                },
                Tags = new []{"Science-Ficton,", "Branding"}

            };
            list.Add(book);

            var app = new Review()
            {
                Id = 2,
                Subject = Subject.Application,
                //Tenant = "Site1",
                Url = "http://pluralsight.com",
                Application = new Application()
                {
                Name = "Pluralsight"
                },
                Tags = new []{"Learning"}
            };
            list.Add(app);

            return list;
        }

        public Review Get(int id)
        {
            var review = new Review
            {
                Id = 1,
                Subject = Subject.Book, 
                //Tenant = "Site1",
                Url = "http://amazon.com",
                Book = new Book()
                {
                    Author = "William Gibson",
                    Title = "Pattern Recognition",
                    Genre = "Science-Fiction?"
                },
                Tags = new[] { "Science-Ficton,", "Branding" }

            };
            return review;

        }

        // POST: api/Reviews
        public HttpResponseMessage Post(Review review)
        {
            Debug.Write(review);
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
