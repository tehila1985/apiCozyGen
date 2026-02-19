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
    public class StyleRepositoryIntegrationTests : IClassFixture<DatabaseFixture>, IDisposable
    {
        private readonly myDBContext _dbContext;
        private readonly DatabaseFixture _fixture;
        private readonly StyleRepository _styleRepository;

        public StyleRepositoryIntegrationTests(DatabaseFixture databaseFixture)
        {
            _fixture = databaseFixture;
            _dbContext = _fixture.Context;
            _styleRepository = new StyleRepository(_dbContext);
            _fixture.ClearDatabase();
        }

        public void Dispose() => _fixture.ClearDatabase();

        private async Task<Style> CreateSampleStyleAsync(string name = "General", string description = "Test style")
        {
            var style = new Style
            {
                Name = name,
                Description = description,
                ImageUrl = "http://example.com/style.png"
            };
            await _dbContext.Styles.AddAsync(style);
            await _dbContext.SaveChangesAsync();
            return style;
        }

        [Fact]
        public async Task AddNewStyle_ShouldAddStyleSuccessfully()
        {
            var style = new Style { Name = "Modern", Description = "Modern style", ImageUrl = "http://example.com/modern.png" };
            await _dbContext.Styles.AddAsync(style);
            await _dbContext.SaveChangesAsync();

            var savedStyle = await _dbContext.Styles.FindAsync(style.StyleId);
            Assert.NotNull(savedStyle);
            Assert.Equal("Modern", savedStyle.Name);
        }

        [Fact]
        public async Task GetStyles_ShouldReturnAllStyles_WhenExist()
        {
            var style1 = await CreateSampleStyleAsync("Classic", "Classic style");
            var style2 = await CreateSampleStyleAsync("Minimalist", "Minimalist style");

            var result = await _styleRepository.GetStyles();

            Assert.NotNull(result);
            Assert.True(result.Count >= 2);
            Assert.Contains(result, s => s.StyleId == style1.StyleId);
            Assert.Contains(result, s => s.StyleId == style2.StyleId);
        }

        [Fact]
        public async Task GetStyles_ShouldReturnEmptyList_WhenNoStylesExist()
        {
            var result = await _styleRepository.GetStyles();

            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetStyles_ShouldReturnStylesWithCorrectProperties()
        {
            var style = await CreateSampleStyleAsync("Vintage", "Vintage style description");

            var styles = await _styleRepository.GetStyles();

            var fetchedStyle = styles.Find(s => s.StyleId == style.StyleId);
            Assert.NotNull(fetchedStyle);
            Assert.Equal("Vintage", fetchedStyle.Name);
            Assert.Equal("Vintage style description", fetchedStyle.Description);
            Assert.Equal("http://example.com/style.png", fetchedStyle.ImageUrl);
        }

        [Fact]
        public async Task AddMultipleStyles_ShouldPersistCorrectly()
        {
            var style1 = new Style { Name = "Industrial", Description = "Industrial style" };
            var style2 = new Style { Name = "Bohemian", Description = "Bohemian style" };

            await _dbContext.Styles.AddRangeAsync(style1, style2);
            await _dbContext.SaveChangesAsync();

            var styles = await _styleRepository.GetStyles();

            Assert.True(styles.Count >= 2);
            Assert.Contains(styles, s => s.Name == "Industrial");
            Assert.Contains(styles, s => s.Name == "Bohemian");
        }
    }
}
