using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.DTOs.TaskDTOs;
using TaskManagementSystem.Application.Models;
using TaskManagementSystem.UnitTests.ControllerTests.ControllerFactory;

namespace TaskManagementSystem.UnitTests.ControllerTests.TaskControllerTests
{
    public class TaskControllerExceptionTest
    {
        private readonly TaskExceptionControllerFactory _fac;

        public TaskControllerExceptionTest()
        {
            _fac = new TaskExceptionControllerFactory();

        }


        [Fact]
        public async Task GetTask_ShouldReturnError()
        {
            //Arrange
            
            
            //Act
            _fac.TaskService.Setup(x => x.ATask(It.IsAny<Guid>())).ThrowsAsync(new Exception("An Error Occurred"));
            var result = await _fac.TaskController.GetTask(TestData.TaskId) as ObjectResult;


            // Assert
            Assert.NotNull(result);
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async System.Threading.Tasks.Task CreateTask_ShouldReturnError() 
        {
            
            //Arrange


            //Act
            _fac.TaskService.Setup(x => x.CreateTask(It.IsAny<CreateTaskDTO>(), It.IsAny<Guid>())).ThrowsAsync(new Exception("An Error Occurred"));
            var result = await _fac.TaskController.CreateTask(new CreateTaskDTO()) as ObjectResult;


            // Assert
            Assert.NotNull(result);
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async System.Threading.Tasks.Task DeleteTask_ShouldReturnError()
        {
            
            //Arrange


            // Act

            _fac.TaskService.Setup(x => x.DeleteTask(It.IsAny<Guid>())).ThrowsAsync(new Exception("An Error Occurred"));

            var result = await _fac.TaskController.DeleteTask(It.IsAny<Guid>()) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetAUserTask_ShouldReturnError()
        {

            //Arrange


            // Act

            _fac.TaskService.Setup(x => x.GetAUserTasks(It.IsAny<Guid>())).ThrowsAsync(new Exception("An Error Occurred"));

            var result = await _fac.TaskController.GetAUserTask(It.IsAny<Guid>()) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetAUserTaskPaginated_ShouldReturnError()
        {
            
            //Arrange


            // Act

            _fac.TaskService.Setup(x => x.GetAUserTasks(It.IsAny<Guid>(), It.IsAny<QueryTaskDTO>())).ThrowsAsync(new Exception("An Error Occurred"));

            var result = await _fac.TaskController.GetAUserTask(It.IsAny<Guid>(), new BaseSearchViewModel()) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetAUserTaskByStatusOrPriority_ShouldReturnError()
        {
            //Arrange


            // Act

            _fac.TaskService.Setup(x => x.GetAUserTasks(It.IsAny<Guid>())).ThrowsAsync(new Exception("An Error Occurred"));

            var result = await _fac.TaskController.GetAUserTask(It.IsAny<Guid>(), new QueryTaskDTO()) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetAUserTaskByStatusOrPriorityPaginated_ShouldReturnError()
        {
            //Arrange


            // Act

            _fac.TaskService.Setup(x => x.GetAUserTasks(It.IsAny<Guid>(), new BaseSearchViewModel())).ThrowsAsync(new Exception("An Error Occurred"));

            var result = await _fac.TaskController.GetAUserTaskPaginated(It.IsAny<Guid>(), new QueryTaskDTO()) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async System.Threading.Tasks.Task UpdateTask_ShouldReturnError()
        {

            //Arrange


            //Act

            _fac.TaskService.Setup(x => x.UpdateTask(It.IsAny<Guid>(), new UpdateTaskDTO(), It.IsAny<Guid>())).ThrowsAsync(new Exception("An Error Occurred"));

            var result = await _fac.TaskController.UpdateTask(It.IsAny<Guid>(), new UpdateTaskDTO { Title = "Title", Description = "Description" }) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async System.Threading.Tasks.Task AssignTask_ShouldReturnError()
        {
            //Arrange


            //Act


            _fac.TaskService.Setup(x => x.AssignTaskToAProject(It.IsAny<AssignAndRemoveTaskFromProjectDTO>(), It.IsAny<Guid>())).ThrowsAsync(new Exception("An Error Occurred"));

            var result = await _fac.TaskController.AssignTaskToProject(new AssignAndRemoveTaskFromProjectDTO { TaskId = It.IsAny<Guid>(), ProjectId = It.IsAny<Guid>() }) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async System.Threading.Tasks.Task RemoveTask_ShouldReturnErrork()
        {
            //Arrange


            //Act


            _fac.TaskService.Setup(x => x.RemoveTaskFromProject(It.IsAny<AssignAndRemoveTaskFromProjectDTO>(), It.IsAny<Guid>())).ThrowsAsync(new Exception("An Error Occurred"));

            var result = await _fac.TaskController.RemoveTaskFromProject(new AssignAndRemoveTaskFromProjectDTO { TaskId = It.IsAny<Guid>(), ProjectId = It.IsAny<Guid>() }) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetAUserTasksForTheCurrentWeek_ShouldReturnError()
        {
            //Arrange


            //Act


            _fac.TaskService.Setup(x => x.GetAUserTasksForTheCurrentWeek(It.IsAny<Guid>())).ThrowsAsync(new Exception("An Error Occurred"));

            var result = await _fac.TaskController.GetAUserTasksForTheCurrentWeek(It.IsAny<Guid>()) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetAUserTasksForTheCurrentWeekPaginated_ShouldReturnError()
        {
            //Arrange


            //Act

            _fac.TaskService.Setup(x => x.GetAUserTasksForTheCurrentWeek(It.IsAny<Guid>())).ThrowsAsync(new Exception("An Error Occurred"));

            var result = await _fac.TaskController.GetAUserTasksForTheCurrentWeek(It.IsAny<Guid>(), new BaseSearchViewModel()) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetAllTasks_ShouldReturnError()
        {
            //Arrange


            //Act


            _fac.TaskService.Setup(x => x.GetAllTasks()).ThrowsAsync(new Exception("An Error Occurred"));

            var result = await _fac.TaskController.GetAllTasks() as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetAllTasksPaginated_ShouldReturnError() 
        {
            //Arrange


            //Act


            _fac.TaskService.Setup(x => x.GetAllTasks()).ThrowsAsync(new Exception("An Error Occurred"));

            var result = await _fac.TaskController.GetAllTasks() as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(500, result.StatusCode);
        }

    }
}
