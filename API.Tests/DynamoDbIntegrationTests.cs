using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Amazon;
using Amazon.DynamoDBv2;
using API.Config;
using API.Data;
using API.Model;
using FluentAssertions;
using Moq;
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

        private static AmazonDynamoDBClient _dbClient = new AmazonDynamoDBClient(DynamoDbHelper.ConfigureDynamoDb());

        [Test]
        public void TestSaveItem()
        {

            var context = new Mock<IContext>();
            context.Setup(c => c.Environment).Returns("LOCAL");

            var db = new ReviewTable(_dbClient, context.Object);
            var review = new Review
            {
                Id = Guid.NewGuid(),
                Category = Category.Book,
                Author = "Some guy on the internet",
                Text = "Short inconsistent rant on something entirely different",
                //Tenant = "Site1",
                Book = new Book()
                {
                    Author = "William Gibson",
                    Title = "Pattern Recognition",
                    Genre = "Science-Fiction?",
                    Url = "http://amazon.com",
                },
                Tags = new List<string>() { "Science-Ficton", "Economics" }

            };
            db.SaveReview(review);
        }

        [Test]
        public void TestGetItems()
        {
            var context = new Mock<IContext>();
            context.Setup(c => c.Environment).Returns("LOCAL");

            var db = new ReviewTable(_dbClient, context.Object);

            var result = db.GetReviews().ToList();
            result.Should().NotBeEmpty();

            var itemToFetch = result.First();

            var theFetchedItem = db.GetReview(itemToFetch.Category, itemToFetch.Id);
            theFetchedItem.Should().NotBeNull();

            var anotherItemToFecth = db.GetReview(Category.App, Guid.NewGuid());
            anotherItemToFecth.Should().BeNull();

        }

        [Test]
        public void TestLikeItem()
        {
            var context = new Mock<IContext>();
            context.Setup(c => c.Environment).Returns("LOCAL");

            var db = new ReviewTable(_dbClient, context.Object);

            var result = db.GetReviews().ToList();
            result.Should().NotBeEmpty();

            var itemToFetch = result.First();

            var theFetchedItem = db.GetReview(itemToFetch.Category, itemToFetch.Id);
            theFetchedItem.Should().NotBeNull();

            var likes = theFetchedItem.Likes;
            db.LikeReview(theFetchedItem.Category, theFetchedItem.Id);
            // TODO: return the new number of likes?

            // may not be updated immediatley of course, but let's see...
            var theLikedItem = db.GetReview(itemToFetch.Category, itemToFetch.Id);
            theLikedItem.Should().NotBeNull();
            theLikedItem.Likes.Should().BeGreaterThan(likes);

        }

        [Test]
        public void TestGetItemsByCategory()
        {
            var context = new Mock<IContext>();
            context.Setup(c => c.Environment).Returns("LOCAL");

            var db = new ReviewTable(_dbClient, context.Object);

            var result = db.GetReviewsByCategory(Category.Book).ToList();
            result.Should().NotBeEmpty();

            var itemToFetch = result.FirstOrDefault(i => i.Category.Equals(Category.Book));

            var theFetchedItem = db.GetReview(itemToFetch.Category, itemToFetch.Id);
            theFetchedItem.Should().NotBeNull();

            var anotherItemToFecth = db.GetReview(Category.App, itemToFetch.Id);
            anotherItemToFecth.Should().BeNull();

        }

    }
}