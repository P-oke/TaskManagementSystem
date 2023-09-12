using Hangfire;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Interfaces;
using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.UnitTests.TaskServiceTests
{
    public class AssignTaskToUserTest
    {
        private readonly TaskServiceFactory _fac;

        public AssignTaskToUserTest()
        {
            _fac = new TaskServiceFactory();
                
        }

        [Fact]
        public async System.Threading.Tasks.Task AssignTaskToUser_ShouldWork()
        {
            //Arrange

            //Act
            var task = TestData.Task();
            await _fac.Context.Tasks.AddAsync(task);
            await _fac.Context.SaveChangesAsync();

            var user = TestData.User();
            await _fac.Context.Users.AddAsync(user);
            await _fac.Context.SaveChangesAsync();


            var result = await _fac.TaskService.AssignTaskToAUser(task.Id, user.Id);

            //Assert
            Assert.False(result.HasError);
        }
    }
}
