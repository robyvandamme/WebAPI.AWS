using System;
using System.Collections.Generic;
using System.Linq;
using API.Data;
using API.Model;
using FluentAssertions;
using NUnit.Framework;

namespace API.Tests
{
    [TestFixture]
    public class DynamoDbIntegrationTests
    {
        //var list = new List<Review>();
        //var book = new Review
        //{
        //    Id = Guid.NewGuid(),
        //    Category = Category.Book, // do we need the subject? if the review includes a book it is a book review, if it includes an app it is an app review...
        //    //Tenant = "Site1",
        //    Book = new Book()
        //    {
        //        Author = "William Gibson",
        //        Title = "Pattern Recognition",
        //        Genre = "Science-Fiction?",
        //        Url = "http://amazon.com",
        //    },
        //    Tags = new[] { "Science-Ficton,", "Branding" }

        //};
        //list.Add(book);

        //var app = new Review()
        //{
        //    Id = Guid.NewGuid(),
        //    Category = Category.App,
        //    //Tenant = "Site1",
        //    App = new App()
        //    {
        //        Name = "Pluralsight",
        //        Url = "http://pluralsight.com",

        //    },
        //    Tags = new[] { "Learning" }
        //};
        //list.Add(app);

        //return list;

        [Test]
        public void TestSaveItem()
        {
           
            var db = new ReviewData();
            //var result = db.GetReviews();
            //result.Should().BeEmpty();
            //Book myBook = new Book
            //{
            //    Id = 501,
            //    Title = "AWS SDK for .NET Object Persistence Model Handling Arbitrary Data",
            //    ISBN = "999-9999999999",
            //    BookAuthors = new List<string> { "Author 1", "Author 2" },
            //    Dimensions = myBookDimensions
            //};

            var review = new Review
            {
                Id = Guid.NewGuid(),
                Category = Category.App,
                Author = "Some guy on the internet",
                Text = "Short inconsistent rant on something entirely different",
                //Tenant = "Site1",
                //Book = new Book()
                //{
                //    Author = "William Gibson",
                //    Title = "Pattern Recognition",
                //    Genre = "Science-Fiction?",
                //    Url = "http://amazon.com",
                //},
                Tags = new List<string>() { "Science-Ficton", "Economics" }

            };

            db.CreateReview(review);

        }

        [Test]
        public void TestGetItems()
        {
            var db = new ReviewData();
            var result = db.GetReviews().ToList();
            result.Should().NotBeEmpty();

            var itemToFetch = result.First();

            var theFetchedItem = db.GetReview(itemToFetch.Category, itemToFetch.Id);
            theFetchedItem.Should().NotBeNull();

            var anotherItemToFecth = db.GetReview(Category.App, Guid.NewGuid());
            anotherItemToFecth.Should().BeNull();

        }

        [Test]
        public void TestGetItemsByCategory()
        {
            var db = new ReviewData();
            var result = db.GetReviewsByCategory(Category.Book).ToList();
            result.Should().NotBeEmpty();

            var itemToFetch = result.First();

            var theFetchedItem = db.GetReview(itemToFetch.Category, itemToFetch.Id);
            theFetchedItem.Should().NotBeNull();

            var anotherItemToFecth = db.GetReview(Category.App, itemToFetch.Id);
            anotherItemToFecth.Should().BeNull();

        }

    }
}