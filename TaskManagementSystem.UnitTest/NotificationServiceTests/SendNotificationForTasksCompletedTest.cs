using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.UnitTests.NotificationTests
{
    public class SendNotificationForTasksCompletedTest
    {

        private readonly NotificationServiceFactory _fac;

        public SendNotificationForTasksCompletedTest()
        {
            _fac = new NotificationServiceFactory();
        }

        [Fact]
        public async Task SendNotificationForTaskCompleted_ShouldWork()
        {
            //Arrange

            //Act
            var notification = TestData.Notification();
            await _fac.Context.Notifications.AddAsync(notification);
            await _fac.Context.SaveChangesAsync();

            var result = await _fac.NotificationService.SendNotificationForTasksCompleted();

            //Assert
            Assert.False(result.HasError);
        }
    }
}
