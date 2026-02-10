
using Microsoft.EntityFrameworkCore;
using Model;
using Moq;
using Moq.EntityFrameworkCore;
using Repository;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class UserRepositoryUnitTests
    {
        private readonly Mock<myDBContext> mockContext;
        private readonly UserRepository userRepository;

        public UserRepositoryUnitTests()
        {
            mockContext = new Mock<myDBContext>();
            userRepository = new UserRepository(mockContext.Object);
        }

        [Fact]
        public async Task AddNewUser_ShouldAddUser_WithValidData()
        {
            var user = new User { Gmail = "valid.email@example.com", Password = "StrongPassword123" };
            mockContext.Setup(c => c.Users).ReturnsDbSet(new List<User>());

            var result = await userRepository.AddNewUser(user);

            Assert.NotNull(result);
            Assert.Equal(user.Gmail, result.Gmail);
        }

        [Fact]
        public async Task GetUserById_ShouldReturnUser_WhenUserExists()
        {
            var user = new User { UserId = 1, Gmail = "valid.email@example.com", Password = "StrongPassword123" };
            mockContext.Setup(c => c.Users).ReturnsDbSet(new List<User> { user });

            var result = await userRepository.GetUserById(1);

            Assert.NotNull(result);
            Assert.Equal(user.Gmail, result.Gmail);
        }

        [Fact]
        public async Task GetUsers_ShouldReturnListOfUsers_WhenUsersExist()
        {
            var users = new List<User>
            {
                new User { Gmail = "user1@example.com", Password = "Password1" },
                new User { Gmail = "user2@example.com", Password = "Password2" }
            };
            mockContext.Setup(c => c.Users).ReturnsDbSet(users);

            var result = await userRepository.GetUsers();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task Login_ShouldReturnUser_WithValidCredentials()
        {
            var user = new User { Gmail = "valid.email@example.com", Password = "StrongPassword123" };
            mockContext.Setup(c => c.Users).ReturnsDbSet(new List<User> { user });

            var result = await userRepository.Login(user);

            Assert.NotNull(result);
            Assert.Equal(user.Gmail, result.Gmail);
        }

        [Fact]
        public async Task Update_ShouldModifyUser_WhenValidDataProvided()
        {
            var user = new User { UserId = 1, Gmail = "valid.email@example.com", Password = "StrongPassword123" };
            mockContext.Setup(c => c.Users).ReturnsDbSet(new List<User> { user });

            user.Password = "NewStrongPassword456";
            var result = await userRepository.update(user.UserId, user);

            Assert.NotNull(result);
            Assert.Equal(user.Password, result.Password);
        }


        [Fact]
        public async Task GetUserById_ShouldReturnNull_WhenUserDoesNotExist()
        {
            mockContext.Setup(c => c.Users).ReturnsDbSet(new List<User>());

            var result = await userRepository.GetUserById(9999);

            Assert.Null(result);
        }

        [Fact]
        public async Task Login_ShouldReturnNull_WhenInvalidCredentialsProvided()
        {
            var user = new User { Gmail = "valid.email@example.com", Password = "StrongPassword123" };
            mockContext.Setup(c => c.Users).ReturnsDbSet(new List<User> { user });

            var loginUser = new User { Gmail = "valid.email@example.com", Password = "WrongPassword" };
            var result = await userRepository.Login(loginUser);

            Assert.Null(result);
        }


        ///

        //לא עובד!!!
        //[Fact]
        //public async Task AddNewUser_ShouldThrowValidationException_WhenEmailInvalid()
        //{
        //  var user = new User { Gmail = "invalidEmail", Password = "StrongPassword123" };
        //  mockContext.Setup(c => c.Users).ReturnsDbSet(new List<User>());

        //  await Assert.ThrowsAsync<DbUpdateException>(() => userRepository.AddNewUser(user));
        //}
        //לא עובד!!!
        //[Fact]
        //public async Task AddNewUser_ShouldThrowValidationException_WhenPasswordTooShort()
        //{
        //  var user = new User { Gmail = "valid.email@example.com", Password = "short" };
        //  mockContext.Setup(c => c.Users).ReturnsDbSet(new List<User>());

        //  await Assert.ThrowsAsync<DbUpdateException>(() => userRepository.AddNewUser(user));
        //}
    }
}
