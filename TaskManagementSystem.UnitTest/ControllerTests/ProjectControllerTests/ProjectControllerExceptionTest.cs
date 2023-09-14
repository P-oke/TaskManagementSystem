using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.DTOs.ProjectDTO;
using TaskManagementSystem.Application.Models;
using TaskManagementSystem.UnitTests.ControllerTests.ControllerFactory;

namespace TaskManagementSystem.UnitTests.ControllerTests.ProjectControllerTests
{
    public class ProjectControllerExceptionTest
    {
        private readonly ProjectControllerExceptionTestFactory _fac;

        public ProjectControllerExceptionTest()
        {
            _fac = new ProjectControllerExceptionTestFactory();
        }

        [Fact]
        public async Task GetProject_ShouldReturnError()
        {
            //Arrange


            //Act
            _fac.ProjectService.Setup(x => x.AProject(It.IsAny<Guid>())).ThrowsAsync(new Exception("An Error Occurred"));
            var result = await _fac.ProjectController.GetProject(TestData.ProjectId) as ObjectResult;


            // Assert
            Assert.NotNull(result);
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async Task CreateProject_ShouldReturnError()
        {
            //Arrange


            //Act
            _fac.ProjectService.Setup(x => x.CreateProject(It.IsAny<CreateProjectDTO>(), It.IsAny<Guid>())).ThrowsAsync(new Exception("An Error Occurred"));
            var result = await _fac.ProjectController.CreateProject(It.IsAny<CreateProjectDTO>()) as ObjectResult;


            // Assert
            Assert.NotNull(result);
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async Task DeleteProject_ShouldReturnError()
        {
            //Arrange


            //Act
            _fac.ProjectService.Setup(x => x.DeleteProject(It.IsAny<Guid>())).ThrowsAsync(new Exception("An Error Occurred"));
            var result = await _fac.ProjectController.DeleteProject(It.IsAny<Guid>()) as ObjectResult;


            // Assert
            Assert.NotNull(result);
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async Task UpdateProject_ShouldReturnError()
        {
            //Arrange


            //Act
            _fac.ProjectService.Setup(x => x.UpdateProject(It.IsAny<Guid>(), It.IsAny<UpdateProjectDTO>(), It.IsAny<Guid>())).ThrowsAsync(new Exception("An Error Occurred"));
            var result = await _fac.ProjectController.UpdateProject(It.IsAny<Guid>(), It.IsAny<UpdateProjectDTO>()) as ObjectResult;


            // Assert
            Assert.NotNull(result);
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async Task GetAllProject_ShouldReturnError()
        {
            //Arrange


            //Act
            _fac.ProjectService.Setup(x => x.GetAllProjects()).ThrowsAsync(new Exception("An Error Occurred"));
            var result = await _fac.ProjectController.GetAllProjects() as ObjectResult;


            // Assert
            Assert.NotNull(result);
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async Task GetAllProjectPaginated_ShouldReturnError()
        {
            //Arrange


            //Act
            _fac.ProjectService.Setup(x => x.GetAllProjects(It.IsAny<BaseSearchViewModel>())).ThrowsAsync(new Exception("An Error Occurred"));
            var result = await _fac.ProjectController.GetAllProjects(It.IsAny<BaseSearchViewModel>()) as ObjectResult;


            // Assert
            Assert.NotNull(result);
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async Task GetAUserProjects_ShouldReturnError()  
        {
            //Arrange


            //Act
            _fac.ProjectService.Setup(x => x.GetAUserProject(It.IsAny<Guid>())).ThrowsAsync(new Exception("An Error Occurred"));
            var result = await _fac.ProjectController.GetAUserProjects(It.IsAny<Guid>()) as ObjectResult;


            // Assert
            Assert.NotNull(result);
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async Task GetAUserProjectPaginated_ShouldReturnError() 
        {
            //Arrange


            //Act
            _fac.ProjectService.Setup(x => x.GetAUserProject(It.IsAny<Guid>())).ThrowsAsync(new Exception("An Error Occurred"));
            var result = await _fac.ProjectController.GetAUserProjectsPaginated(It.IsAny<Guid>(), It.IsAny<BaseSearchViewModel>()) as ObjectResult;


            // Assert
            Assert.NotNull(result);
            Assert.Equal(500, result.StatusCode);
        }
    }
}
