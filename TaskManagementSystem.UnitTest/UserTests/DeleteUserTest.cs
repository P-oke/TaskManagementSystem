using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.UnitTests.UserTests
{
    public class DeleteUserTest
    {
        private readonly UserServiceFactory _fac;

        public DeleteUserTest()
        {
            _fac = new UserServiceFactory();      
        }

        [Fact]
        public async Task DeleteUser_ShouldWork() 
        {
            //Arrange


            //Act

            var user = TestData.User();
            await _fac.Context.Users.AddAsync(user);
            await _fac.Context.SaveChangesAsync();

            var notification = TestData.Notification();
            await _fac.Context.Notifications.AddAsync(notification);
            await _fac.Context.SaveChangesAsync();

            var result = await _fac.UserService.DeleteUser(user.Id);

            //Assert
            Assert.False(result.HasError);

        }

        [Fact]
        public async Task DeleteUser_ShouldReturnError_WhenUserDoesNotExist()   
        {
            //Arrange


            //Act

            var result = await _fac.UserService.DeleteUser(Guid.NewGuid());

            //Assert
            Assert.True(result.HasError);

        }
    }
}
