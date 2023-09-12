using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.DTOs.TaskDTOs;
using TaskManagementSystem.Application.Models;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Domain.Enum;

namespace TaskManagementSystem.UnitTests.TaskServiceTests
{
    public class GetAUserTasksTest
    {
        private readonly TaskServiceFactory _fac;

        public GetAUserTasksTest()
        {
            _fac = new TaskServiceFactory();
        }

        [Fact]
        public async System.Threading.Tasks.Task GetAUserTasks_ShouldReturnUserTasks()
        {
            //Arrange
            Guid userId = TestData.userId;

            var task = TestData.Task();
            await _fac.Context.Tasks.AddAsync(task);
            await _fac.Context.SaveChangesAsync();

            _fac.Context.UserTasks.Add(new UserTask { TaskId = task.Id, UserId = userId});
            await _fac.Context.SaveChangesAsync();

            //Act
            var result = await _fac.TaskService.GetAUserTasks(userId);


            //Assert
            Assert.NotNull(result.Data);
            Assert.True(result.Data.Count >= 1);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetAUserTasks_ShouldReturnPaginatedUserTasks() 
        {
            //Arrange
            Guid userId = TestData.userId;

            var task = TestData.Task();
            await _fac.Context.Tasks.AddAsync(task);
            await _fac.Context.SaveChangesAsync();

            _fac.Context.UserTasks.Add(new UserTask { TaskId = task.Id, UserId = userId });
            await _fac.Context.SaveChangesAsync();

            //Act
            var result = await _fac.TaskService.GetAUserTasks(userId, new BaseSearchViewModel { Keyword = "test"});


            //Assert
            Assert.NotNull(result.Data);
            Assert.True(result.Data.Count >= 1);
            Assert.IsType<PaginatedList<TaskDTO>>(result.Data);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetAUserTasks_ByStatusOrPriority_ShouldReturnListOfUserTasks() 
        {
            //Arrange
            Guid userId = TestData.userId;

            var task = TestData.Task();
            await _fac.Context.Tasks.AddAsync(task);
            await _fac.Context.SaveChangesAsync();

            _fac.Context.UserTasks.Add(new UserTask { TaskId = task.Id, UserId = userId });
            await _fac.Context.SaveChangesAsync();

            //Act
            var result = await _fac.TaskService.GetAUserTasks(userId, new QueryTaskDTO { Status = Status.Pending, Priority = Priority.Low });


            //Assert
            Assert.NotNull(result.Data);
            Assert.True(result.Data.Count >= 1);
            Assert.IsType<List<TaskDTO>>(result.Data); 
        }


        [Fact]
        public async System.Threading.Tasks.Task GetAUserTasks_ByStatusOrPriority_ShouldReturnPaginatedUserTasks()
        {
            //Arrange
            Guid userId = TestData.userId;

            var task = TestData.Task();
            await _fac.Context.Tasks.AddAsync(task);
            await _fac.Context.SaveChangesAsync();

            _fac.Context.UserTasks.Add(new UserTask { TaskId = task.Id, UserId = userId });
            await _fac.Context.SaveChangesAsync();

            //Act
            var result = await _fac.TaskService.GetAUserTasksPaginated(userId, new QueryTaskDTO { Status = Status.Pending, Priority = Priority.Low });


            //Assert
            Assert.NotNull(result.Data);
            Assert.True(result.Data.Count >= 1);
            Assert.IsType<PaginatedList<TaskDTO>>(result.Data);
        }
    }
}
