using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.UnitTests.TaskServiceTests
{
    public class UpdateTaskTest
    {
        private readonly TaskServiceFactory _fac;

        public UpdateTaskTest()
        {
            _fac = new TaskServiceFactory();
                
        }

        [Fact]
        public async System.Threading.Tasks.Task UpdateATask_ShoudBeUpdatedSuccessfully()
        {
            //Arrange
            Guid userId = TestData.userId;

            //Act

            var task = TestData.Task();
            await _fac.Context.Tasks.AddAsync(task);
            await _fac.Context.SaveChangesAsync();

            _fac.Context.UserTasks.Add(new UserTask { TaskId = task.Id, UserId = userId });
            await _fac.Context.SaveChangesAsync();

            var result = await _fac.TaskService.UpdateTask(task.Id, TestData.UpdateTaskDTO(), userId);

            //Assert
            Assert.False(result.HasError);
        }

        [Fact]
        public async System.Threading.Tasks.Task UpdateATask_ShoudReturnError_WhenATaskDoesNotExist() 
        {
            //Arrange
            Guid userId = TestData.userId;
            Guid taskId = TestData.TaskId;


            //Act

            var result = await _fac.TaskService.UpdateTask(taskId, TestData.UpdateTaskDTO(), userId);

            //Assert
            Assert.True(result.HasError);
        }

        [Fact]
        public async System.Threading.Tasks.Task UpdateATask_ShoudReturnError_WhenAUserDoesNotHaveThatTask() 
        {
            //Arrange
            Guid userId = TestData.userId;

            //Act
            var task = TestData.Task();
            await _fac.Context.Tasks.AddAsync(task);
            await _fac.Context.SaveChangesAsync();

            _fac.Context.UserTasks.Add(new UserTask { TaskId = task.Id, UserId = userId });
            await _fac.Context.SaveChangesAsync();

            var result = await _fac.TaskService.UpdateTask(task.Id, TestData.UpdateTaskDTO(), Guid.NewGuid());

            //Assert
            Assert.True(result.HasError);
        }
    }
}
