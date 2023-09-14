using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.DTOs.ProjectDTO;

namespace TaskManagementSystem.UnitTests.ProjectTests
{
    
    public class CreateAProjectTest
    {
        private readonly ProjectServiceFactory _fac;

        public CreateAProjectTest()
        {        
            _fac = new ProjectServiceFactory();
        }

        [Fact]
        public async Task CreateAProject_ShoudBeCreatedSuccessfully() 
        {
            //Arrange

            //Act
            var user = TestData.User();
            await _fac.Context.AddAsync(user);
            await _fac.Context.SaveChangesAsync();

            var result = await _fac.ProjectService.CreateProject(new CreateProjectDTO { Description = "my project description", Name = "my project"}, user.Id);

            //Assert
            Assert.False(result.HasError);
        }

        [Fact]
        public async Task CreateAProject_ShoudReturnError_WhenAProjectTitleExist()
        {
            //Arrange
            Guid userId = TestData.userId;


            //Act

            var project = TestData.Project();
            await _fac.Context.Projects.AddAsync(project);
            await _fac.Context.SaveChangesAsync();

            var result = await _fac.ProjectService.CreateProject(TestData.CreateProjectDTO(), userId);

            //Assert
            Assert.True(result.HasError);
        }
    }
}
