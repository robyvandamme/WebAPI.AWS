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
        //    Category = Category.Books, // do we need the subject? if the review includes a book it is a book review, if it includes an app it is an app review...
        //    //Tenant = "Site1",
        //    Books = new Books()
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
        //    Category = Category.Apps,
        //    //Tenant = "Site1",
        //    Apps = new Apps()
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
                Category = Category.Books,
                Author = "Some guy on the internet",
                Text = "The first of William Gibson\'s usually futuristic novels to be set in the present, Pattern Recognition is a masterful snapshot of modern consumer culture and hipster esoterica. Set in London, Tokyo, and Moscow, Pattern Recognition takes the reader on a tour of a global village inhabited by power-hungry marketeers, industrial saboteurs, high-end hackers, Russian mob bosses, Internet fan-boys, techno archeologists, washed-out spies, cultural documentarians, and our heroine Cayce Pollard--a soothsaying \"cool hunter\" with an allergy to brand names.",
                Book = new Book()
                {
                    Author = "William Gibson",
                    Title = "Pattern Recognition",
                    Genre = "Science-Fiction",
                    Url = "http://amazon.com",
                },
                Tags = new List<string>() { "Science-Ficton", "Branding" }
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

            var anotherItemToFecth = db.GetReview(Category.Apps, Guid.NewGuid());
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

            var result = db.GetReviewsByCategory(Category.Books).ToList();
            result.Should().NotBeEmpty();

            var itemToFetch = result.FirstOrDefault(i => i.Category.Equals(Category.Books));

            var theFetchedItem = db.GetReview(itemToFetch.Category, itemToFetch.Id);
            theFetchedItem.Should().NotBeNull();

            var anotherItemToFecth = db.GetReview(Category.Apps, itemToFetch.Id);
            anotherItemToFecth.Should().BeNull();

        }

    }
}