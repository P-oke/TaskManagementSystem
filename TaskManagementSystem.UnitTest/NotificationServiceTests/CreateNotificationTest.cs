using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.UnitTests.NotificationTests
{
    public class CreateNotificationTest
    {
        private readonly NotificationServiceFactory _fac;

        public CreateNotificationTest()
        {
            _fac = new NotificationServiceFactory(); 
        }


        [Fact]
        public async Task CreateNotification_ShoudBeCreatedSuccessfully()
        {
            //Arrange
            Guid userId = TestData.userId;

            //Act

            var user = TestData.User();
            await _fac.Context.AddAsync(user);
            await _fac.Context.SaveChangesAsync();

            var result = await _fac.NotificationService.CreateNotification(TestData.CreateNotificationDTO(), user.Id);

            //Assert
            Assert.False(result.HasError);
        }

        [Fact]
        public async Task CreateNotification_ShoudReturnError_WhenAUserDoesNotExist() 
        {
            //Arrange
            Guid userId = TestData.userId;


            //Act

            var result = await _fac.NotificationService.CreateNotification(TestData.CreateNotificationDTO(), userId);

            //Assert
            Assert.True(result.HasError);
        }
    }
}
