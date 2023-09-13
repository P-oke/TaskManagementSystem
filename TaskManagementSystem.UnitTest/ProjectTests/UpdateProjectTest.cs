using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.UnitTests.ProjectTests
{
    public class UpdateProjectTest
    {
        private readonly ProjectServiceFactory _fac;

        public UpdateProjectTest()
        {
            _fac = new ProjectServiceFactory();    
        }

        [Fact]
        public async Task UpdateAProject_ShoudBeUpdatedSuccessfully()
        {
            //Arrange
            Guid userId = TestData.userId;

            //Act

            var project = TestData.Project();
            await _fac.Context.Projects.AddAsync(project);
            await _fac.Context.SaveChangesAsync();

            var result = await _fac.ProjectService.UpdateProject(project.Id, TestData.UpdateProjectDTO(), userId);

            //Assert
            Assert.False(result.HasError);
        }

        [Fact]
        public async Task UpdateAProject_ShoudReturnError_WhenATaskDoesNotExist()
        {
            //Arrange
            Guid userId = TestData.userId;
            Guid projectId = TestData.TaskId;


            //Act

            var result = await _fac.ProjectService.UpdateProject(projectId, TestData.UpdateProjectDTO(), userId);

            //Assert
            Assert.True(result.HasError);
        }
    }
}
