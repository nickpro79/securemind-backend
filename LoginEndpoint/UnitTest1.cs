using AuthenticationAPI.Models;
using AuthenticationAPI.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Xunit;

namespace AuthenticationAPI.Tests
{
    public class LoginRepositoryTests
    {
        private readonly Mock<UserManager<IdentityUser>> _userManagerMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly LoginRepository _loginRepository;

        public LoginRepositoryTests()
        {
            _userManagerMock = new Mock<UserManager<IdentityUser>>(
                new Mock<IUserStore<IdentityUser>>().Object,
                null, null, null, null, null, null, null, null);

            _configurationMock = new Mock<IConfiguration>();

            _loginRepository = new LoginRepository(_userManagerMock.Object, _configurationMock.Object);
        }

        [Fact]
        public async Task Login_ShouldReturnToken_WhenCredentialsAreValid()
        {
            // Arrange
            var model = new Login { Username = "testuser", Password = "Password123" };
            var user = new IdentityUser { UserName = "testuser" };
            var roles = new List<string> { "User" };

            _userManagerMock.Setup(um => um.FindByNameAsync(model.Username))
                .ReturnsAsync(user);

            _userManagerMock.Setup(um => um.CheckPasswordAsync(user, model.Password))
                .ReturnsAsync(true);

            _userManagerMock.Setup(um => um.GetRolesAsync(user))
                .ReturnsAsync(roles);

            _configurationMock.SetupGet(c => c["JWTAuthentication:Key"])
                .Returns("your_secret_key");
            _configurationMock.SetupGet(c => c["JWTAuthentication:ValidIssuer"])
                .Returns("your_issuer");
            _configurationMock.SetupGet(c => c["JWTAuthentication:ValidAudience"])
                .Returns("your_audience");

            // Act
            var token = await _loginRepository.Login(model);

            // Assert
            Assert.NotNull(token);
            Assert.IsType<JwtSecurityToken>(token);
            var jwtToken = (JwtSecurityToken)token;
            Assert.Equal("your_issuer", jwtToken.Issuer);

        }

        [Fact]
        public async Task Login_ShouldReturnNull_WhenCredentialsAreInvalid()
        {
            // Arrange
            var model = new Login { Username = "testuser", Password = "WrongPassword" };
            var user = new IdentityUser { UserName = "testuser" };

            _userManagerMock.Setup(um => um.FindByNameAsync(model.Username))
                .ReturnsAsync(user);

            _userManagerMock.Setup(um => um.CheckPasswordAsync(user, model.Password))
                .ReturnsAsync(false);

            // Act
            var token = await _loginRepository.Login(model);

            // Assert
            Assert.Null(token);
        }

        [Fact]
        public async Task Login_ShouldReturnNull_WhenUserNotFound()
        {
            // Arrange
            var model = new Login { Username = "nonexistentuser", Password = "Password123" };

            _userManagerMock.Setup(um => um.FindByNameAsync(model.Username))
                .ReturnsAsync((IdentityUser)null);

            // Act
            var token = await _loginRepository.Login(model);

            // Assert
            Assert.Null(token);
        }
    }
}
