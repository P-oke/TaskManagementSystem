using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.DTOs.AuthDTO;
using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.UnitTests.AuthenticationServiceTests
{
    public class LoginTest
    {
        private readonly AuthenticationServiceFactory _fac;

        public LoginTest()
        {
            _fac = new AuthenticationServiceFactory();
                
        }

        [Fact]
        public async System.Threading.Tasks.Task Login_Should_Work_AndReturn_JWT() 
        {
            //Arrange

            var user = TestData.User();

            //Act

            _fac.UserManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(new User { Id = TestData.userId, Email = TestData.User().Email});
            _fac.UserManager.Setup(x => x.CreateAsync(It.IsAny<User>())).ReturnsAsync(IdentityResult.Success);
            _fac.UserManager.Setup(x => x.CheckPasswordAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(true);
            _fac.UserManager.Setup(x => x.GetClaimsAsync(It.IsAny<User>())).ReturnsAsync(new List<Claim>());
            _fac.UserManager.Setup(x => x.GetRolesAsync(It.IsAny<User>())).ReturnsAsync(new List<string>());

            _fac.Configuration.Setup(x => x["JWT:Secret"]).Returns("648db912-3891-4166-a53a-22c77e7675cc");
            _fac.Configuration.Setup(x => x.GetSection("JWT:ValidIssuer").Value).Returns("ValidIssuer");
            _fac.Configuration.Setup(x => x.GetSection("JWT:ValidAudience").Value).Returns("ValidAudience");
            _fac.Configuration.Setup(x => x.GetSection("JWT:DurationInMinutes").Value).Returns("5");
            _fac.Configuration.Setup(x => x.GetSection("JWT:RefreshTokenExpiration").Value).Returns("5");

            var result = await _fac.AuthenticationService.Login(new LoginDTO(), DateTime.Now);

            //Assert
            Assert.NotNull(result.Data);
        }

        [Fact]
        public async System.Threading.Tasks.Task Login_Should_ReturnErrorWhenEmailDoesNotExist() 
        {
            
            //Arrange


            //Act

            _fac.UserManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(new User());

            var result = await _fac.AuthenticationService.Login(new LoginDTO(), DateTime.Now);

            //Assert
            Assert.True(result.HasError);

        }
    }
}
