using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.UnitTests.NotificationTests
{
    public class ANotificationTest
    {
        private readonly NotificationServiceFactory _fac;

        public ANotificationTest()
        {
            _fac = new NotificationServiceFactory();
        }


        [Fact]
        public async Task GetANotification_ShouldReturnANotificationThatExist()
        {
            //Arrange


            //Act

            //Create a Notification
            var notification = TestData.Notification();
            await _fac.Context.Notifications.AddAsync(notification);
            await _fac.Context.SaveChangesAsync();

            var result = await _fac.NotificationService.ANotification(notification.Id);

            //Assert
            Assert.False(result.HasError);
            Assert.NotNull(result.Data);

        }

        [Fact]
        public async Task GetAProject_ShouldReturnErrorForAProjectThatDoesnotExist()
        {
            //Arrange

            //Act

            var result = await _fac.NotificationService.ANotification(Guid.NewGuid());

            //Assert
            Assert.True(result.HasError);

        }
    }

}
