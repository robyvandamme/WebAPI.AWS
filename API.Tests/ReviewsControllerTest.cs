using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Resources;
using NUnit.Framework;

namespace API.Tests
{
    [TestFixture]
    public class ReviewsControllerTest
    {
        [Test]
        public void TestPost()
        {
            // I wanna make a call to the API and do a post of a review to find out what gets deserialized...
            var book = new BookReview
            {
                Id = 1,
                Type = "Book",
                Tenant = "Site1",
                Url = "http://amazon.com",
                Author = "William Gibson",
                Title = "Pattern Recognition",
                Genre = "Science-Fiction?",
                Tags = new[] { "Science-Ficton,", "Branding" }
            };

         

        }

    }
}
