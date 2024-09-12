using AuthenticationAPI.Contract;
using AuthenticationAPI.Models;
using AuthenticationAPI.Repository;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace AuthenticationAPI.Tests
{
    public class RegisterRepositoryTests
    {
        private readonly Mock<UserManager<IdentityUser>> _userManagerMock;
        private readonly Mock<RoleManager<IdentityRole>> _roleManagerMock;
        private readonly RegisterRepository _registerRepository;

        public RegisterRepositoryTests()
        {
            _userManagerMock = new Mock<UserManager<IdentityUser>>(
                new Mock<IUserStore<IdentityUser>>().Object,
                null, null, null, null, null, null, null, null);

            _roleManagerMock = new Mock<RoleManager<IdentityRole>>(
                new Mock<IRoleStore<IdentityRole>>().Object,
                null, null, null, null);

            _registerRepository = new RegisterRepository(_userManagerMock.Object, _roleManagerMock.Object);
        }

        [Fact]
        public async Task Register_ShouldReturnError_WhenUserAlreadyExists()
        {

            var model = new Register { Username = "testuser", Email = "test@example.com", Password = "Password123" };
            _userManagerMock.Setup(um => um.FindByNameAsync(model.Username))
                .ReturnsAsync(new IdentityUser());

        
            var result = await _registerRepository.Register(model);

            Assert.Equal("Error", result.Status);
            Assert.Equal("User Already Exists!!", result.Message);
        }

        [Fact]
        public async Task Register_ShouldReturnError_WhenUserCreationFails()
        { 
            var model = new Register { Username = "newuser", Email = "newuser@example.com", Password = "Password123" };
            _userManagerMock.Setup(um => um.FindByNameAsync(model.Username))
                .ReturnsAsync((IdentityUser)null);

            _userManagerMock.Setup(um => um.CreateAsync(It.IsAny<IdentityUser>(), model.Password))
                .ReturnsAsync(IdentityResult.Failed());

            var result = await _registerRepository.Register(model);

            Assert.Equal("Error", result.Status);
            Assert.Equal("User creation failed! Please check user details and try again!", result.Message);
        }

        [Fact]
        public async Task Register_ShouldCreateRolesAndAssignUser_WhenSuccessful()
        {
          
            var model = new Register { Username = "newuser", Email = "newuser@example.com", Password = "Password123" };
            _userManagerMock.Setup(um => um.FindByNameAsync(model.Username))
                .ReturnsAsync((IdentityUser)null); 

            _userManagerMock.Setup(um => um.CreateAsync(It.IsAny<IdentityUser>(), model.Password))
                .ReturnsAsync(IdentityResult.Success); 

            _roleManagerMock.Setup(rm => rm.RoleExistsAsync(UserRoles.Admin))
                .ReturnsAsync(false); 
            _roleManagerMock.Setup(rm => rm.RoleExistsAsync(UserRoles.User))
                .ReturnsAsync(false); 
            _roleManagerMock.Setup(rm => rm.CreateAsync(It.IsAny<IdentityRole>()))
                .ReturnsAsync(IdentityResult.Success); 

            _userManagerMock.Setup(um => um.AddToRoleAsync(It.IsAny<IdentityUser>(), UserRoles.User))
                .ReturnsAsync(IdentityResult.Success); 

            
            var result = await _registerRepository.Register(model);

            Assert.Equal("Success", result.Status);
            Assert.Equal("User created successfully", result.Message);
        }
    }
}
