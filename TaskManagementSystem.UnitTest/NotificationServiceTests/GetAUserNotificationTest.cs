using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Models;
using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.UnitTests.NotificationTests
{
    public class GetAUserNotificationTest
    {
        private readonly NotificationServiceFactory _fac;

        public GetAUserNotificationTest()
        {
            _fac = new NotificationServiceFactory();  
        }

        [Fact]
        public async System.Threading.Tasks.Task GetAUserNotification_ShouldReturn_AListOfUserNotifications()
        {
            //Arrange

            Guid userId = TestData.userId;


            //Act

            var notification = TestData.Notification();
            await _fac.Context.Notifications.AddAsync(notification);
            await _fac.Context.SaveChangesAsync();

            var result = await _fac.NotificationService.AUserNotifications(userId);


            //Assert
            Assert.NotNull(result.Data);
            Assert.True(result.Data.Count >= 1);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetAUserNotification_ShouldReturn_APaginatedListOfUserNotifications()
        {
            //Arrange

            Guid userId = TestData.userId;


            //Act

            var notification = TestData.Notification();
            await _fac.Context.Notifications.AddAsync(notification);
            await _fac.Context.SaveChangesAsync();

            var result = await _fac.NotificationService.AUserNotificationsPaginated(userId, new BaseSearchViewModel());


            //Assert
            Assert.NotNull(result.Data);
            Assert.True(result.Data.Count >= 1);
        }
    }
}
