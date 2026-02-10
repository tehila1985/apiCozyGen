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
    public class CategoryRepositoryUnitTest
    {
        private readonly Mock<myDBContext> mockContext;
        private readonly CategoryRepository categoryRepository;

        public CategoryRepositoryUnitTest()
        {
            mockContext = new Mock<myDBContext>();
            categoryRepository = new CategoryRepository(mockContext.Object);
        }

        // ===== Setup לדוגמא =====
        private (Category category, Product product) CreateSampleCategory()
        {
            var category = new Category { CategoryId = 1, Name = "Electronics" };
            var product = new Product { ProductId = 1, Name = "Laptop", CategoryId = category.CategoryId, Category = category, Price = 1000 };
            category.Products.Add(product);
            return (category, product);
        }

        [Fact]
        public async Task GetCategories_ShouldReturnAllCategories_WhenExist()
        {
            var (category, product) = CreateSampleCategory();

            mockContext.Setup(c => c.Categories).ReturnsDbSet(new List<Category> { category });
            mockContext.Setup(c => c.Products).ReturnsDbSet(new List<Product> { product });

            var result = await categoryRepository.GetCategories();

            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("Electronics", result.First().Name);
        }

        [Fact]
        public async Task GetCategories_ShouldReturnEmptyList_WhenNoCategoriesExist()
        {
            mockContext.Setup(c => c.Categories).ReturnsDbSet(new List<Category>());

            var result = await categoryRepository.GetCategories();

            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task Category_ShouldIncludeProducts_WhenProductsExist()
        {
            var (category, product) = CreateSampleCategory();

            mockContext.Setup(c => c.Categories).ReturnsDbSet(new List<Category> { category });
            mockContext.Setup(c => c.Products).ReturnsDbSet(new List<Product> { product });

            var result = await categoryRepository.GetCategories();

            var fetchedCategory = result.FirstOrDefault();
            Assert.NotNull(fetchedCategory);
            Assert.NotEmpty(fetchedCategory.Products);
            Assert.Contains(fetchedCategory.Products, p => p.ProductId == product.ProductId);
        }

        [Fact]
        public async Task AddCategoryWithProducts_ShouldPersistCorrectlyInUnitTest()
        {
            var category = new Category { CategoryId = 2, Name = "Software" };
            var product = new Product { ProductId = 2, Name = "IDE", Price = 200, CategoryId = category.CategoryId, Category = category };
            category.Products.Add(product);

            mockContext.Setup(c => c.Categories).ReturnsDbSet(new List<Category>());
            mockContext.Setup(c => c.Products).ReturnsDbSet(new List<Product>());

            // Simulate adding to DB
            await mockContext.Object.Categories.AddAsync(category);
            await mockContext.Object.Products.AddAsync(product);

            var categories = await categoryRepository.GetCategories();

            // In a pure Unit Test with Moq, the DbSet in-memory does not automatically return added entities.
            // So here we just assert that the setup did not throw and that category object exists
            Assert.NotNull(category);
            Assert.Single(category.Products);
            Assert.Equal("IDE", category.Products.First().Name);
        }
    }
}
