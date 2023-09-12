using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.DTOs.UserDTO;
using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.UnitTests.AuthenticationServiceTests
{
    public class RegisterTest
    {
        private readonly AuthenticationServiceFactory _fac;

        public RegisterTest()
        {
            _fac = new AuthenticationServiceFactory();     
        }

        [Fact]
        public async System.Threading.Tasks.Task Register_ShouldWork()
        {
            //Arrange
            var data = new RegisterUserDTO { EmailAddress = TestData.User().Email, PhoneNumber = TestData.User().PhoneNumber, Password = TestData.Password };

            _fac.UserManager.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);

            //Act

            var result = await _fac.AuthenticationService.Register(data);

            //Assert
            Assert.False(result.HasError);
        }

        [Fact]
        public async System.Threading.Tasks.Task Register_ShouldReturnError_WhenEmailExist()
        {
            //Arrange
            var data = new RegisterUserDTO { EmailAddress = TestData.User().Email, PhoneNumber = TestData.User().PhoneNumber, Password = TestData.Password };

            _fac.UserManager.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Failed());

            //Act

            var result = await _fac.AuthenticationService.Register(data);

            //Assert
            Assert.True(result.HasError);
        }
    }
}
