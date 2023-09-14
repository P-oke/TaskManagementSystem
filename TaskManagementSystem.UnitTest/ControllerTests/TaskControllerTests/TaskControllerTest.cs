using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.DTOs.TaskDTOs;
using TaskManagementSystem.Application.Interfaces;
using TaskManagementSystem.Application.Models;
using TaskManagementSystem.Application.Utils;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.UnitTests.ControllerTests.ControllerFactory;

namespace TaskManagementSystem.UnitTests.ControllerTests.TaskControllerTests
{
    public class TaskControllerTest
    {
        private readonly TaskControllerFactory _fac;

        public TaskControllerTest()
        {
            _fac = new TaskControllerFactory();

        }

        [Fact]
        public async System.Threading.Tasks.Task GetTask_ShouldWork()
        {
            //Arrange

            // Act
            var task = TestData.Task();
            _fac.Context.Tasks.Add(task);
            _fac.Context.SaveChanges();

            var result = await _fac.TaskController.GetTask(task.Id) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }


        [Fact]
        public async System.Threading.Tasks.Task CreateTask_ShouldWork()
        {
            //Arrange

            var result = await _fac.TaskController.CreateTask(new CreateTaskDTO { Title = "task", Description = "description" }) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(201, result.StatusCode);
        }

        [Fact]
        public async System.Threading.Tasks.Task DeleteTask_ShouldWork()
        {
            //Arrange

            // Act
            var task = TestData.Task();
            _fac.Context.Tasks.Add(task);
            _fac.Context.SaveChanges();

            var result = await _fac.TaskController.DeleteTask(task.Id) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetAUserTask_ShouldWork()
        {
            //Arrange

            // Act
            var user = TestData.User();
            _fac.Context.Users.Add(user);
            _fac.Context.SaveChanges();

            var result = await _fac.TaskController.GetAUserTask(user.Id) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetAUserTaskPaginated_ShouldWork()
        {
            //Arrange

            // Act
            var user = TestData.User();
            _fac.Context.Users.Add(user);
            _fac.Context.SaveChanges();

            var result = await _fac.TaskController.GetAUserTask(user.Id, new BaseSearchViewModel()) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetAUserTaskByStatusOrPriority_ShouldWork()
        {
            //Arrange

            // Act
            var user = TestData.User();
            _fac.Context.Users.Add(user);
            _fac.Context.SaveChanges();

            var result = await _fac.TaskController.GetAUserTask(user.Id, new QueryTaskDTO()) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetAUserTaskByStatusOrPriorityPaginated_ShouldWork()
        {
            //Arrange

            // Act
            var user = TestData.User();
            _fac.Context.Users.Add(user);
            _fac.Context.SaveChanges();

            var result = await _fac.TaskController.GetAUserTaskPaginated(user.Id, new QueryTaskDTO()) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async System.Threading.Tasks.Task UpdateTask_ShouldWork()
        {
            //Arrange
            var userId = TestData.userId;

            //Act

            var task = TestData.Task();
            _fac.Context.Tasks.Add(task);
            _fac.Context.SaveChanges();

            _fac.Context.UserTasks.Add(new UserTask { TaskId = task.Id, UserId = userId });
            await _fac.Context.SaveChangesAsync();

            var result = await _fac.TaskController.UpdateTask(task.Id, new UpdateTaskDTO { Title = "Title", Description = "Description" }) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async System.Threading.Tasks.Task AssignTask_ShouldWork()
        {
            //Arrange


            //Act

            var task = TestData.Task();
            _fac.Context.Tasks.Add(task);
            _fac.Context.SaveChanges();

            var project = TestData.Project();
            _fac.Context.Projects.Add(project);
            _fac.Context.SaveChanges();

            var result = await _fac.TaskController.AssignTaskToProject(new AssignAndRemoveTaskFromProjectDTO { TaskId = task.Id, ProjectId = project.Id }) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async System.Threading.Tasks.Task RemoveTask_ShouldWork()
        {
            //Arrange


            //Act

            var task = TestData.Task();
            _fac.Context.Tasks.Add(task);
            _fac.Context.SaveChanges();

            var project = TestData.Project();
            _fac.Context.Projects.Add(project);
            task.ProjectId = project.Id;
            _fac.Context.SaveChanges();

            var result = await _fac.TaskController.RemoveTaskFromProject(new AssignAndRemoveTaskFromProjectDTO { TaskId = task.Id, ProjectId = project.Id }) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetAUserTasksForTheCurrentWeek_ShouldWork()
        {
            //Arrange


            //Act

            var user = TestData.User();
            _fac.Context.Users.Add(user);
            _fac.Context.SaveChanges();


            var result = await _fac.TaskController.GetAUserTasksForTheCurrentWeek(user.Id) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetAUserTasksForTheCurrentWeekPaginated_ShouldWork()
        {
            //Arrange


            //Act

            var user = TestData.User();
            _fac.Context.Users.Add(user);
            _fac.Context.SaveChanges();


            var result = await _fac.TaskController.GetAUserTasksForTheCurrentWeek(user.Id, new BaseSearchViewModel()) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetAllTasks_ShouldWork()  
        {
            //Arrange


            //Act

            var user = TestData.User();
            _fac.Context.Users.Add(user);
            _fac.Context.SaveChanges();


            var result = await _fac.TaskController.GetAllTasks() as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetAllTasksPaginated_ShouldWork()
        {
            //Arrange

            //Act

            var user = TestData.User();
            _fac.Context.Users.Add(user);
            _fac.Context.SaveChanges();


            var result = await _fac.TaskController.GetAllTasks(new BaseSearchViewModel()) as ObjectResult; 

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

    }
}
