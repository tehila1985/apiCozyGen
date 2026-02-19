using Microsoft.EntityFrameworkCore;
using Repository.Models;
using Repository;
using Test;
using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tests
{
    [Collection("Database Collection")]
    public class ProductRepositoryIntegrationTests : IClassFixture<DatabaseFixture>, IDisposable
    {
        private readonly myDBContext _dbContext;
        private readonly DatabaseFixture _fixture;
        private readonly ProductRepository _productRepository;

        public ProductRepositoryIntegrationTests(DatabaseFixture databaseFixture)
        {
            _fixture = databaseFixture;
            _dbContext = _fixture.Context;
            _productRepository = new ProductRepository(_dbContext);
            _fixture.ClearDatabase();
        }

        public void Dispose() => _fixture.ClearDatabase();

        private async Task<Product> CreateSampleProductAsync(
            string name = "Sample Product",
            int price = 100,
            int? categoryId = null)
        {
            if (categoryId == null)
            {
                var category = new Category { Name = $"Category_{Guid.NewGuid()}", Description = "General category" };
                await _dbContext.Categories.AddAsync(category);
                await _dbContext.SaveChangesAsync();
                categoryId = category.CategoryId;
            }

            var product = new Product
            {
                Name = name,
                CategoryId = categoryId.Value,
                Price = price,
                Description = "Test description",
                FrontImageUrl = "http://example.com/image.png",
                Stock = 10,
                IsActive = true
            };

            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();

            return product;
        }

        [Fact]
        public async Task GetProducts_ShouldReturnAllProducts_WhenNoFilters()
        {
            var p1 = await CreateSampleProductAsync("P1", 50);
            var p2 = await CreateSampleProductAsync("P2", 100);
            var p3 = await CreateSampleProductAsync("P3", 150);

            var result = await _productRepository.getProducts(
                position: 1,
                skip: 10,
                desc: null,
                minPrice: null,
                maxPrice: null,
                categoryIds: Array.Empty<int?>(),
                styleIds: Array.Empty<int?>());

            Assert.Equal(3, result.TotalCount);
            Assert.Contains(result.Items, p => p.ProductId == p1.ProductId);
            Assert.Contains(result.Items, p => p.ProductId == p2.ProductId);
            Assert.Contains(result.Items, p => p.ProductId == p3.ProductId);
        }

        [Fact]
        public async Task GetProducts_ShouldFilterByCategory()
        {
            var cat1 = new Category { Name = "Cat1", Description = "Category 1" };
            var cat2 = new Category { Name = "Cat2", Description = "Category 2" };
            await _dbContext.Categories.AddRangeAsync(cat1, cat2);
            await _dbContext.SaveChangesAsync();

            var p1 = await CreateSampleProductAsync("P1", 50, cat1.CategoryId);
            var p2 = await CreateSampleProductAsync("P2", 100, cat2.CategoryId);

            var result = await _productRepository.getProducts(
                position: 1,
                skip: 10,
                desc: null,
                minPrice: null,
                maxPrice: null,
                categoryIds: new int?[] { cat1.CategoryId },
                styleIds: Array.Empty<int?>());

            Assert.Single(result.Items);
            Assert.Equal(p1.ProductId, result.Items.First().ProductId);
            Assert.Equal(1, result.TotalCount);
        }

        [Fact]
        public async Task GetProducts_ShouldFilterByPriceRange()
        {
            var cheap = await CreateSampleProductAsync("Cheap", 50);
            var expensive = await CreateSampleProductAsync("Expensive", 200);

            var result = await _productRepository.getProducts(
                position: 1,
                skip: 10,
                desc: null,
                minPrice: 100,
                maxPrice: 300,
                categoryIds: Array.Empty<int?>(),
                styleIds: Array.Empty<int?>());

            Assert.Single(result.Items);
            Assert.Equal(expensive.ProductId, result.Items.First().ProductId);
        }

        [Fact]
        public async Task GetProducts_ShouldSupportPagination()
        {
            for (int i = 0; i < 5; i++)
            {
                await CreateSampleProductAsync($"Product{i}", 50 + i * 10);
            }

            var result = await _productRepository.getProducts(
                position: 2,
                skip: 2,
                desc: null,
                minPrice: null,
                maxPrice: null,
                categoryIds: Array.Empty<int?>(),
                styleIds: Array.Empty<int?>());

            Assert.Equal(5, result.TotalCount);
            Assert.Equal(2, result.Items.Count);
        }
    }
}
