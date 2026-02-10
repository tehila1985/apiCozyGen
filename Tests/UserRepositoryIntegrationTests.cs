using Microsoft.EntityFrameworkCore;
using Model;
using Repository;
using Test;
using Xunit;
using System;
using System.Threading.Tasks;

namespace Tests
{
    [Collection("Database Collection")]
    public class UserRepositoryIntegrationTests : IClassFixture<DatabaseFixture>, IDisposable
    {
        private readonly myDBContext _dbContext;
        private readonly DatabaseFixture _fixture;
        private readonly UserRepository _userRepository;

        public UserRepositoryIntegrationTests(DatabaseFixture databaseFixture)
        {
            _fixture = databaseFixture;
            _dbContext = _fixture.Context;
            _userRepository = new UserRepository(_dbContext);
            _fixture.ClearDatabase();
        }

        public void Dispose() => _fixture.ClearDatabase();

        [Fact]
        public async Task AddNewUser_ShouldAddUser_WithValidData()
        {
            var user = new User { Gmail = "valid.email@example.com", Password = "StrongPassword123" };
            var result = await _userRepository.AddNewUser(user);
            Assert.NotNull(result);
            Assert.Equal(user.Gmail, result.Gmail);
        }

        [Fact]
        public async Task GetUserById_ShouldReturnNull_WhenUserDoesNotExist()
        {
            var result = await _userRepository.GetUserById(9999);
            Assert.Null(result);
        }

        [Fact]
        public async Task Login_ShouldReturnNull_WhenInvalidCredentialsProvided()
        {
            await _userRepository.AddNewUser(new User { Gmail = "login@test.com", Password = "Password123" });
            var loginUser = new User { Gmail = "login@test.com", Password = "WrongPassword" };
            var result = await _userRepository.Login(loginUser);
            Assert.Null(result);
        }

        [Fact]
        public async Task Update_ShouldThrowException_WhenUserNotFound()
        {
            var user = new User { UserId = 9999, Gmail = "notfound@test.com", Password = "Password123" };
            await Assert.ThrowsAsync<DbUpdateConcurrencyException>(async () => await _userRepository.update(user.UserId, user));
        }

        [Fact]
        public async Task GetUsers_ShouldReturnEmptyList_WhenNoUsersExist()
        {
            var result = await _userRepository.GetUsers();
            Assert.Empty(result);
        }

        [Fact]
        public async Task Login_ShouldReturnNull_WhenPasswordIsEmpty()
        {
            await _userRepository.AddNewUser(new User { Gmail = "empty@test.com", Password = "Password123" });
            var loginUser = new User { Gmail = "empty@test.com", Password = "" };
            var result = await _userRepository.Login(loginUser);
            Assert.Null(result);
        }

        [Fact]
        public async Task GetUserById_ShouldReturnNull_WhenIdIsNegative()
        {
            var result = await _userRepository.GetUserById(-1);
            Assert.Null(result);
        }

        [Fact]
        public async Task Update_ShouldReturnUpdatedUser_WhenValidDataProvided()
        {
            var user = await _userRepository.AddNewUser(new User { Gmail = "update@test.com", Password = "OldPassword" });
            user.Password = "NewPassword123";
            var result = await _userRepository.update(user.UserId, user);
            Assert.NotNull(result);
            Assert.Equal("NewPassword123", result.Password);
        }

        [Fact]
        public async Task AddNewUser_ShouldSetDefaultValues_WhenUserIsCreated()
        {
            var user = new User { Gmail = "default@test.com", Password = "Password123" };
            var result = await _userRepository.AddNewUser(user);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetUserById_ShouldReturnUser_WhenUserExists()
        {
            var user = await _userRepository.AddNewUser(new User { Gmail = "findme@test.com", Password = "Password123" });
            var result = await _userRepository.GetUserById(user.UserId);
            Assert.NotNull(result);
            Assert.Equal("findme@test.com", result.Gmail);
        }

        [Fact]
        public async Task GetUsers_ShouldReturnListOfUsers_WhenUsersExist()
        {
            await _userRepository.AddNewUser(new User { Gmail = "u1@test.com", Password = "Password123" });
            await _userRepository.AddNewUser(new User { Gmail = "u2@test.com", Password = "Password123" });
            var result = await _userRepository.GetUsers();
            Assert.Equal(2, result.Count);
        }
      
        [Fact]
        public async Task Login_ShouldReturnUser_WithValidCredentials()
        {
            await _userRepository.AddNewUser(new User { Gmail = "loginok@test.com", Password = "CorrectPassword" });
            var result = await _userRepository.Login(new User { Gmail = "loginok@test.com", Password = "CorrectPassword" });
            Assert.NotNull(result);
        }

        //[Fact]
        //public async Task AddNewUser_ShouldThrowValidationException_WhenPasswordTooShort()
        //{
        //    var user = new User { Gmail = "short@test.com", Password = "1" };
        //    await Assert.ThrowsAsync<DbUpdateException>(async () => await _userRepository.AddNewUser(user));
        //}
        //[Fact]
        //public async Task Update_ShouldThrowException_WhenDataIsInvalid()
        //{
        //    var user = await _userRepository.AddNewUser(new User { Gmail = "invalid@test.com", Password = "Password123" });
        //    user.Gmail = "not-an-email";
        //    await Assert.ThrowsAsync<DbUpdateException>(async () => await _userRepository.update(user.UserId, user));
        //}

        //[Fact]
        //public async Task AddNewUser_ShouldThrowValidationException_WhenEmailInvalid()
        //{
        //    var user = new User { Gmail = "bad-email", Password = "Password123" };
        //    await Assert.ThrowsAsync<DbUpdateException>(async () => await _userRepository.AddNewUser(user));
        //}
        //[Fact]
        //public async Task AddNewUser_ShouldThrowValidationException_WhenPasswordTooShort()
        //{
        //    var user = new User { Gmail = "short@test.com", Password = "1" };
        //    await Assert.ThrowsAsync<DbUpdateException>(async () => await _userRepository.AddNewUser(user));
        //}
        //[Fact]
        //public async Task AddNewUser_ShouldThrowValidationException_WhenEmailAlreadyExists()
        //{
        //    await _userRepository.AddNewUser(new User { Gmail = "dup@test.com", Password = "Password123" });
        //    var duplicateUser = new User { Gmail = "dup@test.com", Password = "Password456" };
        //    await Assert.ThrowsAsync<DbUpdateException>(async () => await _userRepository.AddNewUser(duplicateUser));
        //}
    }
}








