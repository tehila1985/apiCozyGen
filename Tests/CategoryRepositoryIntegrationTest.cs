using Microsoft.EntityFrameworkCore;
using Model;
using Repository;
using Test;
using Xunit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tests
{
    [Collection("Database Collection")]
    public class CategoryRepositoryIntegrationTest : IClassFixture<DatabaseFixture>, IDisposable
    {
        private readonly myDBContext _dbContext;
        private readonly DatabaseFixture _fixture;
        private readonly CategoryRepository _categoryRepository;

        public CategoryRepositoryIntegrationTest(DatabaseFixture databaseFixture)
        {
            _fixture = databaseFixture;
            _dbContext = _fixture.Context;
            _categoryRepository = new CategoryRepository(_dbContext);
            _fixture.ClearDatabase();
        }

        public void Dispose() => _fixture.ClearDatabase();

        private async Task<Category> CreateSampleCategoryAsync(string name = "General")
        {
            var category = new Category { Name = name };
            await _dbContext.Categories.AddAsync(category);
            await _dbContext.SaveChangesAsync();
            return category;
        }

        private async Task<Product> CreateSampleProductAsync(Category category, string productName = "Sample Product", int price = 100)
        {
            var product = new Product
            {
                Name = productName,
                CategoryId = category.CategoryId,
                Price = price,
                Description = "Test product",
                ImageUrl = "http://example.com/product.png"
            };
            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();
            return product;
        }

        [Fact]
        public async Task AddNewCategory_ShouldAddCategorySuccessfully()
        {
            var category = new Category { Name = "Books" };
            await _dbContext.Categories.AddAsync(category);
            await _dbContext.SaveChangesAsync();

            var savedCategory = await _dbContext.Categories.FindAsync(category.CategoryId);
            Assert.NotNull(savedCategory);
            Assert.Equal("Books", savedCategory.Name);
        }

        [Fact]
        public async Task GetCategories_ShouldReturnAllCategories_WhenExist()
        {
            var category1 = await CreateSampleCategoryAsync("Electronics");
            var category2 = await CreateSampleCategoryAsync("Books");

            var result = await _categoryRepository.GetCategories();

            Assert.NotNull(result);
            Assert.True(result.Count >= 2);
            Assert.Contains(result, c => c.CategoryId == category1.CategoryId);
            Assert.Contains(result, c => c.CategoryId == category2.CategoryId);
        }

        [Fact]
        public async Task GetCategories_ShouldReturnEmptyList_WhenNoCategoriesExist()
        {
            var result = await _categoryRepository.GetCategories();

            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task Category_ShouldIncludeProducts_WhenProductsExist()
        {
            var category = await CreateSampleCategoryAsync("Gadgets");
            var product1 = await CreateSampleProductAsync(category, "Phone", 500);
            var product2 = await CreateSampleProductAsync(category, "Tablet", 800);

            var categories = await _categoryRepository.GetCategories();

            var fetchedCategory = categories.Find(c => c.CategoryId == category.CategoryId);
            Assert.NotNull(fetchedCategory);
            Assert.NotEmpty(fetchedCategory.Products);
            Assert.Contains(fetchedCategory.Products, p => p.ProductId == product1.ProductId);
            Assert.Contains(fetchedCategory.Products, p => p.ProductId == product2.ProductId);
        }

        [Fact]
        public async Task AddCategoryWithProducts_ShouldPersistCorrectly()
        {
            var category = new Category { Name = "Software" };
            var product = new Product { Name = "IDE", Price = 200, Category = category };
            category.Products.Add(product);

            await _dbContext.Categories.AddAsync(category);
            await _dbContext.SaveChangesAsync();

            var savedCategory = await _dbContext.Categories
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.CategoryId == category.CategoryId);

            Assert.NotNull(savedCategory);
            Assert.Single(savedCategory.Products);
            Assert.Equal("IDE", savedCategory.Products.First().Name);
        }
    }
}
