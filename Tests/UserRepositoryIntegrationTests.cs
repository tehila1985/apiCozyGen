using Microsoft.EntityFrameworkCore;
using Repository.Models;
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
            var user = new User { Email = "valid.email@example.com", PasswordHash = "StrongPassword123", FirstName = "John", LastName = "Doe", Role = "Customer", IsClubMember = false };
            var result = await _userRepository.AddNewUser(user);
            Assert.NotNull(result);
            Assert.Equal(user.Email, result.Email);
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
            await _userRepository.AddNewUser(new User { Email = "login@test.com", PasswordHash = "Password123", FirstName = "Test", LastName = "User", Role = "Customer", IsClubMember = false });
            var loginUser = new User { Email = "login@test.com", PasswordHash = "WrongPassword" };
            var result = await _userRepository.Login(loginUser);
            Assert.Null(result);
        }

        [Fact]
        public async Task Update_ShouldThrowException_WhenUserNotFound()
        {
            var user = new User { UserId = 9999, Email = "notfound@test.com", PasswordHash = "Password123", FirstName = "Test", LastName = "User", Role = "Customer", IsClubMember = false };
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
            await _userRepository.AddNewUser(new User { Email = "empty@test.com", PasswordHash = "Password123", FirstName = "Test", LastName = "User", Role = "Customer", IsClubMember = false });
            var loginUser = new User { Email = "empty@test.com", PasswordHash = "" };
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
            var user = await _userRepository.AddNewUser(new User { Email = "update@test.com", PasswordHash = "OldPassword", FirstName = "Test", LastName = "User", Role = "Customer", IsClubMember = false });
            user.PasswordHash = "NewPassword123";
            var result = await _userRepository.update(user.UserId, user);
            Assert.NotNull(result);
            Assert.Equal("NewPassword123", result.PasswordHash);
        }

        [Fact]
        public async Task AddNewUser_ShouldSetDefaultValues_WhenUserIsCreated()
        {
            var user = new User { Email = "default@test.com", PasswordHash = "Password123", FirstName = "Test", LastName = "User", Role = "Customer", IsClubMember = false };
            var result = await _userRepository.AddNewUser(user);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetUserById_ShouldReturnUser_WhenUserExists()
        {
            var user = await _userRepository.AddNewUser(new User { Email = "findme@test.com", PasswordHash = "Password123", FirstName = "Test", LastName = "User", Role = "Customer", IsClubMember = false });
            var result = await _userRepository.GetUserById(user.UserId);
            Assert.NotNull(result);
            Assert.Equal("findme@test.com", result.Email);
        }

        [Fact]
        public async Task GetUsers_ShouldReturnListOfUsers_WhenUsersExist()
        {
            await _userRepository.AddNewUser(new User { Email = "u1@test.com", PasswordHash = "Password123", FirstName = "User", LastName = "One", Role = "Customer", IsClubMember = false });
            await _userRepository.AddNewUser(new User { Email = "u2@test.com", PasswordHash = "Password123", FirstName = "User", LastName = "Two", Role = "Customer", IsClubMember = false });
            var result = await _userRepository.GetUsers();
            Assert.Equal(2, result.Count);
        }
      
        [Fact]
        public async Task Login_ShouldReturnUser_WithValidCredentials()
        {
            await _userRepository.AddNewUser(new User { Email = "loginok@test.com", PasswordHash = "CorrectPassword", FirstName = "Test", LastName = "User", Role = "Customer", IsClubMember = false });
            var result = await _userRepository.Login(new User { Email = "loginok@test.com", PasswordHash = "CorrectPassword" });
            Assert.NotNull(result);
        }

        //[Fact]
        //public async Task AddNewUser_ShouldThrowValidationException_WhenPasswordTooShort()
        //{
        //    var user = new User { Email = "short@test.com", PasswordHash = "1", FirstName = "Test", LastName = "User", Role = "Customer", IsClubMember = false };
        //    await Assert.ThrowsAsync<DbUpdateException>(async () => await _userRepository.AddNewUser(user));
        //}
        //[Fact]
        //public async Task Update_ShouldThrowException_WhenDataIsInvalid()
        //{
        //    var user = await _userRepository.AddNewUser(new User { Email = "invalid@test.com", PasswordHash = "Password123", FirstName = "Test", LastName = "User", Role = "Customer", IsClubMember = false });
        //    user.Email = "not-an-email";
        //    await Assert.ThrowsAsync<DbUpdateException>(async () => await _userRepository.update(user.UserId, user));
        //}

        //[Fact]
        //public async Task AddNewUser_ShouldThrowValidationException_WhenEmailInvalid()
        //{
        //    var user = new User { Email = "bad-email", PasswordHash = "Password123", FirstName = "Test", LastName = "User", Role = "Customer", IsClubMember = false };
        //    await Assert.ThrowsAsync<DbUpdateException>(async () => await _userRepository.AddNewUser(user));
        //}
        //[Fact]
        //public async Task AddNewUser_ShouldThrowValidationException_WhenPasswordTooShort()
        //{
        //    var user = new User { Email = "short@test.com", PasswordHash = "1", FirstName = "Test", LastName = "User", Role = "Customer", IsClubMember = false };
        //    await Assert.ThrowsAsync<DbUpdateException>(async () => await _userRepository.AddNewUser(user));
        //}
        //[Fact]
        //public async Task AddNewUser_ShouldThrowValidationException_WhenEmailAlreadyExists()
        //{
        //    await _userRepository.AddNewUser(new User { Email = "dup@test.com", PasswordHash = "Password123", FirstName = "Test", LastName = "User", Role = "Customer", IsClubMember = false });
        //    var duplicateUser = new User { Email = "dup@test.com", PasswordHash = "Password456", FirstName = "Test", LastName = "User", Role = "Customer", IsClubMember = false };
        //    await Assert.ThrowsAsync<DbUpdateException>(async () => await _userRepository.AddNewUser(duplicateUser));
        //}
    }
}








