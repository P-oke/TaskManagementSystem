using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.UnitTests.AuthenticationServiceTests
{
    public class RefreshTokenTest
    {
        private readonly AuthenticationServiceFactory _fac;

        public RefreshTokenTest()
        {
            _fac = new AuthenticationServiceFactory();      
        }

        [Fact]
        public async System.Threading.Tasks.Task RefreshToken_ShouldWork()
        {
            //Arrange


            //Act

            var refreshToken = new RefreshToken
            {
                UserId = TestData.userId,
                Token = "59Gkt1IO+jBYMgeimnR52xS9WEIM1oyKzvHnQ3vYkII=",
                ExpiresAt = DateTime.Now.AddHours(1)
            };

            await _fac.Context.AddAsync(refreshToken);
            await _fac.Context.SaveChangesAsync();

            _fac.UserManager.Setup(x => x.GetClaimsAsync(It.IsAny<User>())).ReturnsAsync(new List<Claim>());
            _fac.UserManager.Setup(x => x.GetRolesAsync(It.IsAny<User>())).ReturnsAsync(new List<string>());
            _fac.Configuration.Setup(x => x["JWT:Secret"]).Returns("648db912-3891-4166-a53a-22c77e7675cc");
            _fac.Configuration.Setup(x => x.GetSection("JWT:ValidIssuer").Value).Returns("ValidIssuer");
            _fac.Configuration.Setup(x => x.GetSection("JWT:ValidAudience").Value).Returns("ValidAudience");
            _fac.Configuration.Setup(x => x.GetSection("JWT:DurationInMinutes").Value).Returns("5");
            _fac.Configuration.Setup(x => x.GetSection("JWT:RefreshTokenExpiration").Value).Returns("5");
            _fac.UserManager.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(new User { Id = TestData.userId, Email = TestData.User().Email });


            var result = await _fac.AuthenticationService.RefreshToken(TestData.AccessToken, refreshToken.Token);

            //Assert
        }
    }
}
