using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.UnitTests.NotificationTests
{
    public class SendNotificationForTasksWithin48HoursTest
    {

        private readonly NotificationServiceFactory _fac;

        public SendNotificationForTasksWithin48HoursTest()
        {
            _fac = new NotificationServiceFactory();       
        }

        [Fact]
        public async Task SendNotificationForTasksDueDateWithin48HoursTest()
        {
            //Arrange

            //Act
            var notification = TestData.Notification();
            await _fac.Context.Notifications.AddAsync(notification);
            await _fac.Context.SaveChangesAsync();

            var result = await _fac.NotificationService.SendNotificationForTasksDueDateWithin48Hours();

            //Assert
            Assert.False(result.HasError);
        }
    }
}
