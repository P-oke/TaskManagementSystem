using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.DTOs.NotificationDTOs;

namespace TaskManagementSystem.UnitTests.NotificationTests
{
    public class MarkAsReadOrUnReadTest
    {
        private readonly NotificationServiceFactory _fac;

        public MarkAsReadOrUnReadTest()
        {
            _fac = new NotificationServiceFactory();
        }

        [Fact]
        public async Task MarkAsRead_ShouldBeSuccessful()
        {
            //Arrange

            var userId = TestData.userId;

            //Act

            //Act
            var notification = TestData.Notification();
            await _fac.Context.Notifications.AddAsync(notification);
            await _fac.Context.SaveChangesAsync();

            var result = await _fac.NotificationService.MarkAsReadOrUnRead(new MarkAsReadDTO { MarkAsRead = true, NotificationId = notification.Id }, userId);

            //Assert
            Assert.False(result.HasError);
        }

        [Fact]
        public async Task MarkAsUnRead_ShouldBeSuccessful()
        {
            //Arrange

            var userId = TestData.userId;

            //Act
            var notification = TestData.Notification();
            await _fac.Context.Notifications.AddAsync(notification);
            await _fac.Context.SaveChangesAsync();

            var result = await _fac.NotificationService.MarkAsReadOrUnRead(new MarkAsReadDTO { MarkAsRead = false , NotificationId = notification.Id}, userId);

            //Assert
            Assert.False(result.HasError);
        }

        [Fact]
        public async Task MarkAsRead_ShouldReturnError_WhenNotificationDoesNotExist() 
        {
            //Arrange
            var userId = TestData.userId;
            var notificationId = TestData.NotificationId;

            
            //Act
            var result = await _fac.NotificationService.MarkAsReadOrUnRead(new MarkAsReadDTO { MarkAsRead = true, NotificationId = notificationId }, userId);

            //Assert
            Assert.True(result.HasError);
        }

    }
}
