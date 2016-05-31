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
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;

namespace API.Tests
{
    [TestFixture]
    public class ReviewsEndpointTest
    {
        [Test]
        public void Can_Post_BookReview()
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

            var request = client.PostAsync("http://localhost/api/reviews/books/", content);
            var response = request.Result;
            Review result = null;

            if (response.IsSuccessStatusCode)
            {
                result = response.Content.ReadAsAsync<Review>().Result;
            }

            result.Should().NotBeNull();
            result.Category.Should().Be(Category.Books);
            result.Id.Should().NotBeEmpty();
            result.Author.Should().BeEquivalentTo(bookReview.Author);


        }

        [Test]
        public void Can_Post_AppReview()
        {
            var appReview = new Review()
            {
                Author = "Someone else on the internet",
                Text = "Amazing app, does exactly what it says on the tin",
                Tags = new List<string>() { "Time", "iPhone" },
                App = new App()
                {
                    Name = "Klok",
                    Url = "http://fakeurl.com"
                }
            };

            var client = new HttpClient();
            var body = JsonConvert.SerializeObject(appReview);
            var content = new StringContent(body, Encoding.UTF8, "application/json");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var request = client.PostAsync("http://localhost/api/reviews/apps", content);
            var response = request.Result;
            Review result = null;

            if (response.IsSuccessStatusCode)
            {
                result = response.Content.ReadAsAsync<Review>().Result;
            }

            result.Should().NotBeNull();
            result.Category.Should().Be(Category.Apps);
            result.Id.Should().NotBeEmpty();
            result.Author.Should().BeEquivalentTo(appReview.Author);


        }

        [Test]
        public void Can_Not_GetReviews_For_NotExistingCategories()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var request = client.GetAsync("http://localhost/api/reviews/somerandomstring");
            var response = request.Result;

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            response.Content.ReadAsStringAsync().Result.Should().Be(string.Empty);
        }

        [Test]
        public void Can_GetReviews_For_ExistingCategories()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var request = client.GetAsync("http://localhost/api/reviews/apps");
            var response = request.Result;

            response.IsSuccessStatusCode.Should().BeTrue();
            var result = response.Content.ReadAsAsync<List<Review>>().Result;
            result.Should().NotBeNull();
            result.ForEach(o => o.Category.Should().Be(Category.Apps));

        }

        [Test]
        public void Can_Not_GetReviews_For_DefaultUnspecifiedCategory()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var request = client.GetAsync("http://localhost/api/reviews/unspecified");
            var response = request.Result;

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            response.Content.ReadAsStringAsync().Result.Should().Be(string.Empty);

        }


    }
}
