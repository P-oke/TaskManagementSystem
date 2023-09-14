using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.DTOs.ProjectDTO;
using TaskManagementSystem.Application.Models;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.UnitTests.ControllerTests.ControllerFactory;

namespace TaskManagementSystem.UnitTests.ControllerTests.ProjectControllerTests
{
    public class ProjectControllerTest
    {
        private readonly ProjectControllerFactory _fac;

        public ProjectControllerTest()
        {
            _fac = new ProjectControllerFactory();    
        }


        [Fact]
        public async System.Threading.Tasks.Task GetProject_ShouldWork()
        {
            //Arrange

            // Act
            var project = TestData.Project();
            _fac.Context.Projects.Add(project);
            _fac.Context.SaveChanges();

            var result = await _fac.ProjectController.GetProject(project.Id) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async System.Threading.Tasks.Task CreateProject_ShouldWork()
        {
            //Arrange

            // Act

            var result = await _fac.ProjectController.CreateProject( new CreateProjectDTO { Description = "description", Name = "project name"}) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(201, result.StatusCode);
        }

        [Fact]
        public async System.Threading.Tasks.Task DeleteProject_ShouldWork()
        {
            //Arrange

            // Act
            var project = TestData.Project();
            _fac.Context.Projects.Add(project);
            _fac.Context.SaveChanges();

            var result = await _fac.ProjectController.DeleteProject(project.Id) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async System.Threading.Tasks.Task UpdateProject_ShouldWork()
        {
            //Arrange

            // Act
            var project = TestData.Project();
            _fac.Context.Projects.Add(project);
            _fac.Context.SaveChanges();

            var result = await _fac.ProjectController.UpdateProject(project.Id, new UpdateProjectDTO { Name = "name updated", Description = "description updsted"}) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetAllProjects_ShouldWork()
        {
            //Arrange

            // Act
            var project = TestData.Project();
            _fac.Context.Projects.Add(project);
            _fac.Context.SaveChanges();

            var result = await _fac.ProjectController.GetAllProjects() as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetAllProjectsPaginated_ShouldWork() 
        {
            //Arrange

            // Act
            var project = TestData.Project();
            _fac.Context.Projects.Add(project);
            _fac.Context.SaveChanges();

            var result = await _fac.ProjectController.GetAllProjects(new BaseSearchViewModel { Keyword = "keyword"}) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetAUserProjects_ShouldWork()
        {
            //Arrange
            var userId = TestData.userId;

            // Act
            var project = TestData.Project();
            _fac.Context.Projects.Add(project);
            _fac.Context.SaveChanges();

            var task = TestData.Task();
            await _fac.Context.Tasks.AddAsync(task);
            await _fac.Context.SaveChangesAsync();

            task.ProjectId = project.Id;
            _fac.Context.UserTasks.Add(new UserTask { TaskId = task.Id, UserId = userId });
            await _fac.Context.SaveChangesAsync();

            var result = await _fac.ProjectController.GetAUserProjects(userId) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetAUserProjectsPaginated_ShouldWork()
        {
            //Arrange
            var userId = TestData.userId;

            // Act
            var project = TestData.Project();
            _fac.Context.Projects.Add(project);
            _fac.Context.SaveChanges();

            var task = TestData.Task();
            await _fac.Context.Tasks.AddAsync(task);
            await _fac.Context.SaveChangesAsync();

            task.ProjectId = project.Id;
            _fac.Context.UserTasks.Add(new UserTask { TaskId = task.Id, UserId = userId });
            await _fac.Context.SaveChangesAsync();

            var result = await _fac.ProjectController.GetAUserProjectsPaginated(userId, new BaseSearchViewModel { Keyword = "keyword"}) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }
    }
}
