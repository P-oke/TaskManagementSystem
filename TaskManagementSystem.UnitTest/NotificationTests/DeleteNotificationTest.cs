using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.UnitTests.NotificationTests
{
    public class DeleteNotificationTest
    {

        private readonly NotificationServiceFactory _fac;

        public DeleteNotificationTest()
        {
            _fac = new NotificationServiceFactory();
                
        }

        [Fact]
        public async System.Threading.Tasks.Task DeleteANotification_ShoudDeleteSuccessfully() 
        {
            //Arrange


            //Act

            var notification = TestData.Notification();
            await _fac.Context.Notifications.AddAsync(notification);
            await _fac.Context.SaveChangesAsync();

            var result = await _fac.NotificationService.DeleteNotification(notification.Id);

            //Assert
            Assert.False(result.HasError);
        }

        [Fact]
        public async System.Threading.Tasks.Task DeleteANotification_ShoudReturnError_WhenANotificationtDoesNotExist()
        {
            //Arrange
            Guid notificationId = TestData.NotificationId;


            //Act

            var result = await _fac.NotificationService.DeleteNotification(notificationId);

            //Assert
            Assert.True(result.HasError);
        }
    }
}
