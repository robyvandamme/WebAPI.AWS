using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using API.Model;
using Newtonsoft.Json;
using NUnit.Framework;

namespace API.Tests
{
    [TestFixture]
    public class ReviewsEndpointTest
    {
 
        [Test]
        public void TestPost()
        {

            //var list = new List<Review>();
            //var book = new Review
            //{
            //    Id = Guid.NewGuid(),
            //    Category = Category.Book, // do we need the subject? if the review includes a book it is a book review, if it includes an app it is an app review...
            //    Tenant = "Site1",
            //    Book = new Book()
            //    {
            //        Author = "William Gibson",
            //        Title = "Pattern Recognition",
            //        Genre = "Science-Fiction?",
            //        Url = "http://amazon.com",
            //    },
            //    Tags = new List<string>() { "Science-Ficton,", "Branding" }

            //};
            //list.Add(book);

            //var app = new Review()
            //{
            //    Id = Guid.NewGuid(),
            //    Category = Category.App,
            //    Tenant = "Site1",
            //    App = new App()
            //    {
            //        Name = "Pluralsight",
            //        Url = "http://pluralsight.com",

            //    },
            //    Tags = new List<string>() { "Learning" }
            //};
            //list.Add(app);

            //return list;

            var bookReview = new Review()
            {
                Id = Guid.NewGuid(),
                Category = Category.Book,
                //Tenant = "Site1",
                Author = "Someone on the internet", 
                Tags = new List<string>() { "Science-Ficton,", "Branding" },
                Book = new Book()
                {
                    Author = "William Gibson",
                    Title = "Pattern Recognition",
                    Genre = "Science-Fiction?",
                    Url = "http://amazon.com"
                }
            };

            var client = new HttpClient();
            var body = JsonConvert.SerializeObject(bookReview);
            var content = new StringContent(body, Encoding.UTF8, "application/json");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = client.PostAsync("http://localhost/api/reviews", content);
            var result = response.Result.StatusCode;
            
            Assert.AreEqual(HttpStatusCode.OK, result);
        }

    }
}
