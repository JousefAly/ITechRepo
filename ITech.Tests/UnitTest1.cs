using ITech.Data.Entites;
using System;
using Xunit;

namespace ITech.Tests
{
    public class ProductShould
    {
        [Fact]
        public void BeOutOfStockWhenNew()
        {
            var sut = new Product();
            Assert.Equal(0, sut.Stock);
        }
        [Fact(Skip = "Don't need to run this test!")]
        public void NotHaveTitleByDefault()
        {
            var sut = new Product();
            Assert.Null(sut.Title);
        }
    }
}
