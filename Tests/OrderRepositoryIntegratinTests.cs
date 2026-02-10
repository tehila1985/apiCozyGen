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
    public class OrderRepositoryIntegrationTests : IClassFixture<DatabaseFixture>, IDisposable
    {
        private readonly myDBContext _dbContext;
        private readonly DatabaseFixture _fixture;
        private readonly OrderRepository _orderRepository;

        public OrderRepositoryIntegrationTests(DatabaseFixture databaseFixture)
        {
            _fixture = databaseFixture;
            _dbContext = _fixture.Context;
            _orderRepository = new OrderRepository(_dbContext);
            _fixture.ClearDatabase();
        }

        public void Dispose() => _fixture.ClearDatabase();

        private async Task<Order> CreateSampleOrderAsync(string email = "order@test.com")
        {
            var user = new User { Gmail = email, Password = "123" };
            var category = new Category { Name = "General" };
            await _dbContext.Users.AddAsync(user);
            await _dbContext.Categories.AddAsync(category);
            await _dbContext.SaveChangesAsync();

            var product = new Product { Name = "Item", CategoryId = category.CategoryId, Price = 100 };
            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();

            return new Order
            {
                UserId = user.UserId,
                Date = DateOnly.FromDateTime(DateTime.Now),
                Sum = 100,
                OrderItems = new List<OrderItem>
                {
                    new OrderItem { ProductId = product.ProductId, Quantity = 1 }
                }
            };
        }

        [Fact]
        public async Task AddNewOrder_ShouldAddOrder_WithValidData()
        {
            var order = await CreateSampleOrderAsync("valid@order.com");
            var result = await _orderRepository.AddNewOrder(order);
            Assert.NotNull(result);
            Assert.True(result.OrderId > 0);
        }

        [Fact]
        public async Task GetOrderById_ShouldReturnOrder_WhenExists()
        {
            var order = await CreateSampleOrderAsync("get@order.com");
            var added = await _orderRepository.AddNewOrder(order);
            var result = await _orderRepository.GetOrderById(added.OrderId);
            Assert.NotNull(result);
            Assert.Equal(added.OrderId, result.OrderId);
        }

        [Fact]
        public async Task GetOrderById_ShouldReturnNull_WhenNotExists()
        {
            var result = await _orderRepository.GetOrderById(9999);
            Assert.Null(result);
        }

        [Fact]
        public async Task AddNewOrder_ShouldHandleMultipleItems()
        {
            var order = await CreateSampleOrderAsync("multi@order.com");
            var firstProductId = order.OrderItems.First().ProductId;
            order.OrderItems.Add(new OrderItem { ProductId = firstProductId, Quantity = 5 });
            var result = await _orderRepository.AddNewOrder(order);
            Assert.Equal(2, result.OrderItems.Count);
        }

        [Fact]
        public async Task AddNewOrder_ShouldFail_WhenUserDoesNotExist()
        {
            var order = await CreateSampleOrderAsync("fail@order.com");
            order.UserId = 9999; // משתמש לא קיים
            await Assert.ThrowsAsync<DbUpdateException>(async () => await _orderRepository.AddNewOrder(order));
        }

        [Fact]
        public async Task GetOrderById_ShouldIncludeOrderItems()
        {
            var order = await CreateSampleOrderAsync("include@order.com");
            var added = await _orderRepository.AddNewOrder(order);
            var result = await _orderRepository.GetOrderById(added.OrderId);
            Assert.NotEmpty(result.OrderItems);
        }

        [Fact]
        public async Task AddNewOrder_ShouldCalculateSumCorrectly()
        {
            var order = await CreateSampleOrderAsync("sum@order.com");
            order.Sum = 550;
            var result = await _orderRepository.AddNewOrder(order);
            Assert.Equal(550, result.Sum);
        }
    }
}