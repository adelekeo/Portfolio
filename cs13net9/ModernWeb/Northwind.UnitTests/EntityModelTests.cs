
using Northwind.EntityModels;

namespace Northwind.UnitTests
{
    public class UnitTest1
    {
        [Fact]
        public void DatabaseConnectTest()
        {
            using NorthwindContext db = new();
            Assert.True(db.Database.CanConnect());

        }

        [Fact]
        public void CategoryCountTest()
        {
            using NorthwindContext db = new();
            int expected = 8;
            int actual = db.Categories.Count();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ProductIdIsChaiTest()
        {
            using NorthwindContext db = new();
            string expected = "Chai";
            Product? product = db.Products.Find(keyValues: 1);
            string actual = product?.ProductName ?? string.Empty;
            Assert.Equal(expected, actual);
        }
    }
}
