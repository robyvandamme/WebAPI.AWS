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
        public void Post_Book_Review()
        {
            var bookReview = new Review()
            { 
                Author = "Someone on the internet",
                Text = "The first of William Gibson\'s usually futuristic novels to be set in the present, Pattern Recognition is a masterful snapshot of modern consumer culture and hipster esoterica. Set in London, Tokyo, and Moscow, Pattern Recognition takes the reader on a tour of a global village inhabited by power-hungry marketeers, industrial saboteurs, high-end hackers, Russian mob bosses, Internet fan-boys, techno archeologists, washed-out spies, cultural documentarians, and our heroine Cayce Pollard--a soothsaying \"cool hunter\" with an allergy to brand names.",
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
            var response = client.PostAsync("http://localhost/api/reviews/books/", content);
            var result = response.Result.StatusCode;
            
            Assert.AreEqual(HttpStatusCode.OK, result);
        }

    }
}
