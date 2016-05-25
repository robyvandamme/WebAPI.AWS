using API.Config;
using API.Data.DynamoDb;
using API.Model;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace API.Tests
{
    [TestFixture]
    public class DynamoDbTableTests
    {
        [Test]
        public void TableNameTest()
        {
            var context = new Mock<IContext>();
            context.Setup(c => c.Environment).Returns("DEV");

            var table = new DynamoDbTable<Review>(context.Object);
            table.TableName.Should().Be("Review-Dev");
        }
        
    }
}