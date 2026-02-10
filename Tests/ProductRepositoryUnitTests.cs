using Microsoft.EntityFrameworkCore;
using Model;
using Moq;
using Moq.EntityFrameworkCore;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class ProductRepositoryUnitTests
    {
        private readonly Mock<myDBContext> mockContext;
        private readonly ProductRepository productRepository;

        public ProductRepositoryUnitTests()
        {
            mockContext = new Mock<myDBContext>();
            productRepository = new ProductRepository(mockContext.Object);
        }

        // ===== Sample Data =====
        private List<Product> CreateSampleProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    ProductId = 1,
                    Name = "Cheap",
                    CategoryId = 1,
                    Price = 50,
                    Description = "Cheap product"
                },
                new Product
                {
                    ProductId = 2,
                    Name = "Mid",
                    CategoryId = 2,
                    Price = 150,
                    Description = "Mid product"
                },
                new Product
                {
                    ProductId = 3,
                    Name = "Expensive",
                    CategoryId = 1,
                    Price = 300,
                    Description = "Expensive product"
                }
            };
        }

        [Fact]
        public async Task getProducts_ShouldReturnAllProducts_WhenNoFilters()
        {
            var products = CreateSampleProducts();
            mockContext.Setup(c => c.Products).ReturnsDbSet(products);

            var result = await productRepository.getProducts(
                position: 1,
                skip: 10,
                desc: null,
                minPrice: null,
                maxPrice: null,
                categoryIds: Array.Empty<int?>());

            Assert.Equal(3, result.TotalCount);
            Assert.Equal(3, result.Items.Count);
        }

        [Fact]
        public async Task getProducts_ShouldReturnEmpty_WhenNoProductsExist()
        {
            mockContext.Setup(c => c.Products).ReturnsDbSet(new List<Product>());

            var result = await productRepository.getProducts(
                position: 1,
                skip: 10,
                desc: null,
                minPrice: null,
                maxPrice: null,
                categoryIds: Array.Empty<int?>());

            Assert.Empty(result.Items);
            Assert.Equal(0, result.TotalCount);
        }

        [Fact]
        public async Task getProducts_ShouldFilterByCategory()
        {
            var products = CreateSampleProducts();
            mockContext.Setup(c => c.Products).ReturnsDbSet(products);

            var result = await productRepository.getProducts(
                position: 1,
                skip: 10,
                desc: null,
                minPrice: null,
                maxPrice: null,
                categoryIds: new int?[] { 1 });

            Assert.Equal(2, result.TotalCount);
            Assert.All(result.Items, p => Assert.Equal(1, p.CategoryId));
        }

        [Fact]
        public async Task getProducts_ShouldFilterByPriceRange()
        {
            var products = CreateSampleProducts();
            mockContext.Setup(c => c.Products).ReturnsDbSet(products);

            var result = await productRepository.getProducts(
                position: 1,
                skip: 10,
                desc: null,
                minPrice: 100,
                maxPrice: 250,
                categoryIds: Array.Empty<int?>());

            Assert.Single(result.Items);
            Assert.Equal(150, result.Items.First().Price);
        }

        [Fact]
        public async Task getProducts_ShouldFilterByDescription()
        {
            var products = CreateSampleProducts();
            mockContext.Setup(c => c.Products).ReturnsDbSet(products);

            var result = await productRepository.getProducts(
                position: 1,
                skip: 10,
                desc: "Expensive",
                minPrice: null,
                maxPrice: null,
                categoryIds: Array.Empty<int?>());

            Assert.Single(result.Items);
            Assert.Equal("Expensive", result.Items.First().Name);
        }

        [Fact]
        public async Task getProducts_ShouldSupportPagination()
        {
            var products = CreateSampleProducts();
            mockContext.Setup(c => c.Products).ReturnsDbSet(products);

            var result = await productRepository.getProducts(
                position: 2,
                skip: 1,
                desc: null,
                minPrice: null,
                maxPrice: null,
                categoryIds: Array.Empty<int?>());

            Assert.Equal(3, result.TotalCount);
            Assert.Single(result.Items);
        }
    }
}
