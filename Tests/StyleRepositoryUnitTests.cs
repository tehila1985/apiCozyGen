using Microsoft.EntityFrameworkCore;
using Repository.Models;
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
    public class StyleRepositoryUnitTests
    {
        private readonly Mock<myDBContext> mockContext;
        private readonly StyleRepository styleRepository;

        public StyleRepositoryUnitTests()
        {
            mockContext = new Mock<myDBContext>(new DbContextOptions<myDBContext>());
            styleRepository = new StyleRepository(mockContext.Object);
        }

        // ===== Sample Data =====
        private List<Style> CreateSampleStyles()
        {
            return new List<Style>
            {
                new Style
                {
                    StyleId = 1,
                    Name = "Modern",
                    Description = "Modern style",
                    ImageUrl = "http://example.com/modern.png"
                },
                new Style
                {
                    StyleId = 2,
                    Name = "Classic",
                    Description = "Classic style",
                    ImageUrl = "http://example.com/classic.png"
                },
                new Style
                {
                    StyleId = 3,
                    Name = "Minimalist",
                    Description = "Minimalist style",
                    ImageUrl = "http://example.com/minimalist.png"
                }
            };
        }

        [Fact]
        public async Task GetStyles_ShouldReturnAllStyles_WhenExist()
        {
            var styles = CreateSampleStyles();
            mockContext.Setup(c => c.Styles).ReturnsDbSet(styles);

            var result = await styleRepository.GetStyles();

            Assert.NotNull(result);
            Assert.Equal(3, result.Count);
            Assert.Contains(result, s => s.Name == "Modern");
            Assert.Contains(result, s => s.Name == "Classic");
            Assert.Contains(result, s => s.Name == "Minimalist");
        }

        [Fact]
        public async Task GetStyles_ShouldReturnEmptyList_WhenNoStylesExist()
        {
            mockContext.Setup(c => c.Styles).ReturnsDbSet(new List<Style>());

            var result = await styleRepository.GetStyles();

            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetStyles_ShouldReturnStylesWithCorrectProperties()
        {
            var styles = CreateSampleStyles();
            mockContext.Setup(c => c.Styles).ReturnsDbSet(styles);

            var result = await styleRepository.GetStyles();

            var modernStyle = result.FirstOrDefault(s => s.Name == "Modern");
            Assert.NotNull(modernStyle);
            Assert.Equal("Modern style", modernStyle.Description);
            Assert.Equal("http://example.com/modern.png", modernStyle.ImageUrl);
        }

        [Fact]
        public async Task GetStyles_ShouldReturnSingleStyle_WhenOnlyOneExists()
        {
            var style = new Style { StyleId = 1, Name = "Vintage", Description = "Vintage style" };
            mockContext.Setup(c => c.Styles).ReturnsDbSet(new List<Style> { style });

            var result = await styleRepository.GetStyles();

            Assert.Single(result);
            Assert.Equal("Vintage", result.First().Name);
        }
    }
}
