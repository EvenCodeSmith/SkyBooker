using AuthService.Controllers;
using AuthService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace AuthService.Tests
{
    public class AuthControllerTests_LoginFail
    {
        private AuthController GetController()
        {
            var options = new DbContextOptionsBuilder<AuthDbContext>()
                .UseInMemoryDatabase("AuthDb_LoginFail")
                .Options;

            var context = new AuthDbContext(options);
            context.Users.Add(new User
            {
                Username = "wrongpassuser",
                Password = BCrypt.Net.BCrypt.HashPassword("correctpassword"),
                Email = "wrongpass@example.com"
            });
            context.SaveChanges();

            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "Jwt:Key", "thisisaverysecurekey1234567890" },
                    { "Jwt:Issuer", "testissuer" },
                    { "Jwt:Audience", "testaudience" },
                    { "Jwt:ExpireMinutes", "30" }
                })
                .Build();

            return new AuthController(context, config);
        }

        [Fact]
        public async Task Login_ReturnsUnauthorized_ForWrongPassword()
        {
            // Arrange
            var controller = GetController();
            var loginDto = new LoginDTO
            {
                Username = "wrongpassuser",
                Password = "wrongpassword"  // <- absichtlich falsch
            };

            // Act
            var result = await controller.Login(loginDto);

            // Assert
            Assert.IsType<UnauthorizedObjectResult>(result);
        }
    }
}
