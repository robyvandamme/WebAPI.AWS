using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using API.Resources;
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
            var bookReview = new Review()
            {
                Id = 1,
                Subject = Subject.Book,
                //Tenant = "Site1",
                Author = "Someone on the internet", 
                Tags = new[] { "Science-Ficton,", "Branding" },
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
            
            Assert.AreEqual(HttpStatusCode.Accepted, result);
        }

    }
}
